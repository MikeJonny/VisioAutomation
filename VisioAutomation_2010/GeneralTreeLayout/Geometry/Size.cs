namespace GeneralTreeLayout.Geometry
{
    public struct Size
    {
        public double Width { get; }
        public double Height { get; }

        public Size(double width, double height)
            : this()
        {
            if (width < 0.0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(width));
            }
            if (height < 0.0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(height));
            }
            this.Width = width;
            this.Height = height;
        }

        public Size Add(double width, double height)
        {
            return new Size(this.Width + width, this.Height + height);
        }
    }
}