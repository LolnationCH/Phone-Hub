using PhoneConnectionMaster.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneConnectionMaster
{
    public class CommandsVpn
    {
        List<DeviceInfo> DeviceInfoList = new List<DeviceInfo>();

        Process RelayProcess = null;

        private string CommandStr = "Gnirehtet\\gnirehtet.exe";

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

        private bool GetProcess(string arguments)
        {
            Process p = new Process();
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = CommandStr;
            p.StartInfo.Arguments = arguments;
            p.Start();

            p.WaitForExit();
            return p.ExitCode == 0;
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

        private void KillRelay()
        {
            RelayProcess.Kill();
        }
    }
}
