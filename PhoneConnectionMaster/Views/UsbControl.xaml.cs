using PhoneConnectionMaster.Objects;
using System.Windows;
using System.Windows.Controls;
namespace PhoneConnectionMaster.Views
{
  /// <summary>
  /// Interaction logic for UsbControl.xaml
  /// </summary>
  public partial class UsbControl : BaseUserControl
  {
    CommandsVpn commandsVpn = new CommandsVpn();

    public UsbControl()
    {
        InitializeComponent();
        InitializeDeviceList();
        this.Title_2.Content = "Connect with VPN :  ";
    }

    override protected void connect_Click(object sender, RoutedEventArgs e)
    {
      var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
      if (commandsVpn.ConnectVpn(deviceInfo))
      {
          ((DeviceInfo)this.DevicesComboBox.SelectedItem).IsConnected = true;
          InverseConnectButtonEnable(true);
      }
    }
    override protected bool isDeviceAccepted(DeviceInfo device)
    { 
      return device.IsUSB; 
    }

    override protected void disconnect_Click(object sender, RoutedEventArgs e)
    {
      var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
      if (commandsVpn.DisconnectVpn(deviceInfo))
      {
          ((DeviceInfo)this.DevicesComboBox.SelectedItem).IsConnected = false;
        InverseConnectButtonEnable(false);
      }
    }

    override protected void DevicesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
      if (deviceInfo != null)
      {
        if (deviceInfo.IsUSB)
          InverseConnectButtonEnable(deviceInfo.IsConnected);
        else
        {
          this.connect.IsEnabled = false;
          this.disconnect.IsEnabled = false;
        }
      }
    }
  }
}
