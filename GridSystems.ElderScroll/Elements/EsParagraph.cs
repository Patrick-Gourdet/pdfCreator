using System.Collections.Generic;
using iText.Layout.Element;
using GridSystems.ElderScroll.Common;

namespace GridSystems.ElderScroll.Elements
{
    /// <summary>
    /// This class creates a Pragraph elment and add Text to said Pragraph and returns 
    /// an IElement Object.
    /// </summary>
    /// <para>
    /// This class Creates the Paragraph of EsElement consisting of text which
    /// depends on the alignment and margins to create the desired result.
    /// Iterating through each text element we apply the given attributes and then 
    /// add this to the desiered pragraph.
    /// </para>
    public class EsParagraph : EsBlockElement, IEsElement
    {
        public EsParagraph()
        {
            this.Texts = new List<EsText>();
        }
        /// <summary>
        /// The EsText class creates a IList of Text Objects
        /// </summary>
        /// <para>
        /// The EsText element has extended attributes such as bolding or font
        /// type and size.
        /// </para>
        /// <returns>
        /// EsParagraph.Text
        /// </returns>
        public IList<EsText> Texts { get; set; }
        /// <summary>
        ///  EsMargin allows for the setting of the margins for a given  element
        /// this cn be extended to any element which supports the margin attribute.
        /// </summary>
        public EsMargins Margins { get; set; }

        public IElement RenderElement(EsContext esContext)
        {
            Paragraph p = new Paragraph();
            SetBaseAttributes(p, esContext);
            foreach(EsText t in Texts)
            {
                p.Add(t.RenderText(esContext));
            }
            esContext.ApplyMargins(p, Margins);
            return p;
        }
    }
}
