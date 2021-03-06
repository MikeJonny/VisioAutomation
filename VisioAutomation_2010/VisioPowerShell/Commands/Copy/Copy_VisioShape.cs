﻿using System.Management.Automation;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioPowerShell.Commands.Copy
{
    [Cmdlet(VerbsCommon.Copy, VisioPowerShell.Nouns.VisioShape)]
    public class Copy_VisioShape : VisioCmdlet
    {
        [Parameter(Mandatory = false)]
        public IVisio.Shape[] Shapes;

        protected override void ProcessRecord()
        {
            var targets = new VisioAutomation.Scripting.TargetShapes(this.Shapes);
            this.Client.Selection.Duplicate(targets);
        }
    }
}