# ArgParser
ArgParser is a lightweight library that provides expressive and flexible argument parsing. 

### Motivation
I don't much care for the common approach to this problem of decorating POCOs with attributes. I find it difficult to get these frameworks to do things they weren't designed to do. Because of this, this library uses a fluent syntax for describing how arguments should be processed. My goal is to be able to easily recreate the argument parsing and help text generation of most common command line applications.

### Examples
        [Fact]
        public void Pass_The_ReadMe_Example()
        {
            // arrange
            var addParser = new SubCommandArgParser<AddOptions, BaseOptions>(() => new AddOptions())
                .WithName("add")
                .WithPositional(new Positional<AddOptions>
                {
                    TakeWhile = (info, s, arg3) => arg3 == 0
                })
                .WithSwitch(new Switch<AddOptions>
                {
                    GroupLetter = 'a',
                    IsToken = info => info.Cur == "-a" || info.Cur == "--all",
                    TakeWhile = (info, s, arg3) => false,
                    Transformer = (info, options, arg3) => options.All = true
                })
                .WithPositional(new Positional<AddOptions>
                {
                    TakeWhile = (info, s, arg3) => true,
                    Transformer = (info, options, arg3) => options.Files = arg3.ToList()
                })
                .WithValidation((info, instance) =>
                {
                    if (!instance.All && instance.Files == null || instance.Files.Count == 0)
                        info.Errors.Add(new CardinalityError($"You must either specify all or identify files"));
                });

            var commitParser = new SubCommandArgParser<CommitOptions, BaseOptions>(() => new CommitOptions())
                .WithName("commit")
                .WithPositional(new Positional<CommitOptions>
                {
                    TakeWhile = (info, s, i) => i == 0,
                    Transformer = (info, options, arg3) => { }
                })
                .WithSwitch(new Switch<CommitOptions>
                {
                    GroupLetter = 'a',
                    IsToken = info => info.Cur == "-a" || info.Cur == "--all",
                    TakeWhile = (info, e, i) => false,
                    Transformer = (info, opts, strings) => opts.All = true
                })
                .WithSwitch(new Switch<CommitOptions>
                {
                    GroupLetter = 'm',
                    IsToken = info => info.Cur == "-m" || info.Cur == "--message",
                    TakeWhile = (info, e, i) => i < 1,
                    Transformer = (info, opts, strings) => opts.Message = strings[0]
                })
                .WithValidation((info, instance) =>
                {
                    if (instance.Message.Length > 10)
                        info.Errors.Add(
                            new FormatError("Expected message to be 10 or less characters for some reason"));
                });

            var parser = new ArgParser<BaseOptions>(() => new BaseOptions())
                .WithName("base")
                .WithSwitch(new Switch<BaseOptions>
                {
                    GroupLetter = 'n',
                    IsToken = info => new[] {"-n", "--numbers"}.Contains(info.Cur),
                    TakeWhile = (info, e, i) => int.TryParse(e, out var throwAway) && i < 5,

                    Transformer = (info, opts, strings) =>
                        opts.Numbers = strings.Select(x => Convert.ToInt32(x)).ToList()
                })
                .WithPositional(new Positional<BaseOptions>
                {
                    TakeWhile = (info, e, i) => true,
                    Transformer = (info, opts, strings) => opts.Things = strings.ToList()
                })
                .WithSubCommand(new SubCommand<CommitOptions, BaseOptions>
                {
                    IsCommand = info => info.Cur == "commit",
                    ArgParser = commitParser
                })
                .WithSubCommand(new SubCommand<AddOptions, BaseOptions>
                {
                    IsCommand = info => info.Cur == "add",
                    ArgParser = addParser
                });

            // act
            // assert
            var isAddParsed = false;
            var isCommitParsed = false;
            var isAddValidated = false;
            var isCommitValidated = false;

            parser
                .Parse("add -a file1 file2".Split(' '))
                .When<AddOptions>(opts =>
                {
                    isAddParsed = true;
                    opts.All.Should().BeTrue();
                    opts.Files.Should().BeEquivalentTo("file1", "file2");
                });

            parser
                .Parse("add".Split(' '))
                .When<AddOptions>(opts => { })
                .WhenErrored(info =>
                {
                    isAddValidated = true;
                    info.Errors.Should().HaveCount(1);
                });

            parser
                .Parse("commit -am something -n 1 2 3 thing1 thing2".Split(' '))
                .When<CommitOptions>(opts =>
                {
                    isCommitParsed = true;
                    opts.All.Should().BeTrue();
                    opts.Numbers.Should().BeEquivalentTo(new[] {1, 2, 3});
                    opts.Message.Should().Be("something");
                    opts.Things.Should().BeEquivalentTo("thing1", "thing2");
                });

            parser
                .Parse("commit -m somethingreallynotthatlong -n 1 2 3 thing1 thing2".Split(' '))
                .WhenErrored(info =>
                {
                    isCommitValidated = true;
                    info.Errors.Should().HaveCount(1);
                });

            isAddParsed.Should().BeTrue();
            isAddValidated.Should().BeTrue();
            isCommitParsed.Should().BeTrue();
            isCommitValidated.Should().BeTrue();
        }
    }


        
        

### Features
- [x] Fluent syntax
  - [x] Setup of switches and positionals
  - [x] Fluent result parsing 
- [x] Verbs e.g. `git commit`
- [x] Multiple boolean switches e.g. `grep -rnw`
- [x] Boolean cardinality e.g. `nmap -vvv`
- [ ] Groups e.g. input, logging, processing, output
- [ ] Help page generation
- [x] Custom switch syntax strategies e.g `-t` `--thing` `/t` `t:something`
- [x] Required parameters
- [x] Custom multiple value iterators
- [x] Enums

### Glossary
|Term               |Definition|
|-------------------|----------|
|Arg                |Anything passed in the command line arguments passed to an application|
|Options            |Arguments are parsed into options. Options describe the requested behavior of the application|
|Positional         |An input whose purpose is derived from its position in arguments that do not belong to another switch|
|Subcommand         |A subcommand that typically takes has its own set of options. Called verb in other frameworks.|
|Switch             |A flag or token indicating that a behavior is requested, e.g. `-v` `--theme=dark` `-rnw`|
|Switch Group       |A group of switches all passed in a single token|
|Token              |A textual hint that indicates a switch has been found e.g. `-t` `--things` `/?` `filetype:` `--include=`|

### Inspirations
- `git commit -am "something"`
- `nmap -vvv -sP`
- `grep -rnw . --include=\*.cs -e "something"`
- `find / -type f -name something`
- `ps aux`