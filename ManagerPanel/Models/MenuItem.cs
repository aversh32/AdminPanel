using System.Collections.Generic;

namespace ManagerPanel.Models
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
