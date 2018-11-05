using System.Text;

namespace ArgParser.Styles.Default
{
    public class ContextBuilder
    {
        protected internal ParserRepository ParserRepo { get; set; }= new ParserRepository();
        protected internal HierarchyRepository HierarchyRepository { get; set; } = new HierarchyRepository();

        public ParserBuilder AddParser(string id)
        {
            var parser = ParserRepo.Create(id);
            HierarchyRepository.AddParser(id);
            return new ParserBuilder(this, parser);
        }
    }
}
