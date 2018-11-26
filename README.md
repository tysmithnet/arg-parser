# ArgParser
Flexible argument parsing library and help screen generator
![demo](https://i.imgur.com/NgWFpfS.gif "Mock program example")

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

### Badges
|develop|master|
|-|-|
|[![Coverage Status](https://coveralls.io/repos/github/tysmithnet/arg-parser/badge.svg?branch=develop)](https://coveralls.io/github/tysmithnet/arg-parser?branch=develop)|[![Coverage Status](https://coveralls.io/repos/github/tysmithnet/arg-parser/badge.svg?branch=master)](https://coveralls.io/github/tysmithnet/arg-parser?branch=master)|
|[![Build status](https://ci.appveyor.com/api/projects/status/wu5c3q2lphnv45k2/branch/master?svg=true)](https://ci.appveyor.com/project/tysmithnet/arg-parser/branch/develop)|[![Build status](https://ci.appveyor.com/api/projects/status/wu5c3q2lphnv45k2/branch/develop?svg=true)](https://ci.appveyor.com/project/tysmithnet/arg-parser/branch/master)|
|N/A|![Quality Gates](https://sonarcloud.io/api/project_badges/measure?project=tysmithnet_arg-parser&metric=alert_status)|
