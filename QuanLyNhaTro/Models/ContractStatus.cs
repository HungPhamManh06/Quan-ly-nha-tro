using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

public enum ContractStatus
{
    [Display(Name = "Đang hiệu lực")] HieuLuc = 1,
    [Display(Name = "Sắp hết hạn")] SapHetHan = 2,
    [Display(Name = "Đã thanh lý")] DaThanhLy = 3
}
