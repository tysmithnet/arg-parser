using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Flavors.Git
{
    public class GitParameterRepository : IGitParameterRepository
    {
        public virtual void AddParameter(string parserName, GitParameter parameter)
        {
            if (parserName == null)
                throw new ArgumentNullException(nameof(parserName));

            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            if (!Parameters.ContainsKey(parserName))
                Parameters.Add(parserName, new List<GitParameter>());
            Parameters[parserName].Add(parameter);
        }

        public bool Contains(string name) => Parameters.ContainsKey(name);

        public virtual IEnumerable<BooleanSwitch> GetBooleanSwitches(string parserName) =>
            GetParameters(parserName).OfType<BooleanSwitch>();

        public IEnumerable<GitParameter> GetParameters(string parserName)
        {
            if (parserName == null)
                throw new ArgumentNullException(nameof(parserName));
            if (Parameters.ContainsKey(parserName))
                return Parameters[parserName].ToList();
            throw new ArgumentOutOfRangeException($"Could not find a registered parser with name={parserName}");
        }

        public virtual IEnumerable<Positional> GetPositionals(string parserName) =>
            GetParameters(parserName).OfType<Positional>();

        public virtual IEnumerable<Switch> GetSwitches(string parserName) => GetParameters(parserName).OfType<Switch>();

        private Dictionary<string, IList<GitParameter>> Parameters { get; } =
            new Dictionary<string, IList<GitParameter>>();
    }
}