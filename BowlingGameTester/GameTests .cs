using System;
using System.Collections.Generic;
using System.Linq;
using BowlingGame.Enums;
using BowlingGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingGameTester
{
    [TestClass]
    public class GameTests
    {
        Game g;

        [TestMethod]
        public void CheckFinalScoreNoSparesNoStrikes()
        {
            g = new Game();
            GameResult result;
            int totalRolls = 0;
            while (g.CurrentFrameNumber <= 10)
            {
                g.Bowl(RollOptions.RollTypes.MANUAL, 4);
                totalRolls++;
            }
            result = g.GetResults();

            int sumTotalScore = result.Frames.Sum(f => f.CurrentScoreValue);
            int manualTotalScore = (4 * totalRolls);

            Assert.AreEqual(sumTotalScore, result.TotalScore);
            Assert.AreEqual(manualTotalScore, result.TotalScore);
            Assert.AreEqual(manualTotalScore, sumTotalScore);

        }

        [TestMethod]
        public void CheckFinalScoreAllSpares()
        {
            g = new Game();
            GameResult result;
            bool pickup = false;
            while (g.CurrentFrameNumber <= 10)
            {
                if(pickup)
                {
                    g.Bowl(RollOptions.RollTypes.MANUAL, 4);
                }
                else
                {
                    g.Bowl(RollOptions.RollTypes.MANUAL, 6);
                }
            }
            result = g.GetResults();

            int sumTotalScore = result.Frames.Sum(f => f.CurrentScoreValue);

            Assert.AreEqual(sumTotalScore, result.TotalScore);
        }

        [TestMethod]
        public void CheckFinalScoreAllStrikes()
        {
            g = new Game();
            GameResult result;
            while (g.CurrentFrameNumber <= 10)
            {
                g.Bowl(RollOptions.RollTypes.MANUAL, 10);
            }
            result = g.GetResults();

            int sumTotalScore = result.Frames.Sum(f => f.CurrentScoreValue);

            Assert.AreEqual(sumTotalScore, result.TotalScore);
        }
    }
}
