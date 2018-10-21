namespace ArgParser.Core
{
    public class Doer
    {
        public void DoStuff(string[] args)
        {
            var parser = new DefaultParser<BaseOptions>();
            parser.AddSwitch(new Switch<BaseOptions>()
            {
                CanHandle = (instance, info) => info.Current.Raw == "do",
                Handle = (instance, info) =>
                {
                    instance.IsThing = true;
                    return info.Consume(1);
                }
            });
            parser.AddSwitch(new Switch<BaseOptions>()
            {
                CanHandle = (instance, info) => info.Current.Raw == "-t",
                Handle = (instance, info) =>
                {
                    instance.Thing = info.Next?.Raw ?? "unknown";
                    return info.Consume(2);
                }
            });

            var childParser = new DefaultParser<ChildOptions, BaseOptions>();
            childParser.BaseParser = parser;
            
        }
    }
}