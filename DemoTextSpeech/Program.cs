using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace DemoTextSpeech
{
    class Program
    {
        private static string sucriptionKey = "8c979d42db1b4f368ff888dafd686b92";
        private static string serviceRegion = "BrazilSouth";

        static async Task Main()
        {
            Console.WriteLine("Probando el Speech Text");
            await SyntheziAudioSpeakerAsync();
            await SyntheziAudioToFileAsync();
            Console.ReadLine();
        }

        static async Task SyntheziAudioSpeakerAsync()
        {
            var config = SpeechConfig.FromSubscription(sucriptionKey, serviceRegion);
            using var synthesizer = new SpeechSynthesizer(config);
            await synthesizer.SpeakTextAsync("Hello, I am testing the text to speech service at Platzi");
        }

        static async Task SyntheziAudioToFileAsync()
        {
            try
            {
                var config = SpeechConfig.FromSubscription(sucriptionKey, serviceRegion);
                config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Riff24Khz16BitMonoPcm);

                using var synthesizer = new SpeechSynthesizer(config, null);
                var ssml = File.ReadAllText("Configuracion.xml");
                var resultssml = await synthesizer.SpeakSsmlAsync(ssml);

                using var stream = AudioDataStream.FromResult(resultssml);
                await stream.SaveToWaveFileAsync("output-test.wav");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //"8c979d42db1b4f368ff888dafd686b92"
    }
}
