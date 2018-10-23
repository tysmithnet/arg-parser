# ArgParser
ArgParser is a lightweight library that provides expressive and flexible argument parsing. 

### Motivation
I don't much care for the common approach to this problem of decorating POCOs with attributes. I find it difficult to get these frameworks to do things they weren't designed to do. Because of this, this library uses a fluent syntax for describing how arguments should be processed. My goal is to be able to easily recreate the argument parsing and help text generation of most common command line applications.

### General Philosophy
- Provide a verbose and powerful core library
- Create convenient flavors using the core library
  - git flavor
    - `-m --message`, etc
  - find flavor
    - `-type f -name *.log`
- Expose extension methods to enable terse flavor parsing
- Separate nuget packages for flavors and core

### Features
- [x] Switches, positional arguments, sub commands
- [ ] Validation
- [x] Help text generation  
- [x] Lexing args into tokens
- [x] Parsing of tokens into instances
- [ ] Reasonable default implementations
- [ ] Many examples

### Glossary
|Term               |Definition|
|-------------------|----------|
|Args               |A string array, just like you would get in `main(string[] args)`|
|Instance           |We are turning args into instances whose values are set by the `IParser`|
|Iteration Info     |As the parser works, it will consume tokens. This is the state of that process.|
|Lexer              |Transforms args into tokens to be processed by the `IParser` implementations|
|Parameter          |Some behavior modifying value passed in args. e.g. `--message`|
|Parse Result       |Parse strategies will using parsers and args to produce a result that can interface with the instances created|
|Parse Strategy     |Something that knows how to use parsers to create instances of potentially related instances|
|Parser             |Uses the tokens from the `ILexer` implementation to transform an instance|
|Token              |Args are transformed into tokens by the `ILexer` implementation|
|Validator          |Something that can validate the correctness of a parsed instance|


### Inspirations
- `git commit -am "something"`
- `nmap -vvv -sP`
- `grep -rnw . --include=\*.cs -e "something"`
- `find / -type f -name something`
- `ps aux`