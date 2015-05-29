namespace GeneralTreeLayout.Geometry
{
    public struct Rectangle
    {
        public double Left { get; }
        public double Bottom { get; }
        public double Right { get; }
        public double Top { get; }

        public Rectangle(double left, double bottom, double right, double top)
            : this()
        {
            if (right < left)
            {
                throw new System.ArgumentException("left must be <= right");
            }

            if (top < bottom)
            {
                throw new System.ArgumentException("bottom must be <= top");
            }

            this.Left = left;
            this.Bottom = bottom;
            this.Right = right;
            this.Top = top;
        }


        public Rectangle(Geometry.Point lowerleft, Size s)
            : this()
        {
            if (s.Width < 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(s), "width must be non-negative");
            }

            if (s.Height < 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(s), "height must be non-negative");
            }

            this.Left = lowerleft.X;
            this.Bottom = lowerleft.Y;
            this.Right = lowerleft.X + s.Width;
            this.Top = lowerleft.Y + s.Height;
        }



        public Geometry.Point LowerLeft => new Geometry.Point(this.Left, this.Bottom);

        public Geometry.Point LowerRight => new Geometry.Point(this.Right, this.Bottom);

        public Geometry.Point UpperLeft => new Geometry.Point(this.Left, this.Top);

        public Geometry.Point UpperRight => new Geometry.Point(this.Right, this.Top);

        public Size Size => new Size(this.Width, this.Height);

        public double Width => this.Right - this.Left;

        public double Height => this.Top - this.Bottom;

        public Geometry.Point Center => new Geometry.Point((this.Left + this.Right)/2.0, (this.Bottom + this.Top)/2.0);
    }
}