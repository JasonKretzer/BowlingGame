using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Models
{
    public class Roll
    {
        public const int MAX_SINGLE_ROLL = 10;
        public const int MIN_SINGLE_ROLL = 0;
        public int RollValue { get; private set; }

        public Roll(int pinsStanding)
        {
            if (pinsStanding < Roll.MIN_SINGLE_ROLL ||
                pinsStanding > Roll.MAX_SINGLE_ROLL)
            {
                throw new InvalidRollException($"The number of pins standing must be between {MIN_SINGLE_ROLL} and {MAX_SINGLE_ROLL} inclusive.");
            }
            Random random = new Random();
            RollValue = random.Next(0, pinsStanding + 1);
        }
    }
}
