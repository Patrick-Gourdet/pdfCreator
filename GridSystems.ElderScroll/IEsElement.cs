using iText.Layout.Element;
using GridSystems.ElderScroll.Common;

namespace GridSystems.ElderScroll
{
    public interface IEsElement 
    {
        IElement RenderElement(EsContext esContext);
    }
}
