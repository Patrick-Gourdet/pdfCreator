using System;
using System.Collections.Generic;
using System.Reflection;
using GridSystems.ElderScroll.Common;
using GridSystems.ElderScroll.Elements;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Events;
using iText.Layout.Renderer;
using iText.Layout.Layout;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using ITPageSize = iText.Kernel.Geom.PageSize;
using iText.Kernel.Geom;
using System.IO;

namespace GridSystems.ElderScroll
{
    public class EsDocument : EsStyledElement 
    {
        public EsDocument()
        {
            this.Sections = new List<EsSection>();
        }
        public bool EmbedFonts { get; set; }
        public string PageSize { get; set; }
        public bool? RotatePage { get; set; }
        public EsSection Header { get; set; }
        public EsSection Footer { get; set; }
        public EsMargins PageMargins { get; set; }
        public IList<EsSection> Sections { get; private set; }

        internal Document Render(PdfDocument pdfDoc)
        {
            // Page Size
            ITPageSize iTextPageSize = GetPageSize(this.PageSize);
            if (this.RotatePage.GetValueOrDefault(false))
                iTextPageSize.Rotate();
            // Create Document
            Document doc = new Document(pdfDoc, iTextPageSize);
            if (this.PageMargins != null)
            {
                if (this.PageMargins.Top.HasValue)
                    doc.SetTopMargin(this.PageMargins.Top.Value);
                if (this.PageMargins.Bottom.HasValue)
                    doc.SetBottomMargin(this.PageMargins.Bottom.Value);
                if (this.PageMargins.Left.HasValue)
                    doc.SetLeftMargin(this.PageMargins.Left.Value);
                if (this.PageMargins.Right.HasValue)
                    doc.SetRightMargin(this.PageMargins.Right.Value);
            }
            // Create Context
            EsContext esContext = new EsContext(this.EmbedFonts);
            
            // Prepare Header and/or Footer Handlers
            if (this.Header != null || this.Footer != null)
            {
                PageHandler pageHandler = new PageHandler(iTextPageSize, esContext, this.Header, this.Footer, doc);
                pdfDoc.AddEventHandler(PdfDocumentEvent.END_PAGE, pageHandler);
                doc.SetTopMargin(doc.GetTopMargin() + pageHandler.HeaderHeight);
                doc.SetBottomMargin(doc.GetBottomMargin() + pageHandler.FooterHeight);
            }
            // Style
            base.SetBaseAttributes(doc, esContext);
            
            // Complete Elements
            IElement element;
            foreach (EsSection section in this.Sections)
            {
                foreach (IEsElement esElement in section.Elements)
                {
                    // Render Element
                    try
                    {
                        element = esElement.RenderElement(esContext);
                    }
                    catch (Exception ex)
                    {
                        throw new EsRenderingException(esElement, ex);
                    }
                    // Add to Document
                    if (element is Image)
                    {
                        doc.Add((Image)element);
                    }
                    else if (element is AreaBreak)
                    {
                        doc.Add((AreaBreak)element);
                    }
                    else if (element is IBlockElement)
                    {
                        doc.Add((IBlockElement)element);
                    }
                    else
                    {
                        throw new EsRenderingException(string.Concat("Unrecognized iText7 Element Type '", element?.GetType().FullName ?? "", "'"), esElement);
                    }
                }
            }
            // Result
            return doc;
        }

        private ITPageSize GetPageSize(string pageSize)
        {
            if (string.IsNullOrWhiteSpace(pageSize))
            {
                return ITPageSize.LETTER;
            }
            else
            {
                if (pageSize.Contains(","))
                {
                    string[] parts = pageSize.Split(',');
                    float width, height;
                    if (parts.Length == 2
                        && float.TryParse(parts[0], out width)
                        && float.TryParse(parts[1], out height))
                    {
                        return new ITPageSize(width, height);
                    }
                }
                else
                {
                    string pageName = pageSize.Trim().ToUpper();
                    Type pageType = typeof(ITPageSize);
                    FieldInfo fieldInfo = pageType.GetField(pageName);
                    if (fieldInfo != null)
                    {
                        return (ITPageSize)fieldInfo.GetValue(null);
                    }
                }
            }
            throw new EsUnrecognizedParameterException("PageSize", pageSize);
        }

