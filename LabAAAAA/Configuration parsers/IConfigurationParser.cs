using System;
using System.Collections.Generic;

namespace LabAAAAA
{
    public interface IConfigurationParser<T> where T: new()
    {
        void createConfigurationFile(string fileName, string PathToFile, T option);
        List<T> Parse();
    }
}
