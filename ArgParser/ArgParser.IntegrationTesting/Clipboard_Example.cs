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
                    HelpHints = new HelpHints()
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
                    HelpHints = new HelpHints()
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
                .WithIdentity(new IdentityInformation("clip")
                {
                    Version = "v1.2.3.4",
                    ShortDescription = "Interact with the clipboard",
                    Url = "www.example.com"
                })
                .WithSwitch(new Switch<ClipboardOptions>()
                {
                    GroupLetter = 'o',
                    TakeWhile = (info, token, number) => false,
                    Transformer = (info, instance, strings) => instance.IsOverwrite = true,
                    IsToken = info => info.Cur == "-o" || info.Cur == "--overwrite",
                    HelpHints = new HelpHints()
                    {
                        Name = "Overwrite",
                        ShortDescription = "Overwrite the contents of the clipboard with the result of this operation",
                        Synopsis = "-o",
                        Description = "",
                        Url = ""
                    }
                })
                .WithSwitch(new Switch<ClipboardOptions>()
                {
                    TakeWhile = (info, token, number) => false,
                    IsToken = info => Regex.IsMatch(info.Cur, "-v+"),
                    Transformer = (info, instance, strings) =>
                        instance.Verbosity += info.Cur.ToCharArray().Count(x => x == 'v'),
                    HelpHints = new HelpHints()
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
                    HelpHints = new HelpHints()
                    {
                        Name = "Log File",
                        ShortDescription = "Send logs to this file",
                        Synopsis = "-l file.log",
                    }
                }).WithSubCommand(new SubCommand<SortOptions, ClipboardOptions>()
                {
                    Identity = new IdentityInformation("sort"),
                    IsCommand = info => info.Cur == "sort",
                    ArgParser = SortParser,
                    HelpHints = new HelpHints()
                    {
                        Name = "Sort",
                        ShortDescription = "Sort the contents of the clipboard",
                        Synopsis = "sort",
                    },
                }).WithSubCommand(new SubCommand<ZipOptions, ClipboardOptions>()
                {
                    Identity = new IdentityInformation("zip"),
                    IsCommand = info => info.Cur == "zip",
                    ArgParser = ZipParser,
                    HelpHints = new HelpHints()
                    {
                        Name = "Zip",
                        ShortDescription = "Zip the files currently on the clipboard",
                        Synopsis = "zip",
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
                textNode.Text.Trim().Should().Be(@"clip - v1.2.3.4
Interact with the clipboard
Sub Commands:
	sort - Sort the contents of the clipboard
	zip - Zip the files currently on the clipboard
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
