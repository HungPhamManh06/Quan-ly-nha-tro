using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

public enum PaymentMethod
{
    [Display(Name = "Tiền mặt")] TienMat = 1,
    [Display(Name = "Chuyển khoản")] ChuyenKhoan = 2
}

public enum InvoicePaymentStatus
{
    [Display(Name = "Đã thanh toán")] DaThanhToan = 1,
    [Display(Name = "Chưa thanh toán")] ChuaThanhToan = 2
}
