 Part 1 – Basic Chatbot Interaction
 Voice Greeting: Plays a WAV audio file welcoming the user when the app starts.

ASCII Logo: Displays an ASCII art banner styled as a cybersecurity bot.
User Input: Asks for the user's name and uses it in personalized responses.
Basic Questions Answered:

"How are you?"

"What’s your purpose?"

"What can I ask you about?"
 Formatted Console UI: Includes colored text, spacing, borders, and a typing effect for more realistic interactions.

Input Validation: Responds to blank or unsupported input with a helpful default message.

 Part 2 – Dynamic Responses, Sentiment & Memory
Keyword Recognition:

Detects keywords like password, phishing, privacy, and scam.

Provides tips or safety advice based on the keyword.

 Random Responses:

For topics like phishing, the bot gives a different tip each time to keep the chat interesting.

 Sentiment Detection:

Detects if the user is feeling "worried", "frustrated", or "curious" and replies with an empathetic message.
 Memory and Recall:

Remembers user interests (e.g., “I’m interested in privacy”) and refers to it later to offer relevant tips.

 Fallback Handling:

If the bot doesn’t understand the input, it replies with a random polite fallback response.

 Code Concepts Used
Dictionaries: For fast keyword-response mapping.

Lists: To store and randomly pick from multiple responses.

String Manipulation: To analyze user input and remember preferences.

Typing Effect: Simulates real-time chatbot typing using Thread.Sleep.

File Handling: Loads an audio file using System.Media.SoundPlayer.

 How to Run
Make sure you have .NET and Visual Studio installed.

Save your WAV file as greetings.wav.wav inside the bin\Debug folder.

Run the program from Visual Studio or use dotnet run
