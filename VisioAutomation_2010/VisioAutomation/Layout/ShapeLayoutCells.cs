using System.Linq;
using VisioAutomation.Extensions;
using IVisio = Microsoft.Office.Interop.Visio;
using VA = VisioAutomation;
using System.Collections.Generic;

namespace VisioAutomation.Layout
{
    public partial class ShapeLayoutCells : VA.ShapeSheet.CellGroups.CellGroup
    {
        public VA.ShapeSheet.CellData<int> ConFixedCode { get; set; }
        public VA.ShapeSheet.CellData<int> ConLineJumpCode { get; set; }
        public VA.ShapeSheet.CellData<int> ConLineJumpDirX { get; set; }
        public VA.ShapeSheet.CellData<int> ConLineJumpDirY { get; set; }
        public VA.ShapeSheet.CellData<int> ConLineJumpStyle { get; set; }
        public VA.ShapeSheet.CellData<int> ConLineRouteExt { get; set; }
        public VA.ShapeSheet.CellData<int> ShapeFixedCode { get; set; }
        public VA.ShapeSheet.CellData<int> ShapePermeablePlace { get; set; }
        public VA.ShapeSheet.CellData<int> ShapePermeableX { get; set; }
        public VA.ShapeSheet.CellData<int> ShapePermeableY { get; set; }
        public VA.ShapeSheet.CellData<int> ShapePlaceFlip { get; set; }
        public VA.ShapeSheet.CellData<int> ShapePlaceStyle { get; set; }
        public VA.ShapeSheet.CellData<int> ShapePlowCode { get; set; }
        public VA.ShapeSheet.CellData<int> ShapeRouteStyle { get; set; }
        public VA.ShapeSheet.CellData<int> ShapeSplit { get; set; }
        public VA.ShapeSheet.CellData<int> ShapeSplittable { get; set; }
        public VA.ShapeSheet.CellData<int> DisplayLevel { get; set; } // new in visio 2010
        public VA.ShapeSheet.CellData<int> Relationships { get; set; } // new in visio 2010

        public override void ApplyFormulas(ApplyFormula func)
        {
            func(ShapeSheet.SRCConstants.ConFixedCode, this.ConFixedCode.Formula);
            func(ShapeSheet.SRCConstants.ConLineJumpCode, this.ConLineJumpCode.Formula);
            func(ShapeSheet.SRCConstants.ConLineJumpDirX, this.ConLineJumpDirX.Formula);
            func(ShapeSheet.SRCConstants.ConLineJumpDirY, this.ConLineJumpDirY.Formula);
            func(ShapeSheet.SRCConstants.ConLineJumpStyle, this.ConLineJumpStyle.Formula);
            func(ShapeSheet.SRCConstants.ConLineRouteExt, this.ConLineRouteExt.Formula);
            func(ShapeSheet.SRCConstants.ShapeFixedCode, this.ShapeFixedCode.Formula);
            func(ShapeSheet.SRCConstants.ShapePermeablePlace, this.ShapePermeablePlace.Formula);
            func(ShapeSheet.SRCConstants.ShapePermeableX, this.ShapePermeableX.Formula);
            func(ShapeSheet.SRCConstants.ShapePermeableY, this.ShapePermeableY.Formula);
            func(ShapeSheet.SRCConstants.ShapePlaceFlip, this.ShapePlaceFlip.Formula);
            func(ShapeSheet.SRCConstants.ShapePlaceStyle, this.ShapePlaceStyle.Formula);
            func(ShapeSheet.SRCConstants.ShapePlowCode, this.ShapePlowCode.Formula);
            func(ShapeSheet.SRCConstants.ShapeRouteStyle, this.ShapeRouteStyle.Formula);
            func(ShapeSheet.SRCConstants.ShapeSplit, this.ShapeSplit.Formula);
            func(ShapeSheet.SRCConstants.ShapeSplittable, this.ShapeSplittable.Formula);
            func(ShapeSheet.SRCConstants.DisplayLevel, this.DisplayLevel.Formula);
            func(ShapeSheet.SRCConstants.Relationships, this.Relationships.Formula);
        }

        private static ShapeLayoutCells get_cells_from_row(ShapeLayoutQuery query, VA.ShapeSheet.Data.Table<VA.ShapeSheet.CellData<double>> table, int row)
        {
            var cells = new ShapeLayoutCells();
            cells.ConFixedCode = table[row,query.ConFixedCode].ToInt();
            cells.ConLineJumpCode = table[row,query.ConLineJumpCode].ToInt();
            cells.ConLineJumpDirX = table[row,query.ConLineJumpDirX].ToInt();
            cells.ConLineJumpDirY = table[row,query.ConLineJumpDirY].ToInt();
            cells.ConLineJumpStyle = table[row,query.ConLineJumpStyle].ToInt();
            cells.ConLineRouteExt = table[row,query.ConLineRouteExt].ToInt();
            cells.ShapeFixedCode = table[row,query.ShapeFixedCode].ToInt();
            cells.ShapePermeablePlace = table[row,query.ShapePermeablePlace].ToInt();
            cells.ShapePermeableX = table[row,query.ShapePermeableX].ToInt();
            cells.ShapePermeableY = table[row,query.ShapePermeableY].ToInt();
            cells.ShapePlaceFlip = table[row,query.ShapePlaceFlip].ToInt();
            cells.ShapePlaceStyle = table[row,query.ShapePlaceStyle].ToInt();
            cells.ShapePlowCode = table[row,query.ShapePlowCode].ToInt();
            cells.ShapeRouteStyle = table[row,query.ShapeRouteStyle].ToInt();
            cells.ShapeSplit = table[row,query.ShapeSplit].ToInt();
            cells.ShapeSplittable = table[row,query.ShapeSplittable].ToInt();
            cells.DisplayLevel= table[row,query.DisplayLevel].ToInt();
            cells.Relationships = table[row,query.Relationships].ToInt();
            return cells;
        }

