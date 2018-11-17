using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Alba
{
    public class Template<TModel> where TModel : IViewModel
    {
        public virtual TElement Inflate<TElement>(TModel model) where TElement : Element, new()
        {
            return default(TElement);
        }
    }
}