﻿namespace VisioAutomation.ShapeSheet.Queries.Columns
{
    public class ColumnSubQuery : ColumnBase
    {
        public short CellIndex;

        internal ColumnSubQuery(int ordinal, short cell, string name) :
            base(ordinal, name)
        {
            this.CellIndex = cell;
        }
    }
}