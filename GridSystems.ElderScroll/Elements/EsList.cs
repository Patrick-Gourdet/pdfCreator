using System.Collections.Generic;
using GridSystems.ElderScroll.Common;
using iText.Layout.Element;

namespace GridSystems.ElderScroll.Elements
{
    class EsList : EsStyledElement, IEsElement
    {
        public EsList()
        {
            this.ListItems = new List<EsListItem>();
        }

        public IList<EsListItem> ListItems { get; private set; }

        public IElement RenderElement(EsContext esContext)
        {           
            List list = new List();
            SetBaseAttributes(list, esContext);
            foreach(EsListItem listItem in this.ListItems)
            {
                list.Add(listItem.RenderListItem(esContext));
            }
            return list;
        }
    }
}
