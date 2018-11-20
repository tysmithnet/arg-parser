# ArgParser.Core
This project contains the core functionality and building blocks to create parsers and help information.

### Glossary
|Term|Definition|
|-|-|
|Alias|Parsers must have unique ids, but commands can share the same name by using aliases.|
|Args|A string array like you would expect to receive in `Main(string[] args)`|
|Consumer|Something that is capable of using a subsequence of args and doing something with them|
|Context|An environment in which parsers co-exist to collectively parse arguments|
|Hierarchy|Parsers can be related such that they can cooperate to consume arguments. This is most useful when sub commands are desirable.|
|Parameter|A consumer that represents the invididual units of information contained in the args|
|Parser|An aggregate consumer that is capable of producing an instance and using args to populate it|