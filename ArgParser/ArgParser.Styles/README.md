# Arg.Parser.Styles
`ArgParser.Core` provides a small but powerful set of features, but there are many *styles* of parsing that are familiar and common enough to warrant support by default. This project attempts to encapsulate some of these common styles of arg parsing and provide enough integration points that it can be tweaked easily.

### Default Style Explanaition
The default style is my biased opinion as to what is the most middle-of-the-road style.
- Switches are parameters that have a text token identifying them, e.g. `--values 1 2 3`
  - Switches can be identified by letters `-h`, words `--help`, or both
  - `BooleanSwitch`, like `--help` do not require any additional args
  - `SingleValueSwitch` like `--message` take a single string argument
  - `SeparatedSwitch` like `format:png` where a single string contains the switch and value
  - `ValuesSwitch` take any number of arguments
- Positionals are parameters whose significance is derived from their relative order, e.g. `cp src dst`
- SubCommands are args that indicate that a more specific type of parser is to be used
- Parsers are joined together in parent-child relationships and can cooperate to parse args into instances in an inheritance hierarchy
- Boolean switches can be combined and one optional single or values switch can be appended

