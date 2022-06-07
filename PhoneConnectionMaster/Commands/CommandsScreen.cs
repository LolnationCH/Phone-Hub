using System.Diagnostics;
using System.Reflection;

namespace PhoneConnectionMaster.Commands
{
    public class CommandsScreen
    {
        private string CommandStr = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Scrcpy\\scrcpy.exe";
        private const string DefaultOptions = "--lock-video-orientation=0 --always-on-top";

        public void ConnectToScreen(string serial, string options = DefaultOptions)
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = CommandStr;
            proc.StartInfo.Arguments = $"--serial {serial} {options}";
            proc.Start();
        }
    }
}
