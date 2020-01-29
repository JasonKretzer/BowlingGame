using BowlerApp.Enums;
using BowlerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowlingGame.Enums;

namespace BowlerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Bowler bowler = new Bowler();
            List<BowlerOptions.BowlerActions> options = bowler.GetBowlerOptions();
            ConsoleUI ui = new ConsoleUI();
            ui.DisplayWelcome();
            BowlerOptions.BowlerActions choice = ui.DisplayAndGetSelectedOption(options);
            bool continueGame = true;
            while(continueGame)
            {
                switch (choice)
                {
                    case BowlerOptions.BowlerActions.BOWL:
                        if (!bowler.IsGameFinished)
                        {
                            ui.DisplayBowl(bowler.Bowl(RollOptions.RollTypes.RANDOM));
                        }
                        else
                        {
                            ui.DisplayGameIsOver();
                            if(ui.ConfirmReset())
                            {
                                bowler.NewGame();
                            }
                        }
                        break;
                    case BowlerOptions.BowlerActions.VIEW_FRAMES:
                        ui.DisplayFrames(bowler.GetFrames());
                        break;
                    case BowlerOptions.BowlerActions.VIEW_CURRENT_FRAME:
                        ui.DisplaySingleFrame(bowler.GetCurrentFrame());
                        break;
                    case BowlerOptions.BowlerActions.GET_CURRENT_FRAME_NUMBER:
                        ui.DisplayCurrentFrameNumber(bowler.GetCurrentFrameNumber());
                        break;
                    case BowlerOptions.BowlerActions.VIEW_FRAME_AT:
                        ui.DisplaySingleFrame(bowler.GetFrameAtNumber(ui.GetFrameFromUser()));
                        break;
                    case BowlerOptions.BowlerActions.RESET:
                        if(ui.ConfirmReset())
                        {
                            bowler.NewGame();
                        }
                        break;
                    case BowlerOptions.BowlerActions.SIMULATE_RANDOM_GAME:
                        if (ui.ConfirmStartSimulation())
                        {
                            ui.DisplayFrames(bowler.SimulateGame().Frames);
                            ui.DiplaySimulationMessage();

                            if(ui.ConfirmReset())
                            {
                                bowler.NewGame();
                            }
                        }
                        break;
                    case BowlerOptions.BowlerActions.QUIT:
                    default:
                        ui.DisplayFrames(bowler.GetFrames());
                        ui.SayGoodByeUserQuit();
                        continueGame = ui.PlayAgain();
                        break;
                }
                if(!bowler.IsGameFinished && continueGame)
                {
                    choice = ui.DisplayAndGetSelectedOption(options);
                }
                else if(continueGame)
                {
                    ui.DisplayFrames(bowler.GetFrames());
                    ui.SayGoodByeGameOver();
                    continueGame = ui.PlayAgain();
                }
            }
            ui.SayFinalGoodBye();
            Console.Write("Press Any Key to close this window.");
            Console.ReadKey();
        }
    }
}
