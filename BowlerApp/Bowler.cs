using BowlerApp.Enums;
using BowlingGame.Enums;
using BowlingGame.Interfaces;
using BowlingGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BowlerApp
{
    /// <summary>
    /// A simple metaphor that can be used to interact with the bowling engine
    /// </summary>
    public class Bowler : IBowler
    {

        #region Private Fields
        private readonly Game game;
        #endregion Private Fields

        #region Public Properties
        public bool IsGameFinished => game.GameState == GameStateOptions.State.END_GAME;
        #endregion Public Properties

        #region Constructors
        public Bowler()
        {
            game = new Game();
        }
        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Have the bowler bowl.
        /// </summary>
        /// <param name="rollType"></param>
        /// <param name="manualRoll">Used if roll type is manual</param>
        /// <returns></returns>
        public int Bowl(RollOptions.RollTypes rollType, int manualRoll = 0)
        {
            return game.Bowl(rollType, manualRoll);
        }

        /// <summary>
        /// Get all of the frames for the current game.
        /// </summary>
        /// <returns></returns>
        public Frame[] GetFrames()
        {
            return game.Frames;
        }

        /// <summary>
        /// Get the frame that is currently being bowled.
        /// </summary>
        /// <returns></returns>
        public Frame GetCurrentFrame()
        {
            return game.CurrentFrame;
        }

        /// <summary>
        /// Get the frame number for the frame that is currently being bowled.
        /// </summary>
        /// <returns></returns>
        public int GetCurrentFrameNumber()
        {
            return game.CurrentFrameNumber;
        }

        /// <summary>
        /// Gets the frame specified by the given number.
        /// </summary>
        /// <param name="whichFrame"></param>
        /// <returns></returns>
        public Frame GetFrameAtNumber(int whichFrame)
        {
            return game.SearchForFrame(whichFrame);
        }

        /// <summary>
        /// Tell the bowler to start a new game.
        /// </summary>
        public void NewGame()
        {
            game.ResetGame();
        }

        /// <summary>
        /// Simulate an entire game with random rolls (Not very random)
        /// </summary>
        /// <returns></returns>
        public GameResult SimulateGame()
        {
            GameResult result;
            while (!this.IsGameFinished)
            {
                this.Bowl(RollOptions.RollTypes.RANDOM);
            }
            result = game.GetResults();

            return result;
        }

        /// <summary>
        /// Get a list of everything this bowler can do.
        /// </summary>
        /// <returns></returns>
        public List<BowlerOptions.BowlerActions> GetBowlerOptions()
        {
            List<BowlerOptions.BowlerActions> results = Enum.GetValues(typeof(BowlerOptions.BowlerActions)).Cast<BowlerOptions.BowlerActions>().ToList();
            return results;
        }

        #endregion Public Methods
    }
}
