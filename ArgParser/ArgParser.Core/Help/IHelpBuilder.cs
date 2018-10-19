namespace ArgParser.Core.Help
{
    public interface IHelpBuilder<T>
    {
        IHelpBuilder<T> AddSwitch(Switch<T> @switch);
        IHelpBuilder<T> AddSubCommand<TSub>(ISubCommand subCommand) where TSub : T;
        IHelpBuilder<T> AddPositional(Positional<T> positional);
        IHelpBuilder<T> AddHelp(IHelp help);
        Node Build();
    }
}