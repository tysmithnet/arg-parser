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
        public DefaultParameter(Func<object, IIterationInfo, bool> canConsumeCallback,
            Func<object, IIterationInfo, IIterationInfo> consumeCallback)
        {
            CanConsumeCallback = canConsumeCallback ?? throw new ArgumentNullException(nameof(canConsumeCallback));
            ConsumeCallback = consumeCallback ?? throw new ArgumentNullException(nameof(consumeCallback));
        }

        internal DefaultParameter()
        {
        }

            
        public bool CanConsume(object instance, IIterationInfo info) =>
            CanConsumeCallback?.Invoke(instance, info) ?? false;

            
        public IIterationInfo Consume(object instance, IIterationInfo info) =>
            ConsumeCallback?.Invoke(instance, info) ?? info;

            
        public void Reset()
        {
            ;
        }

        public Func<object, IIterationInfo, bool> CanConsumeCallback { get; set; }
        public Func<object, IIterationInfo, IIterationInfo> ConsumeCallback { get; set; }
    }

    public class DefaultParameter<T> : DefaultParameter, IParameter<T>
    {
            
        public DefaultParameter(
            Func<T, IIterationInfo, bool> canConsumeCallback,
            Func<T, IIterationInfo, IIterationInfo> consumeCallback)
        {
            if (canConsumeCallback == null)
                throw new ArgumentNullException(nameof(canConsumeCallback));
            if (consumeCallback == null)
                throw new ArgumentNullException(nameof(consumeCallback));
            CanConsumeCallback = (obj, info) =>
            {
                if (obj is T casted)
                    return canConsumeCallback(casted, info);
                return false;
            };
            ConsumeCallback = (obj, info) =>
            {
                if (obj is T casted)
                    return consumeCallback(casted, info);
                return info;
            };
        }

            
        public bool CanConsume(T instance, IIterationInfo info) => CanConsumeCallback(instance, info);

            
        public IIterationInfo Consume(T instance, IIterationInfo info) => ConsumeCallback(instance, info);
    }
}