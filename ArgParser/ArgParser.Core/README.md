# ArgParser.Core
Herein lies the most base classes for ArgParser. If you need to see how the default implementations are written or find an interface, this is where you will find them.

# Concepts
### Lexing
- Turning `string[] args` into `IEnumberable<IToken> tokens`
- First step in entire process
  - First point of extension
- Massage the input strings if necessary

### Parsing
- Turing `IEnumberable<IToken> tokens` into `T instance`
- Majority of code revolves the construction and execution of `IParser`s

### Validation
- After an instance has been parsed out, it must be validated against a rule set
  - Examples
    - Required arguments
    - Valid ranges
    - Incompatible values