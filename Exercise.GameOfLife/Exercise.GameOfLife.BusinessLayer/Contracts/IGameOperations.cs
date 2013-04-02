using Exercise.Games.DomainEntities;
namespace Exercise.Games.BusinessLayer
{
    interface IGameRules
    {
        Resources ApplyGameRules(Resources resources);
    }
}
