using System;

namespace MonoGameWindowsDesktopApplication1.Utils
{
    public static class MyMath
    {
        public static double Sigmoid(double a)
        {
            var res = 1.0 / (1.0 + Math.Pow(Math.E, -a));
            return res;
        }
    }
}