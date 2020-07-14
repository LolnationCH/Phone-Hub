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
  public partial class TcpControl : BaseUserControl
  {
    List<DeviceInfo> DevicesList = new List<DeviceInfo>();
    CommandADB CommandADB = new CommandADB();

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
