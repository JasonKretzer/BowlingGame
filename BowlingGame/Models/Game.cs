using System;
using System.Collections.Generic;
using System.Linq;
using BowlingGame.Enums;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Models
{
    /// <summary>
    /// A model for a game of 10 pin bowling.
    /// </summary>
    public class Game
    {
        #region Private Fields
        private const int PENULTIMATE_FRAME_INDEX = 8;
        private Frame[] frames;
        private int currentIndex = 0;
        private Queue<Frame> strikeQueue;
        private Frame spareFrame;
        #endregion Private Fields

        #region Public Properties
        /// <summary>
        /// Current game state
        /// </summary>
        public GameStateOptions.State GameState { get; private set; }
        /// <summary>
        /// How many pins are currently standing at this exact moment
        /// </summary>
        public int PinsStanding { get; private set; }

        /// <summary>
        /// Number of current game frame
        /// </summary>
        public int CurrentFrameNumber
        {
            get
            {
                return currentIndex + 1;
            }
            private set
            {
                currentIndex = value;
            }
        }

        /// <summary>
        /// Returns the current game frame
        /// </summary>
        public Frame CurrentFrame => frames[currentIndex];

        /// <summary>
        /// Array of frames for this game.
        /// </summary>
        public Frame[] Frames => frames;

        #endregion Public Properties

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public Game()
        {
            ResetGame();
        }
        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Resets the current game
        /// </summary>
        public void ResetGame()
        {
            frames = new Frame[]
            {
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(),
                new Frame(FrameOptions.FrameTypes.FINAL_FRAME)
            };
            ResetCurrentFrame();
            ResetPins();
            GameState = GameStateOptions.State.NEW_FRAME;
            strikeQueue = new Queue<Frame>(2);
        }

        /// <summary>
        /// Get the frame specified by the frame number specified.
        /// </summary>
        /// <param name="whichFrame"></param>
        /// <returns></returns>
        public Frame SearchForFrame(int whichFrame)
        {
            return frames[whichFrame - 1];
        }

        /// <summary>
        /// Generates a roll of the given type and removes and returns that many pins.
        /// </summary>
        /// <param name="rollType"></param>
        /// <param name="manualValue"></param>
        /// <returns></returns>
        public int Bowl(RollOptions.RollTypes rollType, int manualValue = 0)
        {
            Roll roll = new Roll(PinsStanding, rollType, manualValue);
            ProcessRoll(roll);
            return roll.RollValue;
        }
        #endregion Public Methods


        #region Private Methods
        /// <summary>
        /// Main Engine processor -- processes the roll and advances frames and game state.
        /// </summary>
        /// <param name="roll"></param>
        private void ProcessRoll(Roll roll)
        {
            PinsStanding -= roll.RollValue;

            switch (GameState)
            {
                //starting a new frame
                case GameStateOptions.State.NEW_FRAME:
                    frames[currentIndex].AddRoll(roll);

                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);

                    if (roll.RollValue == Roll.MAX_SINGLE_ROLL)
                    {
                        //a strike
                        strikeQueue.Enqueue(frames[currentIndex]);
                        frames[currentIndex].InStrikeQueue = true;
                        if (currentIndex == PENULTIMATE_FRAME_INDEX)
                        {
                            GameState = GameStateOptions.State.FINAL_FRAME;
                        }
                        else
                        {
                            GameState = GameStateOptions.State.NEW_FRAME;
                        }
                        currentIndex++;
                        ResetPins();
                    }
                    else
                    {
                        //no strike continue frame
                        GameState = GameStateOptions.State.NEW_FRAME_SECOND_ROLL;
                    }

                    break;

                //second roll on the current frame
                case GameStateOptions.State.NEW_FRAME_SECOND_ROLL:

                    frames[currentIndex].AddRoll(roll);

                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);

                    if (PinsStanding == 0)
                    {
                        //a spare
                        spareFrame = frames[currentIndex];
                        spareFrame.InSpareQueue = true;
                    }
                    if (currentIndex == PENULTIMATE_FRAME_INDEX)
                    {
                        GameState = GameStateOptions.State.FINAL_FRAME;
                    }
                    else
                    {
                        //finished with this frame, start a new one
                        GameState = GameStateOptions.State.NEW_FRAME;
                    }
                    ResetPins();
                    currentIndex++;
                    break;

                //starting the final frame
                case GameStateOptions.State.FINAL_FRAME:

                    frames[currentIndex].AddRoll(roll);

                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);

                    if (roll.RollValue == Roll.MAX_SINGLE_ROLL)
                    {
                        //a strike
                        GameState = GameStateOptions.State.FINAL_FRAME_SECOND_ROLL_STRIKE;
                        ResetPins();
                    }
                    else
                    {
                        //no strike
                        GameState = GameStateOptions.State.FINAL_FRAME_SECOND_ROLL;
                    }
                    break;

                //start the second roll if the first roll in this frame was not a strike
                case GameStateOptions.State.FINAL_FRAME_SECOND_ROLL:

                    frames[currentIndex].AddRoll(roll);

                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);

                    if (PinsStanding == 0)
                    {
                        //a spare, so lets go to third roll
                        GameState = GameStateOptions.State.FINAL_FRAME_THIRD_ROLL;
                        ResetPins();
                    }
                    else
                    {
                        GameState = GameStateOptions.State.END_GAME;
                        currentIndex++;
                    }
                    break;

                //first roll of final frame was a strike
                case GameStateOptions.State.FINAL_FRAME_SECOND_ROLL_STRIKE:
                    frames[currentIndex].AddRoll(roll);

                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);

                    GameState = GameStateOptions.State.FINAL_FRAME_THIRD_ROLL;
                    break;

                //in the final frame, first roll was either a strike or the second roll was a spare
                case GameStateOptions.State.FINAL_FRAME_THIRD_ROLL:

                    frames[currentIndex].AddRoll(roll);

                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);

                    GameState = GameStateOptions.State.END_GAME;
                    currentIndex++;
                    break;

                case GameStateOptions.State.END_GAME:
                default:
                    break;
            }
            FinalizeFrames();
        }

        /// <summary>
        /// Runs through the frames and finalizes any that can be
        /// </summary>
        private void FinalizeFrames()
        {
            int sum = 0;
            for (int i = 0; i < currentIndex; i++)
            {
                sum = 0;
                Frame frame = frames[i];
                if(!frame.IsScoreFinal)
                {
                    Frame previousFrame = (i < 1) ? null : frames[i - 1];

                    if (!frame.InSpareQueue && !frame.InStrikeQueue)
                    {
                        if (previousFrame != null && previousFrame.IsScoreFinal)
                        {
                            sum = previousFrame.CurrentScoreValue;
                            frame.FinalizeFrame(sum);
                        }
                        else if (previousFrame == null) //ie. this is the first frame
                        {
                            frame.FinalizeFrame(sum);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Go through the strikeQueue and add the roll to their strike accumulation points.  Then remove any frames which cannot accumulate any more strike points.
        /// </summary>
        /// <param name="roll"></param>
        private void ProcessPendingStrikeFrames(Roll roll)
        {
            foreach (var frame in strikeQueue)
            {
                frame.AddStrike(roll);
            }
            if (strikeQueue.Any() && strikeQueue.Peek().AreStrikesFull())
            {
                strikeQueue.Peek().InStrikeQueue = false;
                strikeQueue.Dequeue();
            }
        }

        /// <summary>
        /// If there is a frame accumulating spare point, add the roll to their spare accumulation points.  Then remove the frame.
        /// </summary>
        /// <param name="roll"></param>
        private void ProcessPendingSpareFrame(Roll roll)
        {
            if (spareFrame != null)
            {
                spareFrame.AddSpare(roll);
                spareFrame.InSpareQueue = false;
                spareFrame = null;
            }
        }
        /// <summary>
        /// Resets standing pins to default value of 10
        /// </summary>
        private void ResetPins()
        {
            PinsStanding = 10;
        }

        /// <summary>
        /// Tells the game to go back to the first frame.
        /// </summary>
        private void ResetCurrentFrame()
        {
            currentIndex = 0;
        }
        #endregion Private Methods

        /// <summary>
        /// Returns a model of the game results.
        /// </summary>
        /// <returns></returns>
        public GameResult GetResults()
        {
            GameResult result = new GameResult(frames, frames.LastOrDefault().CurrentScoreValue);
            return result;
        }
    }
}
