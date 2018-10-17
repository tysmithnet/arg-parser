# ArgParser
ArgParser is a lightweight command line argument parsing library that provides expressive and flexible argument parsing. I don't much care for the common approach to this problem of decorating POCOs with attributes. I find it difficult to get these frameworks to do things they weren't designed to do. Because of this, this library uses a fluent syntax for describing how arguments should be processed. My goal is to be able to easily recreate the argument parsing and help text generation of most common command line applications.

### Examples
    var commitParser = new ArgParser<CommitOptions>(() => new CommitOptions())
        .WithTokenSwitch(s =>
        {
            s.GroupLetter = 'a';
            s.IsToken = info => info.Cur == "-a" || info.Cur == "--all";
            s.TakeWhile = (info, e, i) => false;
            s.Transformer = (info, opts, strings) => opts.All = true;
        })
        .WithTokenSwitch(s =>
        {
            s.GroupLetter = 'm';
            s.IsToken = info => info.Cur == "-m" || info.Cur == "--message";
            s.TakeWhile = (info, e, i) => i < 1;
            s.Transformer = (info, opts, strings) => opts.Message = strings[0];
            s.Validate = (info, opts, strings, errors) =>
            {
                if (strings.Length != 1)
                    errors.Add(new CardinalityError("Message needs a single string"));
            };
        });

    var parser = new ArgParser<BaseOptions>(() => new BaseOptions())
        .WithTokenSwitch(s =>
        {
            s.GroupLetter = 'n';
            s.IsToken = info => new[] {"-n", "--numbers"}.Contains(info.Cur);
            s.TakeWhile = (info, e, i) => int.TryParse(e, out var throwAway) && i < 5;
            s.Validate = (info, opts, strings, errors) =>
            {
                var nonInts = strings.Where(x => !int.TryParse(x, out var throwAway));
                foreach (var bad in nonInts) errors.Add(new FormatError($"Expected int32 but found {bad}"));
            };
            s.Transformer = (info, opts, strings) =>
                opts.Numbers = strings.Skip(1).Select(x => Convert.ToInt32(x));
        })
        .WithPositional(p =>
        {
            p.TakeWhile = (info, e, i) => i < 1;
            p.Validate = (info, opts, strings, errors) =>
            {
                if (strings.Count() != 1)
                    errors.Add(new CardinalityError($"Expected to find 1 word but found 0"))
            };
            p.Transformer = (info, opts, strings) => opts.Things = strings;
        })
        .WithSubCommand<CommitOptions>("commit", commitParser)
        .When<CommitOptions>(opts => Commit(opts))
        .ParseSubCommands(args);

        
        

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