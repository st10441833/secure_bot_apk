using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;

namespace secure_bot_apk
{
    internal class Program
    {
        static string userName = "";
        static string userInterest = "";

        // Cybersecurity keywords and random responses
        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>
        {
            { "password", new List<string> {
                "Use a combination of upper and lowercase letters, numbers, and symbols.",
                "Never reuse passwords across accounts.",
                "Consider using a password manager to store your credentials securely."
            }},
            { "phishing", new List<string> {
                "Be wary of emails that urge immediate action or request sensitive data.",
                "Hover over links to preview URLs before clicking.",
                "Don’t download attachments from unknown senders."
            }},
            { "privacy", new List<string> {
                "Check your social media privacy settings regularly.",
                "Avoid oversharing your location or personal activities.",
                "Always review app permissions before granting access."
            }},
            { "scam", new List<string> {
                "If it sounds too good to be true, it probably is.",
                "Scammers often impersonate trusted companies. Always double-check!",
                "Never give out sensitive information over unsolicited calls or emails."
            }},
        };

        // Sentiment keywords and responses
        static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>
        {
            { "worried", "It's okay to feel that way. Cybersecurity can be intimidating, but I'm here to help." },
            { "curious", "Curiosity is a great start! Let's explore your question together." },
            { "frustrated", "Don't worry, I’ll do my best to guide you. Let’s work through it step-by-step." }
        };

        static List<string> unknownResponses = new List<string>
        {
            "Hmm, I’m not sure I understood that. Can you try saying it differently?",
            "That's interesting! Could you give me more detail?",
            "I didn’t quite catch that. Want to rephrase it?"
        };

        static void Main(string[] args)
        {
            DisplayLogo();

            if (!PlayGreeting())
            {
                Console.WriteLine("[Audio greeting missing. Proceeding with text greeting.]");
            }

            userName = GetUserName();
            DisplayWelcomeMessage(userName);
            StartResponseSystem();
        }

        static void DisplayLogo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', 50));
            Console.WriteLine(@"
      ████████████████████████████
      █░░░░░░░░░░░░░░░░░░░░░░░░░█
      █░░██████░░░██████░░██████░█
      █░░█░░░░█░░█░░░░░█░█░░░░░█░█
      █░░██████░░██████░░██████░█
      █░░█░░░░█░░█░░░░░█░█░░░░░█░█
      █░░██████░░██████░░█░░░░░█░█
      █░░░░░░░░░░░░░░░░░░░░░░░░░█
      ████████████████████████████
              🔒 SECURE BOT 🔒
    ");
            Console.WriteLine(new string('=', 50));
            Console.ResetColor();
        }

        static bool PlayGreeting()
        {
            try
            {
                string filePath = @"C:\Users\mudau\source\repos\secure_bot_apk\secure_bot_apk\bin\Debug\greetings.wav.wav";

                if (!File.Exists(filePath))
                {
                    return false;
                }

                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        static string GetUserName()
        {
            string input;
            do
            {
                Console.Write("\nPlease enter your name: ");
                input = Console.ReadLine()?.Trim();
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        static void DisplayWelcomeMessage(string name)
        {
            Console.WriteLine(new string('-', 50));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{GetTimeBasedGreeting()}, {name}! Welcome to Secure Bot.");
            Console.WriteLine("I’m here to help you stay protected in the digital world.");
            Console.ResetColor();
            Console.WriteLine(new string('-', 50));
        }

        static string GetTimeBasedGreeting()
        {
            int hour = DateTime.Now.Hour;
            if (hour < 12) return "Good morning";
            else if (hour < 18) return "Good afternoon";
            else return "Good evening";
        }

        static void StartResponseSystem()
        {
            while (true)
            {
                Console.Write("\nYou: ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(input))
                {
                    TypingEffect("Bot: Please say something so I can help.");
                    continue;
                }

                if (input == "exit")
                {
                    TypingEffect($"Bot: Goodbye, {userName}. Stay cyber-safe!");
                    break;
                }

                RespondToUser(input);
            }
        }

        static void RespondToUser(string input)
        {
            // Sentiment detection
            foreach (var sentiment in sentimentResponses)
            {
                if (input.Contains(sentiment.Key))
                {
                    TypingEffect("Bot: " + sentiment.Value);
                    return;
                }
            }

            // Memory capture
            if (input.Contains("interested in"))
            {
                int start = input.IndexOf("interested in") + 13;
                userInterest = input.Substring(start).TrimEnd('.', '!');
                TypingEffect($"Bot: Got it! I’ll remember that you're interested in {userInterest}.");
                return;
            }

            // Random keyword response
            foreach (var entry in keywordResponses)
            {
                if (input.Contains(entry.Key))
                {
                    if (string.IsNullOrEmpty(userInterest)) userInterest = entry.Key;
                    List<string> tips = entry.Value;
                    Random rnd = new Random();
                    string randomTip = tips[rnd.Next(tips.Count)];
                    TypingEffect($"Bot: {randomTip}");
                    return;
                }
            }

            // Follow-up support using memory
            if (!string.IsNullOrEmpty(userInterest))
            {
                TypingEffect($"Bot: Since you're interested in {userInterest}, remember to keep your apps updated and use two-factor authentication.");
                return;
            }

            // Fallback unknown input
            Random fallbackRnd = new Random();
            string unknownReply = unknownResponses[fallbackRnd.Next(unknownResponses.Count)];
            TypingEffect("Bot: " + unknownReply);
        }

        static void TypingEffect(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(25);
            }
            Console.WriteLine();
        }
    }
}
