using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CVSPredictionSample
{
    public static class Program
    {
        public static void Main()
        {
            //SET INTERNET EXPLORER AS DEFAULT TRY TO RECREATE ERROR
            Console.Write("Enter Image path:");
            // string imageFilePath = @"E:\CS Project InCollege\PhotosGoHere\" + "0" + ".png"; //MACHINE INDAPENDENT


            string imageFilePath = Console.ReadLine(); //REMOVE IN FINAL

            //Console.WriteLine("FileRequestCreated with path" + imageFilePath);
            MakePredictionRequest(imageFilePath).Wait();

           Console.WriteLine("\n\nHit ENTER to exit..."); //REMOVE IN FINAL
            Console.ReadLine();
            
        }

        public static async Task MakePredictionRequest(string imageFilePath)
        {
            var client = new HttpClient();
         //   Console.WriteLine("Point 1 Reached");
            // Request headers - replace this example key with your valid Prediction-Key.
            client.DefaultRequestHeaders.Add("Prediction-Key", "1adc22d5da7c4472a71490c227513bdd");

            // Prediction URL - replace this example URL with your valid Prediction URL.
            string url = "https://cs57073-prediction.cognitiveservices.azure.com/customvision/v3.0/Prediction/064ee1fd-ebe0-467c-8dda-b6b4058bd348/classify/iterations/Iteration1/image";
            HttpResponseMessage response;

            // Request body. Try this sample with a locally stored image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                


            }
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
}