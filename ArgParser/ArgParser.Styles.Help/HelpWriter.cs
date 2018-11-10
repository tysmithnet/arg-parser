﻿using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Help
{
    public class HelpWriter
    {
        public string CreateHelpText(RootNode rootNode, int width = 80)
        {
            var visitor = new HelpWriterVisitor();
            var doc = (Document) visitor.Visit(rootNode);
            var text = ConsoleRenderer.RenderDocumentToText(doc, new TextRenderTarget(),
                new Rect(0, 0, width, Size.Infinity));
            return text.Length > 0 ? text.Remove(text.Length - 2) : text;
        }
    }
}