using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace secure_bot_apk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Display the ASCII art logo
            DisplayLogo();

            // Play the greeting audio
            if (!PlayGreeting())
            {
                Console.WriteLine("Skipping greeting due to missing or unreadable file.");
            }

            // Ask the user for their name
            Console.Write("Please enter your name: ");
            string userName = Console.ReadLine()?.Trim() ?? "User";

            // Display a personalized welcome message
            DisplayWelcomeMessage(userName);

            // Start the basic response system
            StartResponseSystem(userName);
        }

        static void DisplayLogo()
        {
            string logo = @"
  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄         ▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄       ▄▄▄▄▄▄▄▄▄▄   ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄ 
▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌     ▐░░░░░░░░░░▌ ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌
▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ▐░▌       ▐░▌▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀      ▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀█░▌ ▀▀▀▀█░█▀▀▀▀ 
▐░▌          ▐░▌          ▐░▌          ▐░▌       ▐░▌▐░▌       ▐░▌▐░▌               ▐░▌       ▐░▌▐░▌       ▐░▌     ▐░▌     
▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄▄▄ ▐░▌          ▐░▌       ▐░▌▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄▄▄      ▐░█▄▄▄▄▄▄▄█░▌▐░▌       ▐░▌     ▐░▌     
▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌          ▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌     ▐░░░░░░░░░░▌ ▐░▌       ▐░▌     ▐░▌     
 ▀▀▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░▌          ▐░▌       ▐░▌▐░█▀▀▀▀█░█▀▀ ▐░█▀▀▀▀▀▀▀▀▀      ▐░█▀▀▀▀▀▀▀█░▌▐░▌       ▐░▌     ▐░▌     
          ▐░▌▐░▌          ▐░▌          ▐░▌       ▐░▌▐░▌     ▐░▌  ▐░▌               ▐░▌       ▐░▌▐░▌       ▐░▌     ▐░▌     
 ▄▄▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄█░▌▐░▌      ▐░▌ ▐░█▄▄▄▄▄▄▄▄▄      ▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄█░▌     ▐░▌     
▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌       ▐░▌▐░░░░░░░░░░░▌     ▐░░░░░░░░░░▌ ▐░░░░░░░░░░░▌     ▐░▌     
 ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀         ▀  ▀▀▀▀▀▀▀▀▀▀▀       ▀▀▀▀▀▀▀▀▀▀   ▀▀▀▀▀▀▀▀▀▀▀       ▀      
                                                                               
            "; // Keep the ASCII logo unchanged
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(logo);
            Console.ResetColor();
        }

        static bool PlayGreeting()
        {
            try
            {
                string filePath = @"C:\Users\mudau\source\repos\secure_bot_apk\secure_bot_apk\bin\Debug\greetings.wav.wav";

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Error: Audio file not found at " + filePath);
                    return false;
                }

                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync(); // Ensures the sound plays before continuing
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing greeting: " + ex.Message);
                return false;
            }
        }

        static void DisplayWelcomeMessage(string userName)
        {
            string border = new string('=', 50);
            Console.WriteLine(border);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Welcome, {userName}, to the Cybersecurity Awareness Bot!");
            Console.WriteLine("I'm here to help you stay safe online.");
            Console.ResetColor();
            Console.WriteLine(border);
        }

        static void StartResponseSystem(string userName)
        {
            Console.WriteLine("Feel free to ask me any questions about cybersecurity.");
            while (true)
            {
                Console.Write("You: ");
                string userInput = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Bot: I didn't quite understand that. Could you rephrase?");
                    continue;
                }

                if (userInput == "exit")
                {
                    Console.WriteLine("Thank you for chatting! Stay safe online.");
                    break;
                }

                RespondToUser(userInput);
            }
        }

        static void RespondToUser(string question)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            switch (question)
            {
                case "how are you?":
                    TypingEffect("Bot: I'm just a program, but I'm here to help you!");
                    break;
                case "what's your purpose?":
                    TypingEffect("Bot: My purpose is to provide you with information on cybersecurity best practices.");
                    break;
                case "what can i ask you about?":
                    TypingEffect("Bot: You can ask me about password safety, phishing, safe browsing, and more!");
                    break;
                case "password safety":
                    TypingEffect("Bot: Always use strong, unique passwords and consider using a password manager.");
                    break;
                case "phishing":
                    TypingEffect("Bot: Be cautious of unsolicited emails or messages asking for personal information.");
                    break;
                case "safe browsing":
                    TypingEffect("Bot: Use secure websites (https) and avoid clicking on suspicious links.");
                    break;
                default:
                    TypingEffect("Bot: I didn't quite understand that. Could you rephrase?");
                    break;
            }
            Console.ResetColor();
        }

        static void TypingEffect(string message)
        {
            foreach (char letter in message)
            {
                Console.Write(letter);
                System.Threading.Thread.Sleep(50);
            }
            Console.WriteLine();
        }
    }
}
