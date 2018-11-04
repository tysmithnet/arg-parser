using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;
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

        public void AddGlobalValidator(IValidator validator)
        {
            validator.ThrowIfArgumentNull(nameof(validator));
            if (GlobalValidators.Contains(validator))
                return;
            GlobalValidators.Add(validator);
        }

        public bool Contains(string name) => Validators.ContainsKey(name);

        public IEnumerable<IValidator> GetValidators(string parserName)
        {
            if (!Validators.ContainsKey(parserName))
                throw new KeyNotFoundException(
                    $"Unable to find parser with name={parserName}, are you sure it was added?");
            return Validators[parserName].ToList();
        }

        protected internal IList<IValidator> GlobalValidators { get; set; } = new List<IValidator>();
        
        protected internal Dictionary<string, IList<IValidator>> Validators { get; set; } =
            new Dictionary<string, IList<IValidator>>();
    }
}