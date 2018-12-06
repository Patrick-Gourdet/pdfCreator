using System.Linq;
using iText.Layout.Element;
using GridSystems.ElderScroll.Common;

namespace GridSystems.ElderScroll.Elements
{
    public class EsText : EsBlockElement, IEsElement 
    {
        public string Content { get; set; }

        internal Text RenderText(EsContext esContext)
        {          
            Text t = new Text(esContext.ReplaceTags(Content));
            SetBaseAttributes(t, esContext);
            return t;
        }

        public IElement RenderElement(EsContext esContext)
        {
            Paragraph p = new Paragraph(RenderText(esContext));
            if (this.TextAlign.HasValue)
                p.SetTextAlignment(esContext.GetTextAlign(this.TextAlign.Value));
            return p;
        }
    }
}
