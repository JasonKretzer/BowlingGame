using BowlingGame.Enums;
using BowlingGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Interfaces
{
    /// <summary>
    /// An interface for a sample bowler for clients to implement.
    /// </summary>
    public interface IBowler
    {
        int Bowl(RollOptions.RollTypes rollType, int manualRoll);
        Frame[] GetFrames();
        Frame GetCurrentFrame();
        int GetCurrentFrameNumber();
        Frame GetFrameAtNumber(int whichFrame);
        void NewGame();
        GameResult SimulateGame();

    }
}
