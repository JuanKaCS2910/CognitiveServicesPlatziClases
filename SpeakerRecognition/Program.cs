using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Speaker;

namespace SpeakerRecognition
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sucriptionKey = "8c979d42db1b4f368ff888dafd686b92";
            string serviceRegion = "BrazilSouth";

            var config = SpeechConfig.FromSubscription(sucriptionKey, serviceRegion);
            var profileMapping = new Dictionary<string, string>();
            
            await VerificationEnroll(config, profileMapping);
            Console.ReadLine();
        }

        public static async Task VerificationEnroll(SpeechConfig config, Dictionary<string, string> profileMapping)
        {
            try
            {
                using (var client = new VoiceProfileClient(config))
                using (var profile = await client.CreateProfileAsync(VoiceProfileType.TextDependentVerification, "en-us"))
                {
                    using (var audioInput = AudioConfig.FromDefaultMicrophoneInput())
                    {
                        Console.WriteLine($"Enrolling profile id {profile.Id}.");
                        profileMapping.Add(profile.Id, "Juan ");

                        VoiceProfileEnrollmentResult result = null;

                        while (result is null || result.RemainingEnrollmentsCount > 0)
                        {
                            Console.WriteLine("Speak the passphrase, \"My voice is my passport, verify me,\" ");
                            result = await client.EnrollProfileAsync(profile, audioInput);
                            Console.WriteLine($"Remaining enrollments needed : {result.RemainingEnrollmentsCount}");
                            Console.WriteLine();
                        }

                        if (result.Reason == ResultReason.EnrolledVoiceProfile)
                        {
                            await SpeakerVerify(config, profile, profileMapping);
                        }
                        else if (result.Reason == ResultReason.Canceled)
                        {
                            var cancellation = VoiceProfileEnrollmentCancellationDetails.FromResult(result);
                            Console.WriteLine($"CANCELED {profile.Id} : ErrorCod={cancellation.ErrorCode} ErrorDetaild={cancellation.ErrorDetails} ");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw;
            }
            
        }

        public static async Task SpeakerVerify(SpeechConfig config, VoiceProfile profile, Dictionary<string, string> profileMapping)
        {
            var speakerRecognizer = new SpeakerRecognizer(config, AudioConfig.FromDefaultMicrophoneInput());
            var model = SpeakerVerificationModel.FromProfile(profile);
            Console.WriteLine("Speak the passphrase to verify, \"My voice is my passport, verify me,\" ");
            var result = await speakerRecognizer.RecognizeOnceAsync(model);
            Console.WriteLine($"Verified voice profile for speaker {profileMapping}");
        }

    }
}
