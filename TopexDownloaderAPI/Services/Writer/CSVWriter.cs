using System.Text;

namespace TopexDownloaderAPI.Services.Writer
{
    public static partial class Writer
    {
        public static async Task<byte[]> WriteCSV(this List<TopexDataModel> data, bool gravity)
        {
            var baseStream = new MemoryStream();
            var csvWriter = new StreamWriter(baseStream, Encoding.UTF8);
            byte[] result;
            string type = gravity ? "gravity" : "elevation";

            try
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var item = data[i];
                    string line = i < 1 ? $"lat;lon;{type}" : $"{item.latitude};{item.longitude};{item.value}";
                    csvWriter.WriteLine(line);
                }
                csvWriter.Flush();
                baseStream.Seek(0, SeekOrigin.Begin);
                result = baseStream.ToArray();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                List<Task> tasks = new();
                tasks.Add(Task.Run(() => baseStream.Dispose()));
                tasks.Add(Task.Run(() => csvWriter.Dispose()));

                await Task.WhenAll(tasks);
            }

            return result;
        }
        public static async Task<byte[]> WriteCSV(this List<CompositeModel> data)
        {
            var baseStream = new MemoryStream();
            var csvWriter = new StreamWriter(baseStream, Encoding.UTF8);
            byte[] result;

            try
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var item = data[i];
                    string line = i < 1 ? $"lat;lon;elevation;gravity" : $"{item.latitude};{item.longitude};{item.elevation};{item.gravity}";
                    csvWriter.WriteLine(line);
                }
                csvWriter.Flush();
                baseStream.Seek(0, SeekOrigin.Begin);
                result = baseStream.ToArray();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                List<Task> tasks = new();
                tasks.Add(Task.Run(() => baseStream.Dispose()));
                tasks.Add(Task.Run(() => csvWriter.Dispose()));

                await Task.WhenAll(tasks);
            }

            return result;
        }
    }
}
