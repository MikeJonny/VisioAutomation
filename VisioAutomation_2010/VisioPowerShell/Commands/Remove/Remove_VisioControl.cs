using System.Management.Automation;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioPowerShell.Commands.Remove
{
    [Cmdlet(VerbsCommon.Remove, VisioPowerShell.Nouns.VisioControl)]
    public class Remove_VisioControl : VisioCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Index { get; set; }

        [Parameter(Mandatory = false)]
        public IVisio.Shape[] Shapes;

        protected override void ProcessRecord()
        {
            var targets = new VisioAutomation.Scripting.TargetShapes(this.Shapes);

            this.Client.Control.Delete(targets,this.Index);
        }
    }
}