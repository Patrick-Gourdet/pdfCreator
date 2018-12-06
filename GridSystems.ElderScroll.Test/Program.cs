using System;
using System.IO;
using GridSystems.ElderScroll.Common;
using GridSystems.ElderScroll.Elements;

namespace GridSystems.ElderScroll.Test
{
    class Program 
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".pdf");
            //string jd = GetDocSerialized();
            //CreateDirect(filePath);
            CreateFromJson(filePath);
        }

        private static void CreateDirect(string filePath)
        {
            EsGenerator.Render(GetDocument(), filePath);
        }

        private static void CreateFromJson(string filePath)
        {
            EsGenerator.Render(GetFromJson(), filePath);
        }

        private static string GetDocSerialized()
        {
            EsDocument pd = GetDocument();
            return EsGenerator.SerializeDocument(pd);
        }

        private static EsDocument GetFromJson()
        {
            return EsGenerator.DeserializeDocument(ResourcesHelper.GetText("Resources.Model.json"));
        }

        private static EsDocument GetDocument()
        {
            EsDocument pd = new EsDocument();
            pd.PageSize = "A4";
            pd.PageMargins = new EsMargins() { Top = 20, Bottom = 20, Left = 20, Right = 20 };
            EsSection s = new EsSection();
            pd.Sections.Add(s);
            s.Elements.Add(
                new EsImage()
                {
                    Base64 = Convert.ToBase64String(ResourcesHelper.GetBin("Resources.Logo.png")),
                    HorizontalAlign = EsHorizontalAlign.Center,
                    Width = 75
                }
            );
            s.Elements.Add(
                new EsText()
                {
                    Content = "Overview",
                    FontName = "Verdana",
                    FontSize = 20,
                    TextAlign = EsTextAlign.Right
                }
            );
            s.Elements.Add(
                new EsText()
                {
                    Content = ResourcesHelper.GetText("Resources.Overview.txt"),
                    FontSize = 10,
                    TextAlign = EsTextAlign.Justified
                }
            );
            return pd;
        }

    }
}
