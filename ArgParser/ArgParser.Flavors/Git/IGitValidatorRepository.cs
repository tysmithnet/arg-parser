﻿using System.Collections.Generic;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public interface IGitValidatorRepository
    {
        void AddValidator(string parserName, IValidator validator);
        bool Contains(string name);
        IEnumerable<IValidator> GetValidators(string parserName);
    }
}