using PhoneConnectionMaster.Objects;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PhoneConnectionMaster.Commands
{
  public sealed class CommandsTcp
  {
    private static CommandsTcp instance = null;
    private static readonly object padlock = new object();

    CommandsTcp() {}

    public static CommandsTcp Instance
    {
      get
      {
        lock (padlock)
        {
          if (instance == null)
          {
            instance = new CommandsTcp();
          }
          return instance;
        }
      }
    }
  
    AdbServer server = new AdbServer();
    AdbClient AdbClient = null;
    private string CommandStr = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ADB\\adb.exe";

    private Dictionary<string, DnsEndPoint> DeviceCache = new Dictionary<string, DnsEndPoint>();
    private Dictionary<string, DnsEndPoint> DevicesConnected = new Dictionary<string, DnsEndPoint>();

    private int port = 5554;

    public void ConnectPhone(string Serial)
    {
      StartServer();

      var dns = GetCacheDnsEndPoint(Serial);

      if (dns != null)
      {
          AdbClient.Connect(dns.Host);
          DevicesConnected.Add(Serial, dns);
          return;
      }

      var deviceData = AdbClient.GetDevices().First(x => x.Serial == Serial);

      // Get ip addresse of the device
      var receiver = new ConsoleOutputReceiver();
      AdbClient.ExecuteRemoteCommand("ip addr show wlan0", deviceData, receiver);
      var ip = receiver.ToString().Split('\n').Where(x => x.Contains("inet")).First()
                        .Trim().Split(' ')[1].Split('/').First();

      // Setup the tcpip
      port++;
      CommandADB.StartTcpip(Serial, port);

      AdbClient.Connect(ip);
      dns = new DnsEndPoint(ip, port);
      DevicesConnected.Add(Serial, dns);
      DeviceCache.Add(Serial, dns);
    }

    public bool IsDeviceConnected(string Serial)
    {
      return DevicesConnected.ContainsKey(Serial);
    }

    public void DisconnectPhone(string Serial)
    {
      StartServer();
      if (DevicesConnected.ContainsKey(Serial))
      { 
        AdbClient.Disconnect(DevicesConnected[Serial]);
        DevicesConnected.Remove(Serial);
      }
      else
      {
        var dns = DeviceInfo.CreateDnsEndPoint(Serial);
        AdbClient.Disconnect(dns);
        // Remove the dns from the list
        var item = DevicesConnected.Where(x => x.Value.Host == dns.Host).FirstOrDefault();
        if (item.Key != null)
          DevicesConnected.Remove(item.Key);
      }
    }

    public string GetDeviceSerialOrIp(DeviceInfo device)
    {
      if (device.IsUSB && DevicesConnected.ContainsKey(device.Serial))
        return DevicesConnected[device.Serial].ToString();
      return device.Serial;
    }

    private void StartServer()
    {
      if (AdbClient == null)
      {
          var _ = server.StartServer(CommandStr, restartServerIfNewer: false);
          AdbClient = new AdbClient();
      }
    }

    private DnsEndPoint GetCacheDnsEndPoint(string serial)
    {
      DeviceCache.TryGetValue(serial, out DnsEndPoint value);
      return value;
    }
  }
}
