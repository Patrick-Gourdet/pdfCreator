using iText.Kernel.Pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace GridSystems.ElderScroll
{
    public static class EsGenerator
    {
        private static JsonSerializerSettings settings;

        static EsGenerator()
        {
            settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new EsSerializationBinder(),
                NullValueHandling = NullValueHandling.Ignore
            };
            settings.Converters.Add(new StringEnumConverter());
        }

        public static string SerializeDocument(EsDocument document)
        {
            return JsonConvert.SerializeObject(document, settings);
        }

        public static EsDocument DeserializeDocument(string jsonDocument)
        {
            return JsonConvert.DeserializeObject<EsDocument>(jsonDocument, settings);
        }

        public static byte[] Render(string jsonDocument)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Render(jsonDocument, ms);
                ms.Flush();
                return ms.GetBuffer();
            }
        }

        public static void Render(string jsonDocument, Stream stream)
        {
            Render(DeserializeDocument(jsonDocument), stream);
        }

        public static void Render(string jsonDocument, string fileName)
        {
            Render(DeserializeDocument(jsonDocument), fileName);
        }

        public static void Render(EsDocument document, Stream stream)
        {
            using (PdfWriter pdfWriter = new PdfWriter(stream))
            {
                Render(document, pdfWriter);
            }
        }

        public static void Render(EsDocument document, string filePath)
        {
            EsSection section = new EsSection();
            document.Sections.Add(section);
            using (PdfWriter pdfWriter = new PdfWriter(filePath))
            {
                Render(document, pdfWriter);
            }
        }

        private static void Render(EsDocument document, PdfWriter pdfWriter)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            using (iText.Kernel.Pdf.PdfDocument pdfDoc = new iText.Kernel.Pdf.PdfDocument(pdfWriter))
            {
                document.Render(pdfDoc);
            }
        }

        internal class EsSerializationBinder : DefaultSerializationBinder
        {
            private const string NAMESPACE_PREFIX = "GridSystems.ElderScroll.Elements.";

            public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
            {
                if (serializedType.Assembly.GetName().Name == "GridSystems.ElderScroll" && serializedType.FullName.StartsWith(NAMESPACE_PREFIX))
                {
                    assemblyName = null;
                    typeName = serializedType.Name;
                }
                else
                {
                    base.BindToName(serializedType, out assemblyName, out typeName);
                }
            }

            public override Type BindToType(string assemblyName, string typeName)
            {
                if (string.IsNullOrWhiteSpace(assemblyName))
                {
                    Type t = Type.GetType(NAMESPACE_PREFIX + typeName, false, true);
                    if (t != null)
                        return t;
                }
                return base.BindToType(assemblyName, typeName);
            }
        }
    }
}
