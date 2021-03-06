using System.Collections.Generic;
using VisioAutomation.ShapeSheet.CellGroups;

namespace VisioAutomation.Text
{
    public class TextXFormCells : ShapeSheet.CellGroups.CellGroupSingleRow
    {
        public ShapeSheet.CellData TxtAngle { get; set; }
        public ShapeSheet.CellData TxtWidth { get; set; }
        public ShapeSheet.CellData TxtHeight { get; set; }
        public ShapeSheet.CellData TxtPinX { get; set; }
        public ShapeSheet.CellData TxtPinY { get; set; }
        public ShapeSheet.CellData TxtLocPinX { get; set; }
        public ShapeSheet.CellData TxtLocPinY { get; set; }

        public override IEnumerable<SRCFormulaPair> Pairs
        {
            get
            {
                yield return this.newpair(ShapeSheet.SRCConstants.TxtPinX, this.TxtPinX.Formula);
                yield return this.newpair(ShapeSheet.SRCConstants.TxtPinY, this.TxtPinY.Formula);
                yield return this.newpair(ShapeSheet.SRCConstants.TxtLocPinX, this.TxtLocPinX.Formula);
                yield return this.newpair(ShapeSheet.SRCConstants.TxtLocPinY, this.TxtLocPinY.Formula);
                yield return this.newpair(ShapeSheet.SRCConstants.TxtWidth, this.TxtWidth.Formula);
                yield return this.newpair(ShapeSheet.SRCConstants.TxtHeight, this.TxtHeight.Formula);
                yield return this.newpair(ShapeSheet.SRCConstants.TxtAngle, this.TxtAngle.Formula);
            }
        }

        public static List<TextXFormCells> GetCells(Microsoft.Office.Interop.Visio.Page page, IList<int> shapeids)
        {
            var query = TextXFormCells.lazy_query.Value;
            return query.GetCellGroups(page, shapeids);
        }

        public static TextXFormCells GetCells(Microsoft.Office.Interop.Visio.Shape shape)
        {
            var query = TextXFormCells.lazy_query.Value;
            return query.GetCellGroup(shape);
        }

        private static System.Lazy<TextXFormCellsReader> lazy_query = new System.Lazy<TextXFormCellsReader>();
    }
}