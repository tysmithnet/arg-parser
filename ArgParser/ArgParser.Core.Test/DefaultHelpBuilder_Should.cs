using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core.Help;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultHelpBuilder_Should
    {
        [Fact]
        public void Return_The_Correct_Value_For_Null_Object()
        {
            // arrange
            var builder = new DefaultHelpBuilder();
            builder.AddGenericHelp()

            // act

            // assert
        }
    }
}
