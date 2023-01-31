namespace insomniaHUDTools.Models
{
    public class Color
    {
        private static byte multiplier = 255;

        public static Color HSVToRGB(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * multiplier;
            double v = (value);
            double p = (value * (1 - saturation));
            double q = (value * (1 - f * saturation));
            double t = (value * (1 - (1 - f) * saturation));

            if (hi == 0)
            {
                return new Color(v / multiplier, t / multiplier, p / multiplier, 1.0d);
            }
            else if (hi == 1)
            {
                return new Color(q / multiplier, v / multiplier, p / multiplier, 1.0d);
            }
            else if (hi == 2)
            {
                return new Color(p / multiplier, v / multiplier, t / multiplier, 1.0d);
            }
            else if (hi == 3)
            {
                return new Color(p / multiplier, q / multiplier, v / multiplier, 1.0d);
            }
            else if (hi == 4)
            {
                return new Color(t / multiplier, p / multiplier, v / multiplier, 1.0d);
            }
            else
            {
                return new Color(v / multiplier, p / multiplier, q / multiplier, 1.0d);
            }
        }

        public Color(double red, double green, double blue, double alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public Color(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = 1.0;
        }

        public Color(string resString)
        {
            string[] resStringSplit = resString.Split(" ");
            int[] resStringInt = Array.ConvertAll(resStringSplit, int.Parse);
            double[] resStringDouble = new double[resStringInt.Length];

            for (int i = 0; i < resStringInt.Length; i++)
            {
                if (Convert.ToDouble(resStringInt[i]) == 255.0d)
                {
                    resStringDouble[i] = Convert.ToDouble(resStringInt[i]) / (multiplier);
                }
                else
                {
                    resStringDouble[i] = Convert.ToDouble(resStringInt[i]) / (multiplier + 1);
                }
            }

            Red = resStringDouble[0];
            Green = resStringDouble[1];
            Blue = resStringDouble[2];
            Alpha = resStringDouble[3];
        }

        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public double Alpha { get; set; }

        public override string ToString() => $"{Red} {Green} {Blue} {Alpha}";

        public string AsResString()
        {
            int[] resStringInt = new int[4];

            if (Red == 1.0d)
            {
                resStringInt[0] = Convert.ToInt32(Red * multiplier);
            }
            else
            {
                resStringInt[0] = Convert.ToInt32(Red * (multiplier + 1));
            }

            if (Green == 1.0d)
            {
                resStringInt[1] = Convert.ToInt32(Green * multiplier);
            }
            else
            {
                resStringInt[1] = Convert.ToInt32(Green * (multiplier + 1));
            }

            if (Blue == 1.0d)
            {
                resStringInt[2] = Convert.ToInt32(Blue * multiplier);
            }
            else
            {
                resStringInt[2] = Convert.ToInt32(Blue * (multiplier + 1));
            }

            if (Alpha == 1.0d)
            {
                resStringInt[3] = Convert.ToInt32(Alpha * multiplier);
            }
            else
            {
                resStringInt[3] = Convert.ToInt32(Alpha * (multiplier + 1));
            }

            return $"{resStringInt[0]} {resStringInt[1]} {resStringInt[2]} {resStringInt[3]}";
        }
    }
}