using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace SharpUI.UIMenu {
    /// <summary>
    /// An integer up/down menu item for the <see cref="UIMenu"/> which with you can increase/decrease values.
    /// </summary>
    public class UIIntegerUpDownItem : UIItemBase {

        #region Variables and Properties
        // Variables
        private string _itemText;
        private int _value;
        private int _increment = 1;

        private Action<UIMenu, UIIntegerUpDownItem> _onClickAction;

        // Properties
        /// <summary>Gets or set the text of this <see cref="UIIntegerUpDownItem"/>.</summary>
        public string Text
        {
            get { return _itemText; }
            set { _itemText = value; }
        }

        /// <summary>Gets or sets the current value of the <see cref="UIIntegerUpDownItem"/>.</summary>
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>Gets or sets the value to increment or decrement the <see cref="UIIntegerUpDownItem"/>. Default is 1.</summary>
        public int Increment
        {
            get { return _increment; }
            set { _increment = value; }
        }

        /// <summary>Gets or sets the <see cref="Action"/> that should be executed when the user presses this <see cref="UIIntegerUpDownItem"/>.</summary>
        public Action<UIMenu, UIIntegerUpDownItem> OnClick
        {
            get { return _onClickAction; }
            set { _onClickAction = value; }
        }
        #endregion

        #region Events
        public delegate void ValueChangedDelegate(int newValue);
        public event ValueChangedDelegate ValueChanged;
        #endregion

        #region Constructor
        public UIIntegerUpDownItem(object tag, bool enabled, string text, string desc, int value, int increment, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Description = desc;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(object tag, string text, string desc, int value, int increment, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Tag = tag;
            Text = text;
            Description = desc;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(object tag, bool enabled, string text, string desc, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(object tag, string text, string desc, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Tag = tag;
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }

        public UIIntegerUpDownItem(bool enabled, string text, string desc, int value, int increment, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Description = desc;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(string text, string desc, int value, int increment, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Text = text;
            Description = desc;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(bool enabled, string text, string desc, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(string text, string desc, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }

        public UIIntegerUpDownItem(object tag, bool enabled, string text, int value, int increment, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(object tag, string text, int value, int increment, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Tag = tag;
            Text = text;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(object tag, bool enabled, string text, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(object tag, string text, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Tag = tag;
            Text = text;
            Style = style;
            OnClick = onClick;
        }

        public UIIntegerUpDownItem(bool enabled, string text, int value, int increment, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(string text, int value, int increment, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Text = text;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(bool enabled, string text, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Style = style;
            OnClick = onClick;
        }
        public UIIntegerUpDownItem(string text, UIItemStyle style, Action<UIMenu, UIIntegerUpDownItem> onClick)
        {
            Text = text;
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
                    gfx.DrawBoxFilled(gfx.Device, new Vector2(pos.X, pos.Y), menu.ItemSize, Style.SelectedBackgroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.SelectedForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, Value.ToString()).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 19), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    gfx.DrawString(gfx.Device, menu.FontOverride, "<", new Point(textRect.Location.X - 12, textRect.Location.Y), Style.SelectedForegroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, Value.ToString(), new Rectangle(new Point(textRect.Location.X, textRect.Location.Y), textRect.Size), eD3DFontDrawFlags.Left, Style.SelectedForegroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, ">", new Point(textRect.Location.X + textRect.Width, textRect.Location.Y), Style.SelectedForegroundColor);
                }
                else
                {
                    gfx.DrawBoxFilled(gfx.Device, new Vector2(pos.X, pos.Y), menu.ItemSize, Style.BackgroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.ForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, Value.ToString()).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    gfx.DrawString(gfx.Device, menu.FontOverride, Value.ToString(), new Rectangle(textRect.Location, textRect.Size), eD3DFontDrawFlags.Left, Style.ForegroundColor);
                }
            }
            else
            {
                if (IsSelected)
                {
                    gfx.DrawBoxFilled(gfx.Device, new Vector2(pos.X, pos.Y), menu.ItemSize, Style.SelectedBackgroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.DisabledForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, Value.ToString()).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    gfx.DrawString(gfx.Device, menu.FontOverride, Value.ToString(), new Rectangle(textRect.Location, textRect.Size), eD3DFontDrawFlags.Left, Style.DisabledForegroundColor);
                }
                else
                {
                    gfx.DrawBoxFilled(gfx.Device, new Vector2(pos.X, pos.Y), menu.ItemSize, Style.BackgroundColor);
                    gfx.DrawString(gfx.Device, menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.DisabledForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, Value.ToString()).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    gfx.DrawString(gfx.Device, menu.FontOverride, Value.ToString(), new Rectangle(textRect.Location, textRect.Size), eD3DFontDrawFlags.Left, Style.DisabledForegroundColor);
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
                        OnClick?.Invoke(menu, this);
                    }
                    if (args.KeyCode == menu.Options.NavigateLeft)
                    {
                        Value -= Increment;
                        ValueChanged?.Invoke(Value);
                    }
                    if (args.KeyCode == menu.Options.NavigateRight)
                    {
                        Value += Increment;
                        ValueChanged?.Invoke(Value);
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override void Cleanup()
        {

        }

    }
}
