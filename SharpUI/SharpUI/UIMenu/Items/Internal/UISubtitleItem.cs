using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

namespace SharpUI.UIMenu {
    internal class UISubtitleItem : UIItemBase {

        #region Variables
        public new UISubtitleItemStyle Style;

        public string Subtitle;
        public int SelectedIndex;
        public int ItemCount;
        #endregion

        #region Constructor
        public UISubtitleItem(string subtitle, UISubtitleItemStyle style)
        {
            Subtitle = subtitle;
            Style = style;
        }
        #endregion

        public override void Draw(UIMenu menu, D3DGraphics gfx, Point pos)
        {
            gfx.DrawBoxFilled(gfx.Device, new Vector2(pos.X, pos.Y), menu.ItemSize, Style.BackgroundColor);
            gfx.DrawString(gfx.Device, menu.FontOverride, Subtitle, new Rectangle(new Point(pos.X + 5, pos.Y), new Size(menu.ItemSize.Width - 5, menu.ItemSize.Height)), eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.ForegroundColor);
            gfx.DrawString(gfx.Device, menu.FontOverride, string.Format("{0} / {1}", ItemCount == 0 ? "0" : SelectedIndex.ToString(), ItemCount.ToString()), new Rectangle(pos, new Size(menu.ItemSize.Width - 5, menu.ItemSize.Height)), eD3DFontDrawFlags.Right | eD3DFontDrawFlags.VerticalCenter, Style.ForegroundColor);
        }

        public override void KeyPress(UIMenu menu, KeyEventArgs args, bool isKeyUpEvent, bool shouldBeUsedForNavigation)
        {

        }

        /// <inheritdoc/>
        public override void Cleanup()
        {

        }

    }
}
