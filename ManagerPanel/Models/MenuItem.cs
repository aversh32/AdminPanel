using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public bool IsVisible { get; set; }

        public List<MenuItem> SubItems { get; set; } = new List<MenuItem>();
        public Dictionary<string, string> AllRouteData { get; set; }
    }
}
