using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Flavors.Git
{
    public class AncestorAndDescendentVisitor : IGitFlavorVisitor
    {
        public void Visit(GitParser gitParser)
        {
            var itr = gitParser.BaseFlavor;
            while (itr != null)
            {
                GitFlavors.Add(itr);
                itr = itr.BaseFlavor;
            }

            var queue = new Queue<GitParser>();
            queue.Enqueue(gitParser);

            while (queue.Any())
            {
                var first = queue.Dequeue();
                GitFlavors.Insert(0, first);
                foreach (var sc in first.SubCommands) queue.Enqueue(sc.Value);
            }

            GitFlavors = GitFlavors.OrderByDescending(f => f.Depth).ToList();
        }

        public IList<GitParser> GitFlavors { get; set; } = new List<GitParser>();
    }
}