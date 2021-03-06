namespace VisioAutomation.ShapeSheet
{
    public struct CellData
    {
        public FormulaLiteral Formula { get; }
        public string Result { get; }

        public CellData(FormulaLiteral formula, string result)
            : this()
        {
            this.Formula = formula;
            this.Result = result;
        }

        public override string ToString()
        {
            var fs = (this.Formula.HasValue) ? this.Formula.Value : "null";
            return string.Format(System.Globalization.CultureInfo.InvariantCulture,"(\"{0}\",{1})", fs, this.Result);
        }

        public static implicit operator CellData(FormulaLiteral formula)
        {
            return new CellData(formula,default(string));
        }

        public static implicit operator CellData(string formula)
        {
            return new CellData( formula, default(string));
        }

        public static implicit operator CellData(int formula)
        {
            return new CellData(formula, default(string));
        }

        public static implicit operator CellData(double formula)
        {
            return new CellData(formula, default(string));
        }

        public static implicit operator CellData(bool formula)
        {
            return new CellData(formula, default(string));
        }

        public static CellData[] Combine(string[] formulas, string[] results)
        {
            int n = results.Length;

            if (formulas.Length != results.Length)
            {
                throw new System.ArgumentException("Array Lengths must match");
            }

            var combined_data = new ShapeSheet.CellData[n];
            for (int i = 0; i < n; i++)
            {
                combined_data[i] = new ShapeSheet.CellData(formulas[i], results[i]);
            }
            return combined_data;
        }
    }
}