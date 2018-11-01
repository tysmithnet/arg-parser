using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgParser.Flavors.Git
{
    public class GitParameterRepository : IGitParameterRepository
    {
        private Dictionary<string, IList<GitParameter>> Parameters { get; set; } = new Dictionary<string, IList<GitParameter>>();
        
        public virtual void AddParameter(string flavorName, GitParameter parameter)
        {
            if(flavorName == null)
                throw new ArgumentNullException(nameof(flavorName));

            if(parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            if(!Parameters.ContainsKey(flavorName))
                Parameters.Add(flavorName, new List<GitParameter>());
            Parameters[flavorName].Add(parameter);
        }

        public virtual IEnumerable<Positional> GetPositionals(string flavorName)
        {
            return GetParameters(flavorName).OfType<Positional>();
        }

        public virtual IEnumerable<Switch> GetSwitches(string flavorName)
        {
            return GetParameters(flavorName).OfType<Switch>();
        }

        public virtual IEnumerable<BooleanSwitch> GetBooleanSwitches(string flavorName)
        {
            return GetParameters(flavorName).OfType<BooleanSwitch>();
        }

        public IEnumerable<GitParameter> GetParameters(string flavorName)
        {
            if (flavorName == null)
                throw new ArgumentNullException(nameof(flavorName));
            if (Parameters.ContainsKey(flavorName))
                return Parameters[flavorName].ToList();
            throw new ArgumentOutOfRangeException($"Could not find a registered flavor with name={flavorName}");
        }
    }
}
