
using ArgParser.Core;

namespace ArgParser.Styles.Extensions
{
    /// <summary>
    ///     IContext decorator for Extension integrations
    /// </summary>
    /// <seealso cref="ArgParser.Core.IContext" />
    public class ExtensionContext : IContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExtensionContext" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ExtensionContext(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        /// <summary>
        ///     Gets the alias repository.
        /// </summary>
        /// <value>The alias repository.</value>
        public IAliasRepository AliasRepository => Context.AliasRepository;

        /// <summary>
        ///     Gets the hierarchy repository.
        /// </summary>
        /// <value>The hierarchy repository.</value>
        public IHierarchyRepository HierarchyRepository => Context.HierarchyRepository;

        /// <summary>
        ///     Gets the parser repository.
        /// </summary>
        /// <value>The parser repository.</value>
        public IParserRepository ParserRepository => Context.ParserRepository;

        /// <summary>
        ///     Gets or sets the theme repository.
        /// </summary>
        /// <value>The theme repository.</value>
        public IThemeRepository ThemeRepository { get; protected internal set; } = new ThemeRepository();

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        protected internal IContext Context { get; set; }
    }
}