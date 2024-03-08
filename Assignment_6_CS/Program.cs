using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the URL:");
        string url = Console.ReadLine();

        try
        {
            await DownloadAndWriteToFileAsync(url);
            Console.WriteLine("Content has been successfully written to A.txt.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static async Task DownloadAndWriteToFileAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throws exception if not successful

            using (Stream contentStream = await response.Content.ReadAsStreamAsync())
            {
                using (StreamReader reader = new StreamReader(contentStream))
                {
                    using (StreamWriter writer = new StreamWriter("A.txt"))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = await reader.ReadLineAsync();
                            await writer.WriteLineAsync(line);
                        }
                    }
                }
            }
        }
    }
}
