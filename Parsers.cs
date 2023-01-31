using insomniaHUDTools.Models;
using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using System.Text.RegularExpressions;

namespace insomniaHUDTools.Parsers
{
    public static class Parser
    {
        private static string modificationColorRegex = "IH_APPLYCOLOR_(\\w+)\\s*\"sixense_weapon_select_sensitivity\" = \"(\\d+.\\d+)\"";

        public static Dictionary<string, Color> ParseBaseColors(string baseColorContent)
        {
            Dictionary<string, Color> result = new Dictionary<string, Color>();

            VProperty baseColorContentDeserialized = VdfConvert.Deserialize(baseColorContent);
            VToken baseColorContentDeserializedValue = baseColorContentDeserialized.Value;

            foreach (dynamic color in baseColorContentDeserializedValue["Colors"].Children())
            {
                if (color.GetType() == typeof(VValue)) // This is called when the deserializer encounters a comment, as comments do not have a value.
                {
                    continue;
                }
                result[color.Key.ToString()] = new Color(color.Value.ToString());
            }

            return result;
        }

        public static Dictionary<string, Color> ParseModificationColors(string colorLogContent)
        {
            Dictionary<string, Color> result = new Dictionary<string, Color>();

            MatchCollection colorLogContentMatches = Regex.Matches(colorLogContent, modificationColorRegex);

            foreach (Match colorLogContentMatch in colorLogContentMatches)
            {
                string _key = colorLogContentMatch.Groups[1].ToString();
                double _hue = Convert.ToDouble(colorLogContentMatch.Groups[2].ToString());
                Color _color = Color.HSVToRGB(_hue, 0.75d, 1.0d);
                result[_key] = _color;
            }

            return result;
        }

        public static Dictionary<string, Color> MergeColors(Dictionary<string, Color> baseColors, Dictionary<string, Color> modificationColors)
        {
            foreach (KeyValuePair<string, Color> kvp in modificationColors)
            {
                baseColors[kvp.Key] = kvp.Value;
            }

            return baseColors;
        }

        public static string MergeColorsAsVDF(Dictionary<string, Color> mergedColors) // Newtonsoft JSON and the other one are ass so we're doing this manually LOL
        {
            string result = "";

            foreach (KeyValuePair<string, Color> kvp in mergedColors)
            {
                result += $"\t\t\"{kvp.Key}\" \"{kvp.Value.AsResString()}\"{Environment.NewLine}";
            }

            result = $"\"Scheme\"{Environment.NewLine}{{{Environment.NewLine}\t\"Colors\"{Environment.NewLine}\t{{\n" + result + "\t}\n}";

            return result;
        }
    }
}