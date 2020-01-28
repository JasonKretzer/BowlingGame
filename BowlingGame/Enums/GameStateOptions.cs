using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Enums
{
    public class GameStateOptions
    {
        public enum State
        {
            [Description("The final frame of the game.")]
            NEW_FRAME = 0,

            [Description("The final frame of the game.")]
            NEW_FRAME_SECOND_ROLL = 1,

            [Description("The final frame of the game.")]
            FINAL_FRAME = 2,

            [Description("The final frame of the game.")]
            FINAL_FRAME_SECOND_ROLL_STRIKE = 3,

            [Description("The final frame of the game.")]
            FINAL_FRAME_SECOND_ROLL = 4,

            [Description("The final frame of the game.")]
            FINAL_FRAME_THIRD_ROLL = 5,

            [Description("The final frame of the game.")]
            END_GAME = 6
        }
    }
}