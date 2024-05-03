using System;
using System.Collections.Generic;
using System.Web;
using System.Net;

namespace googletranslate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the text to translate:");
            string inputText = Console.ReadLine();

            // Define the source language (Polish)
            string sourceLanguage = "pl";

            // Define the target languages
            Dictionary<string, string> targetLanguages = new Dictionary<string, string>
            {
                {"French", "fr"},
                {"English", "en"},
                {"Italian", "it"},
                {"German", "de"},
                {"Russian", "ru"}
            };

            // Translate the input text into each target language
            foreach (var kvp in targetLanguages)
            {
                string targetLanguage = kvp.Value;
                string translatedText = TranslateText(inputText, sourceLanguage, targetLanguage);
                Console.WriteLine($"Translated to {kvp.Key}: {translatedText}");
            }
        }

        static string TranslateText(string input, string fromLanguage, string toLanguage)
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(input)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch (Exception)
            {
                return "error";
            }
        }
    }
}
