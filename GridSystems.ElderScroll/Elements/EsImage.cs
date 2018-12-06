using System;
using iText.IO.Image;
using iText.Layout.Element;
using GridSystems.ElderScroll.Common;

namespace GridSystems.ElderScroll.Elements
{
    public class EsImage : EsStyledElement , IEsElement
    {
        public string Base64 { get; set; }
        public float? Width { get; set; }

        public IElement RenderElement(EsContext esContext)
        {
            Image imgLogo = new Image(ImageDataFactory.Create(Convert.FromBase64String(this.Base64)));
            if (this.Width.HasValue)
            {
                imgLogo.SetWidth((float)this.Width);
            }
            
            SetBaseAttributes(imgLogo, esContext);
            return imgLogo;
        }
    }
}
