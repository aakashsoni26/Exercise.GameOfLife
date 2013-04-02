/*************************************************************************************
 * Author: Aakash Soni
 * Description:
 * Created Date:
  *************************************************************************************/

using Exercise.GameOfLife.DomainEntities.Enums;
using Exercise.Games.DomainEntities;

namespace Exercise.Games.BusinessLayer
{
    public interface IGame
    {
        /// <summary>
        /// Method to Prepare the resources of the game
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        Resources Prepare(InputTypeEnum inputType, Resources resource);

        /// <summary>
        /// Method to Play the game with resources
        /// </summary>
        /// <param name="inputCriteria"></param>
        /// <returns></returns>
        Resources Play(Resources inputCriteria);

        /// <summary>
        /// Method to display the result or outcome of the game
        /// </summary>
        /// <param name="outCome"></param>
        void DisplayResult(Resources outCome);
    }

   
}
