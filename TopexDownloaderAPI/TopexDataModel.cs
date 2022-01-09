namespace TopexDownloaderAPI
{
    public class TopexDataModel
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double value { get; set; }

        public TopexDataModel(double lat, double lon, double val)
        {
            this.latitude = lat;
            this.longitude = lon;
            this.value = val;
        }

        public static List<TopexDataModel> Serialize(string input)
        {
            var result = input.TrimStart()
                             .Split(' ')
                             .Where(i => i != "" && i != " ")
                             .Select(i => Double.Parse(i.Replace('.', ',')))
                             .Chunk(3)
                             .Select(i => new TopexDataModel(i[1], i[0], i[2]))
                             .ToList();
            return result;
        }
    }
}