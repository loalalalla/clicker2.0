using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.IO;

namespace WPF_Cookie_Clicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double CookiesPerClick = 1;
        public double CookiesPerSecond;
        public double TotalCookies;

        public double ClickPrice = 15;
        public double GrandmaPrice = 100;
        public double FactoryPrice = 1000;

        public double[] CookiesPs = { 0.1, 1, 10};
        public double[] MachinesBought = { 0, 0, 0,};
        public MainWindow()
        {
            InitializeComponent();
            Thread thread = new Thread(Cookies);
            thread.SetApartmentState(ApartmentState.STA);
            SaveLoad();
            thread.Start();
        }

        private void SaveLoad()
        {
            if (File.Exists("../Save.txt"))
            {
               TotalCookies = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(0));
               MachinesBought[0] = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(1));
               MachinesBought[1] = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(2));
               MachinesBought[2] = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(3));
               ClickPrice = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(4));
               GrandmaPrice = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(5));
               FactoryPrice = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(6));
               txtPaws.Text = Convert.ToString(Convert.ToInt32(ClickPrice));
               txtSlumber.Text = Convert.ToString(Convert.ToInt32(GrandmaPrice));
               txtSweetestHome.Text = Convert.ToString(Convert.ToInt32(FactoryPrice));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string[] Data = { txtTotalCookies.Text, MachinesBought[0].ToString(), MachinesBought[1].ToString(), MachinesBought[2].ToString(), txtPaws.Text, txtSlumber.Text, txtSweetestHome.Text };
            File.WriteAllLines("../Save.txt", Data);
        }

        private void Cookies()
        {
            while (true)
            {

                Thread.Sleep(100);


                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    CookiesPerSecond = CookiesPs[0] * MachinesBought[0] + CookiesPs[1] * MachinesBought[1] + CookiesPs[2] * MachinesBought[2];
                    TotalCookies += CookiesPerSecond/10;
                    txtPawsPs.Text = Convert.ToString(CookiesPerSecond);
                    txtTotalCookies.Text = Convert.ToString(Convert.ToInt32(TotalCookies));

                    if (TotalCookies >= ClickPrice)
                    {
                        btnPaws.IsEnabled = true;
                    } else
                    {
                        btnPaws.IsEnabled = false;
                    }

                    if (TotalCookies >= GrandmaPrice)
                    {
                        btnSlumber.IsEnabled = true;
                    }
                    else
                    {
                        btnSlumber.IsEnabled = false;
                    }
                    if (TotalCookies >= FactoryPrice)
                    {
                        btnSweetestHome.IsEnabled = true;
                    }
                    else
                    {
                        btnSweetestHome.IsEnabled = false;
                    }
                }));
            }
        }

        private void Cookie_Click(object sender, RoutedEventArgs e)
        {
            TotalCookies = TotalCookies + CookiesPerClick;
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            if (TotalCookies >= ClickPrice)
            {
                MachinesBought[0] = MachinesBought[0] + 1;
                TotalCookies = TotalCookies - ClickPrice;
                double Price = ClickPrice * 20 / 100;
                ClickPrice = ClickPrice + Price;
                txtPaws.Text = Convert.ToString(Convert.ToInt32(ClickPrice));
            }
        }

        private void btnGrandma_Click(object sender, RoutedEventArgs e)
        {
            if (TotalCookies >= GrandmaPrice)
            {
                MachinesBought[1] = MachinesBought[1] + 1;
                TotalCookies = TotalCookies - GrandmaPrice;
                double Price = GrandmaPrice * 15 / 100;
                GrandmaPrice = GrandmaPrice + Price;
                txtSlumber.Text = Convert.ToString(Convert.ToInt32(GrandmaPrice));
            }
        }

        private void btnFactory_Click(object sender, RoutedEventArgs e)
        {
            if (TotalCookies >= FactoryPrice)
            {
                MachinesBought[2] = MachinesBought[2] + 1;
                TotalCookies = TotalCookies - FactoryPrice;
                double Price = FactoryPrice * 15 / 100;
                FactoryPrice = FactoryPrice + Price;
                txtSweetestHome.Text = Convert.ToString(Convert.ToInt32(FactoryPrice));
            }
        }

        private void btnUpgrade1_Click(object sender, RoutedEventArgs e)
        {
            if (TotalCookies >= 100)
            {
                TotalCookies = TotalCookies - 100;
                CookiesPs[0] = CookiesPs[0] * 2;
                CookiesPerClick = CookiesPerClick * 2;
                ((Button)sender).Visibility = Visibility.Hidden;
            }
        }
    }
}
