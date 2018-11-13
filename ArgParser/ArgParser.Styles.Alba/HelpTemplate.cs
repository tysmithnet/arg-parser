using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public interface IViewModel
    {
        IContext Context { get; }
    }

    public interface IHelpTemplate<in TVm> where TVm : IViewModel
    {
        void Render(TVm viewModel);
    }

    public class DefaultViewModel : IViewModel
    {
        public IContext Context { get; protected internal set; }

        public string Title { get; set; } = "Program";
        public Visibility TitleVisibility { get; set; } = Visibility.Collapsed;
        public string SubTitle { get; set; } = "A program that does something";
        public Visibility SubTitleVisibility { get; set; } = Visibility.Collapsed;
    }

    public class DefaultHelpTemplate : IHelpTemplate<DefaultViewModel>
    {
        public void Render(DefaultViewModel viewModel)
        {
            using (var fs = File.OpenRead("Views/Default.xaml"))
            {
                var document = ConsoleRenderer.ReadDocumentFromStream(fs, viewModel);
                ConsoleRenderer.RenderDocument(document);
            }
        }
    }
}
