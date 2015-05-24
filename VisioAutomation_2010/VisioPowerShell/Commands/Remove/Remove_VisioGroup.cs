using System.Management.Automation;
using SMA = System.Management.Automation;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioPowerShell.Commands.Remove
{
    [Cmdlet(SMA.VerbsCommon.Remove, "VisioGroup")]
    public class Remove_VisioGroup : VisioCmdlet
    {
        [Parameter(Mandatory = false)]
        public IVisio.Shape[] Shapes;

        protected override void ProcessRecord()
        {
            this.client.Arrange.Ungroup(this.Shapes);
        }
    }
}