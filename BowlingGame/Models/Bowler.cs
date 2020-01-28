using BowlingGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Models
{
    public class Bowler
    {
        private Game game;
        public Bowler()
        {
            game = new Game();
        }

        public List<BowlerOptions.BowlerActions> GetBowlerOptions()
        {
            List<BowlerOptions.BowlerActions> results = Enum.GetValues(typeof(BowlerOptions.BowlerActions)).Cast<BowlerOptions.BowlerActions>().ToList();
            return results;
        }

        public void GoBowling()
        {
            game.ResetGame();
        }

        public GameResult SimulateGame()
        {
            GameResult result;
            while(game.CurrentFrameNumber <= 10)
            {
                game.Bowl(RollOptions.RollTypes.RANDOM);
            }
            result = game.GetResults();

            return result;
        }
    }
}
