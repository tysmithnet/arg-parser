namespace ArgParser.Core
{
    public class DefaultParameter : IParameter
    {
        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info) =>
            CanConsumeCallback?.Invoke(instance, info) ?? false;

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info) =>
            ConsumeCallback?.Invoke(instance, info) ?? info;

        public CanConsumeCallback CanConsumeCallback { get; set; }
        public ConsumeCallback ConsumeCallback { get; set; }
    }

    public class DefaultParameter<T> : DefaultParameter, IParameter<T>
    {
        /// <inheritdoc />
        public bool CanConsume(T instance, IIterationInfo info) => CanConsumeCallback?.Invoke(instance, info) ?? false;

        /// <inheritdoc />
        public IIterationInfo Consume(T instance, IIterationInfo info) =>
            ConsumeCallback?.Invoke(instance, info) ?? info;

        public new CanConsumeCallback<T> CanConsumeCallback { get; set; }
        public new ConsumeCallback<T> ConsumeCallback { get; set; }
    }
}