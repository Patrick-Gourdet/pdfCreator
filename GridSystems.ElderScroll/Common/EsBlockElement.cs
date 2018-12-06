using GridSystems.ElderScroll.Elements;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace GridSystems.ElderScroll.Common
{
    public abstract class EsBlockElement : EsStyledElement
    {
        public float? HeightPerc { get; set; }
        public float? HeightPoint { get; set; }
        public bool? KeepTogether { get; set; }
        /// <summary>
        /// Returns whether the end of this
        /// <see cref="T:iText.Layout.Element.BlockElement`1" />
        /// and the start of the next sibling of this element
        /// should be placed in the same area.
        /// </summary>
        /// <returns>
        /// the current value of the
        /// <see cref="F:iText.Layout.Properties.Property.KEEP_WITH_NEXT" />
        /// property
        public bool? KeepTogetherNext { get; set; }
        public float? MaxHeight { get; set; }
        public float? MinHeight { get; set; }
        public float? Padding { get; set; }
        public float? PaddingLeft { get; set; }
        public float? PaddingRight { get; set; }
        public float? PaddingBottom { get; set; }
        public float? PaddingTop { get; set; }
        public float? WidthPerc { get; set; }
        public float? WidthPoint { get; set; }
        /// <summary>
        /// Sets a ratio which determines in which proportion will word spacing and character spacing
        /// be applied when horizontal alignment is justified.
        /// </summary>
        /// <param name="ratio">
        /// the ratio coefficient. It must be between 0 and 1, inclusive.
        /// It means that <strong>ratio</strong> part of the free space will
        /// be compensated by word spacing, and <strong>1-ratio</strong> part of the free space will
        /// be compensated by character spacing.
        /// If <strong>ratio</strong> is 1, additional character spacing will not be applied.
        /// If <strong>ratio</strong> is 0, additional word spacing will not be applied.
        public float? SpacingRatio { get; set; }
        public VerticalAlignment? EsVerticalAlignment { get; set; }

        protected void SetBaseAttributes<T>(BlockElement<T> blockElement, EsContext esContext) where T: IBlockElement
        {
            if (this.HeightPerc.HasValue && this.HeightPoint.HasValue)
            {
                throw new EsConflictingParameterException("Both Percent and Poitnt values given ", this.HeightPerc.Value.ToString(), this.HeightPoint.Value.ToString());
            }
            else if (this.HeightPerc.HasValue)
            {
                blockElement.SetHeight(UnitValue.CreatePercentValue(this.HeightPerc.Value));
            }
            else if (this.HeightPoint.HasValue)
            {
                blockElement.SetHeight(UnitValue.CreatePointValue(this.HeightPoint.Value));
            }

            if (this.WidthPerc.HasValue && this.WidthPoint.HasValue)
            {
                throw new EsConflictingParameterException("Both Percent and Poitnt values given ", this.WidthPerc.Value.ToString(), this.WidthPoint.Value.ToString());
            }
            else if (this.WidthPerc.HasValue)
            {
                blockElement.SetWidth(UnitValue.CreatePercentValue(this.WidthPerc.Value));
            }
            else if (this.WidthPoint.HasValue)
            {
                blockElement.SetWidth(UnitValue.CreatePointValue(this.WidthPoint.Value));
            }

            if (this.Padding.HasValue)
                blockElement.SetPadding(this.Padding.Value);
            if (this.PaddingBottom.HasValue)
                blockElement.SetPaddingBottom(this.PaddingBottom.Value);
            if (this.PaddingTop.HasValue)
                blockElement.SetPaddingTop(this.PaddingTop.Value);
            if (this.PaddingLeft.HasValue)
                blockElement.SetPaddingLeft(this.PaddingLeft.Value);
            if (this.PaddingRight.HasValue)
                blockElement.SetPaddingRight(this.PaddingRight.Value);
            if (this.KeepTogether.HasValue)
                blockElement.SetKeepTogether(this.KeepTogether.Value);
            if (this.KeepTogetherNext.HasValue)
                blockElement.SetKeepWithNext(this.KeepTogetherNext.Value);
            if (this.SpacingRatio.HasValue)
            {
                if (this.SpacingRatio > 1 || this.SpacingRatio < 0)
                    throw new EsConflictingParameterException(string.Concat(this.SpacingRatio.Value.ToString()," Is out of the expected range"), "SpacingRatio");
                else
                    blockElement.SetSpacingRatio(this.SpacingRatio.Value);
            }
            if (this.EsVerticalAlignment.HasValue)
            {
                switch (this.EsVerticalAlignment)
                {
                    case VerticalAlignment.TOP:
                        blockElement.SetVerticalAlignment(this.EsVerticalAlignment);
                        break;
                    case VerticalAlignment.MIDDLE:
                        blockElement.SetVerticalAlignment(this.EsVerticalAlignment);
                        break;
                    case VerticalAlignment.BOTTOM:
                        blockElement.SetVerticalAlignment(this.EsVerticalAlignment);
                        break;
                    default:
                        blockElement.SetVerticalAlignment(VerticalAlignment.MIDDLE); //Logger
                        break;

                }
            }
            base.SetBaseAttributes(blockElement, esContext);
        }
    }
}
