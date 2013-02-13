using System.Collections.Generic;
using VAS=VisioAutomation.Scripting;
using SMA = System.Management.Automation;

namespace VisioPS.Commands
{
    [SMA.Cmdlet(SMA.VerbsCommon.Remove, "VisioControl")]
    public class Remove_VisioControl : VisioPSCmdlet
    {
        [SMA.Parameter(Position = 0, Mandatory = true)]
        public int ControlIndex { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public IList<Microsoft.Office.Interop.Visio.Shape> Shapes;

        protected override void ProcessRecord()
        {
            var scriptingsession = this.ScriptingSession;
            this.ScriptingSession.Control.Delete(this.Shapes,this.ControlIndex);
        }
    }
}