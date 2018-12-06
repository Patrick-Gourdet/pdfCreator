using iText.Layout.Element;
using System;
using System.Collections.Generic;
using GridSystems.ElderScroll.Common;

namespace GridSystems.ElderScroll.Elements
{
    public class EsTableCell : EsBlockElement
    {
        public IElement element;
        public EsTableCell()
        {
            this.Elements = new List<IEsElement>();
        }
        public IList<IEsElement> Elements { get; private set; }
        public int? RowSpan { get; set; }
        public int? ColSpan { get; set; }

        public Cell RenderCell(EsContext esContext)
        {
            Cell cell;
            if (this.RowSpan.HasValue || this.ColSpan.HasValue)
                cell = new Cell(this.RowSpan.GetValueOrDefault(1), this.ColSpan.GetValueOrDefault(1));
            else
                cell = new Cell();
            SetBaseAttributes(cell, esContext);
            foreach (IEsElement esElement in this.Elements)
            {
                try
                {
                    element = esElement.RenderElement(esContext);
                }
                catch (Exception ex)
                {
                    throw new EsRenderingException(esElement, ex);
                }
                if (element is IBlockElement)
                {
                    cell.Add((IBlockElement)element);
                }

                else if (element is Image)
                {
                    cell.Add((Image)element);
                }
                else
                {
                    throw new EsRenderingException(string.Concat("Unrecognized iText7 Element Type '", element?.GetType().FullName ?? "", "'"), esElement);
                }
            }
            return cell;
        }

    }
}
