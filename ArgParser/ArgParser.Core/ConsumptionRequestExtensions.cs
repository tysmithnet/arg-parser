using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public static class ConsumptionRequestExtensions
    {
        public static IEnumerable<string> FromNowOwn(this ConsumptionRequest request)
        {
            return request.Info.Args.Skip(request.Info.Index).Take(request.Max);
        }

        public static IEnumerable<string> Rest(this ConsumptionRequest request)
        {
            return request.Info.Args.Skip(request.Info.Index + 1).Take(request.Max - 1);
        }

        public static string First(this ConsumptionRequest request)
        {
            return request.FromNowOwn().First();
        }

        public static string Last(this ConsumptionRequest request)
        {
            return request.FromNowOwn().Last();
        }

        public static string Next(this ConsumptionRequest request)
        {
            return request.Info.Args[request.Info.Index + 1];
        }

        public static bool HasNext(this ConsumptionRequest request)
        {
            return request.Info.Index + 1 < request.Info.Index + request.Max;
        }

        public static string Current(this ConsumptionRequest request)
        {
            return request.Info.Args[request.Info.Index];
        }
    }
}