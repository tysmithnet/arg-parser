using System;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public abstract class Template<TVm> where TVm : IViewModel
    {
        public AlbaContext Context { get; set; }
        public TVm ViewModel { get; set; }

        protected Template(IContext context, TVm viewModel)
        {
            Context = new AlbaContext(context.ThrowIfArgumentNull(nameof(context)));
            ViewModel = viewModel.ThrowIfArgumentNull(nameof(viewModel));
        }

        public abstract Document Create();
    }
}
