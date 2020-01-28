using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Models
{
    public class Game
    {
        private const int PENULTIMATE_FRAME_INDEX = 8;
        private Frame[] frames;
        private int currentIndex = 0;
        private Queue<Frame> strikeQueue;
        private Frame spareFrame;

        public Enums.GameStateOptions.State GameState { get; private set; }

        public int PinsStanding { get; private set; }

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

        public Frame CurrentFrame
        {
            get
            {
                return frames[currentIndex];
            }
        }

        public Game()
        {
            ResetGame();
        }

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
                new Frame(Enums.FrameOptions.FrameTypes.FINAL_FRAME)
            };
            ResetCurrentFrame();
            ResetPins();
            GameState = Enums.GameStateOptions.State.NEW_FRAME;
            strikeQueue = new Queue<Frame>(2);
        }

        private void ResetPins()
        {
            PinsStanding = 10;
        }

        private void ResetCurrentFrame()
        {
            currentIndex = 0;
        }

        public void Bowl(Enums.RollOptions.RollTypes rollType, int manualValue = 0)
        {
            Roll roll = new Roll(PinsStanding, rollType, manualValue);
            ProcessRoll(roll);
        }

        private void ProcessRoll(Roll roll)
        {
            PinsStanding -= roll.RollValue;

            switch (GameState)
            {
                case Enums.GameStateOptions.State.NEW_FRAME:
                    frames[currentIndex].AddRoll(roll);

                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);
                    if (roll.RollValue == Roll.MAX_SINGLE_ROLL)
                    {
                        //a strike

                        strikeQueue.Enqueue(frames[currentIndex]);
                        if (currentIndex == PENULTIMATE_FRAME_INDEX)
                        {
                            GameState = Enums.GameStateOptions.State.FINAL_FRAME;
                        }
                        else
                        {
                            GameState = Enums.GameStateOptions.State.NEW_FRAME;
                        }
                        currentIndex++;
                        ResetPins();
                    }
                    else
                    {
                        GameState = Enums.GameStateOptions.State.NEW_FRAME_SECOND_ROLL;
                    }

                    break;
                case Enums.GameStateOptions.State.NEW_FRAME_SECOND_ROLL:

                    frames[currentIndex].AddRoll(roll);
                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);

                    if (PinsStanding == 0)
                    {
                        //a spare
                        spareFrame = frames[currentIndex];
                    }
                    if (currentIndex == PENULTIMATE_FRAME_INDEX)
                    {
                        GameState = Enums.GameStateOptions.State.FINAL_FRAME;
                    }
                    else
                    {
                        GameState = Enums.GameStateOptions.State.NEW_FRAME;
                    }
                    ResetPins();
                    currentIndex++;
                    break;


                case Enums.GameStateOptions.State.FINAL_FRAME:
                    frames[currentIndex].AddRoll(roll);
                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);
                    if (roll.RollValue == Roll.MAX_SINGLE_ROLL)
                    {
                        //a strike
                        GameState = Enums.GameStateOptions.State.FINAL_FRAME_SECOND_ROLL_STRIKE;
                        ResetPins();
                    }
                    else
                    {
                        GameState = Enums.GameStateOptions.State.FINAL_FRAME_SECOND_ROLL;
                    }
                    break;
                case Enums.GameStateOptions.State.FINAL_FRAME_SECOND_ROLL:
                    frames[currentIndex].AddRoll(roll);
                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);

                    if (PinsStanding == 0)
                    {
                        //a spare, so lets go to third roll
                        GameState = Enums.GameStateOptions.State.FINAL_FRAME_THIRD_ROLL;
                        ResetPins();
                    }
                    else
                    {
                        currentIndex++;
                        GameState = Enums.GameStateOptions.State.END_GAME;
                    }
                    break;

                case Enums.GameStateOptions.State.FINAL_FRAME_SECOND_ROLL_STRIKE:
                    frames[currentIndex].AddRoll(roll);

                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);

                    GameState = Enums.GameStateOptions.State.FINAL_FRAME_THIRD_ROLL;
                    break;

                case Enums.GameStateOptions.State.FINAL_FRAME_THIRD_ROLL:
                    frames[currentIndex].AddRoll(roll);
                    ProcessPendingStrikeFrames(roll);
                    ProcessPendingSpareFrame(roll);
                    EndGame();
                    GameState = Enums.GameStateOptions.State.END_GAME;
                    currentIndex++;
                    break;

                case Enums.GameStateOptions.State.END_GAME:
                    EndGame();
                    break;
                default:
                    break;
            }
        }

        private void EndGame()
        {
            foreach (var f in frames)
            {
                Console.WriteLine(f.CurrentScoreValue);
            }
        }

        private void ProcessPendingStrikeFrames(Roll roll)
        {
            foreach (var frame in strikeQueue)
            {
                frame.AddStrike(roll);
            }
            if (strikeQueue.Any() && strikeQueue.Peek().IsScoreFinal)
            {
                strikeQueue.Dequeue();
            }
        }

        private void ProcessPendingSpareFrame(Roll roll)
        {
            if (spareFrame != null)
            {
                spareFrame.AddSpare(roll);
                spareFrame = null;
            }
        }

        public GameResult GetResults()
        {
            GameResult result = new GameResult(frames, frames.Sum(f => f.CurrentScoreValue));
            return result;
        }
    }
}
