using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class GitParameterRepository_Should
    {
        [Fact]
        public void Throw_If_Attempting_Add_Bad_Parameter_Data()
        {
            // arrange
            var repo = new GitParameterRepository();
            Action mightThrow0 = () => repo.AddParameter(null, null);
            Action mightThrow1 = () => repo.AddParameter("bad", null);
            Action mightThrow2 = () => repo.AddParameter(null, new BooleanSwitch());

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_If_Requested_Flavor_Id_Is_Not_Registered_Or_Null()
        {
            // arrange
            var repo = new GitParameterRepository();

            Action mightThrow0 = () => repo.GetParameters(null);
            Action mightThrow1 = () => repo.GetParameters("doesntexist");
            
            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Correctly_Return_Parameters_For_A_Registered_Flavor()
        {
            // arrange
            var repo = new GitParameterRepository();
            foreach (var name in "git commit".Split(' '))
            {
                for (int i = 0; i < 4; i++)
                {
                    repo.AddParameter(name, new BooleanSwitch());
                }
                for (int i = 0; i < 3; i++)
                {
                    repo.AddParameter(name, new Positional());
                }
                for (int i = 0; i < 2; i++)
                {
                    repo.AddParameter(name, new SingleValueSwitch());
                }
                for (int i = 0; i < 1; i++)
                {
                    repo.AddParameter(name, new ValuesSwitch());
                }
            }

            // act
            // assert
            repo.GetParameters("git").Should().HaveCount(10);
            repo.GetBooleanSwitches("git").Should().HaveCount(4);
            repo.GetSwitches("git").Should().HaveCount(7);
            repo.GetPositionals("git").Should().HaveCount(3);
            repo.GetParameters("commit").Should().HaveCount(10);
            repo.GetBooleanSwitches("commit").Should().HaveCount(4);
            repo.GetSwitches("commit").Should().HaveCount(7);
            repo.GetPositionals("commit").Should().HaveCount(3);
        }
    }
}
