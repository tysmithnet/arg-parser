using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Styles.Default
{
    public interface IRequirable
    {
        bool IsRequired { get; }
    }
}
