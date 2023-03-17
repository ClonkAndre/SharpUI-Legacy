using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace SharpUI.UIMenu {
    /// <summary>
    /// List menu item for the <see cref="UIMenu"/>.
    /// </summary>
    public class UIListItem<T> : UIItemBase {

        #region Variables and Properties
        // Variables
        private string _itemText;
        private int _maxItemWidth = 210;

        /// <summary>Collection of all items in this <see cref="UIListItem{T}"/>.</summary>
        public List<T> Items;
        private int _selectedIndex;

        private Action<UIMenu, UIListItem<T>> _onClickAction;

        // Properties
        /// <summary>Gets or set the text of this <see cref="UIItem"/>.</summary>
        public string Text
        {
            get { return _itemText; }
            set { _itemText = value; }
        }

        /// <summary>
        /// Gets or sets how big the item text is allowed to be.<br/>
        /// <para>Default value: 210.</para>
        /// </summary>
        public int MaxItemWidth
        {
            get { return _maxItemWidth; }
            set { _maxItemWidth = value; }
        }

        /// <summary>Gets or sets the currently selected index of the <see cref="Items"/> list for the <see cref="UIListItem{T}"/> item.</summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }
        /// <summary>Gets the currently selected item of the <see cref="UIListItem{T}"/> item.</summary>
        public string SelectedText
        {
            get {
                if (Items.Count == 0)
                    return string.Empty;

                return Items[SelectedIndex].ToString();
            }
        }

        /// <summary>Gets or sets the <see cref="Action"/> that should be executed when the user presses this <see cref="UIItem"/>.</summary>
        public Action<UIMenu, UIListItem<T>> OnClick
        {
            get { return _onClickAction; }
            set { _onClickAction = value; }
        }
        #endregion

        #region Events
        public delegate void SelectedItemChangedDelegate(int newIndex, string selectedItem);
        public event SelectedItemChangedDelegate SelectedItemChanged;
        #endregion

        #region Constructor
        public UIListItem(object tag, bool enabled, string text, string desc, UIItemStyle style, Action<UIMenu, UIListItem<T>> onClick, T[] items)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;

            Items = new List<T>();
            if (items != null) Items.AddRange(items);
        }
        public UIListItem(object tag, string text, string desc, UIItemStyle style, Action<UIMenu, UIListItem<T>> onClick, T[] items)
        {
            Tag = tag;
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;

            Items = new List<T>();
            if (items != null) Items.AddRange(items);
        }
        public UIListItem(bool enabled, string text, string desc, UIItemStyle style, Action<UIMenu, UIListItem<T>> onClick, T[] items)
        {
            IsEnabled = enabled;
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;

            Items = new List<T>();
            if (items != null) Items.AddRange(items);
        }
        public UIListItem(string text, string desc, UIItemStyle style, Action<UIMenu, UIListItem<T>> onClick, T[] items)
        {
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;

            Items = new List<T>();
            if (items != null) Items.AddRange(items);
        }

        public UIListItem(object tag, bool enabled, string text, UIItemStyle style, Action<UIMenu, UIListItem<T>> onClick, T[] items)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Style = style;
            OnClick = onClick;

            Items = new List<T>();
            if (items != null) Items.AddRange(items);
        }
        public UIListItem(object tag, string text, UIItemStyle style, Action<UIMenu, UIListItem<T>> onClick, T[] items)
        {
            Tag = tag;
            Text = text;
            Style = style;
            OnClick = onClick;

            Items = new List<T>();
            if (items != null) Items.AddRange(items);
        }
        public UIListItem(bool enabled, string text, UIItemStyle style, Action<UIMenu, UIListItem<T>> onClick, T[] items)
        {
            IsEnabled = enabled;
            Text = text;
            Style = style;
            OnClick = onClick;

            Items = new List<T>();
            if (items != null) Items.AddRange(items);
        }
        public UIListItem(string text, UIItemStyle style, Action<UIMenu, UIListItem<T>> onClick, T[] items)
        {
            Text = text;
            Style = style;
            OnClick = onClick;

            Items = new List<T>();
            if (items != null) Items.AddRange(items);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Navigates to the previous item in the <see cref="Items"/> list of this <see cref="UIListItem{T}"/>.
        /// </summary>
        public void NavigateLeft()
        {
            if (Items.Count != 0)
            {
                if (SelectedIndex == 0)
                {
                    SelectedIndex = Items.Count - 1;
                }
                else
                {
                    SelectedIndex--;
                }
                SelectedItemChanged?.Invoke(SelectedIndex, SelectedText);
            }
        }

        /// <summary>
        /// Navigates to the next item in the <see cref="Items"/> list of this <see cref="UIListItem{T}"/>.
        /// </summary>
        public void NavigateRight()
        {
            if (Items.Count != 0)
            {
                if (SelectedIndex == (Items.Count - 1))
                {
                    SelectedIndex = 0;
                }
                else
                {
                    SelectedIndex++;
                }
                SelectedItemChanged?.Invoke(SelectedIndex, SelectedText);
            }
        }
        #endregion

        public override void Draw(UIMenu menu, D3DGraphics gfx, Point pos)
        {
            Rectangle textRect = new Rectangle(new Point(pos.X + 5, pos.Y), new Size(menu.ItemSize.Width - 10, menu.ItemSize.Height));

            if (IsEnabled)
            {
                if (IsSelected)
                {
                    gfx.DrawBoxFilled(gfx.Device, new Vector2(pos.X, pos.Y), menu.ItemSize, Style.SelectedBackgroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.SelectedForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, SelectedText).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 19), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    if (textRect.Width > MaxItemWidth)
                        textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (MaxItemWidth + 19), pos.Y + (menu.ItemSize.Height / 2) - 12), new Size(MaxItemWidth, textSize.Height));

                    gfx.DrawString(gfx.Device, menu.FontOverride, "<", new Point(textRect.Location.X - 12, textRect.Location.Y), Style.SelectedForegroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, SelectedText, new Rectangle(new Point(textRect.Location.X, textRect.Location.Y), textRect.Size), eD3DFontDrawFlags.Left, Style.SelectedForegroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, ">", new Point(textRect.Location.X + textRect.Width, textRect.Location.Y), Style.SelectedForegroundColor);
                }
                else
                {
                    gfx.DrawBoxFilled(gfx.Device, new Vector2(pos.X, pos.Y), menu.ItemSize, Style.BackgroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.ForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, SelectedText).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    if (textRect.Width > MaxItemWidth)
                        textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (MaxItemWidth + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), new Size(MaxItemWidth, textSize.Height));

                    gfx.DrawString(gfx.Device, menu.FontOverride, SelectedText, new Rectangle(textRect.Location, textRect.Size), eD3DFontDrawFlags.Left, Style.ForegroundColor);
                }
            }
            else
            {
                if (IsSelected)
                {
                    gfx.DrawBoxFilled(gfx.Device, new Vector2(pos.X, pos.Y), menu.ItemSize, Style.SelectedBackgroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.DisabledForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, SelectedText).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    if (textRect.Width > MaxItemWidth)
                        textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (MaxItemWidth + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), new Size(MaxItemWidth, textSize.Height));

                    gfx.DrawString(gfx.Device, menu.FontOverride, SelectedText, new Rectangle(textRect.Location, textRect.Size), eD3DFontDrawFlags.Left, Style.DisabledForegroundColor);
                }
                else
                {
                    gfx.DrawBoxFilled(gfx.Device, new Vector2(pos.X, pos.Y), menu.ItemSize, Style.BackgroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.DisabledForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, SelectedText).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    if (textRect.Width > MaxItemWidth)
                        textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (MaxItemWidth + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), new Size(MaxItemWidth, textSize.Height));

                    gfx.DrawString(gfx.Device, menu.FontOverride, SelectedText, new Rectangle(textRect.Location, textRect.Size), eD3DFontDrawFlags.Left, Style.DisabledForegroundColor);
                }
            }
        }

        public override void KeyPress(UIMenu menu, KeyEventArgs args)
        {
            if (IsEnabled)
            {
                if (args.KeyCode == menu.Options.AcceptKey)
                {
                    OnClick?.Invoke(menu, this);
                }
                if (args.KeyCode == menu.Options.NavigateLeft)
                {
                    NavigateLeft();
                }
                if (args.KeyCode == menu.Options.NavigateRight)
                {
                    NavigateRight();
                }
            }
        }

        /// <inheritdoc/>
        public override void Cleanup()
        {
            SelectedIndex = 0;
            Items.Clear();
        }

    }
}
