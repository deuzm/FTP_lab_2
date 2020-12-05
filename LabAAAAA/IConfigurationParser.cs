using System;
using System.Collections.Generic;

namespace LabAAAAA
{
    public interface IConfigurationParser
    {
        void createConfigurationFile();
        List<Options> Parse();
    }
}
