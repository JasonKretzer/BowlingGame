using BowlingGame.Enums;
using System;

namespace BowlingGame.Models
{
    /// <summary>
    /// Bowling frame -- may be standard or final type.
    /// </summary>
    public class Frame
    {
        #region Private Fields
        private Roll firstRoll;
        private Roll secondRoll;
        private Roll thirdRoll;

        private Roll spareRoll;

        private Roll strikeRoll1;
        private Roll strikeRoll2;

        private int previousFramesScoreTotal = 0;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Returns whethere this frame is a STANDARD or FINAL frame.
        /// </summary>
        public FrameOptions.FrameTypes FrameType { get; private set; }

        /// <summary>
        /// Is this the final frame in a game
        /// </summary>
        public bool IsFinalFrame => FrameType == FrameOptions.FrameTypes.FINAL_FRAME;

        /// <summary>
        /// Will the CurrentScoreValue change in the future.
        /// </summary>
        public bool IsScoreFinal { get; private set; }

        /// <summary>
        /// Is this frame accumulating points from being a strike.
        /// </summary>
        public bool InStrikeQueue { get; set; }

        /// <summary>
        /// Is this frame accumulating points from being a spare.
        /// </summary>
        public bool InSpareQueue { get; set; }

        /// <summary>
        /// Gives the current running score for this frame.
        /// </summary>
        public int CurrentScoreValue { get { return UpdateScore(); } private set { } }

        /// <summary>
        /// If there was a first roll, returns the roll, else returns empty.
        /// </summary>
        public string FirstRoll
        {
            get
            {
                return (firstRoll == null) ? string.Empty : firstRoll.RollValue.ToString();
            }
            private set { }
        }
        /// <summary>
        /// If there was a second roll, returns the roll, else returns empty.
        /// </summary>
        public string SecondRoll
        {
            get
            {
                return (secondRoll == null) ? string.Empty : secondRoll.RollValue.ToString();
            }
            private set { }
        }
        /// <summary>
        /// If there was a third roll, returns the roll, else returns empty.
        /// </summary>
        public string ThirdRoll
        {
            get
            {
                return (thirdRoll == null) ? string.Empty : thirdRoll.RollValue.ToString();
            }
            private set { }
        }

        /// <summary>
        /// If frame is final, returns the score, else returns empty.
        /// </summary>
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
        #endregion Public Properties

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="frameType"></param>
        public Frame(FrameOptions.FrameTypes frameType = FrameOptions.FrameTypes.STANDARD_FRAME)
        {
            this.FrameType = frameType;
            IsScoreFinal = false;
        }
        #endregion Constructors

        #region Public Methods
        /// <summary>
        /// Adds a roll to this frame.
        /// </summary>
        /// <param name="pinsKnockedDown"></param>
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
            else if (FrameType == FrameOptions.FrameTypes.FINAL_FRAME)
            {
                thirdRoll = pinsKnockedDown;
            }
            else
            {
                throw new ArgumentOutOfRangeException("No more rolls allowed in this frame.");
            }
        }

        /// <summary>
        /// Adds a roll to this frame, if this frame is accumulating points from a strike.
        /// </summary>
        /// <param name="strikeRoll"></param>
        /// <returns></returns>
        public bool AddStrike(Roll strikeRoll)
        {
            if (strikeRoll == null)
            {
                throw new ArgumentNullException("Roll cannot be null.");
            }
            if (strikeRoll1 == null)
            {
                strikeRoll1 = strikeRoll;
                return false;
            }
            else if (strikeRoll2 == null)
            {
                strikeRoll2 = strikeRoll;
            }
            return true;
        }

        /// <summary>
        /// Returns whether this frame can accumulate any more points from a strike.
        /// </summary>
        /// <returns></returns>
        public bool AreStrikesFull()
        {
            return (strikeRoll1 != null && strikeRoll2 != null);
        }

        /// <summary>
        /// Called when this frame can no longer gain any more points and adds the score from the previous frame.
        /// </summary>
        /// <param name="previousFramesScoreTotal"></param>
        public void FinalizeFrame(int previousFramesScoreTotal = 0)
        {
            this.previousFramesScoreTotal = previousFramesScoreTotal;
            IsScoreFinal = true;
        }

        /// <summary>
        /// Adds a roll to this frame, if this frame is accumulating points from a spare.
        /// </summary>
        /// <param name="spareRoll"></param>
        public void AddSpare(Roll spareRoll)
        {
            if (spareRoll == null)
            {
                throw new ArgumentNullException("Roll cannot be null.");
            }
            this.spareRoll = spareRoll;
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Returns an updated score based on summing all rolls, strikes, and spare.
        /// </summary>
        /// <returns></returns>
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
            if (FrameType == FrameOptions.FrameTypes.FINAL_FRAME && thirdRoll != null)
            {
                currentScoreValue += thirdRoll.RollValue;
            }
            if (spareRoll != null)
            {
                currentScoreValue += spareRoll.RollValue;
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
            return currentScoreValue + previousFramesScoreTotal;
        }
        #endregion Private Methods

    }
}
