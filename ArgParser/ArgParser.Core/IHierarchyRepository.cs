﻿using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IHierarchyRepository
    {
        void EstablishParentChildRelationship(string parentParserId, string childParserId);
        IEnumerable<string> GetAncestors(string parserId);
        bool IsParent(string parentParserId, string childParserId);
        IEnumerable<string> GetChildren(string parserId);
    }
}