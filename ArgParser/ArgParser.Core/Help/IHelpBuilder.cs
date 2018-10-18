namespace ArgParser.Core.Help
{
    public interface IHelpBuilder<T>
    {
        IHelpBuilder<T> AddIdentityInfomation(IdentityInformation information);
        IHelpBuilder<T> AddSwitch(Switch<T> @switch);
        IHelpBuilder<T> AddSubCommand<TSub>(ISubCommand subCommand) where TSub : T;
        IHelpBuilder<T> AddPositional(Positional<T> positional);
        Node Build();
    }
}