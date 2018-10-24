using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class GitOptions
    {
        public bool IsHelpRequested { get; set; }
        public bool IsVersionRequested { get; set; }
    }

    public class CommitOptions : GitOptions
    {
        public bool IsAddAll { get; set; }
        public string Message { get; set; }
    }

    public class GitFlavor_Should
    {

        [Fact]
        public void Identify_Help_Requested()
        {
            // arrange
            var flavor = new GitFlavor();
            flavor.Parameters.Add(new GitParameter()
            {
                Letter = 'h',
                Word = "--help"
            });

            // act


            // assert
        }
    }
}
