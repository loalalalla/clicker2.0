using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace _4040;
public partial class MainWindow
{
    public double PawsPerClick = 1;
    public double PawsPerSecond;
    public double TotalPaws;

    public double PawsPrice = 15;
    public double SlumberPrice = 100;
    public double SweetestHomePrice = 1000;

    public double[] PawsPs = { 0.1, 1, 10};
    public double[] MachinesBought = { 0, 0, 0,};
    public MainWindow()
    {
        InitializeComponent();
        Thread thread = new Thread(Paws);
        thread.SetApartmentState(ApartmentState.STA);
        SaveLoad();
        thread.Start();
    }

    private void SaveLoad()
    {
        if (File.Exists("../Save.txt"))
        {
            TotalPaws = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(0));
            MachinesBought[0] = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(1));
            MachinesBought[1] = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(2));
            MachinesBought[2] = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(3));
            ClickPrice = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(4));
            SlumberPrice = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(5));
            SweetestHomePrice = double.Parse(File.ReadLines("../Save.txt").ElementAtOrDefault(6));
            txtPaws.Text = Convert.ToString(Convert.ToInt32(ClickPrice));
            txtSlumber.Text = Convert.ToString(Convert.ToInt32(SlumberPrice));
            txtSweetestHome.Text = Convert.ToString(Convert.ToInt32(SweetestHomePrice));
        }
    }

    public double ClickPrice { get; set; }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        string[] Data = { txtPaws.Text, MachinesBought[0].ToString(), MachinesBought[1].ToString(), MachinesBought[2].ToString(), txtPaws.Text, txtSlumber.Text, txtSweetestHome.Text };
        File.WriteAllLines("../Save.txt", Data);
    }

    private void Paws()
    {
        while (true)
        {

            Thread.Sleep(100);


            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                PawsPerSecond = PawsPs[0] * MachinesBought[0] + PawsPs[1] * MachinesBought[1] + PawsPs[2] * MachinesBought[2];
                TotalPaws += PawsPerSecond/10;
                txtPawsPs.Text = Convert.ToString(PawsPerSecond);
                txtTotalCookies.Text = Convert.ToString(Convert.ToInt32(TotalPaws));

                if (TotalPaws >= ClickPrice)
                {
                    btnPaws.IsEnabled = true;
                } else
                {
                    btnPaws.IsEnabled = false;
                }

                if (TotalPaws >= SlumberPrice)
                {
                    btnSlumber.IsEnabled = true;
                }
                else
                {
                    btnSlumber.IsEnabled = false;
                }
                if (TotalPaws >= SweetestHomePrice)
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
        TotalPaws = TotalPaws + PawsPerClick;
    }

    private void btnClick_Click(object sender, RoutedEventArgs e)
    {
        if (TotalPaws >= ClickPrice)
        {
            MachinesBought[0] = MachinesBought[0] + 1;
            TotalPaws = TotalPaws - ClickPrice;
            double Price = ClickPrice * 20 / 100;
            ClickPrice = ClickPrice + Price;
            txtPaws.Text = Convert.ToString(Convert.ToInt32(ClickPrice));
        }
    }

    private void btnGrandma_Click(object sender, RoutedEventArgs e)
    {
        if (TotalPaws >= SlumberPrice)
        {
            MachinesBought[1] = MachinesBought[1] + 1;
            TotalPaws = TotalPaws - SlumberPrice;
            double Price = SlumberPrice * 15 / 100;
            SlumberPrice = SlumberPrice + Price;
            txtSlumber.Text = Convert.ToString(Convert.ToInt32(SlumberPrice));
        }
    }

    private void btnFactory_Click(object sender, RoutedEventArgs e)
    {
        if (TotalPaws >= SlumberPrice)
        {
            MachinesBought[2] = MachinesBought[2] + 1;
            TotalPaws = TotalPaws - SweetestHomePrice;
            double Price = SweetestHomePrice * 15 / 100;
            SweetestHomePrice = SweetestHomePrice + Price;
            txtSweetestHome.Text = Convert.ToString(Convert.ToInt32(SweetestHomePrice));
        }
    }

    private void btnUpgrade1_Click(object sender, RoutedEventArgs e)
    {
        if (TotalPaws >= 100)
        {
            TotalPaws = TotalPaws - 100;
            PawsPs[0] = PawsPs[0] * 2;
            PawsPerSecond = PawsPerClick * 2;
            ((Button)sender).Visibility = Visibility.Hidden;
        }
    }
}
