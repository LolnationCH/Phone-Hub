using PhoneConnectionMaster.Commands;
using PhoneConnectionMaster.Objects;
using System.Windows;
using System.Windows.Controls;

namespace PhoneConnectionMaster.Views
{
  /// <summary>
  /// Interaction logic for TcpControl.xaml
  /// </summary>
  public partial class TcpControl : BaseUserControl
  {
    public TcpControl()
    {
        InitializeComponent();
        InitializeDeviceList();
        this.Title_2.Content = "Connect over Wifi :";
    }

    override protected void connect_Click(object sender, RoutedEventArgs e)
    {
      var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
      CommandsTcp.Instance.ConnectPhone(deviceInfo.Serial);
      ((DeviceInfo)this.DevicesComboBox.SelectedItem).IsConnected = true;
      InverseConnectButtonEnable(true);
    }

    override protected void disconnect_Click(object sender, RoutedEventArgs e)
    {
      var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
      CommandsTcp.Instance.DisconnectPhone(deviceInfo.Serial);
      deviceInfo.IsConnected = false;
      if (!deviceInfo.IsUSB)
        InitializeDeviceList();
      else
        InverseConnectButtonEnable(false);
    }

    override protected void DevicesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
      if (deviceInfo != null)
      {
        if (deviceInfo.IsUSB)
          InverseConnectButtonEnable(CommandsTcp.Instance.IsDeviceConnected(deviceInfo.Serial));
        else
          InverseConnectButtonEnable(true);
      }
    }
  }
}
