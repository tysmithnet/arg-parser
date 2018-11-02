namespace ArgParser.Flavors.Git
{
    public class GitContext : IGitContext
    {
        public IGitParserRepository ParserRepository { get; set; } = new GitParserRepository();
        public IGitParameterRepository ParameterRepository { get; set; } = new GitParameterRepository();
        public IGitValidatorRepository ValidatorRepository { get; set; } = new GitValidatorRepository();
        public IGitFactoryFunctionRepository FactoryFunctionRepository { get; set; } = new GitFactoryFunctionRepository();
    }
}