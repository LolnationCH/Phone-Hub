using PhoneConnectionMaster.Commands;
using PhoneConnectionMaster.Objects;
using SharpAdbClient;
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

namespace PhoneConnectionMaster
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    CommandsScreen CommandsScreen = new CommandsScreen();

    public MainWindow()
    {
        InitializeComponent();
      CommandADB.MonitorDevicesConnection(this.UpdateComboBox);
    }

    private void ConnectToScreen_Click(object sender, RoutedEventArgs e)
    {
      var device = GetDeviceInfo((Button)sender);
      if (device != null)
        CommandsScreen.ConnectToScreen(device);
    }

    private string GetDeviceInfo(Button button)
    {
      if (button.Name == "USB_scrpy")
        return this.UsbControl.GetCurrentDeviceInfo().Serial;
      else
        return CommandsTcp.Instance.GetDeviceSerialOrIp(this.TcpControl.GetCurrentDeviceInfo());
    }

    private void UpdateComboBox(object sender, DeviceDataEventArgs e)
    {
       this.Dispatcher.Invoke(() =>
       {
       this.UsbControl.InitializeDeviceList();
       this.TcpControl.InitializeDeviceList();
       });
    }
  }
}
