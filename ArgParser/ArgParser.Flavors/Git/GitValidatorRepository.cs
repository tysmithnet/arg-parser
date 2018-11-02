using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class GitValidatorRepository : IGitValidatorRepository
    {
        public void AddValidator(string parserName, IValidator validator)
        {
            if (parserName == null)
                throw new ArgumentNullException(nameof(parserName));
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));
            if (!Validators.ContainsKey(parserName))
                Validators.Add(parserName, new List<IValidator>());
            Validators[parserName].Add(validator);
        }

        public IEnumerable<IValidator> GetValidators(string parserName)
        {
            if (!Validators.ContainsKey(parserName))
                throw new KeyNotFoundException(
                    $"Unable to find parser with name={parserName}, are you sure it was added?");
            return Validators[parserName].ToList();
        }

        protected internal Dictionary<string, IList<IValidator>> Validators { get; set; } = new Dictionary<string, IList<IValidator>>();
    }
}