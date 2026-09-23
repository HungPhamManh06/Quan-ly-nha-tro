# Áp dụng cho các file *.Designer.cs:
#   1) Đổi tên control chung chung (label1, tableLayout1, tab1, mnuSep1...) thành tên có nghĩa
#   2)Thêm thuộc tính Name cho MỖI control (để hiện đúng trong Document Outline / Properties của VS)
#   3) Thêm chú thích đầu file giải thích đơn vị thiết kế + lưu ý khi sửa
# Chạy: python3 tools/apply_names.py          (CHỈ GHI KHI KHÔNG CÓ TÊN TRÙNG)
import re, sys, unicodedata, glob, os

sys.stdout.reconfigure(encoding='utf-8', errors='replace')

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
GENERIC = re.compile(r'^(label|tableLayout|flowLayout|panel|button|tab|groupBox|textBox|comboBox|checkBox)\d+$')
PREFIX = {
    'Label': 'lbl', 'Button': 'btn', 'TextBox': 'txt', 'RichTextBox': 'rtb',
    'ComboBox': 'cbo', 'NumericUpDown': 'num', 'CheckBox': 'chk', 'DateTimePicker': 'dtp',
    'TableLayoutPanel': 'layout', 'FlowLayoutPanel': 'pnl', 'Panel': 'pnl',
    'TabPage': 'tab', 'GroupBox': 'grp', 'TabControl': 'tabs', 'PictureBox': 'pic',
    'LinkLabel': 'lnk', 'ListBox': 'lst', 'ToolStripSeparator': 'mnuSep',
}
# Trường hợp đặc biệt (xem trước bằng tools/analyze_names.py rồi chốt tay)
OVERRIDE = {
    'MoveOutDialog.Designer.cs': {'label1': 'lblTieuDe', 'label3': 'lblHuongDanChiSo'},
    'ServiceDialog.Designer.cs': {'label4': 'lblSpacer'},
    'PaymentDialog.Designer.cs': {'tableLayout1': 'layoutRoot', 'tableLayout2': 'layout'},
}
HEADER = """// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA {name} (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
//
//  ĐƠN VỊ Ở ĐÂY LÀ ĐƠN VỊ THIẾT KẾ 96 DPI — lúc chạy, app tự nhân kích thước theo
//  tỉ lệ màn hình (100% / 125% / 150% / 200%) qua Dpi.ScaleForm. Vì vậy:
//    • Sửa VỊ TRÍ / KÍCH THƯỚC / CHỮ / MÀU thoải mái — lúc chạy vẫn đúng.
//    • KHÔNG đổi AutoScaleMode (phải là None), nếu không sẽ bị nhân kích thước 2 lần.
//    • Cỡ chữ khai bằng point (pt) nên tự đúng ở mọi DPI — không cần tự nhân.
//    • Thêm control mới: kéo từ Toolbox thả vào đây là được, app tự nhân DPI lúc chạy.
// ------------------------------------------------------------------------------
"""

def deaccent(s):
    s = s.replace('đ', 'd').replace('Đ', 'D')
    return ''.join(c for c in unicodedata.normalize('NFD', s) if unicodedata.category(c) != 'Mn')

def pascal(text):
    words = re.findall(r'[A-Za-z0-9]+', deaccent(text))
    return ''.join(w if w.isupper() else w.capitalize() for w in words)

def propose(field, typ, text):
    ov = None
    # OVERRIDE được áp sau khi biết file — truyền vào qua tham số
    return ov

def propose_name(field, typ, text, override):
    if field in override:
        return override[field]
    if typ == 'TableLayoutPanel':
        return 'layout' if text == '' else 'layout' + pascal(text)[:20]
    if typ == 'FlowLayoutPanel':
        return 'pnlButtons'
    if len(text) > 28:                      # nhãn dài = tiêu đề / hướng dẫn
        return 'lblTieuDe' if text.isupper() else 'lblHuongDan'
    if text.strip():
        return (PREFIX.get(typ, 'ctl') + pascal(text))[:40]
    if typ == 'Label':
        return 'lblSpacer'
    m = re.match(r'([A-Za-z]+?)(\d+)$', field)
    return PREFIX.get(typ, 'ctl') + (m.group(1).capitalize() if m else 'Extra') + (m.group(2) if m else '')

summary = []
problems = []

