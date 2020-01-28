using System;
using System.Collections.Generic;
using System.Linq;
using BowlingGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingGameTester
{
    [TestClass]
    public class RollingTests
    {
        private Game g;

        [TestMethod]
        public void TotalRollTest()
        {
            g = new Game();
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 7);
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 2);
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 7);
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 2);
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 7);

            Frame current = g.CurrentFrame;
            int firstRoll = 0;

            Assert.IsTrue(int.TryParse(current.FirstRoll, out firstRoll));
            Assert.AreEqual(7, firstRoll);
            Assert.AreEqual(3, g.CurrentFrameNumber);
            Assert.IsTrue(string.IsNullOrEmpty(current.SecondRoll));

        }

        [TestMethod]
        public void CheckSpares()
        {
            g = new Game();
            Frame spare = g.CurrentFrame;
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 7);
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 3);

            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 7);

            Frame current = g.CurrentFrame;

            Assert.AreEqual(17, spare.CurrentScoreValue);
            Assert.AreEqual(7, current.CurrentScoreValue);
        }

        [TestMethod]
        public void CheckSingleStrike()
        {
            g = new Game();
            Frame strikeFrame = g.CurrentFrame;
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 10);

            Frame firstStrikeAdd = g.CurrentFrame;
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 3);
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 7);

            Assert.AreEqual(10, firstStrikeAdd.CurrentScoreValue);
            Assert.AreEqual(20, strikeFrame.CurrentScoreValue);
            Assert.AreEqual(3, g.CurrentFrameNumber);
        }

        [TestMethod]
        public void CheckTripleStrike()
        {
            g = new Game();
            Frame strikeFrame = g.CurrentFrame;
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 10);

            Frame firstStrikeAdd = g.CurrentFrame;
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 10);

            Frame secondStrikeAdd = g.CurrentFrame;
            g.Bowl(BowlingGame.Enums.RollOptions.RollTypes.MANUAL, 10);


            Assert.AreEqual(30, strikeFrame.CurrentScoreValue);
            Assert.AreEqual(20, firstStrikeAdd.CurrentScoreValue);
            Assert.AreEqual(10, secondStrikeAdd.CurrentScoreValue);
            Assert.AreEqual(4, g.CurrentFrameNumber);
        }
    }
}
