namespace GeneralTreeLayout
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


        public Rectangle(Point lowerleft, Size s)
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



        public Point LowerLeft => new Point(this.Left, this.Bottom);

        public Point LowerRight => new Point(this.Right, this.Bottom);

        public Point UpperLeft => new Point(this.Left, this.Top);

        public Point UpperRight => new Point(this.Right, this.Top);

        public Size Size => new Size(this.Width, this.Height);

        public double Width => this.Right - this.Left;

        public double Height => this.Top - this.Bottom;

        public Point Center => new Point((this.Left + this.Right)/2.0, (this.Bottom + this.Top)/2.0);


        public Rectangle Add(double dx, double dy)
        {
            var r2 = new Rectangle(this.Left + dx, this.Bottom + dy, this.Right + dx, this.Top + dy);
            return r2;
        }



        public Rectangle Subtract(double dx, double dy)
        {
            var r2 = new Rectangle(this.Left - dx, this.Bottom - dy, this.Right - dx, this.Top - dy);
            return r2;
        }



        public Rectangle Multiply(double sx, double sy)
        {
            var r2 = new Rectangle(this.Left*sx, this.Bottom*sy, this.Right*sx, this.Top*sy);
            return r2;
        }
    }
}