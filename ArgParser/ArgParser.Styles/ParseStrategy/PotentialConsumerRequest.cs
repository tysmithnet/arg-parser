﻿using System;
using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class PotentialConsumerRequest
    {
        public ChainIdentificationResult ChainIdentificationResult { get; set; }
        public IterationInfo Info { get; set; }
        public object Instance { get; set; }

        public PotentialConsumerRequest(ChainIdentificationResult chainIdentificationResult, IterationInfo info,
            object instance)
        {
            ChainIdentificationResult = chainIdentificationResult.ThrowIfArgumentNull(nameof(chainIdentificationResult));
            Info = info.ThrowIfArgumentNull(nameof(info));
            Instance = instance.ThrowIfArgumentNull(nameof(instance));
        }
    }
}