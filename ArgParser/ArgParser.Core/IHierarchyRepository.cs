﻿using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IHierarchyRepository
    {
        bool IsParent(string parentParserId, string childParserId);
        void EstablishParentChildRelationship(string parentParserId, string childParserId);
        IEnumerable<Parser> GetAncestors(string parserId);
        Parser Get(string parserId);
    }
}