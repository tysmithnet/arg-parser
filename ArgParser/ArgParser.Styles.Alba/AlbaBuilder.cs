namespace ArgParser.Styles.Alba
{
    public class AlbaBuilder
    {
        protected internal AlbaSettings AlbaSettings { get; set; }
        
        public AlbaBuilder WithParserTheme(string parserId, Theme theme)
        {
            AlbaSettings.ParserThemes.Add(parserId, theme);
            return this;
        }

        public ContextBuilder Finish { get; protected internal set; }
    }
}