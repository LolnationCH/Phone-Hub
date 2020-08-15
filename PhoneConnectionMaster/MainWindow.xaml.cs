using PhoneConnectionMaster.Commands;
using SharpAdbClient;
using System.Windows;

namespace PhoneConnectionMaster
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      CommandADB.MonitorDevicesConnection(this.UpdateComboBox);
      UpdateComboBox(null, null);
    }

    private void UpdateComboBox(object sender, DeviceDataEventArgs e)
    {
      this.Dispatcher.Invoke(() =>
      {
        UsbControl.InitializeDeviceList();
        TcpControl.InitializeDeviceList();
        ConnectControl.InitializeDeviceList();
      });
    }
  }
}
