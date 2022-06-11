namespace TopexDownloaderAPI.Services
{
    public static partial class GeographicServices
    {
        public static double LatLonRectangle(double North, double West, double East, double South)
        {
            return Distance(North, West, East, North) * Distance(North, East, East, South);
        }
    }
}
