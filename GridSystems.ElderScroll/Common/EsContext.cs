using iText.Kernel.Font;
using System;
using System.IO;
using System.Collections.Generic;
using iText.Layout.Properties;
using iText.Layout.Element;
using Microsoft.Win32;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using iText.Layout;
using System.Text.RegularExpressions;

namespace GridSystems.ElderScroll.Common
{
    public class EsContext
    {
        #region Static Load
        private static Dictionary<string, string> fontsFileNames;

        static EsContext()
        {
            LoadFontFileNames();
        }

        //Constants for border evaluation
        public const string ALL = "all";
        public const string NONE = "none";
        public const string TOP = "top";
        public const string BOTTOM = "bottom";
        public const string LEFT = "left";
        public const string RIGHT = "right";
        //Const declarations Colors 
        public const string BLACK = "black";
        public const string BLUE = "blue";
        public const string CYAN = "cyan";
        public const string DARK_GRAY = "dark_gray";
        public const string GRAY = "gray";
        public const string GREEN = "green";
        public const string LIGHT_GRAY = "light_gray";
        public const string MAGENTA = "magenta";
        public const string ORANGE = "orange";
        public const string PINK = "pink";
        public const string RED = "red";
        public const string WHITE = "white";
        public const string YELLOW = "yellow";
        //Area Break
        public const string NEXT_AREA = "nextArea";
        public const string NEXT_PAGE = "nextPage";
        public const string LAST_PAGE = "lastPage";
        public Dictionary<string, string> tags = new Dictionary<string, string>();

        private static void LoadFontFileNames()
        {
            fontsFileNames = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            RegistryKey fonts = null;
            string normalizedName;
            string fileName;
            try
            {
                fonts = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Fonts", false);
                if (fonts == null)
                {
                    fonts = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Fonts", false);
                    if (fonts == null)
                    {
                        throw new Exception("Can't find font registry database.");
                    }
                }
                foreach (string keyName in fonts.GetValueNames())
                {
                    fileName = fonts.GetValue(keyName).ToString();
                    foreach (string fontName in keyName.Replace("(TrueType)", "").Split('&'))
                    {
                        normalizedName = fontName.Trim();
                        if (!fontsFileNames.ContainsKey(normalizedName))
                            fontsFileNames.Add(normalizedName, fileName);
                    }
                }
            }
            finally
            {
                if (fonts != null)
                {
                    fonts.Dispose();
                }
            }
        }
        #endregion

        #region Instance
        internal EsContext(bool embedFonts)
        {
            this.embedFonts = embedFonts;
        }

        private bool embedFonts;
        private IDictionary<string, PdfFont> cache = new Dictionary<string, PdfFont>();

        public PdfFont GetFont(string fontName)
        {
            PdfFont pdfFont;
            if (cache.ContainsKey(fontName))
            {
                pdfFont = cache[fontName];
            }
            else
            {
                pdfFont = SolveFont(fontName);
                cache.Add(fontName, pdfFont);
            }
            return pdfFont;
        }

