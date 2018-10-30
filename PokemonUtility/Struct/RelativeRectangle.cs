namespace PokemonUtility.Struct
{
    struct RelativeRectangle
    {
        public double X;
        public double Y;
        public double Width;
        public double Height;

        public RelativeRectangle(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
