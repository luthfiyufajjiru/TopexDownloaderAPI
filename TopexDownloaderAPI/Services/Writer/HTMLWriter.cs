using System.Text;

namespace TopexDownloaderAPI.Services.Writer
{
    public static partial class Writer
    {
        public static async Task<byte[]> WriteHTML(string htmlstring)
        {
            var baseStream = new MemoryStream();
            var Writer = new StreamWriter(baseStream, Encoding.UTF8);
            byte[] result;

            try
            {
                Writer.Write(htmlstring);
                Writer.Flush();
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
                tasks.Add(Task.Run(() => Writer.Dispose()));

                await Task.WhenAll(tasks);
            }

            return result;
        }
    }
}
