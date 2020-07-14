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
  public partial class UsbControl : UserControl
  {
    List<DeviceInfo> DevicesList = new List<DeviceInfo>();

    CommandsVpn commandsVpn = new CommandsVpn();
    CommandADB CommandADB = new CommandADB();

    public UsbControl()
    {
        InitializeComponent();
        InitializeDeviceList();
    }

    public DeviceInfo GetCurrentDeviceInfo()
    {
        return ((DeviceInfo)this.DevicesComboBox.SelectedItem);
    }

    public void InitializeDeviceList()
    {
      DevicesList = CommandADB.GetDevicesInfo();
      if (DevicesList.Count == 0)
        this.DevicesComboBox.IsEnabled = false;
      else
      {
        this.DevicesComboBox.IsEnabled = true;
        this.VPN_connect.IsEnabled = true;
        this.DevicesComboBox.ItemsSource = DevicesList;
        this.DevicesComboBox.SelectedItem = DevicesList[0];
      }
    }

    private void InverseVPNButtonEnable(bool IsConnected)
    {
      this.VPN_connect.IsEnabled = !IsConnected;
      this.VPN_disconnect.IsEnabled = IsConnected;
    }

    private void VPN_connect_Click(object sender, RoutedEventArgs e)
    {
      var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
      if (commandsVpn.ConnectVpn(deviceInfo))
      {
          ((DeviceInfo)this.DevicesComboBox.SelectedItem).IsConnected = true;
          InverseVPNButtonEnable(true);
      }
    }

    private void VPN_disconnect_Click(object sender, RoutedEventArgs e)
    {
      var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
      if (commandsVpn.DisconnectVpn(deviceInfo))
      {
          ((DeviceInfo)this.DevicesComboBox.SelectedItem).IsConnected = false;
          InverseVPNButtonEnable(false);
      }
    }

    private void DevicesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
      if (deviceInfo != null)
      {
        if (deviceInfo.IsUSB)
          InverseVPNButtonEnable(deviceInfo.IsConnected);
        else
        {
          this.VPN_connect.IsEnabled = false;
          this.VPN_disconnect.IsEnabled = false;
        }
      }
    }

    private void RefreshDevicesButton_Click(object sender, RoutedEventArgs e)
    {
      InitializeDeviceList();
    }
  }
}
