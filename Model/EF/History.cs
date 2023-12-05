using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    [Table("Histories")]
    public partial class History
    {
        [Key]
        public long Id { get; set; }
        public Guid? AnonymousId { get; set; }
        public long? UserId { get; set; }
        public long TruyenId { get; set; }
        public long ChuongTruyenId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
