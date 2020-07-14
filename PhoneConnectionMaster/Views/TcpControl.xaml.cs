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

namespace PhoneConnectionMaster.Views
{
  /// <summary>
  /// Interaction logic for TcpControl.xaml
  /// </summary>
  public partial class TcpControl : UserControl
  {
      List<DeviceInfo> DevicesList = new List<DeviceInfo>();
      CommandADB CommandADB = new CommandADB();

      public TcpControl()
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
            this.WIFI_connect.IsEnabled = true;
            this.DevicesComboBox.ItemsSource = DevicesList;
            this.DevicesComboBox.SelectedItem = DevicesList[0];
        }
      }

      private void InverseWIFIButtonEnable(bool IsConnected)
      {
          this.WIFI_connect.IsEnabled = !IsConnected;
          this.WIFI_disconnect.IsEnabled = IsConnected;
      }

      private void WIFI_connect_Click(object sender, RoutedEventArgs e)
      {
        var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
        CommandsTcp.Instance.ConnectPhone(deviceInfo.Serial);
        ((DeviceInfo)this.DevicesComboBox.SelectedItem).IsConnected = true;
        InverseWIFIButtonEnable(true);
      }

    private void WIFI_disconnect_Click(object sender, RoutedEventArgs e)
    {
      var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
      CommandsTcp.Instance.DisconnectPhone(deviceInfo.Serial);
      deviceInfo.IsConnected = false;
      if (!deviceInfo.IsUSB)
        InitializeDeviceList();
      else
          InverseWIFIButtonEnable(false);
    }

    private void DevicesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       var deviceInfo = ((DeviceInfo)this.DevicesComboBox.SelectedItem);
       if (deviceInfo != null)
       {
        if (deviceInfo.IsUSB)
          InverseWIFIButtonEnable(CommandsTcp.Instance.IsDeviceConnected(deviceInfo.Serial));
        else
          InverseWIFIButtonEnable(true);
       }
    }

    private void RefreshDevicesButton_Click(object sender, RoutedEventArgs e)
    {
      InitializeDeviceList();
    }
  }
}
