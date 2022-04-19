using System;
using Azure;
using Azure.AI.TextAnalytics;

namespace DemoTextAnalytics
{
    class Program
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("3649bc77c8ba48e4a87a615788c46258");
        private static readonly Uri endPoint = new Uri("https://textanalyticsplatzijc.cognitiveservices.azure.com/");
        static void Main(string[] args)
        {
            Console.WriteLine("Probando Text Analytics desde aplicativo consola");
            Console.WriteLine("Digite el texto a consultar : ");
            var client = new TextAnalyticsClient(endPoint, credentials);
            var mensaje = Console.ReadLine();
            KeyPhraseExtraction(client, mensaje);
            Console.ReadLine();
        }

        static void KeyPhraseExtraction(TextAnalyticsClient client, string texto)
        {
            var response = client.ExtractKeyPhrases(texto);
            Console.WriteLine("Frase clave detectadas : ");
            foreach (string keyphrase in response.Value)
            {
                Console.WriteLine("keyphrase : " + keyphrase);
            }
        }
    }
}
