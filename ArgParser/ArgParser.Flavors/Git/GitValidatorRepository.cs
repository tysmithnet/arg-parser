using System.Collections.Generic;
using System.Linq;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class GitValidatorRepository : IGitValidatorRepository
    {
        internal List<IValidator> Validators { get; set; } = new List<IValidator>();

        public void AddValidator(IValidator validator)
        {
            Validators.Add(validator);
        }

        /// <inheritdoc />
        public IEnumerable<IValidator> GetValidators()
        {
            return Validators.ToList();
        }
    }
}