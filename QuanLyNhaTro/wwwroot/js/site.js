// ===== Sidebar (mobile drawer + desktop ổn định) =====
// Desktop: sidebar luôn hiện (CSS fixed). Mobile: drawer + backdrop, ESC để đóng.
(function () {
    const toggle = document.getElementById('sidebarToggle');
    const backdrop = document.getElementById('sidebarBackdrop');

    function open() {
        document.body.classList.add('sidebar-open');
        toggle?.setAttribute('aria-expanded', 'true');
    }
    function close() {
        document.body.classList.remove('sidebar-open');
        toggle?.setAttribute('aria-expanded', 'false');
    }

    toggle?.addEventListener('click', function () {
        document.body.classList.contains('sidebar-open') ? close() : open();
    });
    backdrop?.addEventListener('click', close);
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') close();
    });

    // Đóng drawer sau khi điều hướng (mobile)
    document.querySelectorAll('.sidebar .nav-link-item').forEach(function (link) {
        link.addEventListener('click', close);
    });
})();

// ===== Toast tự hiện rồi tự ẩn =====
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.toast-modern').forEach(function (el) {
        new bootstrap.Toast(el, { delay: el.dataset.bsDelay ? parseInt(el.dataset.bsDelay) : 4200 }).show();
    });
});

// ===== Confirm dialog dùng chung =====
// Form có thuộc tính data-confirm="câu hỏi" sẽ hiện modal xác nhận trước khi submit.
// Bấm "Đồng ý" sẽ submit NGUYÊN form gốc (giữ nguyên token + input),
// còn "Hủy" thì đóng modal. Dùng cho: xóa phòng, xóa người thuê, thanh lý hợp đồng,
// xác nhận thanh toán, quyết toán trả phòng...
// Form có data-confirm-danger sẽ đổi icon modal sang màu đỏ.
let pendingForm = null;
let confirmModal = null;

function openConfirmModal(form) {
    const modalEl = document.getElementById('confirmModal');
    if (!modalEl || typeof bootstrap === 'undefined') {
        if (window.confirm(form.dataset.confirm)) {
            form.dataset.confirmed = 'true';
            form.submit();
        }
        return;
    }

    const msgEl = document.getElementById('confirmMessage');
    const titleEl = document.getElementById('confirmTitle');
    const iconEl = document.getElementById('confirmIcon');
    const okBtn = document.getElementById('confirmOkBtn');

    if (msgEl) msgEl.textContent = form.dataset.confirm;
    if (titleEl) titleEl.textContent = form.dataset.confirmTitle || 'Xác nhận thao tác';
    if (iconEl) iconEl.classList.toggle('danger', form.dataset.confirmDanger === 'true');
    if (okBtn) okBtn.className = form.dataset.confirmDanger === 'true' ? 'btn btn-danger' : 'btn btn-primary';

    pendingForm = form;
    confirmModal = confirmModal || new bootstrap.Modal(modalEl);
    confirmModal.show();
}

document.addEventListener('submit', function (e) {
    const form = e.target;
    if (!form.matches('[data-confirm]')) return;
    // Nếu validation phía client (jquery validate) đã chặn → không hiện confirm
    if (e.isDefaultPrevented && e.isDefaultPrevented()) return;

    e.preventDefault();
    e.stopPropagation();
    openConfirmModal(form);
});

// Nút Đồng ý trong modal → submit form gốc
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('confirmOkBtn')?.addEventListener('click', function () {
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
