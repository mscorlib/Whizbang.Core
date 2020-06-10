using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Whizbang.Core.Hosting
{
    public class AssemblyHelper
    {
        public static IEnumerable<Assembly> GetAllAssemblies()
        {
            return
                Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                    .Select(Assembly.LoadFrom);
        }
    }
}