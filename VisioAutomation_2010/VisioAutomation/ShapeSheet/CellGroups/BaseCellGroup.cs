using System.Collections.Generic;
using IVisio = Microsoft.Office.Interop.Visio;
using VA = VisioAutomation;

namespace VisioAutomation.ShapeSheet.CellGroups
{
    public abstract class BaseCellGroup
    {
        public delegate T RowToObject<T,RT>(CellData<RT>[] data);

        public struct SRCFormulaPair
        {
            public SRC SRC;
            public FormulaLiteral Formula;

            public SRCFormulaPair(SRC src, FormulaLiteral formula)
            {
                this.SRC = src;
                this.Formula = formula;
            }
        }

        protected SRCFormulaPair srcvaluepair(SRC src, FormulaLiteral f)
        {
            return new SRCFormulaPair(src, f);
        }

        public abstract IEnumerable<SRCFormulaPair> Pairs();
    }
}