using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Models
{
    /// <summary>
    /// A model for the results of a game
    /// </summary>
    public class GameResult
    {
        #region Public Properties

        /// <summary>
        /// All the frames in this game.
        /// </summary>
        public Frame[] Frames { get; private set; }

        /// <summary>
        /// Current total score of this game.
        /// </summary>
        public int TotalScore { get; private set; }
        #endregion Public Properties

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="totalScore"></param>
        public GameResult(Frame[] frames, int totalScore)
        {
            Frames = frames;
            TotalScore = totalScore;
        }
        #endregion Constructors
    }
}
