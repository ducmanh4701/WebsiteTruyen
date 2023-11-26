namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Truyen")]
    public partial class Truyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Truyen()
        {
            BinhLuans = new HashSet<BinhLuan>();
            BoSuuTaps = new HashSet<BoSuuTap>();
            ChuongTruyens = new HashSet<ChuongTruyen>();
        }

        [Key]
        public long Id_Truyen { get; set; }

        [StringLength(255)]
        public string Ten_Truyen { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string Avt_Truyen { get; set; }

        [Column(TypeName = "ntext")]
        public string GioiThieu_Truyen { get; set; }

        public long? TotalView { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? TopHot { get; set; }

        public long? Id_TacGia { get; set; }

        public long? Id_TheLoai { get; set; }

        public long? Id_TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoSuuTap> BoSuuTaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuongTruyen> ChuongTruyens { get; set; }

        public virtual TacGia TacGia { get; set; }

        public virtual TheLoai TheLoai { get; set; }

        public virtual TrangThai TrangThai { get; set; }
    }
}
