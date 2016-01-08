﻿using System.Collections.Generic;
using System.Linq;

namespace VisioAutomation.ShapeSheet.Query
{
    internal class SectionColumnDetails
    {
        public SectionColumn SectionColumn { get; private set; }
        public short ShapeID { get; private set; }
        public int RowCount  { get; }

        internal SectionColumnDetails(SectionColumn sq, short shapeid, int numrows)
        {
            this.SectionColumn = sq;
            this.ShapeID = shapeid;
            this.RowCount = numrows;
        }

        public IEnumerable<int> RowIndexes
        {
            get { return Enumerable.Range(0, this.RowCount); }
        }
    }
}