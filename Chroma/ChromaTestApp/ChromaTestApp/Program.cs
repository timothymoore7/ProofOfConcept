using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;

namespace ChromaTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Chroma.Instance.Initialize();
                var currentEffect = Chroma.Instance.Keyboard.CurrentEffectId;
                Console.WriteLine($"Current effect Id {currentEffect}");
                Chroma.Instance.Keyboard.Clear();
                SetKeyColor();
                SetKeysColor();

                RunAsync().GetAwaiter().GetResult();
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e}");
            }

            Console.ReadLine();
        }

        static async Task RunAsync()
        {
            var surfLineCondition = GetSurfLineCondition();
            SetKeyBoardColor(surfLineCondition);
        }

        public static void SetKeyColor()
        {
            var color = Color.Orange;
            var key = Key.W;
            Chroma.Instance.Keyboard.SetKey(key, color);
            var currentEffectId = Chroma.Instance.Keyboard.CurrentEffectId;
            Console.WriteLine($"Current effect Id {currentEffectId}");
        }

        public static void SetKeysColor()
        {
            var color = Color.Yellow;
            Key[] keys = {Key.A, Key.S, Key.D};

            Chroma.Instance.Keyboard.SetKeys(color, Key.Escape, keys);

            var currentEffectId = Chroma.Instance.Keyboard.CurrentEffectId;
            Console.WriteLine($"Current effect Id {currentEffectId}");
        }

        public static void SetKeyBoardColor(string condition)
        {
            uint lightBlue = 0x515355;
            Dictionary<string, Color> surfingDictionary = new Dictionary<string, Color>();
            surfingDictionary.Add("very poor", Color.FromRgb(lightBlue));
            surfingDictionary.Add("poor", Color.Blue);
            surfingDictionary.Add("fair", Color.Green);
            surfingDictionary.Add("good", Color.Orange);

            foreach (var def in surfingDictionary)
            {
                if (condition.ToLower() == def.Key)
                {
                    Chroma.Instance.Keyboard.SetAll(def.Value);
                }
            }

            var currentEffectId = Chroma.Instance.Keyboard.CurrentEffectId;
            Console.WriteLine($"Current effect Id {currentEffectId}");

            return;
        }

        public static string GetSurfLineCondition()
        {
            var oceanBeachSurfReportUrl = "https://api.surfline.com/v1/mobile/report/4253";

            return "very poor";
        }
    }
}
