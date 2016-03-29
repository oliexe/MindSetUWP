using TestApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
    class HamburgerItem
    {
        public object Glyph { get; set; }
        public string Text { get; set; }
        public Type Page { get; set; }

        public HamburgerItem(object glyph = null, string text = "", Type page = null)
        {
            Glyph = glyph;
            Text = text;
            Page = (page == null ? typeof(PageNotFound) : page);
        }
    }
}
