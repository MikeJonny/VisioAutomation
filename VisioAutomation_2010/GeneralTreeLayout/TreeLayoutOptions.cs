namespace GeneralTreeLayout
{
    public class TreeLayoutOptions
    {
        public TreeLayoutOptions()
        {
            this.SubtreeSeparation = 1;
            this.SiblingSeparation = 1;
            this.Direction = TreeLayoutDirection.TopToBottom;
            this.Alignment = AlignmentVertical.Top;
            this.MaximumDepth = 100;
            this.LevelSeparation = 1;
            this.DefaultNodeSize = new Geometry.Size(1, 1);
        }

        public Geometry.Point TopAdjustment { get; set; }
        public Geometry.Size DefaultNodeSize { get; set; }
        public double LevelSeparation { get; set; }
        public int MaximumDepth { get; set; }
        public AlignmentVertical Alignment { get; set; }
        public TreeLayoutDirection Direction { get; set; }
        public double SiblingSeparation { get; set; }
        public double SubtreeSeparation { get; set; }
    }
}