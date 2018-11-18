using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public static class ConsumptionRequestExtensions
    {
        public static IEnumerable<string> AllToBeConsumed(this ConsumptionRequest request) =>
            request.Info.Args.Skip(request.Info.Index).Take(request.Max);

        public static string First(this ConsumptionRequest request)
        {
            var from = request.AllToBeConsumed().ToList();
            if (!from.Any())
                throw new ArgumentOutOfRangeException();
            return from.First();
        }

        public static bool HasNext(this ConsumptionRequest request) =>
            request.Info.Index + 1 < request.Info.Index + request.Max;

        public static string Last(this ConsumptionRequest request)
        {
            var from = request.AllToBeConsumed().ToList();
            if (!from.Any())
                throw new ArgumentOutOfRangeException();
            return from.Last();
        }

        public static string Next(this ConsumptionRequest request)
        {
            if (request.Max < 2)
                throw new ArgumentOutOfRangeException(nameof(request.Max),
                    "Cannot get next arg because it is outside the range of consumable values");
            return request.Info.Args[request.Info.Index + 1];
        }

        public static IEnumerable<string> Rest(this ConsumptionRequest request) =>
            request.Info.Args.Skip(request.Info.Index + 1).Take(request.Max - 1);
    }
}