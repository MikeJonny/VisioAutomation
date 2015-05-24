using System.Linq;
using System.Management.Automation;
using SMA = System.Management.Automation;

namespace VisioPowerShell.Commands.New
{
    [Cmdlet(SMA.VerbsCommon.New, "VisioPolyLine")]
    public class New_VisioPolyLine : VisioCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public double[] Doubles { get; set; }

        protected override void ProcessRecord()
        {
            var points = VisioAutomation.Drawing.Point.FromDoubles(this.Doubles).ToList();
            var shape = this.client.Draw.PolyLine(points);
            this.WriteObject(shape);
        }
    }
}