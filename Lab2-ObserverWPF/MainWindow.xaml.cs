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
using System.Windows.Threading;
using Lab2CL.Conracts;
using Lab2CL.Observers;
using Lab2CL.Subjects;

namespace Lab2_ObserverWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainStock _stock;
        
        private readonly DispatcherTimer _simulationTimer;
        
        private readonly Random _random = new Random();

        private bool _isSimulatiing = false;
        
        public MainWindow()
        {
            InitializeComponent();
            
            _stock = new MainStock();
            
            _stock.AddObserver(new SteelProvider(123, 600));
            _stock.AddObserver(new CementProvider(215, 1260));

            
            _simulationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            _simulationTimer.Tick += SimulationTimer_Tick;

            AddProviderButton.Click += AddProviderButton_Click;
            RemoveProviderButton.Click += RemoveProviderButton_Click;
            // SimulationSpeedSlider.ValueChanged += SimulationSpeedSlider_ValueChanged;
            SimulationButton.Click += SimulationButton_Click;
            
            UpdateStockUI();
        }

        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isSimulatiing)
            {
                _simulationTimer.Stop();
                _isSimulatiing = false;
            }
            else
            {
                _simulationTimer.Start();
                _isSimulatiing = true;
            }
        }
        
        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                int material = _random.Next(0, 2); 
                float amount = (float)_random.Next(-100, 100);

                
                _stock.Provide(material, amount);
                
                UpdateStockUI();
                UpdateBrokersListUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void UpdateStockUI()
        {
            SteelAmountTextBlock.Text = _stock.AmountKg.ToString("0.00");
            CementAmountTextBlock.Text = _stock.AmountM.ToString("0.00");
        }
        
        private void UpdateBrokersListUI()
        {
            ProvidersListBox.Items.Clear(); 
            int index = 0;
            
            foreach (var provider in _stock.GetProviders())
            {
                string providerType = provider is SteelProvider ? "Сталь" : "Цемент";
                ProvidersListBox.Items.Add($"Поставщик {index} ({providerType})");
                
                var dataLines = provider.GetData();
                foreach (var line in dataLines)
                {
                    ProvidersListBox.Items.Add("  " + line);
                }
                index++;
            }
        }
        

        private void AddProviderButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedCompany = (ProviderTypeComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem).Content.ToString();

            if (!float.TryParse(ThresholdTextBox.Text, out float threshold))
            {
                MessageBox.Show("Неверно введен порог!");
                return;
            }

            if (!float.TryParse(MinAmountTextBox.Text, out float amount))
            {
                MessageBox.Show("Неверно введено минимальное значение!");
                return;
            }

            IProvider newProvider = null;
            if (selectedCompany == "Сталь")
            {
                newProvider = new SteelProvider(threshold, amount);
            }
            else if (selectedCompany == "Цемент")
            {
                newProvider = new CementProvider(threshold, amount);
            }
 
            _stock.AddObserver(newProvider);

            UpdateBrokersListUI(); 

        }

        private void RemoveProviderButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ProviderIdTextBox.Text, out int index))
            {
                try
                {
                    _stock.RemoveObserver(index);
                    UpdateBrokersListUI();
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Брокера с таким номером не существует!");
                }
            }
            else
            {
                MessageBox.Show("Введите корректный номер (индекс) поставщика!");
            }
        }
        
        private void SimulationSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue == 0)
            {
                _simulationTimer.Stop();
                return;
            }
                
            else
            {
                _simulationTimer.Start();
            }
            double newInterval = 2000 / (e.NewValue == 0 ? 1 : e.NewValue); 
            
            if (_simulationTimer != null)
            {
                _simulationTimer.Interval = TimeSpan.FromMilliseconds(newInterval);
            }
        }
    }
}