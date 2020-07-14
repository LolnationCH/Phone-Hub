using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhoneConnectionMaster.Objects
{
    public class DeviceInfo
    {
        public string Features { get; set; }
        public string Messages { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Product { get; set; }
        public string TransportId { get; set; }

        public DeviceState State { get; set; }

        public bool IsUSB { get {

          return DnsEndPoint == null;
          }
        }

        public DnsEndPoint DnsEndPoint { get; set; } = null;

        public bool IsConnected { get; set; } = false;

        public override string ToString() {
            return Name + (IsUSB ? "" : ": (WIFI)");
        }

        public static DnsEndPoint CreateDnsEndPoint(string endPoint)
        {
          string[] ep = endPoint.Split(':');
          if (ep.Length != 2) return null;
          return new DnsEndPoint(ep[0], int.Parse(ep[1]));
        }
  }
}
