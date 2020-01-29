using BowlingGame.Models;
using BowlingGame.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingGameTester
{
    [TestClass]
    public class FrameTests
    {
        Frame f;

        [TestMethod]
        public void CheckAddRolls()
        {
            f = new Frame(FrameOptions.FrameTypes.STANDARD_FRAME);

            Assert.IsTrue(string.IsNullOrEmpty(f.FirstRoll));
            Assert.IsTrue(string.IsNullOrEmpty(f.SecondRoll));

            f.AddRoll(new Roll(10, RollOptions.RollTypes.MANUAL, 4));

            Assert.AreEqual("4", f.FirstRoll);

            f.AddRoll(new Roll(6, RollOptions.RollTypes.MANUAL, 2));

            Assert.AreEqual("2", f.SecondRoll);

            f.FinalizeFrame(0);

            Assert.AreEqual(6, f.CurrentScoreValue);
            Assert.AreEqual("6", f.PrintableScore);
        }

        [TestMethod]
        public void CheckStrikeRolls()
        {
            f = new Frame(FrameOptions.FrameTypes.STANDARD_FRAME);

            Assert.IsTrue(string.IsNullOrEmpty(f.FirstRoll));
            Assert.IsTrue(string.IsNullOrEmpty(f.SecondRoll));

            f.AddRoll(new Roll(10, RollOptions.RollTypes.MANUAL, 10));

            Assert.AreEqual("10", f.FirstRoll);
            Assert.IsTrue(string.IsNullOrEmpty(f.SecondRoll));

            f.AddStrike(new Roll(10, RollOptions.RollTypes.MANUAL, 6));

            Assert.AreEqual(16, f.CurrentScoreValue);
            f.AddStrike(new Roll(4, RollOptions.RollTypes.MANUAL, 3));

            Assert.AreEqual(19, f.CurrentScoreValue);

            f.FinalizeFrame(0);

            Assert.AreEqual(19, f.CurrentScoreValue);
            Assert.AreEqual("19", f.PrintableScore);
        }

        [TestMethod]
        public void CheckSpareRolls()
        {
            f = new Frame(FrameOptions.FrameTypes.STANDARD_FRAME);

            Assert.IsTrue(string.IsNullOrEmpty(f.FirstRoll));
            Assert.IsTrue(string.IsNullOrEmpty(f.SecondRoll));

            f.AddRoll(new Roll(10, RollOptions.RollTypes.MANUAL, 4));

            Assert.AreEqual("4", f.FirstRoll);

            f.AddRoll(new Roll(6, RollOptions.RollTypes.MANUAL, 6));

            Assert.AreEqual("6", f.SecondRoll);

            f.AddSpare(new Roll(10, RollOptions.RollTypes.MANUAL, 7));

            Assert.AreEqual(17, f.CurrentScoreValue);

            f.FinalizeFrame(0);

            Assert.AreEqual(17, f.CurrentScoreValue);
            Assert.AreEqual("17", f.PrintableScore);
        }
    }
}
