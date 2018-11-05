﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public static class ConsumptionRequestExtensions
    {
        public static IEnumerable<string> FromNowOn(this ConsumptionRequest request)
        {
            return request.Info.Args.Skip(request.Info.Index).Take(request.Max);
        }

        public static IEnumerable<string> Rest(this ConsumptionRequest request)
        {
            return request.Info.Args.Skip(request.Info.Index + 1).Take(request.Max - 1);
        }

        public static string First(this ConsumptionRequest request)
        {
            var from = request.FromNowOn().ToList();
            if(!from.Any())
                throw new IndexOutOfRangeException();
            return from.First();
        }

        public static string Last(this ConsumptionRequest request)
        {
            var from = request.FromNowOn().ToList();
            if (!from.Any())
                throw new IndexOutOfRangeException();
            return from.Last();
        }

        public static string Next(this ConsumptionRequest request)
        {
            if(request.Max < 2)
                throw new IndexOutOfRangeException("Cannot get next arg because it is outside the range of consumable values");
            return request.Info.Args[request.Info.Index + 1];
        }

        public static bool HasNext(this ConsumptionRequest request)
        {
            return request.Info.Index + 1 < request.Info.Index + request.Max;
        }

    }
}