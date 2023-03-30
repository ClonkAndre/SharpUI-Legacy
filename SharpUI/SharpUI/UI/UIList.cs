using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

namespace SharpUI.UI
{
    /// <summary>
    /// An advanced list which can draw multiple, fully customizable items at once on the screen.
    /// </summary>
    public class UIList : UIBase
    {
        #region Variables and Properties
        // Variables
        /// <summary>Collection of all items in this <see cref="UIList"/>.</summary>
        public List<Entry> Items;

        private int _itemDistance;
        private D3DResource _fontOverride;

        // Properties
        /// <summary>Gets or sets the distance between the items. Default is 0.</summary>
        public int DistanceBetweenItems
        {
            get { return _itemDistance; }
            set { _itemDistance = value; }
        }

        /// <summary>
        /// Gets or sets the overriden font for this <see cref="UIList"/>.<br/>
        /// If null, the default font of IV-SDK .NET will be used to draw all the items in this <see cref="UIList"/>.
        /// </summary>
        public D3DResource FontOverride
        {
            get { return _fontOverride; }
            set { _fontOverride = value; }
        }
        #endregion

        #region Classes
        /// <summary>
        /// The <see cref="Entry"/> for the <see cref="UIList"/>.
        /// </summary>
        public class Entry
        {
            #region Variables and Properties
            // Variables
            private bool _isSpacer;
            private bool _canBeDrawn = true;
            private string _text;
            private Color _color;

            private Func<Entry, string> _func;

            // Properties
            /// <summary>Gets if this <see cref="Entry"/> is used as a spacer.</summary>
            public bool IsSpacer
            {
                get { return _isSpacer; }
                private set { _isSpacer = value; }
            }
            /// <summary>
            /// Gets or sets if this <see cref="Entry"/> can be drawn on screen.
            /// </summary>
            public bool CanBeDrawn
            {
                get { return _canBeDrawn; }
                set { _canBeDrawn = value; }
            }

            /// <summary>Gets or sets the text of this <see cref="Entry"/>.</summary>
            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }
            /// <summary>Gets or sets the color in which the text of this <see cref="Entry"/> should be drawn.</summary>
            public Color Color
            {
                get { return _color; }
                set { _color = value; }
            }

            /// <summary>Gets your custom function for this <see cref="Entry"/>.</summary>
            public Func<Entry, string> Func
            {
                get { return _func; }
                private set { _func = value; }
            }
            #endregion

            #region Constructor
            /// <summary>
            /// Creates a new <see cref="Entry"/> with a custom function that returns a string which will end up being drawed on screen with the specified color.
            /// </summary>
            /// <param name="color">The color that the text returned by your custom function of this <see cref="Entry"/> will be drawn on screen.</param>
            /// <param name="func">Your custom function which returns a string which will end up being drawed on screen.</param>
            public Entry(Color color, Func<Entry, string> func)
            {
                IsSpacer = false;
                Text = string.Empty;
                Color = color;
                Func = func;
            }
            /// <summary>
            /// Creates a new <see cref="Entry"/> with a custom function that returns a string which will end up being drawed on screen.
            /// </summary>
            /// <param name="func">Your custom function which returns a string which will end up being drawed on screen.</param>
            public Entry(Func<Entry, string> func)
            {
                IsSpacer = false;
                Text = string.Empty;
                Color = Color.White;
                Func = func;
            }

            /// <summary>
            /// Creates a new <see cref="Entry"/> with simple text and a custom color.
            /// </summary>
            /// <param name="color">The color that the text of this <see cref="Entry"/> will be drawn on screen.</param>
            /// <param name="text">The text of this <see cref="Entry"/> which will be drawn on screen.</param>
            public Entry(Color color, string text)
            {
                IsSpacer = false;
                Text = text;
                Color = color;
                Func = null;
            }
            /// <summary>
            /// Creates a new <see cref="Entry"/> with simple text.
            /// </summary>
            /// <param name="text">The text of this <see cref="Entry"/> which will be drawn on screen.</param>
            public Entry(string text)
            {
                IsSpacer = false;
                Text = text;
                Color = Color.White;
                Func = null;
            }

            internal Entry()
            {
                IsSpacer = true;
            }
            #endregion

            #region Static
            /// <summary>
            /// Used as a spacer.
            /// </summary>
            /// <returns>An empty <see cref="Entry"/> item which can be used as a spacer item.</returns>
            public static Entry Spacer()
            {
                return new Entry();
            }
            #endregion
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="UIList"/>.
        /// </summary>
        /// <param name="tag">The tag of this <see cref="UIList"/>.</param>
        /// <param name="position">The position of this <see cref="UIList"/>.</param>
        /// <param name="items">The items you want to create this <see cref="UIList"/> with.</param>
        public UIList(object tag, Point position, Entry[] items)
        {
            Tag = tag;
            Position = position;
            IsVisible = true;

            Items = new List<Entry>();
            if (items != null) Items.AddRange(items);
        }

        /// <summary>
        /// Creates a new <see cref="UIList"/>.
        /// </summary>
        /// <param name="tag">The tag of this <see cref="UIList"/>.</param>
        /// <param name="position">The position of this <see cref="UIList"/>.</param>
        public UIList(object tag, Point position)
        {
            Tag = tag;
            Position = position;
            IsVisible = true;

            Items = new List<Entry>();
        }

        /// <summary>
        /// Creates a new <see cref="UIList"/>.
        /// </summary>
        /// <param name="position">The position of this <see cref="UIList"/>.</param>
        /// <param name="items">The items you want to create this <see cref="UIList"/> with.</param>
        public UIList(Point position, Entry[] items)
        {
            Items = new List<Entry>();
            Position = position;
            IsVisible = true;

            Items = new List<Entry>();
            if (items != null) Items.AddRange(items);
        }

        /// <summary>
        /// Creates a new <see cref="UIList"/>.
        /// </summary>
        /// <param name="position">The position of this <see cref="UIList"/>.</param>
        public UIList(Point position)
        {
            Position = position;
            IsVisible = true;

            Items = new List<Entry>();
        }
        #endregion

        /// <inheritdoc/>
        public override void Draw(D3DGraphics gfx)
        {
            if (Items == null)
                return;
            
            if (IsVisible)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    Entry entry = Items[i];

                    if (!entry.CanBeDrawn)
                        continue;
                    if (entry.IsSpacer)
                        continue;

                    string text = entry.Func != null ? entry.Func.Invoke(entry) : entry.Text;
                    Size textSize = gfx.MeasureText(FontOverride, text).Size;
                    gfx.DrawString(text, new Point(Position.X, Position.Y + (textSize.Height + DistanceBetweenItems) * i), entry.Color);
                }
            }
        }

        /// <inheritdoc/>
        public override void KeyPress(KeyEventArgs args, bool isKeyUpEvent, bool shouldBeUsedForNavigation)
        {

        }

        /// <inheritdoc/>
        public override void Cleanup()
        {
            if (Items != null)
            {
                Items.Clear();
                Items = null;
            }
        }

    }
}
