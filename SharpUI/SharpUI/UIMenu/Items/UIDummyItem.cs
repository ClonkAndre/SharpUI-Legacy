using System.Drawing;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

namespace SharpUI.UIMenu {
    /// <summary>
    /// Dummy item.
    /// </summary>
    public class UIDummyItem : UIItemBase {

        /// <inheritdoc/>
        public override void Draw(UIMenu menu, D3DGraphics gfx, Point pos)
        {

        }

        /// <inheritdoc/>
        public override void KeyPress(UIMenu menu, KeyEventArgs args, bool isKeyUpEvent, bool shouldBeUsedForNavigation)
        {

        }

        /// <inheritdoc/>
        public override void PerformClick(UIMenu parentMenu, bool ignoreEnabledState)
        {

        }

        /// <inheritdoc/>
        public override void Cleanup()
        {

        }

    }
}
