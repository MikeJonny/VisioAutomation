﻿using System.Linq;
using System.Management.Automation;
using IVisio = Microsoft.Office.Interop.Visio;
using SMA = System.Management.Automation;

namespace VisioPowerShell.Commands.Get
{
    [Cmdlet(SMA.VerbsCommon.Get, "VisioShapeCell")]
    public class Get_VisioShapeCell : VisioCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string[] Cells { get; set; }

        [Parameter(Mandatory = false)]
        public IVisio.Shape[] Shapes { get; set; }

        [Parameter(Mandatory = false)] 
        public SMA.SwitchParameter GetResults;

        [Parameter(Mandatory = false)] 
        public Model.ResultType ResultType = Model.ResultType.String;

        protected override void ProcessRecord()
        {
            var cellmap = CellSRCDictionary.GetCellMapForShapes();
            if (this.Cells == null || this.Cells.Length < 1 || this.Cells.Contains("*"))
            {
                this.Cells = cellmap.GetNames().ToArray();
            }

            Get_VisioPageCell.EnsureEnoughCellNames(this.Cells);
            var target_shapes = this.Shapes ?? this.client.Selection.GetShapes();
            this.WriteVerbose("Valid Names: " + string.Join(",", cellmap.GetNames()));
            var query = cellmap.CreateQueryFromCellNames(this.Cells);
            var surface = this.client.ShapeSheet.GetShapeSheetSurface();
            var target_shapeids = target_shapes.Select(s => s.ID).ToList();
            var dt = Helpers.QueryToDataTable(query, this.GetResults, this.ResultType, target_shapeids, surface);
            this.WriteObject(dt);
        }
    }
}