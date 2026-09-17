using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

/// <summary>Lịch sử thay đổi hợp đồng (gia hạn, thanh lý...).</summary>
public class ContractHistory
{
    public int MaLichSu { get; set; }

    public int MaHopDong { get; set; }
    public Contract? Contract { get; set; }

    [StringLength(50)]
    [Display(Name = "Thao tác")]
    public string HanhDong { get; set; } = null!;

    [StringLength(500)]
    [Display(Name = "Nội dung")]
    public string NoiDung { get; set; } = null!;

    [Display(Name = "Thời gian")]
    public DateTime ThoiGian { get; set; } = DateTime.Now;
}
