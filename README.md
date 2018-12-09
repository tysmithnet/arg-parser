# ArgParser
Flexible argument parsing library and help screen generator
![demo](https://i.imgur.com/NgWFpfS.gif "Mock program example")

### Install + Start Hacking
    Install-Package ArgParser -Version 1.0.0-rc3
    dotnet add package ArgParser --version 1.0.0-rc3
    paket add ArgParser --version 1.0.0-rc3

[Setup your context](https://github.com/tysmithnet/arg-parser/blob/master/ArgParser/ArgParser.Testing.Common/DefaultBuilder.cs)

[Parser some args](https://github.com/tysmithnet/arg-parser/blob/master/ArgParser/ArgParser.TestApp/Program.cs)

The `ArgParser.TestApp` program is what is used to create the gif on the root README. You can use it to test your own applications or see how the gif was made.

### Why?
Most existing libraries in this space do so using attributes and reflection. I find this approach to be restrictive and not conducive to extension. Extension is important because there a seemingly infinite amount of styles for how args should be interpretted.
 - switch style
   - `-h`, `--help`, `/?`, `-help`, `--value=something`, `value:something`
 - groupable parameters
   - `git commit -am something`
 - sub commands
   - `dotnet new`
 - count switches
   - `-vvv`

### Goal
> Provide a framework that allows for the creation of any type of argument parser, and a set of extensions that allow the user to opt into common styles.

It needs to be fairly trivial to create parsers for some of the most commonly used commands: git, find, dotnet along with similar help generation.

### Trivial Example
``` C#
using System;
using System.Linq;
using ArgParser.Core;
using ArgParser.Styles;
using ArgParser.Styles.Extensions;
using Figgle;

namespace testnuget
{
    class UtilOptions
    {
        public bool IsHelpRequested { get; set; }
        public int SomeValue { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new ContextBuilder()
            .AddParser<UtilOptions>("util", help =>
            {
                help
                    .SetName("utility")
                    .SetShortDescription("General utility tool");
            })
            .WithFactoryFunction(() => new UtilOptions())
            .WithBooleanSwitch('h', "help", o => o.IsHelpRequested = true)
            .WithSingleValueSwitch('v', "value", (o, s) => o.SomeValue = Convert.ToInt32(s), help =>
            {
                help
                    .SetName("Some Value")
                    .SetDefaultValue("1")
                    .SetShortDescription("Some value for something");
            })
            .Finish
            .RegisterExtensions()
            .AddAutoHelp((parseResults, exceptions) =>
            {
                foreach (var kvp in parseResults)
                    if (kvp.Key is UtilOptions casted && casted.IsHelpRequested)
                        return kvp.Value.Id;

                var missingValues = exceptions.OfType<MissingValueException>();
                var first = missingValues.FirstOrDefault();
                return first?.Parser.Id;
            })
            .SetTheme("util",
                    Theme.Create(FiggleFonts.Doom, ConsoleColor.Green, ConsoleColor.DarkGreen, ConsoleColor.Yellow, ConsoleColor.Red,
                        ConsoleColor.Yellow))
            .Parse(args)
            .When<UtilOptions>((o, p) => {
                Console.WriteLine(o.SomeValue);
            });
            Console.ReadKey();
        }
    }
}

```

### Badges
|develop|master|
|-|-|
|[![Coverage Status](https://coveralls.io/repos/github/tysmithnet/arg-parser/badge.svg?branch=develop)](https://coveralls.io/github/tysmithnet/arg-parser?branch=develop)|[![Coverage Status](https://coveralls.io/repos/github/tysmithnet/arg-parser/badge.svg?branch=master)](https://coveralls.io/github/tysmithnet/arg-parser?branch=master)|
|[![Build status](https://ci.appveyor.com/api/projects/status/wu5c3q2lphnv45k2/branch/master?svg=true)](https://ci.appveyor.com/project/tysmithnet/arg-parser/branch/develop)|[![Build status](https://ci.appveyor.com/api/projects/status/wu5c3q2lphnv45k2/branch/develop?svg=true)](https://ci.appveyor.com/project/tysmithnet/arg-parser/branch/master)|
|N/A|![Quality Gates](https://sonarcloud.io/api/project_badges/measure?project=tysmithnet_arg-parser&metric=alert_status)|
