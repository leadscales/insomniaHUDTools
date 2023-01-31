using insomniaHUDTools.Models;
using static insomniaHUDTools.Parsers.Parser;

namespace insomniaHUDTools
{
    class Program
    {
        public static void ApplyColors(string baseColorFile, string modificationColorFile, string outFile)
        {
            string baseColors = System.IO.File.ReadAllText(baseColorFile);
            string modificationColors = System.IO.File.ReadAllText(modificationColorFile);

            Dictionary<string, Color> baseColorsDictionary = ParseBaseColors(baseColors);
            Dictionary<string, Color> modificationColorsDictionary = ParseModificationColors(modificationColors);
            Dictionary<string, Color> mergedColorsDictionary = MergeColors(baseColorsDictionary, modificationColorsDictionary);

            string mergedColorsAsVDF = MergeColorsAsVDF(mergedColorsDictionary);

            System.IO.File.WriteAllText(outFile, mergedColorsAsVDF, System.Text.Encoding.ASCII);
        }

        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine("ERROR: No arguments specified.");
            }
            else if (args[0] == "APPLY_COLORS")
            {
                if (args.Length != 4)
                {
                    Console.Error.WriteLine("ERROR: Not enough arguments specified.");
                }
                else
                {
                    try
                    {
                        ApplyColors(args[1], args[2], args[3]);
                    }
                    catch (FileNotFoundException exception)
                    {

                    }
                }
            }
            else
            {
                Console.Error.WriteLine("ERROR: Invalid argument(s)");

                for (int i = 0; i < args.Length; i++)
                {
                    Console.Error.WriteLine($"{i}: {args[i]}");
                }
            }
        }
    }
}