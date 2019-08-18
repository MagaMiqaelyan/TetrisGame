using System.Windows.Forms;

namespace Tetris
{
    internal class ShapePreview : DataGridView
    {
        // Constants used for ignoring DGV focussing
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_KEYDOWN = 0x100;

        // Avoids focussing
        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            int p = (int)e.PaintParts;
            p -= (int)DataGridViewPaintParts.Focus;
            e.PaintParts = (DataGridViewPaintParts)p;
            base.OnRowPrePaint(e);
        }

        // Ignores focussing
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK || m.Msg == WM_LBUTTONDOWN || m.Msg == WM_KEYDOWN)
            {
                return;
            }
            base.WndProc(ref m);
        }


    }
}
