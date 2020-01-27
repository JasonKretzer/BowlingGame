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
        
        public FrameOptions.FrameTypes frameType = FrameOptions.FrameTypes.STANDARD_FRAME;

        public Frame(FrameOptions.FrameTypes frameType)
        {
            this.frameType = frameType;
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

    }
}
