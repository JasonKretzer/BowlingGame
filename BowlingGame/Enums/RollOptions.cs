using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Enums
{
    /// <summary>
    /// Provides a flag to tell roll to either be random or set manually.
    /// </summary>
    public class RollOptions
    {
        public enum RollTypes
        {
            [Description("Randomly generate a value for this roll.")]
            RANDOM = 0,

            [Description("Provide a value for this roll.")]
            MANUAL = 1
        }
    }
}
