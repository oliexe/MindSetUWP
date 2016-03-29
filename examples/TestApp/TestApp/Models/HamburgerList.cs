using TestApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
    class HamburgerList
    {
        public string BackgroundColor { get; set; }
        public List<HamburgerItem> MenuItems { get; set; }
        public HamburgerList()
        {
            MenuItems = new List<HamburgerItem>() {
                new HamburgerItem("\uE1CE","Realtime", typeof(Start)),
                new HamburgerItem("\uE1CF","Recording", typeof(PageNotFound))
            };
        }
    }
}
