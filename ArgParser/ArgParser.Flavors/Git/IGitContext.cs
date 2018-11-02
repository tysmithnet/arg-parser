namespace ArgParser.Flavors.Git
{
    public interface IGitContext
    {
        IGitParserRepository ParserRepository { get; }
        IGitParameterRepository ParameterRepository { get; }
        IGitValidatorRepository ValidatorRepository { get; }
        IGitFactoryFunctionRepository FactoryFunctionRepository { get; }
    }
}