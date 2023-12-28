using Firebase.Storage;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;

namespace Upload_Image
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine(await uploadAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static async Task<string> uploadAsync()
        {
            Guid guid = Guid.NewGuid();
            var stream = File.Open(@"C:\\Users\\Samet\\Desktop\\Masaüstü2\\vesikalık.png", FileMode.Open);

            var task = new FirebaseStorage("uploadimageiot.appspot.com")
             .Child("carImages")
             .Child(guid.ToString() + ".png")
             .PutAsync(stream);

            // Track progress of the upload
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // Await the task to wait until upload is completed and get the download url
            var downloadUrl = await task;
            return downloadUrl.ToString();
        }
    }
}