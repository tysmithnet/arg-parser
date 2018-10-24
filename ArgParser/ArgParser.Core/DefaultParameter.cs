// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-21-2018
// ***********************************************************************
// <copyright file="Parameter.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace ArgParser.Core
{
    public class DefaultParameter : IParameter
    {
        public Func<object, IIterationInfo, bool> CanConsumeCallback { get; set; }
        public Func<object, IIterationInfo, IIterationInfo> ConsumeCallback { get; set; }

        /// <inheritdoc />
        public DefaultParameter(Func<object, IIterationInfo, bool> canConsumeCallback, Func<object, IIterationInfo, IIterationInfo> consumeCallback)
        {
            CanConsumeCallback = canConsumeCallback ?? throw new ArgumentNullException(nameof(canConsumeCallback));
            ConsumeCallback = consumeCallback ?? throw new ArgumentNullException(nameof(consumeCallback));
        }

        internal DefaultParameter()
        {

        }

        /// <inheritdoc />
        public bool CanConsume(object instance, IIterationInfo info)
        {
            return CanConsumeCallback?.Invoke(instance, info) ?? false;
        }

        /// <inheritdoc />
        public IIterationInfo Consume(object instance, IIterationInfo info)
        {
            return ConsumeCallback?.Invoke(instance, info) ?? info;
        }
    }

    public class DefaultParameter<T> : DefaultParameter, IParameter<T>
    {
        /// <inheritdoc />
        public DefaultParameter(Func<T, IIterationInfo, bool> canConsumeCallback, Func<T, IIterationInfo, IIterationInfo> consumeCallback)
        {
            CanConsumeCallback = (obj, info) =>
            {
                if (obj is T casted)
                    return canConsumeCallback?.Invoke(casted, info) ?? false;
                return false;
            };
            ConsumeCallback = (obj, info) =>
            {
                if (obj is T casted)
                    return consumeCallback?.Invoke(casted, info) ?? info;
                return info;
            };
        }

        /// <inheritdoc />
        public bool CanConsume(T instance, IIterationInfo info)
        {
            return CanConsumeCallback?.Invoke(instance, info) ?? base.CanConsume(instance, info);
        }

        /// <inheritdoc />
        public IIterationInfo Consume(T instance, IIterationInfo info)
        {
            return ConsumeCallback?.Invoke(instance, info) ?? base.ConsumeCallback(instance, info);
        }
    }
}