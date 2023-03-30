using System.Drawing;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

namespace SharpUI {
    /// <summary>
    /// Base class for everything that can be drawn on screen. Like an <see cref="UIMenu.UIMenu"/>.
    /// </summary>
    public abstract class UIBase {

        #region Variables and Properties
        // Variables
        private Point _pos;
        private bool _isVisible;
        private bool _hasFocus;
        private object _tag;

        // Properties
        /// <summary>Gets or sets the position of this element.</summary>
        public Point Position
        {
            get { return _pos; }
            set { _pos = value; }
        }

        /// <summary>
        /// Gets if this element should be visible or not.
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            internal set { _isVisible = value; }
        }

        /// <summary>
        /// Gets if this element has currently the focus or not. Use <see cref="SetFocus(bool)"/> to set the focus of this <see cref="UIBase"/> element.
        /// </summary>
        public bool HasFocus
        {
            get { return _hasFocus; }
            internal set { _hasFocus = value; }
        }

        /// <summary>
        /// Gets or sets the tag of this element.
        /// This can be used to store custom data for this element.
        /// </summary>
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        #endregion

        #region Events
        /// <summary>The delegate for the <see cref="FocusChanged"/> event.</summary>
        /// <param name="sender">The <see cref="UIBase"/> item of which the focus changed.</param>
        /// <param name="focused">Is this <see cref="UIBase"/> element focused or not.</param>
        public delegate void FocusChangedDelegate(UIBase sender, bool focused);
        /// <summary>Raised when the focus of this <see cref="UIBase"/> element changes.</summary>
        public event FocusChangedDelegate FocusChanged;

        /// <summary>The delegate for the <see cref="VisibilityChanged"/> event.</summary>
        /// <param name="sender">The <see cref="UIBase"/> item of which the visiblity changed.</param>
        /// <param name="visible">Is this <see cref="UIBase"/> element visible or not.</param>
        public delegate void VisibilityChangedDelegate(UIBase sender, bool visible);
        /// <summary>Raised when the visibility of this <see cref="UIBase"/> element changes.</summary>
        public event VisibilityChangedDelegate VisibilityChanged;
        #endregion

        #region Methods
        /// <summary>
        /// Sets the focus of this <see cref="UIBase"/> element.
        /// <para>Raises the <see cref="FocusChanged"/> event.</para>
        /// </summary>
        /// <param name="focused">Focused or not.</param>
        public void SetFocus(bool focused)
        {
            HasFocus = focused;
            FocusChanged?.Invoke(this, HasFocus);
        }

        /// <summary>
        /// Sets the visibility of this <see cref="UIBase"/> element.
        /// <para>Raises the <see cref="VisibilityChanged"/> event.</para>
        /// </summary>
        /// <param name="visible">Visible or not.</param>
        public void SetVisibility(bool visible)
        {
            IsVisible = visible;
            VisibilityChanged?.Invoke(this, IsVisible);
        }
        #endregion

        /// <summary>
        /// Responsible for drawing the element.
        /// </summary>
        /// <param name="gfx">The <see cref="D3DGraphics"/> object needed for drawing.</param>
        public abstract void Draw(D3DGraphics gfx);

        /// <summary>
        /// Responsible for handling key presses.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> from either the <see cref="IVSDKDotNet.Script.KeyUp"/> or <see cref="IVSDKDotNet.Script.KeyDown"/> event.</param>
        /// <param name="isKeyUpEvent">If this method is being called from the <see cref="IVSDKDotNet.Script.KeyUp"/> event, this should be set to true.</param>
        /// <param name="shouldBeUsedForNavigation">Sets if this key press should be used for navigating. When called from both, the <see cref="IVSDKDotNet.Script.KeyUp"/> or <see cref="IVSDKDotNet.Script.KeyDown"/> event, you should only set this to true once.</param>
        public abstract void KeyPress(KeyEventArgs args, bool isKeyUpEvent, bool shouldBeUsedForNavigation);

        /// <summary>
        /// Performs some cleaning up actions.
        /// <para>Should be called from your <see cref="IVSDKDotNet.Script.Uninitialize"/> event.</para>
        /// </summary>
        public abstract void Cleanup();

    }
}
