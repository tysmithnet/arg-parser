namespace ArgParser.Flavors.Git
{
    public interface IGitContext
    {
        IGitFactoryFunctionRepository FactoryFunctionRepository { get; }
        IGitParameterRepository ParameterRepository { get; }
        IGitParserRepository ParserRepository { get; }
        IGitValidatorRepository ValidatorRepository { get; }
    }
}