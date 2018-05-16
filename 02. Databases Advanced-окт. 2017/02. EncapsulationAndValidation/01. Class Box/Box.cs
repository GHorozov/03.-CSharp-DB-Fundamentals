public class Box
{
    private double length;
    private double width;
    private double height;

    public Box(double length, double width, double height)
    {
        this.Length = length;
        this.Width = width;
        this.Height = height;
    }

    public double Length { get => length; set => length = value; }
    public double Width { get => width; set => width = value; }
    public double Height { get => height; set => height = value; }

    public double SurfaceArea(double length, double width, double height)
    {
        var result = (2 * length * width) + (2 * length * height) + (2 * width * height);
        return result;
    }

    public double LiteralSurfaceArea(double length, double width, double height)
    {
        var result =(2*length*height + 2*width*height);
        return result;
    }

    public double Volume(double length, double width, double height)
    {
        var result = (length*width*height);
        return result;
    }
}

