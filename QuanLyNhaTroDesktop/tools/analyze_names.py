# Phân tích control có tên chung chung (label1, tableLayout1...) trong các file *.Designer.cs
# và đề xuất tên mới theo CHỮ hiển thị trên control đó. CHỈ IN BÁO CÁO — chưa đổi gì.
# Chạy: python3 tools/analyze_names.py
import re, sys, unicodedata, glob, os

sys.stdout.reconfigure(encoding='utf-8', errors='replace')  # console Windows dùng cp1252

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
GENERIC = re.compile(r'^(label|tableLayout|flowLayout|panel|button|tab|groupBox|textBox|comboBox|checkBox)\d+$')
PREFIX = {
    'Label': 'lbl', 'Button': 'btn', 'TextBox': 'txt', 'RichTextBox': 'rtb',
    'ComboBox': 'cbo', 'NumericUpDown': 'num', 'CheckBox': 'chk', 'DateTimePicker': 'dtp',
    'TableLayoutPanel': 'layout', 'FlowLayoutPanel': 'pnl', 'Panel': 'pnl',
    'TabPage': 'tab', 'GroupBox': 'grp', 'TabControl': 'tabs', 'PictureBox': 'pic',
    'LinkLabel': 'lnk', 'ListBox': 'lst',
}

def deaccent(s):
    s = s.replace('đ', 'd').replace('Đ', 'D')
    return ''.join(c for c in unicodedata.normalize('NFD', s) if unicodedata.category(c) != 'Mn')

def pascal(text):
    words = re.findall(r'[A-Za-z0-9]+', deaccent(text))
    return ''.join(w if w.isupper() else w.capitalize() for w in words)

def propose(field, typ, text):
    # Tên đặc biệt cho bảng/nút
    if typ == 'TableLayoutPanel' and text == '':
        return 'layout'
    if typ == 'FlowLayoutPanel' and text == '':
        return 'pnlButtons'
    if text.strip():
        p = PREFIX.get(typ, 'ctl')
        name = p + pascal(text)
        return name[:40]
    p = PREFIX.get(typ, 'ctl')
    m = re.match(r'([A-Za-z]+?)(\d+)$', field)
    return p + (m.group(1).capitalize() if m else 'Extra') + (m.group(2) if m else '')

report = []
for path in sorted(glob.glob(os.path.join(ROOT, 'Forms', '*.Designer.cs')) +
                   glob.glob(os.path.join(ROOT, 'Dialogs', '*.Designer.cs'))):
    src = open(path, encoding='utf-8-sig').read()
    base = os.path.basename(path).replace('.Designer.cs', '')
    sibling = path.replace('.Designer.cs', '.cs')
    sib = open(sibling, encoding='utf-8-sig').read() if os.path.exists(sibling) else ''

    # field -> (type, text)
    inst = dict(re.findall(r'this\.(\w+) = new System\.Windows\.Forms\.(\w+)\(\);', src))
    texts = dict(re.findall(r'this\.(\w+)\.Text = "((?:[^"\\]|\\.)*)";', src))

    for field, typ in sorted(inst.items()):
        if not GENERIC.match(field):
            continue
        text = texts.get(field, '')
        new = propose(field, typ, text)
        # kiểm tra trùng trong cùng file (kể cả tên mới có thể trùng với tên đã có)
        all_fields = set(inst)
        if new in all_fields and new != field:
            new = new + '_XUNG'
        used_in_cs = bool(re.search(r'\b' + re.escape(field) + r'\b', sib))
        report.append((os.path.basename(path), field, typ, text, new, used_in_cs))

print(f"{'FILE':38} {'CŨ':14} {'LOẠI':18} {'CHỮ TRÊN CONTROL':34} {'ĐỀ XUẤT':22} .cs?")
print('-' * 140)
for f, old, typ, text, new, used in report:
    t = (text[:32] + '…') if len(text) > 33 else (text or '(trống)')
    print(f"{f:38} {old:14} {typ:18} {t:34} {new:22} {'CÓ' if used else ''}")
print(f"\nTổng: {len(report)} control cần đổi tên trong {len(set(r[0] for r in report))} file.")
dup = [r for r in report if r[4].endswith('_XUNG')]
if dup:
    print(f"⚠ {len(dup)} tên đề xuất bị trùng, cần sửa tay:")
    for r in dup: print(f"   {r[0]}: {r[1]} → {r[4]}")
