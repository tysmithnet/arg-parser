// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-12-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-25-2018
// ***********************************************************************
// <copyright file="ParserRepository.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    ///     Default IParserRepository implementation
    /// </summary>
    /// <seealso cref="ArgParser.Core.IParserRepository" />
    public class ParserRepository : IParserRepository
    {
        /// <summary>
        ///     Determines whether there is a parser registered with a specific id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if there is a parser registered with a specific id otherwise, <c>false</c>.</returns>
        public bool Contains(string id) => Parsers.ContainsKey(id);

        /// <summary>
        ///     Creates a parser with the specified id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The created parser</returns>
        /// <exception cref="ArgumentException">Parser already exists with id={id}</exception>
        public virtual Parser Create(string id)
        {
            if (Parsers.ContainsKey(id))
                throw new ArgumentException($"Parser already exists with id={id}");
            var parser = new Parser(id);
            if (FirstAdded == null)
                FirstAdded = parser;
            Parsers.Add(id, parser);
            return parser;
        }

        /// <summary>
        ///     Creates a parser with the specified id
        /// </summary>
        /// <typeparam name="T">The type of the instance the parser can create</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>The created parser</returns>
        /// <exception cref="ArgumentException">Parser already exists with id={id}</exception>
        public virtual Parser<T> Create<T>(string id)
        {
            if (Parsers.ContainsKey(id))
                throw new ArgumentException($"Parser already exists with id={id}");
            var parser = new Parser<T>(id);
            if (FirstAdded == null)
                FirstAdded = parser;
            Parsers.Add(id, parser);
            return parser;
        }

        /// <summary>
        ///     Gets the parser with the specified id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Existing Parser.</returns>
        /// <exception cref="KeyNotFoundException">Unable to find parser with id={id}</exception>
        public virtual Parser Get(string id)
        {
            if (!Parsers.ContainsKey(id))
                throw new KeyNotFoundException(
                    $"Unable to find parser with id={id}, are you sure it was added and you are using the correct id?");
            return Parsers[id];
        }

        /// <summary>
        ///     Gets the parser with the specified id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Existing parser</returns>
        public virtual Parser<T> Get<T>(string id)
        {
            var parser = Get(id);
            return (Parser<T>) parser;
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;Parser&gt;.</returns>
        public virtual IEnumerable<Parser> GetAll() => Parsers.Values;

        /// <summary>
        ///     Gets or sets the first added.
        /// </summary>
        /// <value>The first added.</value>
        protected internal Parser FirstAdded { get; set; }

        /// <summary>
        ///     Gets or sets the parsers.
        /// </summary>
        /// <value>The parsers.</value>
        protected internal Dictionary<string, Parser> Parsers { get; set; } = new Dictionary<string, Parser>();
    }
}