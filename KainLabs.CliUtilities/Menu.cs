using System;
using System.Collections.Generic;
using System.Linq;

namespace KainLabs.CliUtilities
{
    public class Menu
    {
        private List<IMenuItem> MenuItems { get; set; }

        public Menu()
        {
            MenuItems = new List<IMenuItem>();

            var type = typeof(IMenuItem);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                .ToList();

            foreach (var currentType in types)
            {
                var obj = Activator.CreateInstance(currentType);
                MenuItems.Add((IMenuItem)obj);
            }
        }

        public void Run()
        {
            while (true)
            {
                Show();

                var selectionIndex = KLConsole.ReadInt("Select an item: ");

                var selection = MenuItems.FirstOrDefault(x => x.Key == selectionIndex);

                if (selection != null)
                {
                    KLConsole.Clear();

                    var reshowMenu = selection.Execute();

                    if (!reshowMenu)
                    {
                        break;
                    }

                    KLConsole.WriteLine();
                    KLConsole.ReadKey("Press any key to continue...");
                }
            }
        }

        private void Show()
        {
            KLConsole.Clear();
            KLConsole.WriteLine();

            var key = 1;
            foreach (var menuItem in MenuItems.OrderBy(x => x.Order).ToList())
            {
                menuItem.Key = key++;
                KLConsole.WriteLine(string.Format("{0}) {1}", menuItem.Key, menuItem.Name));
            }

            KLConsole.WriteLine();
        }
    }
}
