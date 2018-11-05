using System.Text;

namespace ArgParser.Styles.Default
{
    public class ContextBuilder
    {
        protected internal ParserRepository ParserRepository { get; set; }= new ParserRepository();
        protected internal HierarchyRepository HierarchyRepository { get; set; } = new HierarchyRepository();

        public ParserBuilder AddParser(string id)
        {
            var parser = ParserRepository.Create(id);
            HierarchyRepository.AddParser(id);
            return new ParserBuilder(this, parser);
        }

        public ContextBuilder CreateParentChildRelationship(string parent, string child)
        {
            HierarchyRepository.EstablishParentChildRelationship(parent, child);
            return this;
        }

        public Context BuildContext()
        {
            return new Context()
            {
                HierarchyRepository = HierarchyRepository,
                ParserRepository = ParserRepository
            };
        }
    }
}
