using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Models
{
    public class GameResult
    {
        public Frame[] Frames { get; private set; }
        public int TotalScore { get; private set; }

        public GameResult(Frame[] frames, int totalScore)
        {
            Frames = frames;
            TotalScore = totalScore;
        }

    }
}
