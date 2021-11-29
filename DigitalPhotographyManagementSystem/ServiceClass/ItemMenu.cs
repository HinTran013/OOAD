using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DigitalPhotographyManagementSystem.ServiceClass
{
    public class ItemMenu
    {
        public enum CommandType
        {
            About = 1,
            Exit = 2,
            LogOut = 3,
            UControl = 4
        }
        public ItemMenu(string header, List<SubItem> subItems, PackIconKind icon)
        {
            Header = header;
            SubItems = subItems;
            Icon = icon;
        }
        public ItemMenu(string header, PackIconKind icon, CommandType commandtype, UserControl uc = null)
        {
            Header = header;
            Screen = uc;
            commandType = commandtype;
            Icon = icon;
        }
        public ItemMenu(string header, PackIconKind icon)
        {
            Header = header;
            Icon = icon;
        }
        public ItemMenu(string header, PackIconKind icon, CommandType commandtype)
        {
            Header = header;
            Icon = icon;
            commandType = commandtype;
        }
        public CommandType commandType { get; private set; }
        public string Header { get; private set; }
        public PackIconKind Icon { get; private set; }
        public List<SubItem> SubItems { get; private set; }
        public UserControl Screen { get; private set; }
    }
}