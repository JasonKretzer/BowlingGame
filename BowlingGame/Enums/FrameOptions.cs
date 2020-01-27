using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Enums
{
    public class FrameOptions
    {
        public enum FrameTypes
        {
            [Description("A standard two roll frame.")]
            STANDARD_FRAME = 0,

            [Description("The final frame of the game.")]
            FINAL_FRAME = 1
        }
    }
}
