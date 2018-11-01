using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Flavors.Git
{
    public class GitBuilder
    {
        public IGitParserRepository GitParserRepository { get; set; } = new GitParserRepository();
        public IGitParameterRepository GitParameterRepository { get; set; } = new GitParameterRepository();
        public IGitValidatorRepository GitValidatorRepository { get; set; } = new GitValidatorRepository();
    }
}
