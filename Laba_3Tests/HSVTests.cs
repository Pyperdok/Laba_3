using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Media;

namespace Laba_3.Tests
{
    [TestClass()]
    public class HSVTests
    {
        byte[,] RGBColors = new byte[16, 3]
        {
                { 0xFF, 0xFF, 0xFF }, //white
                { 0xC0, 0xC0, 0xC0 }, //silver
                { 0x80, 0x80, 0x80 }, //gray
                { 0x0, 0x0, 0x0 }, //black
                { 0xFF, 0x0, 0x0 }, //red
                { 0x80, 0x0, 0x0 }, //marron
                { 0xFF, 0xFF, 0x0 }, //yellow
                { 0x80, 0x80, 0x0 }, //olive
                { 0x0, 0xFF, 0x0 }, //lime
                { 0x00, 0x80, 0x0 }, //green 
                { 0x00, 0xFF, 0xFF }, //aqua
                { 0x00, 0x80, 0x80 }, //teal
                { 0x00, 0x00, 0xFF }, //blue
                { 0x00, 0x00, 0x80 }, //navy
                { 0xFF, 0x00, 0xFF }, //fuchsia
                { 0x80, 0x00, 0x80 } //purple
        };

        float[,] HSVColors = new float[16, 3]
        {
                { 0f, 0f, 1f }, //white
                { 0f, 0f, 0.75f }, //silver
                { 0f, 0f, 0.5f }, //gray
                { 0f, 0f, 0f }, //black
                { 0f, 1f, 1f }, //red
                { 0f, 1f, 0.5f }, //marron
                { 60f, 1f, 1f }, //yellow
                { 60f, 1f, 0.5f }, //olive
                { 120f, 1f, 1f }, //lime
                { 120f, 1f, 0.5f }, //green
                { 180f, 1f, 1f }, //aqua
                { 180f, 1f, 0.5f }, //teal
                { 240f, 1f, 1f }, //blue
                { 240f, 1f, 0.5f }, //navy
                { 300f, 1f, 1f }, //fuchsia
                { 300f, 1f, 0.5f } //purple
        };
        [TestMethod()]
        public void RgbToHsv() //Перевод RGB в HSV
        {
            for(int i = 0; i < 16; i++)
            {
                HSV hsv = new HSV(Color.FromRgb(RGBColors[i,0], RGBColors[i,1], RGBColors[i,2]));

                if (HSVColors[i,0] == hsv.H && HSVColors[i, 1] == hsv.S && HSVColors[i, 2] == hsv.V)
                {
                    Console.WriteLine($"Color i = {i}" +
                        $" RGB {RGBColors[i, 0]} {RGBColors[i, 1]} {RGBColors[i, 2]} -> HSV {hsv.H} {hsv.S} {hsv.V}");
                }
                else
                {
                    Console.WriteLine($"Color i ={i} is invalid" +
                        $" RGB {RGBColors[i, 0]} {RGBColors[i, 1]} {RGBColors[i, 2]} -> HSV {hsv.H} {hsv.S} {hsv.V}");
                    Console.WriteLine($"Expected  {HSVColors[i, 0]} {HSVColors[i, 1]} {HSVColors[i, 2]}");
                    Assert.Fail();
                }
            }
        }
        [TestMethod()]
        public void HsvToRgb() //Перевод HSV в RGB
        {
            for (int i = 0; i < 16; i++)
            {
                Color rgb = new HSV(HSVColors[i, 0], HSVColors[i, 1], HSVColors[i, 2]).ToRgb();

                if (RGBColors[i, 0] == rgb.R && RGBColors[i, 1] == rgb.G && RGBColors[i, 2] == rgb.B)
                {
                    Console.WriteLine($"Color i = {i}" +
                        $" HSV {HSVColors[i, 0]} {HSVColors[i, 1]} {HSVColors[i, 2]} -> RGB {rgb.R} {rgb.G} {rgb.B}");
                }
                else
                {
                    Console.WriteLine($"Color i ={i} is invalid {rgb.R} {rgb.G} {rgb.B}");
                    Console.WriteLine($"Expected  {RGBColors[i, 0]} {RGBColors[i, 1]} {RGBColors[i, 2]}");
                    Assert.Fail();
                }
            }
        }
    }
}