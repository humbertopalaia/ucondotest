using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChartAccountAPI.Models.ChartAccount
{
    public class ChartAccountListModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool AcceptEntry { get; set; }

        public List<ChartAccountListModel> Children { get; set; }
    }
}
