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
        [TestMethod]
        public void PinsStandingGreaterThanEqualToZero()
        {
            try
            {
                Roll roll = new Roll(-2);
            }
            catch (InvalidRollException ire)
            {
                //if it throws, it passes
                //TODO : maybe refactor
            }
        }

        [TestMethod]
        public void PinsStandingLessThanEqualToTen()
        {
            try
            {
                Roll roll = new Roll(12);
            }
            catch (InvalidRollException ire)
            {
                //if it throws, it passes
                //TODO : maybe refactor
            }
        }

        [TestMethod]
        public void PinsStandingGreaterThanEqualToZeroAndLessThanEqualToTen()
        {
            List<Roll> rolls = new List<Roll>();
            for (int i = Roll.MIN_SINGLE_ROLL; i <= Roll.MAX_SINGLE_ROLL; i++)
            {
                rolls.Add(new Roll(i));
            }

            Assert.IsFalse(rolls.Any(r => r.RollValue < Roll.MIN_SINGLE_ROLL));
            Assert.IsFalse(rolls.Any(r => r.RollValue > Roll.MAX_SINGLE_ROLL));
        }
    }
}
