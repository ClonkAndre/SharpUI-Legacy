using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace SharpUI.UIMenu {
    /// <summary>
    /// Default menu item for the <see cref="UIMenu"/>.
    /// </summary>
    public class UIItem : UIItemBase {

        #region Variables and Properties
        // Variables
        private string _itemText, _itemRightText;
        private D3DResource _icon;
        private Action<UIMenu, UIItem> _onClickAction;

        // Properties
        /// <summary>Gets or sets the text of this <see cref="UIItem"/>.</summary>
        public string Text
        {
            get { return _itemText; }
            set { _itemText = value; }
        }
        /// <summary>Gets or sets the text that should be drawn at the right sise of this <see cref="UIItem"/>.</summary>
        public string RightText
        {
            get { return _itemRightText; }
            set { _itemRightText = value; }
        }

        /// <summary>
        /// Gets or sets the icon that should be drawn on the right side of the <see cref="UIItem"/>.
        /// </summary>
        public D3DResource Icon
        {
            get { return _icon; }
            set {
                if (value == null || value.DXType == eD3D9ResourceType.Font)
                    return;

                _icon = value;
            }
        }

        /// <summary>Gets or sets the <see cref="Action"/> that should be executed when the user presses this <see cref="UIItem"/>.</summary>
        public Action<UIMenu, UIItem> OnClick
        {
            get { return _onClickAction; }
            set { _onClickAction = value; }
        }
        #endregion

        #region Constructor
        public UIItem(object tag, bool enabled, D3DResource icon, string text, string desc, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Icon = icon;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(object tag, bool enabled, string text, string desc, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Icon = null;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(object tag, D3DResource icon, string text, string desc, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Tag = tag;
            Text = text;
            Icon = icon;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(object tag, string text, string desc, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Tag = tag;
            Text = text;
            Icon = null;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(bool enabled, D3DResource icon, string text, string desc, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Icon = icon;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(bool enabled, string text, string desc, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Icon = null;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(D3DResource icon, string text, string desc, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Text = text;
            Icon = icon;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(string text, string desc, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Text = text;
            Icon = null;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }

        public UIItem(object tag, bool enabled, D3DResource icon, string text, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Icon = icon;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(object tag, bool enabled, string text, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Icon = null;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(object tag, D3DResource icon, string text, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Tag = tag;
            Text = text;
            Icon = icon;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(object tag, string text, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Tag = tag;
            Text = text;
            Icon = null;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(bool enabled, D3DResource icon, string text, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Icon = icon;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(bool enabled, string text, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Icon = null;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(D3DResource icon, string text, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Text = text;
            Icon = icon;
            Style = style;
            OnClick = onClick;
        }
        public UIItem(string text, UIItemStyle style, Action<UIMenu, UIItem> onClick)
        {
            Text = text;
            Icon = null;
            Style = style;
            OnClick = onClick;
        }
        #endregion

        /// <inheritdoc/>
        public override void Draw(UIMenu menu, D3DGraphics gfx, Point pos)
        {
            Rectangle textRect = new Rectangle(new Point(pos.X + 5, pos.Y), new Size(menu.ItemSize.Width - 10, menu.ItemSize.Height));

            if (IsEnabled)
            {
                if (IsSelected)
                {
                    gfx.DrawBoxFilled(new Vector2(pos.X, pos.Y), menu.ItemSize, Style.SelectedBackgroundColor);
                    gfx.DrawString(menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.SelectedForegroundColor);

                    // Draw text and icon
                    if (!string.IsNullOrWhiteSpace(RightText))
                    {
                        Rectangle rect = new Rectangle(textRect.X, textRect.Y, textRect.Width, textRect.Height);
                        if (Icon != null)
                        {
                            rect = new Rectangle(textRect.X, textRect.Y, textRect.Width - textRect.Height, textRect.Height);
                            gfx.DrawTexture(Icon, new RectangleF((pos.X + menu.ItemSize.Width - 2f) - menu.ItemSize.Height, pos.Y + 2f, menu.ItemSize.Height, menu.ItemSize.Height - 2f));
                        }
                        gfx.DrawString(menu.FontOverride, RightText, rect, eD3DFontDrawFlags.Right | eD3DFontDrawFlags.VerticalCenter, Style.SelectedForegroundColor);
                    }
                    else
                    {
                        if (Icon != null)
                            gfx.DrawTexture(Icon, new RectangleF((pos.X + menu.ItemSize.Width - 2f) - menu.ItemSize.Height, pos.Y + 2f, menu.ItemSize.Height, menu.ItemSize.Height - 2f));
                    }
                }
                else
                {
                    gfx.DrawBoxFilled(new Vector2(pos.X, pos.Y), menu.ItemSize, Style.BackgroundColor);
                    gfx.DrawString(menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.ForegroundColor);

                    // Draw text and icon
                    if (!string.IsNullOrWhiteSpace(RightText))
                    {
                        Rectangle rect = new Rectangle(textRect.X, textRect.Y, textRect.Width, textRect.Height);
                        if (Icon != null)
                        {
                            rect = new Rectangle(textRect.X, textRect.Y, textRect.Width - textRect.Height, textRect.Height);
                            gfx.DrawTexture(Icon, new RectangleF((pos.X + menu.ItemSize.Width - 2f) - menu.ItemSize.Height, pos.Y + 2f, menu.ItemSize.Height, menu.ItemSize.Height - 2f));
                        }
                        gfx.DrawString(menu.FontOverride, RightText, rect, eD3DFontDrawFlags.Right | eD3DFontDrawFlags.VerticalCenter, Style.ForegroundColor);
                    }
                    else
                    {
                        if (Icon != null)
                            gfx.DrawTexture(Icon, new RectangleF((pos.X + menu.ItemSize.Width - 2f) - menu.ItemSize.Height, pos.Y + 2f, menu.ItemSize.Height, menu.ItemSize.Height - 2f));
                    }
                }
            }
            else
            {
                if (IsSelected)
                {
                    gfx.DrawBoxFilled(new Vector2(pos.X, pos.Y), menu.ItemSize, Style.SelectedBackgroundColor);
                    gfx.DrawString(menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.DisabledForegroundColor);

                    // Draw text and icon
                    if (!string.IsNullOrWhiteSpace(RightText))
                    {
                        Rectangle rect = new Rectangle(textRect.X, textRect.Y, textRect.Width, textRect.Height);
                        if (Icon != null)
                        {
                            rect = new Rectangle(textRect.X, textRect.Y, textRect.Width - textRect.Height, textRect.Height);
                            gfx.DrawTexture(Icon, new RectangleF((pos.X + menu.ItemSize.Width - 2f) - menu.ItemSize.Height, pos.Y + 2f, menu.ItemSize.Height, menu.ItemSize.Height - 2f));
                        }
                        gfx.DrawString(menu.FontOverride, RightText, rect, eD3DFontDrawFlags.Right | eD3DFontDrawFlags.VerticalCenter, Style.DisabledForegroundColor);
                    }
                    else
                    {
                        if (Icon != null)
                            gfx.DrawTexture(Icon, new RectangleF((pos.X + menu.ItemSize.Width - 2f) - menu.ItemSize.Height, pos.Y + 2f, menu.ItemSize.Height, menu.ItemSize.Height - 2f));
                    }
                }
                else
                {
                    gfx.DrawBoxFilled(new Vector2(pos.X, pos.Y), menu.ItemSize, Style.BackgroundColor);
                    gfx.DrawString(menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.DisabledForegroundColor);

                    // Draw text and icon
                    if (!string.IsNullOrWhiteSpace(RightText))
                    {
                        Rectangle rect = new Rectangle(textRect.X, textRect.Y, textRect.Width, textRect.Height);
                        if (Icon != null)
                        {
                            rect = new Rectangle(textRect.X, textRect.Y, textRect.Width - textRect.Height, textRect.Height);
                            gfx.DrawTexture(Icon, new RectangleF((pos.X + menu.ItemSize.Width - 2f) - menu.ItemSize.Height, pos.Y + 2f, menu.ItemSize.Height, menu.ItemSize.Height - 2f));
                        }
                        gfx.DrawString(menu.FontOverride, RightText, rect, eD3DFontDrawFlags.Right | eD3DFontDrawFlags.VerticalCenter, Style.DisabledForegroundColor);
                    }
                    else
                    {
                        if (Icon != null)
                            gfx.DrawTexture(Icon, new RectangleF((pos.X + menu.ItemSize.Width - 2f) - menu.ItemSize.Height, pos.Y + 2f, menu.ItemSize.Height, menu.ItemSize.Height - 2f));
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override void KeyPress(UIMenu menu, KeyEventArgs args, bool isKeyUpEvent, bool shouldBeUsedForNavigation)
        {
            if (IsEnabled)
            {
                if (shouldBeUsedForNavigation)
                {
                    if (args.KeyCode == menu.Options.AcceptKey)
                    {
                        menu.PlaySelectSound();
                        OnClick?.Invoke(menu, this);
                    }
                }
            }
            else
            {
                if (args.KeyCode == menu.Options.AcceptKey)
                    menu.PlayErrorSound();
            }
        }

        /// <inheritdoc/>
        public override void PerformClick(UIMenu parentMenu, bool ignoreEnabledState)
        {
            if (ignoreEnabledState)
            {
                OnClick?.Invoke(parentMenu, this);
            }
            else
            {
                if (IsEnabled)
                {
                    OnClick?.Invoke(parentMenu, this);
                }
            }
        }

        /// <inheritdoc/>
        public override void Cleanup()
        {

        }

    }
}
