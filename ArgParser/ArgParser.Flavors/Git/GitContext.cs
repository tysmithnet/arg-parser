namespace ArgParser.Flavors.Git
{
    public class GitContext : IGitContext
    {
        public IGitFactoryFunctionRepository FactoryFunctionRepository { get; set; } =
            new GitFactoryFunctionRepository();

        public IGitParameterRepository ParameterRepository { get; set; } = new GitParameterRepository();
        public IGitParserRepository ParserRepository { get; set; } = new GitParserRepository();
        public IGitValidatorRepository ValidatorRepository { get; set; } = new GitValidatorRepository();
    }
}