        private PdfFont SolveFont(string fontName)
        {
            if (!fontsFileNames.TryGetValue(fontName, out string fontFile))
                throw new EsUnrecognizedFontNameException(fontName);
            if (!Path.IsPathRooted(fontFile))
                fontFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), fontFile);
            return PdfFontFactory.CreateFont(fontFile, true);
        }

        public HorizontalAlignment GetHorizontalAlign(EsHorizontalAlign align)
        {
            switch (align)
            {
                case EsHorizontalAlign.Center:
                    return HorizontalAlignment.CENTER;
                case EsHorizontalAlign.Right:
                    return HorizontalAlignment.RIGHT;
                default:
                    return HorizontalAlignment.LEFT;
            }
        }

        public TextAlignment GetTextAlign(EsTextAlign align)
        {
            switch (align)
            {
                case EsTextAlign.Center:
                    return TextAlignment.CENTER;
                case EsTextAlign.Right:
                    return TextAlignment.RIGHT;
                case EsTextAlign.Justified:
                    return TextAlignment.JUSTIFIED;
                case EsTextAlign.JustifiedAll:
                    return TextAlignment.JUSTIFIED_ALL;
                default:
                    return TextAlignment.LEFT;
            }
        }
        public AreaBreakType AreaBreakType(string type)
        {
            switch (type)
            {
                case NEXT_AREA:
                    return iText.Layout.Properties.AreaBreakType.NEXT_AREA;
                case NEXT_PAGE:
                    return iText.Layout.Properties.AreaBreakType.NEXT_PAGE;
                case LAST_PAGE:
                    return iText.Layout.Properties.AreaBreakType.LAST_PAGE;
                default:
                    return iText.Layout.Properties.AreaBreakType.NEXT_AREA;               
            }
        }
        public Color GetColor(string color, string parameterName = "Color")
        {
            try
            {
                color = color ?? BLACK;
                System.Drawing.Color netColor = System.Drawing.ColorTranslator.FromHtml(color.ToLower().Trim());
                // NOTE: Alpha/Opacity is discarted
                return new DeviceRgb(netColor.R, netColor.G, netColor.B);
            }
            catch (Exception ex)
            {
                throw new EsUnrecognizedParameterException(parameterName, color, ex);
            }
        }

        public float GetOpacity(float? opacity)
        {
            float resOpacity = opacity.GetValueOrDefault(1);
            if (opacity < 0 || opacity > 1)
                throw new EsUnrecognizedParameterException("Opacity", opacity.ToString());
            return resOpacity;
        }
        #region Tags
        public void SetTag(string tagName, string value = "")
        {
            tags[tagName] = value;
        }

        private static readonly Regex tagMatchRegex = new Regex("\\{\\{(?<tagname>[A-Za-z0-9_-]+)\\}\\}");

        public string ReplaceTags(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            string result = text;
            // Do not replace any tags if the dictionary is empty.
            if (tags.Count > 0)
            {
                result = tagMatchRegex.Replace(text, match => TagEvaluator(match));
            }
            return result;
        }

        private string TagEvaluator(Match match)
        {
            string tagName = match.Groups["tagname"].Value;
            string replaceValue = null;
            if (!tags.TryGetValue(tagName, out replaceValue))
            {
                replaceValue = match.Value;
            }
            return replaceValue;
        }
        #endregion

        public Border GetBorder(EsBoadersStyle? borderStyle, float? width, string color, float? opacity)
        {
            float borderWidth = width.GetValueOrDefault(1);
            float borderOpacity = GetOpacity(opacity);
            switch (borderStyle.GetValueOrDefault(EsBoadersStyle.Solid))
            {
                case EsBoadersStyle.Dashed:
                    return new DashedBorder(GetColor(color), borderWidth, borderOpacity);
                case EsBoadersStyle.Dotted:
                    return new DottedBorder(GetColor(color), borderWidth, borderOpacity);
                case EsBoadersStyle.Double:
                    return new DoubleBorder(GetColor(color), borderWidth, borderOpacity);
                case EsBoadersStyle.RoundDots:
                    return new RoundDotsBorder(GetColor(color), borderWidth, borderOpacity);
                case EsBoadersStyle.Solid:
                    return new SolidBorder(GetColor(color), borderWidth, borderOpacity);
                case EsBoadersStyle.Groove:
                    return new GrooveBorder((DeviceRgb)GetColor(color), borderWidth, borderOpacity);
                case EsBoadersStyle.Inset:
                    return new InsetBorder((DeviceRgb)GetColor(color), borderWidth, borderOpacity);
                case EsBoadersStyle.Outset:
                    return new OutsetBorder((DeviceRgb)GetColor(color), borderWidth, borderOpacity);
                case EsBoadersStyle.Ridge:
                    return new RidgeBorder((DeviceRgb)GetColor(color), borderWidth, borderOpacity);
                default:
                    throw new NotImplementedException("Unrecognized BorderStyle");
            }
        }
        //FUNCTION SUGGESTION TO MAKE PROPERTIES GENERIC
        //public void JEsSetBoarder(EsBoadersStyle? EsBorderStyle, string EsColor, float Width,object t,string p, float Opacity = 1.0f)
        //{
        //    if(t is Text)
        //    {
        //       ((Text)t).SetBorder(new SolidBorder(GetColor(EsColor), Width, Opacity));
        //    }
        //    if (t is Cell)
        //    {
        //        ((Cell)t).SetBorder(new SolidBorder(GetColor(EsColor), Width, Opacity));
        //    }
        //}
        public void ApplyMargins<T>(BlockElement<T> blockElement, EsMargins margins) where T : IElement
        {
            if (margins != null)
            {
                if (margins.Top.HasValue)
                    blockElement.SetMarginTop(margins.Top.Value);
                if (margins.Bottom.HasValue)
                    blockElement.SetMarginBottom(margins.Bottom.Value);
                if (margins.Left.HasValue)
                    blockElement.SetMarginLeft(margins.Left.Value);
                if (margins.Right.HasValue)
                    blockElement.SetMarginRight(margins.Right.Value);
            }
        }
        #endregion
    }
}