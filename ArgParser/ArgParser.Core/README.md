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

### Help Building
- You can't have an argument parsing library without support for help generation
- There is a subset of DOM-like elements exposed by default, but the default parser only produces a single block of text
  - [x] Root
  - [x] Text
  - [x] Code
  - [x] Table
    - [x] Table Row
    - [x] Table Data
  - [x] Heading
  - [ ] Ordered List
  - [ ] Unordered List
