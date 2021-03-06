using System.Collections.Generic;
using VisioAutomation.ShapeSheet.CellGroups;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioAutomation.Shapes.UserDefinedCells
{
    public class UserDefinedCell : ShapeSheet.CellGroups.CellGroupMultiRow
    {
        public string Name { get; set; }
        public ShapeSheet.CellData Value { get; set; }
        public ShapeSheet.CellData Prompt { get; set; }

        public UserDefinedCell()
        {
        }

        public UserDefinedCell(string name)
        {
            UserDefinedCellHelper.CheckValidName(name);
            this.Name = name;
        }

        public UserDefinedCell(string name, string value)
        {
            UserDefinedCellHelper.CheckValidName(name);

            if (value == null)
            {
                throw new System.ArgumentNullException(nameof(value));
            }

            this.Name = name;
            this.Value = value;
        }

        public UserDefinedCell(string name, string value, string prompt)
        {
            UserDefinedCellHelper.CheckValidName(name);

            if (value == null)
            {
                throw new System.ArgumentNullException(nameof(value));
            }
            
            this.Name = name;
            this.Value = value;
            this.Prompt = prompt;
        }

        public override IEnumerable<SRCFormulaPair> Pairs
        {
            get
            {
                yield return this.newpair(ShapeSheet.SRCConstants.User_Value, this.Value.Formula);
                yield return this.newpair(ShapeSheet.SRCConstants.User_Prompt, this.Prompt.Formula);
            }
        }

        public override string ToString()
        {
            string s = string.Format("(Name={0},Value={1},Prompt={2})", this.Name, this.Value, this.Prompt);
            return s;
        }

        public static List<List<UserDefinedCell>> GetCells(IVisio.Page page, IList<int> shapeids)
        {
            var query = UserDefinedCell.lazy_query.Value;
            return query.GetCellGroups(page, shapeids);
        }

        public static List<UserDefinedCell> GetCells(IVisio.Shape shape)
        {
            var query = UserDefinedCell.lazy_query.Value;
            return query.GetCellGroups(shape);
        }

        private static System.Lazy<UserDefinedCellsReader> lazy_query = new System.Lazy<UserDefinedCellsReader>();


    }
}