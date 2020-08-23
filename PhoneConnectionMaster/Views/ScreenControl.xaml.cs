using PhoneConnectionMaster.Commands;
using PhoneConnectionMaster.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhoneConnectionMaster.Views
{
  /// <summary>
  /// Interaction logic for ScreenControl.xaml
  /// </summary>
  public partial class ScreenControl : UserControl
  {
    public List<DeviceInfo> DevicesList = new List<DeviceInfo>();
    CommandADB CommandADB = new CommandADB();
    CommandsScreen CommandsScreen = new CommandsScreen();

    public ScreenControl()
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
      DevicesList = CommandADB.GetDevicesInfo().ToList();
      if (DevicesList.Count == 0)
      {
        this.DevicesComboBox.IsEnabled = false;
        this.connect.IsEnabled = false;
        this.DevicesComboBox.SelectedItem = null;
      }
      else
      {
        this.DevicesComboBox.IsEnabled = true;
        this.connect.IsEnabled = true;
        this.DevicesComboBox.ItemsSource = DevicesList;
        this.DevicesComboBox.SelectedItem = DevicesList[0];
      }
    }

    string GetScrcpyOptions()
    {
      var options = "";
      if (LockPortraitOption.IsChecked.HasValue && LockPortraitOption.IsChecked.Value)
        options += "--lock-video-orientation 0 ";
      if (AlwaysOnTopOption.IsChecked.HasValue && AlwaysOnTopOption.IsChecked.Value)
        options += " --always-on-top ";
      return options;
    }

    void connect_Click(object sender, RoutedEventArgs e)
    {
      var device = CommandsTcp.Instance.GetDeviceSerialOrIp((DeviceInfo)this.DevicesComboBox.SelectedItem);
      if (device != null)
        CommandsScreen.ConnectToScreen(device, GetScrcpyOptions());
    }

    void RefreshDevicesButton_Click(object sender, RoutedEventArgs e)
    {
      InitializeDeviceList();
    }

        private void AlwaysOnTopOption_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
