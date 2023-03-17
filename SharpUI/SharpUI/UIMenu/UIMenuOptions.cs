using System;
using System.Windows.Forms;

namespace SharpUI.UIMenu {
    /// <summary>
    /// Options for the <see cref="UIMenu"/>.
    /// </summary>
    public class UIMenuOptions {

        #region Variables and Properties
        // Variables
        private Keys navUp, navDown;
        private Keys navLeft, navRight;
        private Keys aKey;

        // Properties
        /// <summary>
        /// Key for navigating up in the <see cref="UIMenu"/>.
        /// </summary>
        public Keys NavigateUp
        {
            get { return navUp; }
            set { navUp = value; }
        }

        /// <summary>
        /// Key for navigating down in the <see cref="UIMenu"/>.
        /// </summary>
        public Keys NavigateDown
        {
            get { return navDown; }
            set { navDown = value; }
        }

        /// <summary>
        /// Key for navigating left in the <see cref="UIMenu"/>.
        /// </summary>
        public Keys NavigateLeft
        {
            get { return navLeft; }
            set { navLeft = value; }
        }

        /// <summary>
        /// Key for navigating right in the <see cref="UIMenu"/>.
        /// </summary>
        public Keys NavigateRight
        {
            get { return navRight; }
            set { navRight = value; }
        }

        /// <summary>
        /// Key for clicking on items in a <see cref="UIMenu"/>.
        /// </summary>
        public Keys AcceptKey
        {
            get { return aKey; }
            set { aKey = value; }
        }
        #endregion

        #region Constructor
        public UIMenuOptions(Keys up, Keys down, Keys left, Keys right, Keys acceptKey)
        {
            NavigateUp = up;
            NavigateDown = down;
            NavigateLeft = left;
            NavigateRight = right;
            AcceptKey = acceptKey;
        }
        #endregion

        /// <summary>
        /// Gets the default <see cref="UIMenuOptions"/>.
        /// Default navigation controls are Numpad 2, 8, 4, 6 and 5.
        /// </summary>
        /// <returns>The default <see cref="UIMenuOptions"/>.</returns>
        public static UIMenuOptions Default()
        {
            return new UIMenuOptions(Keys.NumPad8, Keys.NumPad2, Keys.NumPad4, Keys.NumPad6, Keys.NumPad5);
        }

    }
}
