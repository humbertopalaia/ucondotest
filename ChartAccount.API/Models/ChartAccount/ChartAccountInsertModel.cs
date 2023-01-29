using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChartAccountAPI.Models.ChartAccount
{
    public class ChartAccountInsertModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool AcceptEntry { get; set; }

        public List<ChartAccountInsertModel> Children { get; set; }
    }
}
