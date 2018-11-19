namespace ArgParser.Core
{
    public interface IAliasRepository
    {
        string GetAlias(string parserId);
        void SetAlias(string parserId, string alias);
    }
}