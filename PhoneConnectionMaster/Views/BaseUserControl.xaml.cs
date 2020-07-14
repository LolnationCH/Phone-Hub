using PhoneConnectionMaster.Commands;
using PhoneConnectionMaster.Objects;
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
  /// Interaction logic for BaseUserControl.xaml
  /// </summary>
  public partial class BaseUserControl : UserControl
  {
    List<DeviceInfo> DevicesList = new List<DeviceInfo>();
    CommandADB CommandADB = new CommandADB();

    public BaseUserControl()
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
        this.connect.IsEnabled = true;
        this.DevicesComboBox.ItemsSource = DevicesList;
        this.DevicesComboBox.SelectedItem = DevicesList[0];
      }
    }

    protected void InverseConnectButtonEnable(bool IsConnected)
    {
      this.connect.IsEnabled = !IsConnected;
      this.disconnect.IsEnabled = IsConnected;
    }

    virtual protected void connect_Click(object sender, RoutedEventArgs e) { }

    virtual protected void disconnect_Click(object sender, RoutedEventArgs e) { }

    virtual protected void DevicesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

    protected void RefreshDevicesButton_Click(object sender, RoutedEventArgs e)
    {
      InitializeDeviceList();
    }
  }
}
