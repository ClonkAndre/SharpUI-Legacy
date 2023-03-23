using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Changes the visibility of every item in this <see cref="UIPool"/> but only of the given type.
        /// This allows you to easily show/hide, for example, all of your <see cref="UIMenu.UIMenu"/>'s at once.
        /// </summary>
        /// <typeparam name="T">The type you want to target.</typeparam>
        /// <param name="visible">The new visibility for every item in this <see cref="UIPool"/> that match the type.</param>
        public void ChangeVisibilityOfEveryElementOfType<T>(bool visible)
        {
            Items.ForEach(x => {
                if (x.GetType() == typeof(T)) x.SetVisibility(visible);
            });
        }
        #endregion

        #region Functions
        /// <summary>
        /// Gets all items of this type from the <see cref="Items"/> list of this <see cref="UIPool"/>.
        /// </summary>
        /// <typeparam name="T">The type you want to target.</typeparam>
        /// <returns>An array of all items in this <see cref="UIPool"/> that match the given type.</returns>
        public UIBase[] GetItemsOfType<T>()
        {
            return Items.Where(x => x.GetType() == typeof(T)).ToArray();
        }

        /// <summary>
        /// Gets if any item of the given type in this <see cref="UIPool"/> is visible or not.
        /// </summary>
        /// <typeparam name="T">The type you want to target.</typeparam>
        /// <returns>True if there is an item in this <see cref="UIPool"/> of the given type which is visible. Otherwise false.</returns>
        public bool IsAnyItemOfTypeVisible<T>()
        {
            return Items.Any(x => x.GetType() == typeof(T) && x.IsVisible);
        }

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
        /// Responsible for drawing the all items in this <see cref="UIPool"/>.
        /// </summary>
        /// <param name="gfx">The <see cref="D3DGraphics"/> object needed for drawing.</param>
        public void ProcessDrawing(D3DGraphics gfx)
        {
            if (gfx == null)
                return;

            Items.ForEach(x => x.Draw(gfx));
        }

        /// <summary>
        /// Responsible for handling key down events.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> from the <see cref="IVSDKDotNet.Script.KeyDown"/> method.</param>
        /// <param name="isKeyUpEvent">If this method is being called from the <see cref="IVSDKDotNet.Script.KeyUp"/> event, this should be set to true.</param>
        /// <param name="shouldBeUsedForNavigation">Sets if this key press should be used for navigating. When called from both, the <see cref="IVSDKDotNet.Script.KeyUp"/> or <see cref="IVSDKDotNet.Script.KeyDown"/> event, you should only set this to true once.</param>
        public void ProcessKeys(KeyEventArgs args, bool isKeyUpEvent, bool shouldBeUsedForNavigation)
        {
            if (args == null)
                return;

            for (int i = 0; i < Items.Count; i++)
            {
                UIBase item = Items[i];
                if (item.HasFocus)
                {
                    item.KeyPress(args, isKeyUpEvent, shouldBeUsedForNavigation);
                    break;
                }
            }
        }

    }
}
