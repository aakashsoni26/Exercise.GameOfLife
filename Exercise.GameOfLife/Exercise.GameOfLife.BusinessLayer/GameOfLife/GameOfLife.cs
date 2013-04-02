using Exercise.GameOfLife.DomainEntities.Enums;
using Exercise.Games.DomainEntities;

namespace Exercise.Games.BusinessLayer
{
    public class MyGameOfLife : IGame
    {
        private GameOfLifeOperations _gameOfLifeOperations = new GameOfLifeOperations();

        public Resources Resource { get; set; }
        public Resources Prepare(InputTypeEnum inputType, Resources resource)
        {
            if (inputType == InputTypeEnum.Random)
                Resource = _gameOfLifeOperations.PrepareGameOfLife();
            else
                Resource = resource;

            return Resource;
        }

        public Resources Play(Resources inputCriteria)
        {
            var gameResult = _gameOfLifeOperations.ApplyGameRules(Resource);
            return gameResult;
        }

        public void DisplayResult(Resources resource)
        {
            _gameOfLifeOperations.Display(resource);
        }


    }
}
