using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GridSystems.ElderScroll
{
    public static class ResourcesHelper
    {
        public static byte[] GetBin(string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + '.' + resourceName))
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static string GetText(string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + '.' + resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static string[] GetLines(string resourceName)
        {
            List<string> lines = new List<string>();
            string line;
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + '.' + resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        if (!String.IsNullOrWhiteSpace(line))
                            lines.Add(line);
                    }
                }
            }
            return lines.ToArray();
        }
    }
}
