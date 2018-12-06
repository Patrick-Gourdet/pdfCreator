using GridSystems.ElderScroll.Common;
using iText.Layout.Element;
using System;
using System.Collections.Generic;

namespace GridSystems.ElderScroll.Elements
{
    public class EsListItem : EsStyledElement
    {
        public EsListItem()
        {
            this.Elements = new List<IEsElement>();
        }

        public IList<IEsElement> Elements { get; private set; }

        public ListItem RenderListItem(EsContext esContext)
        {
            ListItem listItem = new ListItem();
            SetBaseAttributes(listItem, esContext);
            IElement element;
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
                    listItem.Add((IBlockElement)element);
                else if (element is Image)
                    listItem.Add((Image)element);
                else
                {
                    throw new EsRenderingException(string.Concat("Unrecognized iText7 Element Type '", element?.GetType().FullName ?? "", "'"), esElement);
                }
            }
            return listItem;
        }
    }
}
