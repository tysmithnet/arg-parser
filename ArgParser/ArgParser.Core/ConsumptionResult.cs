﻿using System.Collections.Generic;
using ArgParser.Core.Extensions;

namespace ArgParser.Core
{
    public class ConsumptionResult
    {
        public ConsumptionResult(params ParseException[] parseExceptions)
        {
            foreach (var parseException in parseExceptions.PreventNull()) ParseExceptions.Add(parseException);
        }

        public ConsumptionResult(IterationInfo originalInfo, int numConsumed, Parameter consumingParameter)
        {
            NumConsumed = numConsumed;
            Info = originalInfo.Consume(numConsumed);

            // can be null, means there was no consumption
            ConsumingParameter = consumingParameter;
        }

        public Parameter ConsumingParameter { get; set; }
        public IterationInfo Info { get; set; }
        public int NumConsumed { get; protected internal set; }
        public IList<ParseException> ParseExceptions { get; set; } = new List<ParseException>();
    }
}