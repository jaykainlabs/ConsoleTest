using KainLabs.CliUtilities;

namespace ConsoleTest.MenuItems
{
    public class Test : IMenuItem
    {
        public string Name { get { return "Test"; } }

        public int Order { get { return 1; } }

        public int Key { get; set; }

        public bool Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
