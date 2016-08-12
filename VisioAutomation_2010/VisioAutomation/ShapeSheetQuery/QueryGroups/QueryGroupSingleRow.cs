using IVisio = Microsoft.Office.Interop.Visio;
using System.Collections.Generic;
using VisioAutomation.ShapeSheet;
using VisioAutomation.ShapeSheetQuery.Results;

namespace VisioAutomation.ShapeSheetQuery.QueryGroups
{
    public abstract class QueryGroupSingleRow : QueryGroupBase
    {
        private static void check_query(Query query)
        {
            if (query.Cells.Count < 1)
            {
                throw new AutomationException("Query must contain at least 1 Column");
            }

            if (query.SubQueries.Count != 0)
            {
                throw new AutomationException("Query should not contain contain any sections");
            }
        }

        protected static IList<T> _GetCells<T, RT>(
            IVisio.Page page, IList<int> shapeids,
            Query query,
            RowToObject<T, RT> row_to_object)
        {
            check_query(query);

            var surface = new QuerySurface(page);
            var data_for_shapes = query.GetCellData<RT>( surface, shapeids);
            var list = new List<T>(shapeids.Count);
            foreach (var data_for_shape in data_for_shapes)
            {
                var cells = row_to_object(data_for_shape.Cells);
                list.Add(cells);
            }
            return list;
        }

        protected static T _GetCells<T, RT>(
            IVisio.Shape shape,
            Query query,
            RowToObject<T, RT> row_to_object)
        {
            check_query(query);

            var ss1 = new QuerySurface(shape);
            Result<CellData<RT>> data_for_shape = query.GetCellData<RT>(ss1);
            var cells = row_to_object(data_for_shape.Cells);
            return cells;
        }
    }
}