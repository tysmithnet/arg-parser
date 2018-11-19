// ***********************************************************************
// Assembly         : ArgParser.Styles
// Author           : @tysmithnet
// Created          : 11-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParseStrategy.cs" company="ArgParser.Styles">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    /// <summary>
    /// Default IParseStrategy implementation
    /// <remarks>
    /// Steps:
    /// 1. Identify the parser chain
    /// 2. Mutate the args for those parsers
    /// 3. Create the iteration info from the results of the first steps
    /// 4. Create an instance from the most derived parser
    /// 5. Get potential consumption results
    /// 6. Evaluate them for the best option
    /// 7. Create a consumption request for the identified consumer
    /// 8. Ask the consumer to process the consumption request
    /// 9. Repeat 5-8 until the args are consumed
    /// </remarks>
    /// </summary>
    /// <seealso cref="ArgParser.Core.IParseStrategy" />
    public class ParseStrategy : IParseStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseStrategy"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="rootParserId">The root parser identifier.</param>
        public ParseStrategy(IContext context, string rootParserId)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
            RootParserId = rootParserId.ThrowIfArgumentNull(nameof(rootParserId));
            SetContext(context);
        }

        /// <summary>
        /// The context
        /// </summary>
        private IContext _context;

        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The result of the parsing</returns>
        /// <exception cref="NoFactoryFunctionException"></exception>
        /// <exception cref="UnexpectedArgException"></exception>
        public virtual IParseResult Parse(string[] args)
        {
            ChainIdentificationResult chainRes = null;
            try
            {
                chainRes = ChainIdentificationStrategy.Identify(new ChainIdentificationRequest(args, Context));
                var mutatedArgs = ArgsMutator.Mutate(new MutateArgsRequest(args, chainRes.Chain, Context));
                var info = IterationInfoFactory.Create(new IterationInfoRequest(chainRes, mutatedArgs, args));
                if (chainRes.IdentifiedParser.FactoryFunction == null)
                    throw new NoFactoryFunctionException(chainRes.IdentifiedParser);
                var instance = chainRes.IdentifiedParser.FactoryFunction();
                while (!info.IsComplete())
                {
                    var potentialConsumerRequest = new PotentialConsumerRequest(chainRes, info, instance);
                    var potentialConsumerResult = PotentialConsumerStrategy.IdentifyPotentialConsumer(potentialConsumerRequest);
                    if (!potentialConsumerResult.Success)
                        throw new UnexpectedArgException(info);
                    var selected = ConsumerSelectionStrategy.Select(potentialConsumerResult);
                    var consumptionRequest = ConsumptionRequestFactory.Create(potentialConsumerResult, selected);
                    var consumptionResult = selected.ConsumingParameter.Consume(instance, consumptionRequest);
                    if (consumptionResult.ParseExceptions.Any())
                        return new ParseResult(new Dictionary<object, Parser>(), consumptionResult.ParseExceptions);
                    info = consumptionResult.Info;
                }

                return ParseResultFactory.Create(new Dictionary<object, Parser>
                {
                    [instance] = chainRes.IdentifiedParser
                }, null);
            }
            catch (ParseException e)
            {
                return new ParseResult(null, e.ToEnumerableOfOne());
            }
            finally
            {
                chainRes?.Chain.ToList().ForEach(p => p.Reset());
            }
        }

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="context">The context.</param>
        private void SetContext(IContext context)
        {
            _context = context;
            if (ArgsMutator == null)
                ArgsMutator = new ArgsMutator(context);
            if (ChainIdentificationStrategy == null)
                ChainIdentificationStrategy = new ParserChainIdentificationStrategy(context);
            if (ConsumerSelectionStrategy == null)
                ConsumerSelectionStrategy = new ConsumerSelectionStrategy(context);
            if (ConsumptionRequestFactory == null)
                ConsumptionRequestFactory = new ConsumptionRequestFactory(context);
            if (IterationInfoFactory == null)
                IterationInfoFactory = new IterationInfoFactory(context);
            if (ParseResultFactory == null)
                ParseResultFactory = new ParseResultFactory(context);
            if (PotentialConsumerStrategy == null)
                PotentialConsumerStrategy = new PotentialConsumerStrategy(context);
            ArgsMutator.Context = context;
            ChainIdentificationStrategy.Context = context;
            ConsumerSelectionStrategy.Context = context;
            ConsumptionRequestFactory.Context = context;
            IterationInfoFactory.Context = context;
            ParseResultFactory.Context = context;
            PotentialConsumerStrategy.Context = context;
        }

        /// <summary>
        /// Gets or sets the arguments mutator.
        /// </summary>
        /// <value>The arguments mutator.</value>
        public IArgsMutator ArgsMutator { get; set; }
        /// <summary>
        /// Gets or sets the chain identification strategy.
        /// </summary>
        /// <value>The chain identification strategy.</value>
        public IParserChainIdentificationStrategy ChainIdentificationStrategy { get; set; }
        /// <summary>
        /// Gets or sets the consumer selection strategy.
        /// </summary>
        /// <value>The consumer selection strategy.</value>
        public IConsumerSelectionStrategy ConsumerSelectionStrategy { get; set; }
        /// <summary>
        /// Gets or sets the consumption request factory.
        /// </summary>
        /// <value>The consumption request factory.</value>
        public IConsumptionRequestFactory ConsumptionRequestFactory { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IContext Context
        {
            get => _context;
            set => SetContext(value);
        }

        /// <summary>
        /// Gets or sets the iteration information factory.
        /// </summary>
        /// <value>The iteration information factory.</value>
        public IIterationInfoFactory IterationInfoFactory { get; set; }
        /// <summary>
        /// Gets or sets the parse result factory.
        /// </summary>
        /// <value>The parse result factory.</value>
        public IParseResultFactory ParseResultFactory { get; set; }
        /// <summary>
        /// Gets or sets the potential consumer strategy.
        /// </summary>
        /// <value>The potential consumer strategy.</value>
        public IPotentialConsumerStrategy PotentialConsumerStrategy { get; set; }
        /// <summary>
        /// Gets or sets the root parser identifier.
        /// </summary>
        /// <value>The root parser identifier.</value>
        public string RootParserId { get; protected internal set; }
    }
}