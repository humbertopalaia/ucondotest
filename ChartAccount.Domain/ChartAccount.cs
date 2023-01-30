using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartAccountDomain
{
    public class ChartAccount
    {

        [Key]
        public int Id { get; set; }
        public int? ParentAccountId { get; set; }
        public string Code { get; set; }
        public int LevelCode { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool AcceptEntry { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [ForeignKey("ParentAccountId")]
        public virtual IEnumerable<ChartAccount> Children { get; set; }

    }
}
