using System;
using System.Windows.Media;

namespace Laba_3
{
   public class HSV
    {
        //Поля и свойства HSV
        private float _h;
        private float _s;
        private float _v;
        public float H { get => (float)Math.Round(_h, 2); set => _h = value; }
        public float S { get => (float)Math.Round(_s, 2); set => _s = value; }
        public float V { get => (float)Math.Round(_v, 2); set => _v = value; }

        private enum RGB
        {
            Red,
            Green,
            Blue
        }
        private enum Circle //Круг разделенный на 6 частей. Используется для вычисления RGB из HSV
        {
            First,
            Second,
            Thirst,
            Fourth,
            Fifth,
            Sixth
        }
        //Функция для вычисления тона HSV из RGB
        private float Hue(float[] rgb, float delta, RGB Cmax)
        {
            float result = 0;
            switch (Cmax)
            {
                case RGB.Red:
                    result = 60 * ((rgb[1] - rgb[2]) / delta % 6);
                    break;
                case RGB.Green:
                    result = 60 * ((rgb[2] - rgb[0]) / delta + 2);
                    break;
                case RGB.Blue:
                    result = 60 * ((rgb[0] - rgb[1]) / delta + 4);
                    break;
            }
            result %= 360;
            return result < 0 ? 360 + result : result;
        }
        //Конструктор для инициализации HSV
        public HSV(float h, float s, float v)
        {
            H = h;
            S = s;
            V = v;
        }

        //Конструктор создающий HSV из RGB
        public HSV(Color color)
        {
            float[] rgb = new float[3]
            {
                color.R / 255f,
                color.G / 255f,
                color.B / 255f
            };

            RGB cMax = rgb[0] > rgb[1] ? RGB.Red : rgb[1] > rgb[2] ? RGB.Green : RGB.Blue;
            RGB cMin = rgb[0] < rgb[1] ? RGB.Red : rgb[1] < rgb[2] ? RGB.Green : RGB.Blue;

            float delta = rgb[(int)cMax] - rgb[(int)cMin];

            V = rgb[(int)cMax];
            S = V != 0 ? delta / V : 0;
            H = delta != 0 ? Hue(rgb, delta, cMax) : 0;
        }

        //Перевод HSV в RGB
        public Color ToRgb()
        {
            float C = V * S;
            float X = C * (1 - Math.Abs(H / 60 % 2 - 1));
            float m = V - C;
            float[] _rgb = new float[3];
            Circle circle = (Circle)(H / 60);

            switch (circle)
            {
                case Circle.First:
                    _rgb[0] = C;
                    _rgb[1] = X;
                    _rgb[2] = 0;
                    break;
                case Circle.Second:
                    _rgb[0] = X;
                    _rgb[1] = C;
                    _rgb[2] = 0;
                    break;
                case Circle.Thirst:
                    _rgb[0] = 0;
                    _rgb[1] = C;
                    _rgb[2] = X;
                    break;
                case Circle.Fourth:
                    _rgb[0] = 0;
                    _rgb[1] = X;
                    _rgb[2] = C;
                    break;
                case Circle.Fifth:
                    _rgb[0] = X;
                    _rgb[1] = 0;
                    _rgb[2] = C;
                    break;
                case Circle.Sixth:
                    _rgb[0] = C;
                    _rgb[1] = 0;
                    _rgb[2] = X;
                    break;
            }

            byte[] rgb = new byte[3]
            {
               (byte)((_rgb[0]+m)*255),
               (byte)((_rgb[1]+m)*255),
               (byte)((_rgb[2]+m)*255)
            };

            rgb[0] += (byte)((rgb[0] != 255 && rgb[0] != 0) ? 1 : 0);
            rgb[1] += (byte)((rgb[1] != 255 && rgb[1] != 0) ? 1 : 0);
            rgb[2] += (byte)((rgb[2] != 255 && rgb[2] != 0) ? 1 : 0);

            return Color.FromRgb(rgb[0], rgb[1], rgb[2]);
        }
    }
}
