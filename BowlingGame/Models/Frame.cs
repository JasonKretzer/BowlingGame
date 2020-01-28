using BowlingGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Models
{
    public class Frame
    {

        private Roll firstRoll;
        private Roll secondRoll;
        private Roll thirdRoll;

        private Roll spareRoll;

        private Roll strikeRoll1;
        private Roll strikeRoll2;

        public FrameOptions.FrameTypes frameType = FrameOptions.FrameTypes.STANDARD_FRAME;

        public bool IsScoreFinal { get; private set; }

        public int CurrentScoreValue { get { return UpdateScore(); } private set { } }

        public string FirstRoll
        {
            get
            {
                return (firstRoll == null) ? string.Empty : firstRoll.RollValue.ToString();
            }
        }
        public string SecondRoll
        {
            get
            {
                return (secondRoll == null) ? string.Empty : firstRoll.RollValue.ToString();
            }
        }

        public string PrintableScore
        {
            get
            {
                string result = string.Empty;
                if (IsScoreFinal)
                {
                    result = CurrentScoreValue.ToString();
                }
                return result;
            }
            private set { }
        }

        public Frame(FrameOptions.FrameTypes frameType = FrameOptions.FrameTypes.STANDARD_FRAME)
        {
            this.frameType = frameType;
            IsScoreFinal = false;
        }

        public void AddRoll(Roll pinsKnockedDown)
        {
            if (pinsKnockedDown == null)
            {
                throw new ArgumentNullException("Roll cannot be null.");
            }

            if (firstRoll == null)
            {
                firstRoll = pinsKnockedDown;
            }
            else if (secondRoll == null)
            {
                secondRoll = pinsKnockedDown;
            }
            else if (frameType == FrameOptions.FrameTypes.FINAL_FRAME)
            {
                thirdRoll = pinsKnockedDown;
            }
            else
            {
                throw new ArgumentOutOfRangeException("No more rolls allowed in this frame.");
            }
        }

        private int UpdateScore()
        {
            int currentScoreValue = 0;
            if (firstRoll != null)
            {
                currentScoreValue += firstRoll.RollValue;
            }
            if (secondRoll != null)
            {
                currentScoreValue += secondRoll.RollValue;
            }
            if (frameType == FrameOptions.FrameTypes.FINAL_FRAME && thirdRoll != null)
            {
                currentScoreValue += thirdRoll.RollValue;
            }
            if (spareRoll != null)
            {
                currentScoreValue +=
                    spareRoll.RollValue;
            }
            else
            {
                if (strikeRoll1 != null)
                {
                    currentScoreValue += strikeRoll1.RollValue;
                }
                if (strikeRoll2 != null)
                {
                    currentScoreValue += strikeRoll2.RollValue;
                }
            }

            return currentScoreValue;
        }

        public void AddStrike(Roll strikeRoll)
        {
            if (strikeRoll == null)
            {
                throw new ArgumentNullException("Roll cannot be null.");
            }
            if (strikeRoll1 == null)
            {
                strikeRoll1 = strikeRoll;
            }
            else if (strikeRoll2 == null)
            {
                strikeRoll2 = strikeRoll;
                FinalizeFrame();
            }
        }

        public void FinalizeFrame()
        {
            IsScoreFinal = true;
        }

        public void AddSpare(Roll spareRoll)
        {
            if (spareRoll == null)
            {
                throw new ArgumentNullException("Roll cannot be null.");
            }
            this.spareRoll = spareRoll;
            FinalizeFrame();
        }
    }
}
