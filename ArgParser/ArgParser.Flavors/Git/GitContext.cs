namespace ArgParser.Flavors.Git
{
    public class GitContext : IGitContext
    {
        public IGitParserRepository GitParserRepository { get; set; } = new GitParserRepository();
        public IGitParameterRepository GitParameterRepository { get; set; } = new GitParameterRepository();
        public IGitValidatorRepository GitValidatorRepository { get; set; } = new GitValidatorRepository();
        public IGitFactoryFunctionRepository GitFactoryFunctionRepository { get; set; } = new GitFactoryFunctionRepository();
    }
}