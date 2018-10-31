# ArgParser.Flavors.Git
This flavor is similar to how git parses command line arguments.

### Glossary
|Term               |Definition|
|-------------------|----------|
|Sub Command        |A single word at the start of the args that identifies a logic cohesive subset of functionality. Hierarchies of option types can be parsed using sub commands|
|Switch             |A single letter preceeded by `-` or a word preceeded by `--` that identifies a specific set of values|
|Positional         |A list of one or more args that are meant to be consumed in order|
