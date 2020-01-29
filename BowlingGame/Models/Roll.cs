using BowlingGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Models
{
    /// <summary>
    /// Model for a single ball roll
    /// </summary>
    public class Roll
    {
        #region Public Constants
        public const int MAX_SINGLE_ROLL = 10;
        public const int MIN_SINGLE_ROLL = 0;
        #endregion Public Constants

        #region Public Properties

        /// <summary>
        /// Is this random or a manual roll
        /// </summary>
        public RollOptions.RollTypes RollType { get; private set; }

        /// <summary>
        /// The value of this roll.
        /// </summary>
        public int RollValue { get; private set; }
        #endregion Public Properties

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinsStanding"></param>
        /// <param name="rollType"></param>
        /// <param name="manualRoll"></param>
        public Roll(int pinsStanding, RollOptions.RollTypes rollType, int manualRoll=0)
        {
            if (pinsStanding < Roll.MIN_SINGLE_ROLL ||
                pinsStanding > Roll.MAX_SINGLE_ROLL)
            {
                throw new InvalidRollException($"The number of pins standing must be between {MIN_SINGLE_ROLL} and {MAX_SINGLE_ROLL} inclusive.");
            }
            RollType = rollType;
            switch (RollType)
            {
                case RollOptions.RollTypes.MANUAL:
                    RollValue = manualRoll;
                    break;
                case RollOptions.RollTypes.RANDOM:
                default:
                    //this is not a very good random number generator
                    Random random = new Random((int)DateTime.UtcNow.TimeOfDay.TotalMilliseconds);
                    RollValue = random.Next(0, pinsStanding + 1);
                    break;
            }
        }
        #endregion Constructors
    }
}
