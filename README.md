# ArgParser
ArgParser is a lightweight command line argument parsing library that provides and expressive and flexible parsing of arguments. I don't much care for the common approach of decorating POCOs with attributes. Because of this, this library uses a fluent syntax for describing how arguments should be processed. My goal is to be able to easily recreate the argument parsing and help text generation of most common command line applications.

### Examples
    # myprog --file c:\temp\a.txt -t target0 target1 -d duke charlie
    new ArgParser<MyOptions>()
        .WithTokenStrategy(TokenStrategy.SingleAndDoubleDashses)
        .WithBoolean('d', opts => opts.Debug = true)
        .WithSingleSwitch('f', "file", (opts, f) => opts.File = new FileInfo(f))
        .WithMultipleSwitch('t', (opts, s) => opts.Targets = s, min:1)
        .WithPositional((opts, strings) => opts.Dogs = strings)
        .Parse<MyOptions>(myOptions, args);

    # git commit -am "New stuff"
    var commitParser = new ArgParser<CommitOptions>()
        .WithTokenStrategy(Strategy.SingleAndDoubleDashses)
        .WithGroupingStrategy(GroupingStrategy.Letters.WithLastSwitchTakesArgs())
        .WithBooleanSwitch('-a', "--all", opts => opts.All = true)
        .WithSingleSwitch('-m', "--message", (opts, s) => opts.Message = s)
    new ArgParser<GitOptions>()
        .WithSingleSwitch("--git-dir", (opts, s) => opts.GitDirectory = s, separator:"=")
        .WithSubCommand<CommitOptions>(commitParser)
        .Parse
        
        

### Features
- [ ] Fluent syntax
  - [x] Setup of switches and positionals
  - [ ] Fluent result parsing 
- [ ] Verbs e.g. `git commit`
- [x] Multiple boolean switches e.g. `grep -rnw`
- [ ] Boolean cardinality e.g. `nmap -vvv`
- [ ] Groups e.g. input, logging, processing, output
- [ ] Help page generation
- [ ] Custom switch syntax strategies e.g `-t` `--thing` `/t` `t:something`
- [ ] Required parameters
- [ ] Custom multiple value iterators
- [ ] Enums

### Glossary
|Term               |Definition|
|-------------------|----------|
|Arg                |Anything passed in the command line arguments passed to an application|
|Options            |Arguments are parsed into options. Options describe the requested behavior of the application|
|Positional         |An input whose purpose is derived from its position in arguments that do not belong to another switch|
|Subcommand         |A subcommand that typically takes has its own set of options. Called verb in other frameworks.|
|Switch             |A flag or token indicating that a behavior is requested, e.g. `-v` `--theme=dark` `-rnw`|
|Token              |A textual hint that indicates a switch has been found e.g. `-t` `--things` `/?` `filetype:` `--include=`|

### Inspirations
- `git commit -am "something"`
- `nmap -vvv -sP`
- `grep -rnw . --include=\*.cs -e "something"`
- `find / -type f -name something`
- `ps aux`