        internal class PageHandler : IEventHandler
        {
            private Document doc;
            private EsContext esContext;
            private EsSection headerSection = null;
            private EsSection footerSection = null;

            public PageHandler(ITPageSize pageSize, EsContext esContext, EsSection headerSection, EsSection footerSection, Document doc)
            {
                this.doc = doc;
                this.esContext = esContext;
                if (headerSection != null && headerSection.Elements != null && headerSection.Elements.Count > 0)
                    this.headerSection = headerSection;
                if (footerSection != null && footerSection.Elements != null && footerSection.Elements.Count > 0)
                    this.footerSection = footerSection;
                this.HeaderHeight = CalculateHeight(headerSection, pageSize);
                this.FooterHeight = CalculateHeight(footerSection, pageSize);
            }

            public void HandleEvent(Event hEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)hEvent;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                int pageNumber = pdfDoc.GetPageNumber(page);
                int totalNumberOfPages = pdfDoc.GetNumberOfPages();
                esContext.SetTag("PageNumber", pageNumber.ToString());
                esContext.SetTag("TotalNumberOfPages", totalNumberOfPages.ToString());
                if (this.headerSection != null)
                    AddElement(pdfDoc, page, true);
                if (this.footerSection != null)
                    AddElement(pdfDoc, page, false);
            }

            private void AddElement(PdfDocument pdfDoc, PdfPage page, bool header)
            {
                EsSection section;
                Rectangle rect;
                if (header)
                {
                    section = this.headerSection;
                    rect = new Rectangle(pdfDoc.GetDefaultPageSize().GetX() + doc.GetLeftMargin(), pdfDoc.GetDefaultPageSize().GetTop() - doc.GetTopMargin(), pdfDoc.GetDefaultPageSize().GetWidth() - (doc.GetLeftMargin() + doc.GetRightMargin()), this.HeaderHeight);
                }
                else
                {
                    section = this.footerSection;
                    rect = new Rectangle(pdfDoc.GetDefaultPageSize().GetX() + doc.GetLeftMargin(), pdfDoc.GetDefaultPageSize().GetBottom() + doc.GetBottomMargin() - this.FooterHeight, pdfDoc.GetDefaultPageSize().GetWidth() - (doc.GetLeftMargin() + doc.GetRightMargin()), this.FooterHeight);
                }
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
                Canvas elementCanvas = new Canvas(canvas, pdfDoc, rect);
                foreach (IEsElement esElement in section.Elements)
                {
                    IElement element = esElement.RenderElement(this.esContext);
                    // Add to Document
                    if (element is Image)
                    {
                        elementCanvas.Add((Image)element);
                    }
                    else if (element is IBlockElement)
                    {
                        elementCanvas.Add((IBlockElement)element);
                    }
                    else
                    {
                        throw new EsRenderingException(string.Concat("Unrecognized iText7 Element Type '", element?.GetType().FullName ?? "", "'"), esElement);
                    }
                }
            }

            public float HeaderHeight { get; private set; }
            public float FooterHeight { get; private set; }

            private float CalculateHeight(EsSection section, PageSize pageSize)
            {
                if (section == null)
                    return 0;
                Div divSection = new Div();
                foreach (IEsElement esElement in section.Elements)
                {
                    IElement element = esElement.RenderElement(this.esContext);
                    // Add to Document
                    if (element is Image)
                    {
                        divSection.Add((Image)element);
                    }
                    else if (element is IBlockElement)
                    {
                        divSection.Add((IBlockElement)element);
                    }
                    else
                    {
                        throw new EsRenderingException(string.Concat("Unrecognized iText7 Element Type '", element?.GetType().FullName ?? "", "'"), esElement);
                    }
                }
                IRenderer renderer = divSection.CreateRendererSubTree();
                renderer.SetParent(new Document(new PdfDocument(new PdfWriter(new MemoryStream()))).GetRenderer());
                return renderer.Layout(new LayoutContext(new LayoutArea(0, doc.GetPageEffectiveArea(pageSize)))).GetOccupiedArea().GetBBox().GetHeight();
            }
        }
    }
}
