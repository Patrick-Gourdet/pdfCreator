using System.Linq;
using System.Collections.Generic;
using iText.Layout.Element;
using iText.Layout.Properties;
using GridSystems.ElderScroll.Common;

namespace GridSystems.ElderScroll.Elements
{
    public class EsTable : EsBlockElement, IEsElement
    {
        public EsTable()
        {
            this.BodyCells = new List<EsTableCell>();
        }
        //public EsBorder esBorder { get; set; }
        private IList<float> columnsWidthPoints = null;
        public IList<float> ColumnsWidthPoints
        {
            get
            {
                if (this.columnsWidthPoints == null)
                    this.columnsWidthPoints = new List<float>();
                return this.columnsWidthPoints;
            }
        }

        private IList<float> columnsWidthPercs = null;
        public IList<float> ColumnsWidthPercs
        {
            get
            {
                if (this.columnsWidthPercs == null)
                    this.columnsWidthPercs = new List<float>();
                return this.columnsWidthPercs;
            }
        }

        public int? ColumnsCount { get; set; }

        public IList<EsTableCell> BodyCells { get; private set; }

        private IList<EsTableCell> headerCells = null;
        public IList<EsTableCell> HeaderCells
        {
            get
            {
                if (this.headerCells == null)
                    this.headerCells = new List<EsTableCell>();
                return this.headerCells;
            }
        }

        private IList<EsTableCell> footerCells = null;
        public IList<EsTableCell> FooterCells
        {
            get
            {
                if (this.footerCells == null)
                    this.footerCells = new List<EsTableCell>();
                return this.footerCells;
            }
        }

        public IElement RenderElement(EsContext esContext)
        {
            Table table = null;
            if (this.ColumnsWidthPoints != null && this.ColumnsWidthPoints.Count > 0)
                table = new Table(UnitValue.CreatePointArray(this.ColumnsWidthPoints.ToArray()));
            else if (this.ColumnsWidthPercs != null && this.ColumnsWidthPercs.Count > 0)
                table = new Table(UnitValue.CreatePercentArray(this.ColumnsWidthPercs.ToArray()));
            else if (this.ColumnsCount.HasValue)
                table = new Table(this.ColumnsCount.Value);
            else
                throw new EsConflictingParameterException("None of the parameters have a value", "ColumnsWidthPoints", "ColumnsWidthPercs", "ColumnsCount");
            SetBaseAttributes(table, esContext);
            if (this.headerCells != null && this.headerCells.Count > 0)
            {
                foreach (EsTableCell hc in this.headerCells)
                    table.AddHeaderCell(hc.RenderCell(esContext));
            }
            foreach (EsTableCell cl in this.BodyCells)
            {
                table.AddCell(cl.RenderCell(esContext));
            }
            if (this.footerCells != null && this.footerCells.Count > 0)
            {
                foreach (EsTableCell fc in this.footerCells)
                    table.AddFooterCell(fc.RenderCell(esContext));
            }
            return table;
        }
    }
}