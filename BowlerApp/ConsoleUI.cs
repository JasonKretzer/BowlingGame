using BowlerApp.Enums;
using BowlingCommon;
using BowlingGame.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BowlerApp
{
    /// <summary>
    /// Basic Console based user interface handler for the Bowling Engine
    /// </summary>
    public class ConsoleUI
    {
        /// <summary>
        /// Displays the entire Bowler options list and prompts the user for a choice then forces user to enter a valid choice.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public BowlerOptions.BowlerActions DisplayAndGetSelectedOption(List<BowlerOptions.BowlerActions> options)
        {
            Console.WriteLine("Please choose an option below:\n");
            foreach (var item in options)
            {
                Console.WriteLine($"{(int)item}. {EnumHelper.GetDescription(item)}");
            }
            string choice = GetValidOption(options);
            int result = -1;
            int.TryParse(choice, out result);
            return (BowlerOptions.BowlerActions)result;
        }

        /// <summary>
        /// Displays that the game is over.
        /// </summary>
        public void DisplayGameIsOver()
        {
            Console.WriteLine("Current game is over, no more rolls will be accepted.");
        }

        /// <summary>
        /// Displays messages about how the bowler did on the roll given by the parameter.
        /// </summary>
        /// <param name="roll"></param>
        public void DisplayBowl(int roll)
        {
            if(roll == 10)
            {
                Console.WriteLine("WOW -- say it again backwards, WOW");
            }
            else if(roll > 3 && roll < 7)
            {
                Console.WriteLine("Could be better...");
            }
            else if(roll >= 7)
            {
                Console.WriteLine("Not bad.");
            }

            Console.WriteLine($"You knocked down {roll} pins!");
        }

        /// <summary>
        /// Displays a single frame as given by the parameter.
        /// </summary>
        /// <param name="frame"></param>
        public void DisplaySingleFrame(Frame frame)
        {
            Console.Write($"|{frame.FirstRoll}| |{frame.SecondRoll}| ");
            if (frame.IsFinalFrame)
            {
                Console.Write($"|{frame.ThirdRoll}|");
            }
            Console.WriteLine($"\n{frame.PrintableScore}");
            Console.WriteLine("===================\n");

        }

        /// <summary>
        /// Displays the current frame number as given by the parameter.
        /// </summary>
        /// <param name="frameNumber"></param>
        public void DisplayCurrentFrameNumber(int frameNumber)
        {
            Console.WriteLine($"Current Frame: {frameNumber}");
            Console.WriteLine("\n\n");

        }

        /// <summary>
        /// Prompts the user for the frame number and forces them to enter a valid frame number.
        /// </summary>
        /// <returns></returns>
        public int GetFrameFromUser()
        {
            string choice = string.Empty;
            List<int> options = new List<int>
            {
                1,2,3,4,5,6,7,8,9,10
            };
            bool notValidChoice = true;
            do
            {
                Console.Write("\n\nFrame Choice: ");
                choice = Console.ReadLine();
                if (!ValidateFrameOption(choice, options))
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
                else
                {
                    notValidChoice = false;
                }
            } while (notValidChoice);
            int result = -1;
            int.TryParse(choice, out result);
            return result;
        }

        /// <summary>
        /// Displays that the simulation is complete.
        /// </summary>
        public void DiplaySimulationMessage()
        {
            Console.WriteLine("Simulation Complete.");
        }

        /// <summary>
        /// Displays a goodbye message for when a game ends.
        /// </summary>
        public void SayGoodByeGameOver()
        {
            Console.WriteLine("Congratulations, you finished a whole game. Here is your final score.");
        }

        /// <summary>
        /// Chides the user for quitting a game.
        /// </summary>
        public void SayGoodByeUserQuit()
        {
            Console.WriteLine("Sorry to see you cop out like this. Here is your final score.");
        }

        /// <summary>
        /// Display final goodbye message.
        /// </summary>
        public void SayFinalGoodBye()
        {
            Console.WriteLine("Alas, all good things must come to an end.");
        }

        /// <summary>
        /// Prompts the user to play again and forces user to return a valid response.
        /// </summary>
        /// <returns></returns>
        public bool PlayAgain()
        {
            Console.WriteLine("Would you like to play again? ");
            bool notValidChoice = true;
            bool isYes = false;
            do
            {
                if (!ValidateYesNo(out isYes))
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
                else
                {
                    notValidChoice = false;
                }
            } while (notValidChoice);
            return isYes;
        }

        /// <summary>
        /// Confirms whether a new simulation should be run or not.
        /// </summary>
        /// <returns></returns>
        public bool ConfirmStartSimulation()
        {
            Console.WriteLine("This will end your current game.  Do you wish to simulate a new game? ");
            bool notValidChoice = true;
            bool isYes = false;
            do
            {
                if (!ValidateYesNo(out isYes))
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
                else
                {
                    notValidChoice = false;
                }
            } while (notValidChoice);
            return isYes;
        }

        /// <summary>
        /// Confirms whether a new game should be started or not
        /// </summary>
        /// <returns></returns>
        public bool ConfirmReset()
        {
            Console.WriteLine("Would you like to start a new game? ");
            bool notValidChoice = true;
            bool isYes = false;
            do
            {
                if(!ValidateYesNo(out isYes))
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
                else
                {
                    notValidChoice = false;
                }
            } while (notValidChoice);
            return isYes;
        }
        /// <summary>
        /// Prints a complete listing of all frames.
        /// </summary>
        /// <param name="frames"></param>
        public void DisplayFrames(Frame[] frames)
        {
            int frameNumber = 1;
            foreach (var frame in frames)
            {
                Console.WriteLine($"Frame: {frameNumber++}");
                DisplaySingleFrame(frame);
            }
        }

        /// <summary>
        /// Displays a welcome message.
        /// </summary>
        public void DisplayWelcome()
        {
            Console.WriteLine("Welcome To Bowling");
            Console.WriteLine("A new game has already been started for you.");
        }

        #region Private Methods

        /// <summary>
        /// Forces the user to enter a valid bowler option
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private string GetValidOption(List<BowlerOptions.BowlerActions> options)
        {
            string choice = string.Empty;
            bool notValidChoice = true;
            do
            {
                Console.Write("\n\nChoice: ");
                choice = Console.ReadLine();
                if (!ValidateOption(choice, options))
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
                else
                {
                    notValidChoice = false;
                }
            } while (notValidChoice);
            return choice;
        }

        /// <summary>
        /// Validates whether a user entered string is a yes or a no.
        /// </summary>
        /// <param name="isYes">Is written to by this method specifying whether the user entered yes or no.</param>
        /// <returns></returns>
        private bool ValidateYesNo(out bool isYes)
        {
            Console.Write("Choice (yes/no) : ");
            string choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice))
            {
                isYes = false;
                return false;
            }
            choice = choice.Trim();
            if (Regex.IsMatch(choice.ToUpper(), @"^[YES|NO]*$"))
            {
                isYes = string.Equals(choice, "YES", StringComparison.CurrentCultureIgnoreCase);
                return true;
            }
            isYes = false;
            return false;
        }

        /// <summary>
        /// Validates if the user entered a proper frame number for the number for the list of options.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private bool ValidateFrameOption(string input, List<int> options)
        {
            if (string.IsNullOrEmpty(input) || input.Length > 2)
            {
                return false;
            }
            input = input.Trim();
            int parsedValue = -1;
            if (int.TryParse(input, out parsedValue))
            {
                if (parsedValue <= 0 || parsedValue > options.Count)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Validates the user entered string is a valid bowler option.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private bool ValidateOption(string input, List<BowlerOptions.BowlerActions> options)
        {
            if(string.IsNullOrEmpty(input) || input.Length > 1)
            {
                return false;
            }
            input = input.Trim();
            int parsedValue = -1;
            if(int.TryParse(input, out parsedValue))
            {
                if(parsedValue < 0 || parsedValue >= options.Count)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion Private Methods
    }
}
