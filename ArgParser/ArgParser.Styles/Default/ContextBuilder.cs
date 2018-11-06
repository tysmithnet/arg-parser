namespace ArgParser.Styles.Default
{
    public class ContextBuilder
    {
        public ParserBuilder AddParser(string id)
        {
            var parser = ParserRepository.Create(id);
            HierarchyRepository.AddParser(id);
            return new ParserBuilder(this, parser);
        }

        public ParserBuilder<T> AddParser<T>(string id)
        {
            var parser = ParserRepository.Create<T>(id);
            HierarchyRepository.AddParser(id);
            return new ParserBuilder<T>(this, parser);
        }

        public Context BuildContext() => new Context
        {
            HierarchyRepository = HierarchyRepository,
            ParserRepository = ParserRepository
        };

        public ContextBuilder CreateParentChildRelationship(string parent, string child)
        {
            HierarchyRepository.EstablishParentChildRelationship(parent, child);
            return this;
        }

        protected internal HierarchyRepository HierarchyRepository { get; set; } = new HierarchyRepository();
        protected internal ParserRepository ParserRepository { get; set; } = new ParserRepository();
    }
}