using System.Management.Automation;
using IVisio = Microsoft.Office.Interop.Visio;
using SMA = System.Management.Automation;

namespace VisioPowerShell.Commands.New
{
    [Cmdlet(SMA.VerbsCommon.New, "VisioConnection")]
    public class New_VisioConnection : VisioCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public IVisio.Shape[] From { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public IVisio.Shape[] To { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public IVisio.Master Master { get; set; }

        protected override void ProcessRecord()
        {
            var connectors = this.client.Connection.Connect(this.From, this.To, this.Master);
            this.WriteObject(connectors, false);
        }
    }
}