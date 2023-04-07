using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace SharpUI.UIMenu
{
    /// <summary>
    /// A number up/down menu item for the <see cref="UIMenu"/> which with you can increase/decrease values.
    /// </summary>
    /// <typeparam name="T">The target numeric type.</typeparam>
    public class UINumberUpDownItem<T> : UIItemBase where T : IComparable<T>
    {

        #region Variables and Properties
        // Variables
        private string _itemText;
        private T _value;
        private T _increment = (dynamic)((byte)1);

        private Action<UIMenu, UINumberUpDownItem<T>> _onClickAction;

        // Properties
        /// <summary>Gets or set the text of this <see cref="UINumberUpDownItem{T}"/>.</summary>
        public string Text
        {
            get { return _itemText; }
            set { _itemText = value; }
        }

        /// <summary>Gets or sets the current value of the <see cref="UINumberUpDownItem{T}"/>.</summary>
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>Gets or sets the value to increment or decrement the <see cref="UINumberUpDownItem{T}"/>. Default is 1.</summary>
        public T Increment
        {
            get { return _increment; }
            set { _increment = value; }
        }

        /// <summary>Gets or sets the <see cref="Action"/> that should be executed when the user presses this <see cref="UINumberUpDownItem{T}"/>.</summary>
        public Action<UIMenu, UINumberUpDownItem<T>> OnClick
        {
            get { return _onClickAction; }
            set { _onClickAction = value; }
        }
        #endregion

        #region Events
        public delegate void ValueChangedDelegate(T newValue);
        public event ValueChangedDelegate ValueChanged;
        #endregion

        #region Constructor
        public UINumberUpDownItem(object tag, bool enabled, string text, string desc, T value, T increment, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
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
        public UINumberUpDownItem(object tag, string text, string desc, T value, T increment, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            Tag = tag;
            Text = text;
            Description = desc;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(object tag, bool enabled, string text, string desc, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(object tag, string text, string desc, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            Tag = tag;
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }

        public UINumberUpDownItem(bool enabled, string text, string desc, T value, T increment, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Description = desc;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(string text, string desc, T value, T increment, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            Text = text;
            Description = desc;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(bool enabled, string text, string desc, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(string text, string desc, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            Text = text;
            Description = desc;
            Style = style;
            OnClick = onClick;
        }

        public UINumberUpDownItem(object tag, bool enabled, string text, T value, T increment, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(object tag, string text, T value, T increment, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            Tag = tag;
            Text = text;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(object tag, bool enabled, string text, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            Tag = tag;
            IsEnabled = enabled;
            Text = text;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(object tag, string text, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            Tag = tag;
            Text = text;
            Style = style;
            OnClick = onClick;
        }

        public UINumberUpDownItem(bool enabled, string text, T value, T increment, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(string text, T value, T increment, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            Text = text;
            Value = value;
            Increment = increment;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(bool enabled, string text, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
        {
            IsEnabled = enabled;
            Text = text;
            Style = style;
            OnClick = onClick;
        }
        public UINumberUpDownItem(string text, UIItemStyle style, Action<UIMenu, UINumberUpDownItem<T>> onClick)
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
                    gfx.DrawBoxFilled(new Vector2(pos.X, pos.Y), menu.ItemSize, Style.SelectedBackgroundColor);
                    gfx.DrawString(menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.SelectedForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, Value.ToString()).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 19), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    gfx.DrawString(menu.FontOverride, "<", new Point(textRect.Location.X - 12, textRect.Location.Y), Style.SelectedForegroundColor);
                    gfx.DrawString(menu.FontOverride, Value.ToString(), new Rectangle(new Point(textRect.Location.X, textRect.Location.Y), textRect.Size), eD3DFontDrawFlags.Left, Style.SelectedForegroundColor);
                    gfx.DrawString(menu.FontOverride, ">", new Point(textRect.Location.X + textRect.Width, textRect.Location.Y), Style.SelectedForegroundColor);
                }
                else
                {
                    gfx.DrawBoxFilled(new Vector2(pos.X, pos.Y), menu.ItemSize, Style.BackgroundColor);
                    gfx.DrawString(menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.ForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, Value.ToString()).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    gfx.DrawString(menu.FontOverride, Value.ToString(), new Rectangle(textRect.Location, textRect.Size), eD3DFontDrawFlags.Left, Style.ForegroundColor);
                }
            }
            else
            {
                if (IsSelected)
                {
                    gfx.DrawBoxFilled(new Vector2(pos.X, pos.Y), menu.ItemSize, Style.SelectedBackgroundColor);
                    gfx.DrawString(menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.DisabledForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, Value.ToString()).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    gfx.DrawString(menu.FontOverride, Value.ToString(), new Rectangle(textRect.Location, textRect.Size), eD3DFontDrawFlags.Left, Style.DisabledForegroundColor);
                }
                else
                {
                    gfx.DrawBoxFilled(new Vector2(pos.X, pos.Y), menu.ItemSize, Style.BackgroundColor);
                    gfx.DrawString(menu.FontOverride, Text, textRect, eD3DFontDrawFlags.Left | eD3DFontDrawFlags.VerticalCenter, Style.DisabledForegroundColor);

                    // Draw list
                    Size textSize = gfx.MeasureText(menu.FontOverride, Value.ToString()).Size;
                    textRect = new Rectangle(new Point((pos.X + menu.ItemSize.Width) - (textSize.Width + 8), pos.Y + (menu.ItemSize.Height / 2) - 12), textSize);

                    gfx.DrawString(menu.FontOverride, Value.ToString(), new Rectangle(textRect.Location, textRect.Size), eD3DFontDrawFlags.Left, Style.DisabledForegroundColor);
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
                    if (args.KeyCode == menu.Options.NavigateLeft)
                    {
                        Value -= (dynamic)Increment;
                        menu.PlaySound("FRONTEND_MENU_SLIDER_DOWN");
                        ValueChanged?.Invoke(Value);
                    }
                    if (args.KeyCode == menu.Options.NavigateRight)
                    {
                        Value += (dynamic)Increment;
                        menu.PlaySound("FRONTEND_MENU_SLIDER_UP");
                        ValueChanged?.Invoke(Value);
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
