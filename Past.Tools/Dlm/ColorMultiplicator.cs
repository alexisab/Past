namespace Past.Tools.Dlm
{
    public class ColorMultiplicator
    {
        public int Red { get; private set; }
        public int Green { get; private set; }
        public int Blue { get; private set; }
        public bool IsOne { get; private set; }

        public ColorMultiplicator(int redComponent, int greenComponent, int blueComponent, bool forceCalculation = false)
        {
            Red = redComponent;
            Green = greenComponent;
            Blue = blueComponent;
            if (!forceCalculation && redComponent + greenComponent + blueComponent == 0)
            {
                IsOne = true;
            }
        }

        public static int Clamp(int value, int min, int max)
        {
             if (value > max)
             {
                return max;
             }
             return value < min ? min : value;
        }

        public ColorMultiplicator Multiply(ColorMultiplicator cm)
        {
            if (IsOne)
                return cm;
            if (cm.IsOne)
                return this;
            var cmr = new ColorMultiplicator(0, 0, 0)
            {
                Red = Red + cm.Red,
                Green = Green + cm.Green,
                Blue = Blue + cm.Blue
            };
            cmr.Red = Clamp(cmr.Red, -128, 127);
            cmr.Green = Clamp(cmr.Green, -128, 127);
            cmr.Blue = Clamp(cmr.Blue, -128, 127);
            cmr.IsOne = false;
            return cmr;
        }

        public override string ToString() => $"[r: {Red}, g: {Green}, b: {Blue }";
    }
}
