namespace TopexDownloaderAPI.Services
{
    public static partial class GeographicServices
    {
        public static double Radians(double degrees) => degrees * Math.PI / 180;
        public static double Degrees(double radians) => radians * 180 / Math.PI;

        /// <summary>
        /// Calculate distance between two geographical points.
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lon2"></param>
        /// <param name="lat2"></param>
        /// <returns>Distance in meter</returns>
        public static double Distance(double lat1, double lon1, double lon2, double lat2)
        {
            var R = 6378.137;
            var dLat = Radians(lat2) - Radians(lat1);
            var dLon = Radians(lon2) - Radians(lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d * 1000;
        }
    }
}
