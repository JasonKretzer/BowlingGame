using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Enums
{
    /// <summary>
    /// A set of basic game states
    /// </summary>
    public class GameStateOptions
    {
        public enum State
        {
            [Description("Starting a fresh standard frame.")]
            NEW_FRAME = 0,

            [Description("Bowling the second roll for a standard frame.")]
            NEW_FRAME_SECOND_ROLL = 1,

            [Description("Starting the final frame of the game.")]
            FINAL_FRAME = 2,

            [Description("In the final frame, bowling the second roll after a first roll strike.")]
            FINAL_FRAME_SECOND_ROLL_STRIKE = 3,

            [Description("In the final frame, bowling the second roll.")]
            FINAL_FRAME_SECOND_ROLL = 4,

            [Description("In the final frame, bowling the third roll after either a spare or a strike.")]
            FINAL_FRAME_THIRD_ROLL = 5,

            [Description("Game is complete.")]
            END_GAME = 6
        }
    }
}