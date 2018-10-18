// ***********************************************************************
// Assembly         : ArgParser.Core
// Author           : @tysmithnet
// Created          : 10-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-17-2018
// ***********************************************************************
// <copyright file="IIterationInfo.cs" company="ArgParser.Core">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArgParser.Core
{
    /// <summary>
    ///     Represents an object that can identify where in the parsing process
    ///     we currently are.
    /// </summary>
    public interface IIterationInfo
    {
        /// <summary>
        ///     Adds the error.
        /// </summary>
        /// <param name="error">The error.</param>
        void AddError(ParsingError error);

        /// <summary>
        ///     Clones this instance.
        /// </summary>
        /// <returns>IterationInfo.</returns>
        IIterationInfo Clone();

        /// <summary>
        ///     Gets all arguments.
        /// </summary>
        /// <value>All arguments.</value>
        string[] AllArgs { get; }

        /// <summary>
        ///     Gets the current token
        /// </summary>
        /// <value>The current.</value>
        string Cur { get; }

        /// <summary>
        ///     Gets the tokens from the current one onward
        /// </summary>
        /// <value>The current on.</value>
        string[] CurOn { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value><c>true</c> if this instance has errors; otherwise, <c>false</c>.</value>
        bool HasErrors { get; }

        /// <summary>
        ///     Gets the index.
        /// </summary>
        /// <value>The index.</value>
        int Index { get; set; }

        /// <summary>
        ///     Gets a value indicating whether iteration has reached the end
        /// </summary>
        /// <value><c>true</c> if this instance is at the end; otherwise, <c>false</c>.</value>
        bool IsEnd { get; }

        /// <summary>
        ///     Gets the tokens following the current one to the end
        /// </summary>
        /// <value>The rest.</value>
        string[] Rest { get; }
    }
}