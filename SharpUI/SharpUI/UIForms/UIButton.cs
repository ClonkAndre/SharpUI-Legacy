using System;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

using IVSDKDotNet;
using IVSDKDotNet.Direct3D9;

namespace SharpUI.UIForms
{
    /// <summary>
    /// A button which can be drawn on screen and interacted with.
    /// <para>Will later be very useful when window forms are gonna be added to SharpUI.</para>
    /// </summary>
    public class UIButton : UIBase
    {

        #region Variables and Properties
        // Variables
        private string m_sText;
        private D3DResource m_fFont;

        private Rectangle m_rRect;
        private Size m_sSize;

        private bool m_bHasShadow;
        private int m_iShadowOffset;

        private bool m_bShowBorder;
        private float m_fBorderThickness;

        private Color m_cTextColor;
        private Color m_cDisabledTextColor;
        private Color m_cBorderColor;
        private Color m_cDefaultColor;
        private Color m_cDisabledColor;
        private Color m_cHoverColor;
        private Color m_cClickedColor;

        private Action<UIButton> m_aClickedAction;
        private Action<UIButton> m_aMouseOverAction;

        private bool m_bIsEnabled;
        private bool m_bIsMouseOver;
        private bool m_bWasClicked;

        private object m_oTag;

        // Properties
        public bool Enabled
        {
            get { return m_bIsEnabled; }
            set { m_bIsEnabled = value; }
        }
        public bool IsMouseOver
        {
            get { return m_bIsMouseOver; }
            private set { m_bIsMouseOver = value; }
        }

        public string Text
        {
            get { return m_sText; }
            set { m_sText = value; }
        }
        public D3DResource Font
        {
            get { return m_fFont; }
            set { m_fFont = value; }
        }

        public Rectangle ButtonRectangle
        {
            get { return new Rectangle(Position, Size); }
            private set { m_rRect = value; }
        }
        public Size Size
        {
            get { return m_sSize; }
            set { m_sSize = value; }
        }

        public bool HasShadow
        {
            get { return m_bHasShadow; }
            set { m_bHasShadow = value; }
        }
        public int ShadowOffset
        {
            get { return m_iShadowOffset; }
            set { m_iShadowOffset = value; }
        }

        public bool ShowBorder
        {
            get { return m_bShowBorder; }
            set { m_bShowBorder = value; }
        }
        public float BorderThickness
        {
            get { return m_fBorderThickness; }
            set { m_fBorderThickness = value; }
        }

        public object Tag
        {
            get { return m_oTag; }
            set { m_oTag = value; }
        }

        public Color TextColor
        {
            get { return m_cTextColor; }
            set { m_cTextColor = value; }
        }
        public Color DisabledTextColor
        {
            get { return m_cDisabledTextColor; }
            set { m_cDisabledTextColor = value; }
        }
        public Color BorderColor
        {
            get { return m_cBorderColor; }
            set { m_cBorderColor = value; }
        }
        public Color DefaultColor
        {
            get { return m_cDefaultColor; }
            set { m_cDefaultColor = value; }
        }
        public Color DisabledColor
        {
            get { return m_cDisabledColor; }
            set { m_cDisabledColor = value; }
        }
        public Color HoverColor
        {
            get { return m_cHoverColor; }
            set { m_cHoverColor = value; }
        }
        public Color ClickedColor
        {
            get { return m_cClickedColor; }
            set { m_cClickedColor = value; }
        }
        #endregion

        #region Events
        public delegate void ButtonDelegate(UIButton sender);
        public event ButtonDelegate Clicked;
        public event ButtonDelegate Released;
        #endregion

