using UnityEngine;
/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Utils
{
    public static class Jokes
    {
        // source: https://www.rd.com/list/short-jokes/
        public static readonly string[] allJokes = new string[]
        {
            "What did the left eye say to the right eye? Between you and me, something smells.",
            "A man tells his doctor, 'Doc, help me. I’m addicted to Twitter!' The doctor replies, 'Sorry, I don’t follow you …'",
            "What do you call a parade of rabbits hopping backwards? A receding hare-line.",
            "How does Moses make tea? He brews.",
            "What sits at the bottom of the sea and twitches? A nervous wreck.",
            "How do you drown a hipster? Throw him in the mainstream.",
            "Why did the chicken go to the séance? To get to the other side.",
            "Why don’t scientists trust atoms? Because they make up everything.",
            "Did you hear about the claustrophobic astronaut? He just needed a little space.",
            "A bear walks into a bar and says, 'Give me a whiskey and … cola' 'Why the big pause?' asks the bartender. The bear shrugged. 'I’m not sure; I was born with them.'",
            "Hear about the new restaurant called Karma? There’s no menu: You get what you deserve.",
            "What’s the best thing about Switzerland? I don’t know, but the flag is a big plus.",
            "What do you call a fake noodle? An impasta."
        };

        public static string GetRandomJoke()
        {
            var randomIndex = Random.Range(0, allJokes.Length);
            return allJokes[randomIndex];
        }
    }
}