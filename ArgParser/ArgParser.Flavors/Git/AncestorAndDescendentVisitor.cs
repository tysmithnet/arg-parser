using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Flavors.Git
{
    public class AncestorAndDescendentVisitor : IGitFlavorVisitor
    {
            
        public void Visit(GitFlavor gitFlavor)
        {
            var itr = gitFlavor.BaseFlavor;
            while (itr != null)
            {
                GitFlavors.Add(itr);
                itr = itr.BaseFlavor;
            }

            var queue = new Queue<GitFlavor>();
            queue.Enqueue(gitFlavor);

            while (queue.Any())
            {
                var first = queue.Dequeue();
                GitFlavors.Insert(0, first);
                foreach (var sc in first.SubCommands) queue.Enqueue(sc.Value);
            }

            GitFlavors = GitFlavors.OrderByDescending(f => f.Depth).ToList();
        }

        public IList<GitFlavor> GitFlavors { get; set; } = new List<GitFlavor>();
    }
}