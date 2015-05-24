using System.Management.Automation;
using SMA=System.Management.Automation;

namespace VisioPowerShell.Commands.Close
{
    [Cmdlet(SMA.VerbsCommon.Close, "VisioMaster")]
    public class Close_VisioMaster : VisioCmdlet
    {
        protected override void ProcessRecord()
        {
            this.client.Master.CloseMasterEditing();
        }
    }
}