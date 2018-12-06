using iText.Layout.Element;
using System;
using System.Collections.Generic;

namespace GridSystems.ElderScroll
{
    public class EsSection
    {
        public EsSection()
        {
            this.Elements = new List<IEsElement>();
        }

        public IList<IEsElement> Elements { get; private set; }
        public string Base64PdfFile { get; set; }
    }
}
