using System;
using GridSystems.ElderScroll.Common;
using iText.Layout.Element;

namespace GridSystems.ElderScroll.Elements
{
    public class EsAreaBreak : IEsElement
    {
        public string areaBreakType { get; set; }
        public IElement RenderElement(EsContext esContext)
        {
            return new AreaBreak(esContext.AreaBreakType(areaBreakType));
        }
    }
}
