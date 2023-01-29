using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChartAccountAPI.Models.ChartAccount
{
    public class ChartAccountModel
    {
        public int Id { get; set; }
        public int? ParentAccountId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool AcceptEntry { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual IEnumerable<ChartAccountModel> Children { get; set; }
    }
}