        #region Constructor
        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, float borderThickness, Size size, Color textColor, Color borderColor, Color defaultColor, Color hoverColor, Color clickedColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = hoverColor;
            ClickedColor = clickedColor;
            m_aClickedAction = clickedAction;
        }
        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, float borderThickness, Size size, Color textColor, Color borderColor, Color defaultColor, Color hoverColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = hoverColor;
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, float borderThickness, Size size, Color textColor, Color borderColor, Color defaultColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, float borderThickness, Size size, Color textColor, Color borderColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, float borderThickness, Size size, Color textColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = Color.FromArgb(255, 255, 255, 255);
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }

        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, Size size, Color textColor, Color borderColor, Color defaultColor, Color hoverColor, Color clickedColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = hoverColor;
            ClickedColor = clickedColor;
            m_aClickedAction = clickedAction;
        }
        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, Size size, Color textColor, Color borderColor, Color defaultColor, Color hoverColor, Action<UIButton> clickedAction)
        {
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = hoverColor;
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, Size size, Color textColor, Color borderColor, Color defaultColor, Action<UIButton> clickedAction)
        {
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, Size size, Color textColor, Color borderColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, Size size, Color textColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = Color.FromArgb(255, 255, 255, 255);
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }

        public UIButton(string text, D3DResource font, bool showBorder, float borderThickness, Size size, Color textColor, Color borderColor, Color defaultColor, Color hoverColor, Color clickedColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = hoverColor;
            ClickedColor = clickedColor;
            m_aClickedAction = clickedAction;
        }
        public UIButton(string text, D3DResource font, bool showBorder, float borderThickness, Size size, Color textColor, Color borderColor, Color defaultColor, Color hoverColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = hoverColor;
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(string text, D3DResource font, bool showBorder, float borderThickness, Size size, Color textColor, Color borderColor, Color defaultColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(string text, D3DResource font, bool showBorder, float borderThickness, Size size, Color textColor, Color borderColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(string text, D3DResource font, bool showBorder, float borderThickness, Size size, Color textColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = Color.FromArgb(255, 255, 255, 255);
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }

        public UIButton(string text, D3DResource font, bool showBorder, Size size, Color textColor, Color borderColor, Color defaultColor, Color hoverColor, Color clickedColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = hoverColor;
            ClickedColor = clickedColor;
            m_aClickedAction = clickedAction;
        }
        public UIButton(string text, D3DResource font, bool showBorder, Size size, Color textColor, Color borderColor, Color defaultColor, Color hoverColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = hoverColor;
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(string text, D3DResource font, bool showBorder, Size size, Color textColor, Color borderColor, Color defaultColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = defaultColor;
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(string text, D3DResource font, bool showBorder, Size size, Color textColor, Color borderColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = borderColor;
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(string text, D3DResource font, bool showBorder, Size size, Color textColor, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = textColor;
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = Color.FromArgb(255, 255, 255, 255);
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }

        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, float borderThickness, Size size, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = Color.FromArgb(255, 255, 255, 255);
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = Color.FromArgb(255, 255, 255, 255);
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(bool enabled, string text, D3DResource font, bool showBorder, Size size, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = Color.FromArgb(255, 255, 255, 255);
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = Color.FromArgb(255, 255, 255, 255);
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(bool enabled, string text, D3DResource font, Size size, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = enabled;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = true;
            BorderThickness = 5f;

            TextColor = Color.FromArgb(255, 255, 255, 255);
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = Color.FromArgb(255, 255, 255, 255);
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }

        public UIButton(string text, D3DResource font, bool showBorder, float borderThickness, Size size, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;

            TextColor = Color.FromArgb(255, 255, 255, 255);
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = Color.FromArgb(255, 255, 255, 255);
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(string text, D3DResource font, bool showBorder, Size size, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;

            TextColor = Color.FromArgb(255, 255, 255, 255);
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = Color.FromArgb(255, 255, 255, 255);
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }
        public UIButton(string text, D3DResource font, Size size, Action<UIButton> clickedAction)
        {
            IsVisible = true;
            Enabled = true;
            Text = text;
            Font = font;
            Size = size;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = true;
            BorderThickness = 5f;

            TextColor = Color.FromArgb(255, 255, 255, 255);
            DisabledTextColor = Color.FromArgb(255, 150, 150, 150);
            BorderColor = Color.FromArgb(255, 255, 255, 255);
            DefaultColor = Color.FromArgb(255, 168, 168, 168);
            DisabledColor = Color.FromArgb(255, 70, 70, 70);
            HoverColor = Color.FromArgb(255, 13, 69, 255);
            ClickedColor = Color.FromArgb(255, 36, 86, 255);
            m_aClickedAction = clickedAction;
        }

        internal UIButton()
        {

        }
        #endregion

        #region Statics
        public static UIButton Placeholder()
        {
            return new UIButton();
        }
        #endregion

        /// <inheritdoc/>
        public override void Draw(D3DGraphics gfx)
        {
            if (!IsVisible)
                return;

            Color backgroundColor = Enabled ? DefaultColor : DisabledColor;
            ButtonRectangle = new Rectangle(Position, Size);

            // Mouse cursor hovers over button
            if (Enabled)
            {
                if (CGame.Mouse.IntersectsWith(ButtonRectangle))
                {
                    backgroundColor = HoverColor;

                    // Invoke mouse over event
                    if (!IsMouseOver)
                    {
                        m_aMouseOverAction?.Invoke(this);
                        IsMouseOver = true;
                    }

                    // Check if button was clicked
                    if (CGame.Mouse.LeftButtonDown)
                    {
                        backgroundColor = ClickedColor;
                        m_bWasClicked = true;
                    }
                    else
                    {
                        if (m_bWasClicked)
                        {
                            m_aClickedAction?.Invoke(this);
                            Clicked?.Invoke(this);
                            m_bWasClicked = false;
                        }
                    }
                }
                else
                {
                    IsMouseOver = false;
                    if (m_bWasClicked)
                    {
                        Released?.Invoke(this);
                        m_bWasClicked = false;
                    }
                }
            }

            // Draw Shadow
            if (HasShadow) gfx.DrawBoxFilled(new Vector2(ButtonRectangle.X + ShadowOffset, ButtonRectangle.Y + ShadowOffset), new SizeF(!ShowBorder ? ButtonRectangle.Width : ButtonRectangle.Width + BorderThickness, !ShowBorder ? ButtonRectangle.Height : ButtonRectangle.Height + BorderThickness), Color.Black);

            // Draw Border
            if (ShowBorder) gfx.DrawBox(new Vector2(ButtonRectangle.X, ButtonRectangle.Y), new SizeF(ButtonRectangle.Width, ButtonRectangle.Height), BorderThickness, BorderColor);

            // Draw Button
            gfx.DrawBoxFilled(new Vector2(ButtonRectangle.X, ButtonRectangle.Y), new SizeF(ButtonRectangle.Width, ButtonRectangle.Height), backgroundColor);

            // Draw Text
            Size textSize = gfx.MeasureText(Font, Text).Size;
            gfx.DrawString(Font, Text, new Point(ButtonRectangle.X + (ButtonRectangle.Width / 2) - (textSize.Width / 2), ButtonRectangle.Y + (ButtonRectangle.Height / 2) - (textSize.Height / 2)), Enabled ? TextColor : DisabledTextColor);
        }

        /// <inheritdoc/>
        public override void KeyPress(KeyEventArgs args, bool isKeyUpEvent, bool shouldBeUsedForNavigation)
        {

        }

        /// <inheritdoc/>
        public override void Cleanup()
        {

        }

    }
}
