namespace ArgParser.Core
{
    public class Switch<T> : ISwitch<T>
    {
        /// <inheritdoc />
        public CanHandleCallback<T> CanHandle { get; set; }

        /// <inheritdoc />
        public HandlerCallback<T> Handle { get; set; }
    }
}