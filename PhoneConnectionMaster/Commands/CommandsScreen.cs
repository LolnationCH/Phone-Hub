using System.Diagnostics;
using System.Reflection;

namespace PhoneConnectionMaster.Commands
{
    public class CommandsScreen
    {
        private string CommandStr = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Scrcpy\\scrcpy-noconsole.exe";
        private const string DefaultOptions = "--lock-video-orientation 0 --always-on-top";

        public void ConnectToScreen(string serial, string options = DefaultOptions)
        {
            Process.Start(CommandStr, $"--serial {serial} {options}");
        }
    }
}
