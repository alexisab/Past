﻿using Past.Common.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Past.Common.Data
{
    public class DataManager
    {
        public static void InitializeDatas()
        {
            Stopwatch sw = Stopwatch.StartNew();
            IEnumerable<MethodInfo> methods = Assembly.GetExecutingAssembly().GetTypes().Where(c => c.IsClass && c.Namespace == "Past.Common.Data").SelectMany(t => t.GetMethods()).Where(m => m.Name.Equals("Initialize"));
            foreach (var method in methods)
            {
                method.Invoke(null, null);
                if (method.DeclaringType != null)
                    ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"{method.DeclaringType.Name} Successfully loaded in {sw.ElapsedMilliseconds} ms ...");
                sw.Restart();
            }
            sw.Stop();
        }
    }
}
