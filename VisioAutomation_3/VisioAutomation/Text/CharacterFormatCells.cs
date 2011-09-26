using System.Collections.Generic;
using IVisio = Microsoft.Office.Interop.Visio;
using VA = VisioAutomation;
using VisioAutomation.Extensions;

namespace VisioAutomation.Text
{
    public class CharacterFormatCells: VA.ShapeSheet.CellSectionDataGroup
    {
        public VA.ShapeSheet.CellData<int> Color { get; set; }
        public VA.ShapeSheet.CellData<int> Font { get; set; }
        public VA.ShapeSheet.CellData<double> Size { get; set; }
        public VA.ShapeSheet.CellData<int> Style { get; set; }
        public VA.ShapeSheet.CellData<double> Transparency { get; set; }

        protected override void _Apply(VA.ShapeSheet.CellSectionDataGroup.ApplyFormula func, short row)
        {
            func(VA.ShapeSheet.SRCConstants.Char_Color.ForRow(row), this.Color.Formula);
            func(VA.ShapeSheet.SRCConstants.Char_Font.ForRow(row), this.Font.Formula);
            func(VA.ShapeSheet.SRCConstants.Char_Size.ForRow(row), this.Size.Formula);
            func(VA.ShapeSheet.SRCConstants.Char_Style.ForRow(row), this.Style.Formula);
            func(VA.ShapeSheet.SRCConstants.Char_ColorTrans.ForRow(row), this.Transparency.Formula);
        }

        internal static IList<List<CharacterFormatCells>> GetCells(IVisio.Page page, IList<int> shapeids)
        {
            var query = new CharacterFormatQuery();
            return VA.ShapeSheet.CellSectionDataGroup._GetObjectsFromRowsGrouped(page, shapeids, query, get_cells_from_row);
        }

        internal static IList<CharacterFormatCells> GetCells(IVisio.Shape shape)
        {
            var query = new CharacterFormatQuery();
            return VA.ShapeSheet.CellSectionDataGroup._GetObjectsFromRows(shape, query, get_cells_from_row);
        }

        private static CharacterFormatCells get_cells_from_row(CharacterFormatQuery query, VA.ShapeSheet.Query.QueryDataRow<double> row)
        {
            var cells = new CharacterFormatCells();
            cells.Color = row.GetItem(query.Color).ToInt();
            cells.Transparency = row.GetItem(query.Trans);
            cells.Font = row.GetItem(query.Font).ToInt();
            cells.Size = row.GetItem(query.Size);
            cells.Style = row.GetItem(query.Style).ToInt();

            return cells;
        }

        class CharacterFormatQuery : VA.ShapeSheet.Query.SectionQuery
        {
            public VA.ShapeSheet.Query.SectionQueryColumn Font { get; set; }
            public VA.ShapeSheet.Query.SectionQueryColumn Style { get; set; }
            public VA.ShapeSheet.Query.SectionQueryColumn Color { get; set; }
            public VA.ShapeSheet.Query.SectionQueryColumn Size { get; set; }
            public VA.ShapeSheet.Query.SectionQueryColumn Trans { get; set; }

            public CharacterFormatQuery() :
                base(IVisio.VisSectionIndices.visSectionCharacter)
            {
                Color = this.AddColumn(VA.ShapeSheet.SRCConstants.Char_Color, "Color");
                Trans = this.AddColumn(VA.ShapeSheet.SRCConstants.Char_ColorTrans, "Trans");
                Font = this.AddColumn(VA.ShapeSheet.SRCConstants.Char_Font, "Font");
                Size = this.AddColumn(VA.ShapeSheet.SRCConstants.Char_Size, "Size");
                Style = this.AddColumn(VA.ShapeSheet.SRCConstants.Char_Style, "Style");
            }
        }
    }
}