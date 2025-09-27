using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDK.Shop
{
    public static class PlaceholderShopIntroDialogue
    {
        private static List<string> PlaceholderShopIntro = new List<string>()
        {
            "Hello there! Welcome in, feel free to look around.",
            "Good morning! Let me know if I can help you find anything.",
            "Well, hello! It's great to see a new face in the shop today.",
            "Welcome! We've just had a new shipment come in, so there's lots to see.",
            "Hi, welcome! If you have any questions, I'm right here.",
            "Come on in out of the cold/heat! Take your time browsing.",
            "Greetings! Is this your first time visiting our little shop?",
            "Hey there! Everything on this back wall is 20% off today.",
            "Good afternoon! I'm just finishing up with this customer, but I'll be right with you.",
            "Hello and welcome! I hope you're having a wonderful day so far.",
            "Hi there! Make yourself at home. The story behind each piece is on the tag.",
            "Welcome, welcome! Don't hesitate to ask if you'd like to try anything on."
        };

        public static string GetPlaceholderShopIntro()
        {
            var randomIdx = UnityEngine.Random.Range(0, PlaceholderShopIntro.Count - 1);
            return PlaceholderShopIntro[randomIdx];
        }
    }
}
