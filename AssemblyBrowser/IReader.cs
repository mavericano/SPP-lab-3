using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    internal interface IReader
    {
        List<AssemblyData> GetAssemblyData(string path);
    }
}
