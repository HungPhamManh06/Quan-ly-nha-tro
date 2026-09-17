// Toggle sidebar
const sidebarToggle = document.getElementById('sidebarToggle');
if (sidebarToggle) {
    sidebarToggle.addEventListener('click', function () {
        document.getElementById('sidebar-wrapper')?.classList.toggle('toggled');
    });
}

// Toast tự ẩn sau khi hiện
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.toast').forEach(function (el) {
        new bootstrap.Toast(el, { delay: el.dataset.bsDelay ? parseInt(el.dataset.bsDelay) : 4000 }).show();
    });
});

// ===== Confirm dialog dùng chung =====
// Form có thuộc tính data-confirm="câu hỏi" sẽ hiện modal xác nhận trước khi submit.
// Bấm "Đồng ý" sẽ submit NGUYÊN form gốc (giữ nguyên token + input),
// còn "Hủy" thì đóng modal. Dùng cho: xóa phòng, xóa người thuê, thanh lý hợp đồng,
// xác nhận thanh toán, quyết toán trả phòng...
let pendingForm = null;
let confirmModal = null;

document.addEventListener('submit', function (e) {
    const form = e.target;
    if (!form.matches('[data-confirm]')) return;
    // Nếu validation phía client (jquery validate) đã chặn → không hiện confirm
    if (e.isDefaultPrevented && e.isDefaultPrevented()) return;

    e.preventDefault();
    e.stopPropagation();

    const modalEl = document.getElementById('confirmModal');
    if (!modalEl || typeof bootstrap === 'undefined') {
        if (window.confirm(form.dataset.confirm)) {
            form.dataset.confirmed = 'true';
            form.submit(); // gọi native submit, không lặp lại sự kiện
        }
        return;
    }

    document.getElementById('confirmMessage').textContent = form.dataset.confirm;
    pendingForm = form;
    confirmModal = confirmModal || new bootstrap.Modal(modalEl);
    confirmModal.show();
});

// Nút Đồng ý trong modal → submit form gốc
document.addEventListener('DOMContentLoaded', function () {
    const okBtn = document.getElementById('confirmOkBtn');
    okBtn?.addEventListener('click', function () {
        if (pendingForm) {
            confirmModal?.hide();
            pendingForm.dataset.confirmed = 'true';
            pendingForm.submit(); // native submit bỏ qua handler data-confirm → không lặp
            pendingForm = null;
        }
    });
});

// Định dạng tiền tệ VNĐ (hỗ trợ hiển thị nơi cần)
function formatVnd(number) {
    return new Intl.NumberFormat('vi-VN').format(number) + ' đ';
}
