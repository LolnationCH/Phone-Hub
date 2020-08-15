using PhoneConnectionMaster.Objects;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace PhoneConnectionMaster
{
    public class CommandsVpn
    {
        List<DeviceInfo> DeviceInfoList = new List<DeviceInfo>();
        Process RelayProcess = null;

        private string CommandStr = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Gnirehtet\\gnirehtet.exe";

        public bool ConnectVpn(DeviceInfo device)
        {
            DeviceInfoList.Add(device);

            if (RelayProcess == null)
                StartRelay();

            InstallApk(device);

            var process = Process.Start(CommandStr, $"start {device.Serial}");
            process.WaitForExit();
            return process.ExitCode == 0;
        }

        public bool DisconnectVpn(DeviceInfo device)
        {
            DeviceInfoList.Remove(device);
            var process = Process.Start(CommandStr, $"stop {device.Serial}");
            process.WaitForExit();
            return process.ExitCode == 0;
        }

        private void InstallApk(DeviceInfo device)
        {
            var process = Process.Start(CommandStr, $"install {device.Serial}");
            process.WaitForExit();
        }

        private void StartRelay()
        {
            RelayProcess = new Process();
            RelayProcess.StartInfo.FileName = CommandStr;
            RelayProcess.StartInfo.Arguments = "relay";

            RelayProcess.Start();
        }
    }
}
