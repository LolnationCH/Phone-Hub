using PhoneConnectionMaster.Objects;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;

namespace PhoneConnectionMaster.Commands
{
  public class CommandADB
  {
    AdbServer server = new AdbServer();
    AdbClient AdbClient = null;

    private static string AdbPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ADB\adb.exe";

    public List<DeviceData> GetDevicesData()
    {
      StartServer();
      return AdbClient.GetDevices();
    }

    public List<DeviceInfo> GetDevicesInfo()
    {
      var devices = GetDevicesData();
      return devices.Select(x => new DeviceInfo()
             {
               Features = x.Features,
               Messages = x.Message,
               Serial = x.Serial,
               DnsEndPoint = DeviceInfo.CreateDnsEndPoint(x.Serial),
               Name = x.Name,
               State = x.State,
               TransportId = x.TransportId,
             }).ToList();
    }

    public static void StartTcpip(string Serial, int port)
    {
      var process = Process.Start(AdbPath, $"-s {Serial} tcpip {port}");
      process.WaitForExit();
    }

    public static void MonitorDevicesConnection(EventHandler<DeviceDataEventArgs> eventHandler)
    {
      var monitor = new DeviceMonitor(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)));
      monitor.DeviceChanged += eventHandler;
      monitor.DeviceDisconnected += eventHandler;
      monitor.Start();
    }

    void OnDeviceConnected(object sender, DeviceDataEventArgs e)
    {
      Console.WriteLine($"The device {e.Device.Name} has connected to this PC");
    }

    private void StartServer()
    {
      if (AdbClient == null)
      {
        server.StartServer(AdbPath, restartServerIfNewer: false);
        AdbClient = new AdbClient();
      }
    }
  }
}
