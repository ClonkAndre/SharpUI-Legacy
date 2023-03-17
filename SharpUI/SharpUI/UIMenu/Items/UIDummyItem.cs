using System.Drawing;
using System.Windows.Forms;
using IVSDKDotNet.Direct3D9;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace SharpUI.UIMenu {
    /// <summary>
    /// Dummy item.
    /// </summary>
    public class UIDummyItem : UIItemBase {

        public override void Draw(UIMenu menu, D3DGraphics gfx, Point pos)
        {

        }
        public override void KeyPress(UIMenu menu, KeyEventArgs args)
        {

        }

        /// <inheritdoc/>
        public override void Cleanup()
        {

        }

    }
}
