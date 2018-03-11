using System;
using System.Collections.Generic;
using System.Linq;

namespace MontyHallProblemSim
{
    class Program
    {
        public static Random random = new Random();
        static void Main(string[] args)
        {
            int gamesToPlay = 0;          
            while (gamesToPlay < 1)
            {
                Console.WriteLine("Number of games to play: ");
                string input = Console.ReadLine();
                int.TryParse(input, out gamesToPlay);
                Console.Clear();
            }

            int gamesWonIfKeepingChoice = RunGamesKeepingChoice(gamesToPlay, true);
            int gamesWonIfChangingChoice = RunGamesKeepingChoice(gamesToPlay, false);

            Console.WriteLine("Games won by keeping choice: " + gamesWonIfKeepingChoice);
            Console.WriteLine("Games won by changing choice: " + gamesWonIfChangingChoice);
            Console.Read();
        }
        /// <summary>
        /// Runs through N number of games with a particular Let's Make a Deal Strategy (Keep first choice or change after reveal)
        /// </summary>
        /// <param name="gamesToPlay"></param>
        /// <param name="strategy"></param>
        /// <returns>An Integer indicating games won out of the provided gamesToPlay variable</returns>
        static int RunGamesKeepingChoice(int gamesToPlay, bool strategy)
        {
            int result = 0;
            for (int i = 0; i < gamesToPlay; i++)
            {
                if (GameResultByKeepingChoice(strategy))
                result++;
            }
            return result;
        }
        /// <summary>
        /// Get's a single result of a round based on the choosen strategy
        /// </summary>
        /// <param name="keepsChoice"></param>
        /// <returns>True if the car was won, False if the car was not.</returns>
        static bool GameResultByKeepingChoice(bool keepsChoice)
        {
            List<Door> doors = ListOfDoors(3);

            int doorWithCar = random.Next(0, 3);
            doors[doorWithCar].HasCar = true;

            int contestantChoice = random.Next(0,3);
            doors[contestantChoice].IsContestantChoice = true;

            Door doorToReveal = doors.First(x => (!x.HasCar && !x.IsContestantChoice));
            doorToReveal.IsRevealed = true;

            if (keepsChoice)
            {
                bool gameResult = (doorWithCar == contestantChoice);
                return gameResult;
            }
            else
            {
                Door newDoorChoice = doors.First(x => (!x.IsRevealed && !x.IsContestantChoice));
                bool gameResult = newDoorChoice.HasCar;
                return gameResult;
            }
        }

        static List<Door> ListOfDoors(int quantity)
        {
            List<Door> doors = new List<Door>();

            for (int i = 0; i < quantity; i++)
                doors.Add(new Door());

            return doors;
        }
    }

    class Door
    {
        public bool HasCar;
        public bool IsRevealed;
        public bool IsContestantChoice;
    }
}
