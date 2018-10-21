﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ArgParser.Core;
using Xunit;

namespace ArgParser.IntegrationTests
{
    public class Git_Example
    {
        private class BaseOptions
        {
            public bool IsVersionRequested { get; set; }
            public bool IsHelpRequested { get; set; }

        }

        private class AddOptions : BaseOptions
        {
            public bool IsAll { get; set; }
            public bool IsVerbose { get; set; }
            public string[] PathSpecs { get; set; }
        }

        private class CommitOptions : BaseOptions
        {
            public bool IsAll { get; set; }
            public string Author { get; set; }
            public string[] Files { get; set; }
            public string Message { get; set; }
            public bool IsVerbose { get; set; }
        }

        private class GitLexer : ILexer
        {
            /// <inheritdoc />
            public IEnumerable<IToken> Lex(string[] args)
            {
                var list = args.ToList();
                for (var index = 0; index < list.Count; index++)
                {
                    var item = list[index];
                    if (!Regex.IsMatch(item.Trim(), "^-[avm]+$")) continue;
                    var commands = item.Trim().Substring(1).ToCharArray().Reverse();
                    foreach (var command in commands)
                    {
                        list.RemoveAt(index);
                        list.Insert(index, $"-{command}");
                    }
                }

                return list.Select(s => new Token(s));
            }
        }

        [Fact]
        public void Add_All_Commit_With_Message()
        {
            // arrange
            var commitParser = new DefaultParser<CommitOptions>();
            commitParser.AddParameter(new Parameter<CommitOptions>()
            {
                CanHandle = (instance, info) => info.Current.Raw == ""
            });

            // act

            // assert
        }
    }
}
