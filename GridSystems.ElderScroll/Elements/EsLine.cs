using System;
using iText.Layout.Element;
using GridSystems.ElderScroll.Common;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Properties;

namespace GridSystems.ElderScroll.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public class EsLine : EsBlockElement, IEsElement
    {
        //LineSeperator Specific variables
        private const string DOTTED = "dotted";
        private const string DASHED = "dashed";
        private const string SOLID = "solid";
        public float? LineThinkness { get; set; }
        public string LineType { get; set; }

        public IElement RenderElement(EsContext esContext)
        {
            LineSeparator l = CreateLine(esContext);
            SetBaseAttributes(l, esContext);
            return l;
        }
        protected LineSeparator CreateLine(EsContext esContext)
        {
            LineSeparator l;
            float width = this.LineThinkness.GetValueOrDefault(1);
            string lineType = (this.LineType ?? string.Empty).ToLower();
            switch (lineType)
            {
                case DASHED:
                    DashedLine dashed = new DashedLine(width);
                    dashed.SetColor(esContext.GetColor(this.Color));
                    l = new LineSeparator((ILineDrawer)dashed);
                    break;
                case DOTTED:
                    DottedLine dotted = new DottedLine(width);
                    dotted.SetColor(esContext.GetColor(this.Color));
                    l = new LineSeparator((ILineDrawer)dotted);
                    break;
                case SOLID:
                    SolidLine solid = new SolidLine(width);
                    solid.SetColor(esContext.GetColor(this.Color));
                    l = new LineSeparator(solid);
                    break;
                default:
                    l = new LineSeparator(new SolidLine(width)); //Needs Logging function
                    break;
            }
            return l;
        }
    }
}
