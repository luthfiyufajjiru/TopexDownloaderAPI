namespace TopexDownloaderAPI
{
    public class TopexDataModel
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double value { get; set; }

        public TopexDataModel(double lat, double lon, double val)
        {
            latitude = lat;
            longitude = lon;
            value = val;
        }

        public static List<TopexDataModel> Serialize(string input)
        {
            var result = input.TrimStart()
                             .Split(' ')
                             .Where(i => i != "" && i != " ")
                             .Select(i => Double.Parse(i))
                             .Chunk(3)
                             .Select(i => new TopexDataModel(i[1], i[0], i[2]))
                             .ToList();
            return result;
        }
    }

    public class CompositeModel
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double elevation { get; set; }
        public double gravity { get; set; }

        public CompositeModel(double lat, double lon, double z, double g)
        {
            latitude = lat;
            longitude = lon;
            elevation = z;
            gravity = g;
        }
    }
}