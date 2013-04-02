using System;
using Exercise.GameOfLife.DomainEntities.Enums;
using Exercise.Games.BusinessLayer;


namespace Exercise.Games
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame _game = new MyGameOfLife();
            LaunchGame(_game);
            Console.ReadLine();
        }

        private static void LaunchGame(IGame _game)
        {
            //Prepare the Game
            var inputResources = _game.Prepare(InputTypeEnum.Random, null);

            //Play the Game
            var gameResult = _game.Play(inputResources);

            //Display the Result
            _game.DisplayResult(gameResult);
        }


    }
}
