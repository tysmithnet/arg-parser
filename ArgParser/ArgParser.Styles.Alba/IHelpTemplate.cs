namespace ArgParser.Styles.Alba
{
    public interface IHelpTemplate<in TVm> where TVm : IViewModel
    {
        void Render(TVm viewModel);
    }
}