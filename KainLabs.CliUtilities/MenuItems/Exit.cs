using System;

namespace KainLabs.CliUtilities.MenuItems
{
    public class Exit : IMenuItem
    {
        public string Name { get { return "Exit"; } }
        public int Order { get { return 99; } }
        public int Key { get; set; }

        public bool Execute()
        {
            return false;
        }
    }
}
