using PhoneConnectionMaster.Commands;
using PhoneConnectionMaster.Objects;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