for path in sorted(glob.glob(os.path.join(ROOT, 'Forms', '*.Designer.cs')) +
                   glob.glob(os.path.join(ROOT, 'Dialogs', '*.Designer.cs'))):
    fname = os.path.basename(path)
    src = open(path, encoding='utf-8-sig', newline='').read()
    orig = src
    inst = dict(re.findall(r'this\.(\w+) = new System\.Windows\.Forms\.(\w+)\(\);', src))
    texts = dict(re.findall(r'this\.(\w+)\.Text = "((?:[^"\\]|\\.)*)";', src))
    override = dict(OVERRIDE.get(fname, {}))

    # ---- 1) Xác định các cặp đổi tên ----
    renames = {}
    for field, typ in inst.items():
        if GENERIC.match(field):
            renames[field] = propose_name(field, typ, texts.get(field, ''), override)

    # Tên ngăn menu (ToolStripSeparator): lấy tên menu cha từ AddRange
    for field, typ in inst.items():
        if typ == 'ToolStripSeparator':
            m = re.search(r'this\.(\w+)\.DropDownItems\.AddRange\(new System\.Windows\.Forms\.ToolStripItem\[\] \{[^}]*this\.' + field + r'\b', src, re.S)
            parent = m.group(1) if m else ''
            renames[field] = 'mnuSep' + (parent[3:].capitalize() if parent.startswith('mnu') else '')

    # ---- 2) Kiểm tra trùng tên trong từng file ----
    final_names = set(inst) - set(renames) | set(renames.values())
    if len(final_names) != len(inst):
        clash = [n for n in set(renames.values()) if list(renames.values()).count(n) > 1]
        problems.append(f"{fname}: tên mới bị trùng: {sorted(set(clash))}")
        continue
    for old, new in renames.items():
        if new in (set(inst) - {old}):
            problems.append(f"{fname}: '{new}' đã tồn tại (đổi '{old}')")
            break
    else:
        # ---- 3) Đổi tên (whole word) trong file Designer và file .cs kèm theo ----
        targets = [path]
        sibling = path.replace('.Designer.cs', '.cs')
        if os.path.exists(sibling):
            targets.append(sibling)
        for t in targets:
            s = open(t, encoding='utf-8-sig', newline='').read()
            for old, new in sorted(renames.items(), key=lambda kv: -len(kv[0])):
                s = re.sub(r'\b' + re.escape(old) + r'\b', new, s)
            if s != open(t, encoding='utf-8-sig', newline='').read():
                open(t, 'w', encoding='utf-8', newline='').write(s)
            if t == path:
                src = s

        # ---- 4) Thêm Name cho control chưa có ----
        added = 0
        eol = '\r\n' if '\r\n' in src else '\n'
        for field, typ in inst.items():
            new_field = renames.get(field, field)
            if re.search(r'this\.' + re.escape(new_field) + r'\.Name = ', src):
                continue
            line = f'this.{new_field} = new System.Windows.Forms.{typ}();'
            if line in src:
                src = src.replace(line, line + eol + '            ' + f'this.{new_field}.Name = "{new_field}";', 1)
                added += 1

        # ---- 5) Chú thích đầu file ----
        header_added = False
        if 'ĐƠN VỊ THIẾT KẾ 96 DPI' not in src:
            formname = fname.replace('.Designer.cs', '')
            hdr = HEADER.format(name=formname)
            if src.lstrip().startswith('//'):
                # thay cụm chú thích cũ (nếu có) — lấy đến dòng '// -----...' đóng
                lines = src.split(eol)
                end = 0
                for i, ln in enumerate(lines[:20]):
                    if i > 0 and ln.startswith('// ---'):
                        end = i
                        break
                src = hdr + eol.join(lines[end + 1:])
            else:
                src = hdr + src
            header_added = True

        if src != orig:
            open(path, 'w', encoding='utf-8', newline='').write(src)

        summary.append((fname, len(renames), added, header_added,
                        ', '.join(f'{o}→{n}' for o, n in sorted(renames.items()))))

# ---- Báo cáo ----
print(f"{'FILE':38} {'ĐỔI TÊN':8} {'THÊM Name':10} {'CHÚ THÍCH':10} CHI TIẾT")
print('-' * 130)
for f, r, a, h, d in summary:
    print(f"{f:38} {r:<8} {a:<10} {'x' if h else '':10} {d}")

if problems:
    print("\n⚠ BỊ BỎ QUA (cần sửa tay):")
    for p in problems:
        print("  " + p)
else:
    print(f"\nOK: {len(summary)} file, không có tên trùng.")
