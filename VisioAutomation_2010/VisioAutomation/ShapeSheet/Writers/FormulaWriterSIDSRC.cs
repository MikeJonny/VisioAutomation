using System.Collections.Generic;

namespace VisioAutomation.ShapeSheet.Writers
{
    public class FormulaWriterSIDSRC : WriterBase<VisioAutomation.ShapeSheet.SIDSRC,FormulaLiteral>
    {
        public FormulaWriterSIDSRC() : base()
        {
        }

        public FormulaWriterSIDSRC(int capacity) : base(capacity)
        {
        }

        public void SetFormula(short id, SRC src, FormulaLiteral formula)
        {
            var sidsrc = new SIDSRC(id, src);
            this.__SetFormulaIgnoreNull(sidsrc, formula);
        }

        public void SetFormula(SIDSRC sidsrc, FormulaLiteral formula)
        {
            this.__SetFormulaIgnoreNull(sidsrc, formula);
        }

        protected void __SetFormulaIgnoreNull(SIDSRC sidsrc, FormulaLiteral formula)
        {
            if (formula.HasValue)
            {
                this.StreamItems.Add(sidsrc);
                this.ValueItems.Add(formula);
            }
        }

        protected override void _commit_to_surface(ShapeSheetSurface surface)
        {
            // Do nothing if there aren't any updates
            if (this.ValueItems.Count < 1)
            {
                return;
            }

            var stream = SIDSRC.ToStream(this.StreamItems);
            var formulas = WriterHelper.build_formulas_array(this.ValueItems);
            var flags = this.ComputeGetFormulaFlags();
            int c = surface.SetFormulas(stream, formulas, (short)flags);
        }
    }
}