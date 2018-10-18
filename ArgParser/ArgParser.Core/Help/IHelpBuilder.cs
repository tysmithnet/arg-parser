namespace ArgParser.Core.Help
{
    public interface IHelpBuilder<T>
    {
        void AddIdentityInfomation(string name = null, string version = null, string shortDescription = null, string url = null);
        void AddSwitch(Switch<T> @switch);
        void AddSubCommand<TSub>(SubCommand<TSub, T> subCommand) where TSub : T;
        void AddPositional(Positional<T> positional);
    }
}