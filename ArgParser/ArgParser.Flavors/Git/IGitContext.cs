namespace ArgParser.Flavors.Git
{
    public interface IGitContext
    {
        IGitParserRepository GitParserRepository { get; }
        IGitParameterRepository GitParameterRepository { get; }
        IGitValidatorRepository GitValidatorRepository { get; }
        IGitFactoryFunctionRepository GitFactoryFunctionRepository { get; }
    }
}