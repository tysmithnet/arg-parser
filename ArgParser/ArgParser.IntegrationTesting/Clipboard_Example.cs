using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ArgParser.Core;
using ArgParser.Core.Help;
using FluentAssertions;
using Xunit;

namespace ArgParser.IntegrationTesting
{
    public class Clipboard_Example
    {
        private class ClipboardOptions
        {
            public bool IsOverwrite { get; set; }
            public int Verbosity { get; set; }
            public string LogFile { get; set; }
        }

        private class SortOptions : ClipboardOptions
        {
            public bool IsDescending { get; set; }
        }

        private class ZipOptions : ClipboardOptions
        {
            public string FileName { get; set; }
        }

        private SubCommandArgParser<SortOptions, ClipboardOptions> SortParser { get; set; }
        private SubCommandArgParser<ZipOptions, ClipboardOptions> ZipParser { get; set; }
        private ArgParser<ClipboardOptions> BaseParser { get; set; }

        /// <inheritdoc />
        public Clipboard_Example()
        {
            Setup();
        }

        private void Setup()
        {
            BaseParser = new ArgParser<ClipboardOptions>(() => new ClipboardOptions());
            SortParser = new SubCommandArgParser<SortOptions, ClipboardOptions>(() => new SortOptions());
            ZipParser = new SubCommandArgParser<ZipOptions, ClipboardOptions>(() => new ZipOptions());
            SetupSortParser();
            SetupZipParser();
            SetupBaseParser();
        }

        private void SetupSortParser()
        {
            SortParser
                .WithSwitch(new Switch<SortOptions>()
                {
                    TakeWhile = (info, token, number) => false,
                    GroupLetter = 'd',
                    IsToken = info => info.Cur == "-d" || info.Cur == "--descending" || info.Cur == "--desc",
                    Transformer = (info, instance, strings) => instance.IsDescending = true,
                    Help = new HelpInfo()
                    {
                        Name = "Sort Descending",
                        ShortDescription = "Sort from highest to lowest",
                        Synopsis = "-d",
                    }
                });
        }

        private void SetupZipParser()
        {
            ZipParser
                .WithSwitch(new Switch<ZipOptions>()
                {
                    TakeWhile = (info, token, number) => number == 0,
                    GroupLetter = 'f',
                    IsToken = info => info.Cur == "-f" || info.Cur == "--file", 
                    Transformer = (info, instance, strings) => instance.FileName = strings[0],
                    Help = new HelpInfo()
                    {
                        Name = "Sort Descending",
                        ShortDescription = "Sort from highest to lowest",
                        Synopsis = "-d",
                    }
                });
        }

        private void SetupBaseParser()
        {
            BaseParser
                .WithHelp(new HelpInfo()
                {
                    Name = "clip",
                    ShortDescription = "Interact with clipboard items",
                    Description = @"Interact with the clipboard
    * Text
    * Audio
    * Files
    * Data",
                    Version = "1.0.0.0",
                    Synopsis = "clip -o -vvv -l file.log",
                    Url = "www.example.org"
                })
                .WithSwitch(new Switch<ClipboardOptions>()
                {
                    GroupLetter = 'o',
                    TakeWhile = (info, token, number) => false,
                    Transformer = (info, instance, strings) => instance.IsOverwrite = true,
                    IsToken = info => info.Cur == "-o" || info.Cur == "--overwrite",
                    Help = new HelpInfo()
                    {
                        Name = "Overwrite",
                        ShortDescription = "Overwrite the contents of the clipboard with the result of this operation",
                        Synopsis = "-o",
                    }
                })
                .WithSwitch(new Switch<ClipboardOptions>()
                {
                    TakeWhile = (info, token, number) => false,
                    IsToken = info => Regex.IsMatch(info.Cur, "-v+"),
                    Transformer = (info, instance, strings) =>
                        instance.Verbosity += info.Cur.ToCharArray().Count(x => x == 'v'),
                    Help = new HelpInfo()
                    {
                        Name = "Verbosity",
                        ShortDescription = "Increase the amount of feedback to be given",
                        Synopsis = "-vvv",
                        Description = "You can add an arbitrary number of v's to increase or decrease verbosity"
                    }
                })
                .WithSwitch(new Switch<ClipboardOptions>()
                {
                    TakeWhile = (info, token, number) => number == 0,
                    Transformer = (info, instance, strings) => instance.LogFile = strings[0],
                    IsToken = info => info.Cur == "-l" || info.Cur == "--log-file",
                    GroupLetter = 'l',
                    Help = new HelpInfo()
                    {
                        Name = "Log File",
                        ShortDescription = "Send logs to this file",
                        Synopsis = "-l file.log",
                    }
                }).WithSubCommand(new SubCommand<SortOptions, ClipboardOptions>()
                {
                    IsCommand = info => info.Cur == "sort",
                    ArgParser = SortParser,
                    Help = new HelpInfo()
                    {
                        Name = "Sort",
                        ShortDescription = "Sort the contents of the clipboard",
                        Synopsis = "sort -d",
                    },
                }).WithSubCommand(new SubCommand<ZipOptions, ClipboardOptions>()
                {
                    IsCommand = info => info.Cur == "zip",
                    ArgParser = ZipParser,
                    Help = new HelpInfo()
                    {
                        Name = "Zip",
                        ShortDescription = "Zip the files currently on the clipboard",
                        Synopsis = "zip -n file.zip",
                    }
                });
        }

        [Fact]
        public void Test_Help_Text()
        {
            // arrange
            Setup();

            // act
            // assert
            if(BaseParser.HelpBuilder.Build() is LeafNode textNode)
            {
                textNode.Text.Trim().Should().Be(@"clip - 1.0.0.0
Interact with clipboard items

Synopsis:
    clip -o -vvv -l file.log [sort -d] [zip -n file.zip]

Sub Commands:
    sort -d - Sort the contents of the clipboard
    zip -n file.zip - Zip the files currently on the clipboard

Switches:
    -o
    Overwrite the contents of the clipboard with the result of this operation

    -vvv
    Increase the amount of feedback to be given

    -l file.log
    Send logs to this file

Positionals:
".Trim());
            }
            else
            {
                true.Should().BeFalse("We are expecting a TextNode but didn't get one");
            }
        }
    }
}
