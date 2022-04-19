using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace SpeechtoText
{
    class Program
    {
        static async Task Main()
        {
            var speechConfig = SpeechConfig.FromSubscription("8c979d42db1b4f368ff888dafd686b92", "BrazilSouth");
            await FromMic(speechConfig);
            
            await FromFile(speechConfig);
            Console.ReadLine();
        }

        async static Task FromMic(SpeechConfig speechConfig)
        {
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var recognizer = new SpeechRecognizer(speechConfig,"es-MX" ,audioConfig);
            Console.WriteLine("Habla al micrófono");
            var result = await recognizer.RecognizeOnceAsync();
            Console.WriteLine("Tu dijiste lo siguiente : " + result.Text);
         }

        async static Task FromFile(SpeechConfig speechConfig)
        {
            using var audioConfig = AudioConfig.FromWavFileInput("test.wav");
            using var recognizer = new SpeechRecognizer(speechConfig, audioConfig);
            
            var result = await recognizer.RecognizeOnceAsync();
            Console.WriteLine("El resultado es : " + result.Text);
        }
    }
}
