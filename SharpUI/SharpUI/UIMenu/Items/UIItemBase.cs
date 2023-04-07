using System.Drawing;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

namespace SharpUI.UIMenu {
    /// <summary>
    /// Base class for all items.
    /// You can create custom menu items using the <see cref="UIItemBase"/>.
    /// </summary>
    public abstract class UIItemBase {

        #region Variables and Properties
        // Variables
        private string _desc;
        private bool _isEnabled = true;
        private bool _isSelected;
        private UIItemStyle _style;
        private object _tag;

        // Properties
        /// <summary>Gets or sets the description of this item.</summary>
        public string Description
        {
            get { return _desc; }
            set { _desc = value; }
        }

        /// <summary>Gets or sets if this item is enabled or not.</summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        /// <summary>Gets or sets if this item is selected or not.</summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        /// <summary>
        /// Gets or sets the style of this item.
        /// </summary>
        public UIItemStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary>
        /// Gets or sets the tag of this item.
        /// This can be used to store custom data for this item.
        /// </summary>
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        #endregion

        /// <summary>
        /// Responsible for drawing the item.
        /// </summary>
        /// <param name="menu">Reference to the <see cref="UIMenu"/>.</param>
        /// <param name="gfx">The <see cref="D3DGraphics"/> object needed for drawing.</param>
        /// <param name="pos">The position of the item on screen.</param>
        public abstract void Draw(UIMenu menu, D3DGraphics gfx, Point pos);

        /// <summary>
        /// Responsible for handling key presses.
        /// </summary>
        /// <param name="menu">Reference to the <see cref="UIMenu"/>.</param>
        /// <param name="args">The <see cref="KeyEventArgs"/> from either the <see cref="IVSDKDotNet.Script.KeyUp"/> or <see cref="IVSDKDotNet.Script.KeyDown"/> method.</param>
        /// <param name="isKeyUpEvent">If this method is being called from the <see cref="IVSDKDotNet.Script.KeyUp"/> event, this should be set to true.</param>
        /// <param name="shouldBeUsedForNavigation">Sets if this key press should be used for navigating. When called from both, the <see cref="IVSDKDotNet.Script.KeyUp"/> or <see cref="IVSDKDotNet.Script.KeyDown"/> event, you should only set this to true once.</param>
        public abstract void KeyPress(UIMenu menu, KeyEventArgs args, bool isKeyUpEvent, bool shouldBeUsedForNavigation);

        /// <summary>
        /// Generates a click event for the item.
        /// </summary>
        /// <param name="parentMenu">Reference to the parent <see cref="UIMenu"/> the item belongs to.</param>
        /// <param name="ignoreEnabledState">Sets if the <see cref="IsEnabled"/> check should be ignored.</param>
        public abstract void PerformClick(UIMenu parentMenu, bool ignoreEnabledState);

        /// <summary>
        /// Performs some cleaning up actions for the item.
        /// </summary>
        public abstract void Cleanup();

    }
}