        public static IList<ShapeLayoutCells> GetCells(IVisio.Page page, IList<int> shapeids)
        {
            var query =get_query();
            return VA.ShapeSheet.CellGroups.CellGroup.CellsFromRows(page, shapeids, query, get_cells_from_row);
        }

        public static ShapeLayoutCells GetCells(IVisio.Shape shape)
        {
            var query = get_query();
            return VA.ShapeSheet.CellGroups.CellGroup.CellsFromRow(shape, query, get_cells_from_row);
        }

        private static ShapeLayoutQuery m_query;
        private static ShapeLayoutQuery get_query()
        {
            if (m_query == null)
            {
                m_query = new ShapeLayoutQuery();
            }
            return m_query;
        }

        class ShapeLayoutQuery : VA.ShapeSheet.Query.CellQuery
        {
            public VA.ShapeSheet.Query.QueryColumn ConFixedCode { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ConLineJumpCode { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ConLineJumpDirX { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ConLineJumpDirY { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ConLineJumpStyle { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ConLineRouteExt { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ShapeFixedCode { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ShapePermeablePlace { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ShapePermeableX { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ShapePermeableY { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ShapePlaceFlip { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ShapePlaceStyle { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ShapePlowCode { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ShapeRouteStyle { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ShapeSplit { get; set; }
            public VA.ShapeSheet.Query.QueryColumn ShapeSplittable { get; set; }
            public VA.ShapeSheet.Query.QueryColumn DisplayLevel { get; set; }
            public VA.ShapeSheet.Query.QueryColumn Relationships { get; set; }

            public ShapeLayoutQuery() :
                base()
            {
                this.ConFixedCode = this.AddColumn(VA.ShapeSheet.SRCConstants.ConFixedCode, "ConFixedCode");
                this.ConLineJumpCode = this.AddColumn(VA.ShapeSheet.SRCConstants.ConLineJumpCode, "ConLineJumpCode");
                this.ConLineJumpDirX = this.AddColumn(VA.ShapeSheet.SRCConstants.ConLineJumpDirX, "ConLineJumpDirX");
                this.ConLineJumpDirY = this.AddColumn(VA.ShapeSheet.SRCConstants.ConLineJumpDirY, "ConLineJumpDirY");
                this.ConLineJumpStyle = this.AddColumn(VA.ShapeSheet.SRCConstants.ConLineJumpStyle, "ConLineJumpStyle");
                this.ConLineRouteExt = this.AddColumn(VA.ShapeSheet.SRCConstants.ConLineRouteExt, "ConLineRouteExt");
                this.ShapeFixedCode = this.AddColumn(VA.ShapeSheet.SRCConstants.ShapeFixedCode, "ShapeFixedCode");
                this.ShapePermeablePlace = this.AddColumn(VA.ShapeSheet.SRCConstants.ShapePermeablePlace, "ShapePermeablePlace");
                this.ShapePermeableX = this.AddColumn(VA.ShapeSheet.SRCConstants.ShapePermeableX, "ShapePermeableX");
                this.ShapePermeableY = this.AddColumn(VA.ShapeSheet.SRCConstants.ShapePermeableY, "ShapePermeableY");
                this.ShapePlaceFlip = this.AddColumn(VA.ShapeSheet.SRCConstants.ShapePlaceFlip, "ShapePlaceFlip");
                this.ShapePlaceStyle = this.AddColumn(VA.ShapeSheet.SRCConstants.ShapePlaceStyle, "ShapePlaceStyle");
                this.ShapePlowCode = this.AddColumn(VA.ShapeSheet.SRCConstants.ShapePlowCode, "ShapePlowCode");
                this.ShapeRouteStyle = this.AddColumn(VA.ShapeSheet.SRCConstants.ShapeRouteStyle, "ShapeRouteStyle");
                this.ShapeSplit = this.AddColumn(VA.ShapeSheet.SRCConstants.ShapeSplit, "ShapeSplit");
                this.ShapeSplittable = this.AddColumn(VA.ShapeSheet.SRCConstants.ShapeSplittable, "ShapeSplittable");
                this.DisplayLevel= this.AddColumn(VA.ShapeSheet.SRCConstants.DisplayLevel, "DisplayLevel");
                this.Relationships = this.AddColumn(VA.ShapeSheet.SRCConstants.Relationships, "Relationships");
            }
        }

    }
}