using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Enums
{
    public class BowlerOptions
    {
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

            [Description("Reset the game.")]
            RESET = 4,

            [Description("Quit the current game.")]
            QUIT = 5,

            [Description("Start a game.")]
            START = 6,

            [Description("Bowl a random game.")]
            SIMULATE_RANDOM_GAME = 7,
        }
    }
}
