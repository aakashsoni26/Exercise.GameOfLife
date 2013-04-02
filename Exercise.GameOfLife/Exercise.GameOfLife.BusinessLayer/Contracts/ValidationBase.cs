using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Games.BusinessLayer.Contracts
{
    public abstract class ValidationBase
    {
        public ValidationBase()
        {
            ErrorMessages = new List<string>();
        }

        public List<String> ErrorMessages { get; set; }

        /// <summary>
        /// Validation Method to Validate the resource inputs
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public abstract bool IsValid(Exercise.Games.DomainEntities.Resources resources);
    }
}
