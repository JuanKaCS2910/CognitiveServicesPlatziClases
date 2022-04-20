using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;

namespace Translation
{
    class Program
    {
        static readonly string speech_sucriptionKey = "";
        static readonly string speech_serviceRegion = "BrazilSouth";
        async static Task Main()
        {
            Console.WriteLine("Hello World!");
            await TranslateSpeechAsync();
            Console.ReadLine();
        }

        async static Task TranslateSpeechAsync()
        {
            var translationConfig = SpeechTranslationConfig.FromSubscription(speech_sucriptionKey, speech_serviceRegion);
            var fromLanguage = "es-MX";
            var toLanguage = new List<string> { "en", "fr", "de","pt" };
            translationConfig.SpeechRecognitionLanguage = fromLanguage;
            toLanguage.ForEach(translationConfig.AddTargetLanguage);

            using var recognizer = new TranslationRecognizer(translationConfig);
            Console.WriteLine("Di algo en el idioma : "+ fromLanguage);
            Console.WriteLine($"We'll translate into' {string.Join("', '",toLanguage)}'.\n");

            var result = await recognizer.RecognizeOnceAsync();

            if (result.Reason == ResultReason.TranslatedSpeech)
            {
                Console.WriteLine($"Recognized: \"{result.Text}\":");
                foreach (var (language,traslation) in result.Translations)
                {
                    Console.WriteLine($"Translated into '{language}':{traslation}");
                }
            }

        }
        //8c979d42db1b4f368ff888dafd686b92
    }
}
