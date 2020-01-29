using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlerApp.Enums
{
    public class BowlerOptions
    {
        /// <summary>
        /// The specific actions that a bowler can take
        /// </summary>
        public enum BowlerActions
        {
            [Description("Roll the ball.")]
            BOWL = 0,

            [Description("View the scores of all frames.")]
            VIEW_FRAMES = 1,

            [Description("View the current frame.")]
            VIEW_CURRENT_FRAME = 2,

            [Description("Get the current frame number.")]
            GET_CURRENT_FRAME_NUMBER = 3,

            [Description("View the frame with a given number.")]
            VIEW_FRAME_AT = 4,

            [Description("Reset the game.")]
            RESET = 5,

            [Description("Bowl a complete game with random rolls.")]
            SIMULATE_RANDOM_GAME = 6,

            [Description("Exit this game.")]
            QUIT = 7,
        }
    }
}
