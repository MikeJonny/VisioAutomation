using System.Management.Automation;
using SMA = System.Management.Automation;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioPowerShell.Commands.Open
{
    [Cmdlet(SMA.VerbsCommon.Open, "VisioMaster")]
    public class Open_VisioMaster : VisioCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        [ValidateNotNull]
        public IVisio.Master Master;

        protected override void ProcessRecord()
        {
            this.client.Master.OpenForEdit(this.Master);
        }
    }
}