using PhoneConnectionMaster.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneConnectionMaster.Commands
{
    public class CommandsScreen
    {
        private string CommandStr = "Scrcpy\\scrcpy-noconsole.exe";
        private string DefaultOptions = "--lock-video-orientation 0";

        public void ConnectToScreen(string serial)
        {
            // Todo, add a way for the user to specify options
            string options = DefaultOptions;

            Process.Start(CommandStr, $"--serial {serial} {options}");
        }
    }
}
