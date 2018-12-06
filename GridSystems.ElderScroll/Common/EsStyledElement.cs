using System;
using System.Collections.Generic;
using GridSystems.ElderScroll.Common;
using iText.Layout.Borders;
using iText.Layout;

namespace GridSystems.ElderScroll.Elements
{
    public abstract class EsStyledElement 
    {
        public string FontName { get; set; }
        public int? FontSize { get; set; }
        public bool? Bold { get; set; }
        public bool? Italic { get; set; }
        public bool? UnderLine { get; set; }
        public float? CharSpacing { get; set; }
        public float? WordSpacing { get; set; }
        public float? Opacity { get; set; }
        public string Color { get; set; }
        public string StrokeColor { get; set; }
        public string BackGroundColor { get; set; }
        public EsTextAlign? TextAlign { get; set; }
        public EsHorizontalAlign? HorizontalAlign { get; set; }
        public EsVerticalAlign? VerticalAlign { get; set; }
        public List<EsBorder> Borders { get; set; }
        
        
        protected void SetBaseAttributes<T>(ElementPropertyContainer<T> styledElement, EsContext esContext) where T : IPropertyContainer
        {
            if (this.TextAlign.HasValue)
                styledElement.SetTextAlignment(esContext.GetTextAlign(this.TextAlign.Value));
            if (this.HorizontalAlign.HasValue)
                styledElement.SetHorizontalAlignment(esContext.GetHorizontalAlign(this.HorizontalAlign.Value));
            if (!string.IsNullOrWhiteSpace(this.FontName))
                styledElement.SetFont(esContext.GetFont(this.FontName));
            if (this.FontSize.HasValue)
                styledElement.SetFontSize(this.FontSize.Value);
            if (this.Bold.GetValueOrDefault(false))
                styledElement.SetBold();
            if (this.Italic.GetValueOrDefault(false))
                styledElement.SetItalic();
            if (this.UnderLine.GetValueOrDefault(false))
                styledElement.SetUnderline();
            if (!string.IsNullOrWhiteSpace(this.Color))
                styledElement.SetFontColor(esContext.GetColor(this.Color));
            if (!string.IsNullOrWhiteSpace(this.StrokeColor))
                styledElement.SetStrokeColor(esContext.GetColor(this.StrokeColor));
            if (!string.IsNullOrWhiteSpace(this.BackGroundColor))
                styledElement.SetBackgroundColor(esContext.GetColor(this.BackGroundColor));
            if (this.Borders != null && this.Borders.Count > 0)
            {
                styledElement.SetBorder(Border.NO_BORDER);
                string borderSides;
                foreach (EsBorder esBorder in this.Borders)
                {
                    borderSides = esBorder.Sides ?? EsContext.NONE;
                    foreach (string borderItem in borderSides.ToLower().Split(','))
                    {
                        switch (borderItem.Trim())
                        {
                            case EsContext.NONE:
                                styledElement.SetBorder(Border.NO_BORDER);
                                break;
                            case EsContext.ALL:
                                styledElement.SetBorder(esContext.GetBorder(esBorder.BorderStyle, esBorder.Width, esBorder.Color, esBorder.Opacity));
                                break;
                            case EsContext.TOP:
                                styledElement.SetBorderTop(esContext.GetBorder(esBorder.BorderStyle, esBorder.Width, esBorder.Color, esBorder.Opacity));
                                break;
                            case EsContext.BOTTOM:
                                styledElement.SetBorderBottom(esContext.GetBorder(esBorder.BorderStyle, esBorder.Width, esBorder.Color, esBorder.Opacity));
                                break;
                            case EsContext.LEFT:
                                styledElement.SetBorderLeft(esContext.GetBorder(esBorder.BorderStyle, esBorder.Width, esBorder.Color, esBorder.Opacity));
                                break;
                            case EsContext.RIGHT:
                                styledElement.SetBorderRight(esContext.GetBorder(esBorder.BorderStyle, esBorder.Width, esBorder.Color, esBorder.Opacity));
                                break;
                            default:
                                throw new EsUnrecognizedParameterException("BorderSides", borderItem);
                        }
                    }
                }
            }
            if(this.Opacity.HasValue)
                styledElement.SetOpacity(esContext.GetOpacity(this.Opacity));
            if (this.WordSpacing.HasValue)
                styledElement.SetWordSpacing((float)this.WordSpacing);
            if (this.CharSpacing.HasValue)
                styledElement.SetCharacterSpacing((float)this.CharSpacing);            
        } 
    }
}
