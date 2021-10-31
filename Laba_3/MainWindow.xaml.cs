using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Laba_3
{
    using Controls = List<(Slider sliders, TextBox textBoxes)>; //Список кортежей Slider и TextBox
    using DictControls = Dictionary<FrameworkElement, FrameworkElement>; //Двухнаправленный словарь, связывающий два контрола
    public partial class MainWindow : Window
    {
        private HSV Hsv;
        private Color Rgb;

        private Slider[] sliders; //Массив ползунков
        private TextBox[] textBoxes; //Массив техстовых полей

        private DictControls dictControls = new DictControls(); //Хранит ( Ползунок <-> Текстовое Поле )
        private Controls controls = new Controls(); // Все взаимодействующие контролы
        public MainWindow()
        {
            InitializeComponent();
            textBoxes = GR.Children.OfType<TextBox>().ToArray();
            sliders = GR.Children.OfType<Slider>().ToArray();

            for(int i = 0; i < 6; i++) 
            {
                controls.Add((sliders[i], textBoxes[i]));
                dictControls.Add(sliders[i], textBoxes[i]);
                dictControls.Add(textBoxes[i], sliders[i]);
            }
        }

        //Переходит на следующее текстовое поле при нажатии на Enter
        private void TB_Next(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                for (var i = 0; i < textBoxes.Length - 1; i++)
                {
                    if (!int.TryParse(((TextBox)sender).Text, out _))
                    {
                        MessageBox.Show("Ошибка! Пожалуйста укажите цвет");
                        break;
                    }
                    else if (sender == textBoxes[i])
                    {
                        textBoxes[i + 1].SelectionStart = textBoxes[i + 1].Text.Length;
                        textBoxes[i + 1].Focus();
                    }
                }
            }
        }

        //Включает/Отключает событие ColorChanged от контролов, чтобы не зациклить работу приложения
        private void SwitchEvents(bool status)
        {
             foreach((var slider, var textbox) in controls)
             {
                 if (status)
                 {
                     slider.ValueChanged += ColorChanged;
                     textbox.TextChanged += ColorChanged;
                 }
                 else
                 {
                     slider.ValueChanged -= ColorChanged;
                     textbox.TextChanged -= ColorChanged;
                 }
             }
        }

        //Вызывается каждый раз, когда пользователь меняет цвет
        private void ColorChanged(object sender, object e)
        {
            if (TB_R == null || TB_G == null || TB_B == null) return;
            if (TB_H == null || TB_S == null || TB_V == null) return;

            SwitchEvents(false);
            try
            {
                if (sender is TextBox)
                {
                    TextBox textbox = (TextBox)sender;
                    ((Slider)dictControls[textbox]).Value = int.Parse(textbox.Text);
                }
                else if (sender is Slider)
                {
                    Slider slider = (Slider)sender;
                    ((TextBox)dictControls[slider]).Text = ((int)slider.Value).ToString();
                }

                if(((FrameworkElement)sender).Tag is "RGB")
                {
                    //RGB->HSV
                    Rgb = Color.FromRgb(byte.Parse(TB_R.Text), byte.Parse(TB_G.Text), byte.Parse(TB_B.Text));
                    Hsv = new HSV(Rgb);

                    TB_H.Text = ((int)Hsv.H).ToString();
                    TB_S.Text = ((int)(Hsv.S * 100)).ToString();
                    TB_V.Text = ((int)(Hsv.V * 100)).ToString();

                    Slider_H.Value = (int)Hsv.H;
                    Slider_S.Value = (int)(Hsv.S * 100);
                    Slider_V.Value = (int)(Hsv.V * 100);
                }
                else //HSV
                {
                    //HSV->RGB
                    Hsv = new HSV(float.Parse(TB_H.Text), float.Parse(TB_S.Text) / 100f, float.Parse(TB_V.Text) / 100f);
                    Rgb = Hsv.ToRgb();

                    TB_R.Text = Rgb.R.ToString();
                    TB_G.Text = Rgb.G.ToString();
                    TB_B.Text = Rgb.B.ToString();

                    Slider_R.Value = Rgb.R;
                    Slider_G.Value = Rgb.G;
                    Slider_B.Value = Rgb.B;
                }

                SolidColorBrush solidColorBrush = new SolidColorBrush(Rgb); //Заливка цветом
                B_Color.Background = solidColorBrush;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SwitchEvents(true);
        }
    }
}
