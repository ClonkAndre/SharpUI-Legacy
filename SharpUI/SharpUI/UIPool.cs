using System;
using System.Collections.Generic;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

namespace SharpUI {
    /// <summary>
    /// Pool where for example a <see cref="UIMenu.UIMenu"/> can be added to.
    /// </summary>
    public class UIPool {

        #region Variables
        /// <summary>Collection of all items in this <see cref="UIPool"/>.</summary>
        public List<UIBase> Items;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="UIPool"/>.
        /// </summary>
        public UIPool()
        {
            Items = new List<UIBase>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Cleans up everything and removes all items from this <see cref="UIPool"/>.
        /// <para>Should be called from your <see cref="IVSDKDotNet.Script.Uninitialize"/> event.</para>
        /// </summary>
        public void Cleanup()
        {
            if (Items.Count == 0)
                return;

            Items.ForEach(x => x.Cleanup());
            Items.Clear();
        }

        /// <summary>
        /// Changes the visibility of every item in this <see cref="UIPool"/> but only of this <see cref="Type"/>.
        /// This allows you to easily show/hide, for example, all of your <see cref="UIMenu.UIMenu"/>'s at once.
        /// </summary>
        /// <param name="targetType">The type you want to target.</param>
        /// <param name="visible">The new visibility for every item in this <see cref="UIPool"/> that match the type.</param>
        public void ChangeVisibilityOfEveryElementOfType(Type targetType, bool visible)
        {
            Items.ForEach(x => {
                if (x.GetType() == targetType) x.SetVisibility(visible);
            });
        }
        #endregion

        #region Functions
        /// <summary>
        /// Sets if the given item should have the focus or not. If yes, then all other items inside this <see cref="UIPool"/> loose their focus.
        /// </summary>
        /// <param name="item">The item you want to change the focus of.</param>
        /// <param name="focus">The new focus value.</param>
        /// <returns>True if the focus changed. Otherwise false.</returns>
        public bool SetFocus(UIBase item, bool focus)
        {
            if (item == null)
                return false;

            if (!focus)
            {
                item.SetFocus(false);
            }
            else
            {
                // Set focus of each item in the list to false except for the given item.
                Items.ForEach(x => {
                    if (x != item) x.SetFocus(false);
                });

                item.SetFocus(true);
            }

            return true;
        }
        #endregion

        /// <summary>
        /// Responsible for drawing every element in the <see cref="Items"/> list.
        /// </summary>
        /// <param name="gfx">The <see cref="D3DGraphics"/> object needed for drawing.</param>
        public void ProcessDrawing(D3DGraphics gfx)
        {
            if (gfx == null)
                return;

            Items.ForEach(x => x.Draw(gfx));
        }

        /// <summary>
        /// Responsible for handling key presses for every element in the <see cref="Items"/> list.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> from either the <see cref="IVSDKDotNet.Script.KeyDown"/> or <see cref="IVSDKDotNet.Script.KeyUp"/> method.</param>
        public void ProcessKeys(KeyEventArgs args)
        {
            if (args == null)
                return;

            for (int i = 0; i < Items.Count; i++)
            {
                UIBase item = Items[i];
                if (item.HasFocus)
                {
                    item.KeyPress(args);
                    break;
                }
            }
        }

    }
}
