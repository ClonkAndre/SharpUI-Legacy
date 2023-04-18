using System;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

using IVSDKDotNet;
using IVSDKDotNet.Direct3D9;

namespace SharpUI.UIForms
{
    /// <summary>
    /// A progressbar which can be drawn on screen which can be used to show progress or it can act as a slider.
    /// <para>Will later be very useful when window forms are gonna be added to SharpUI.</para>
    /// </summary>
    public class UIProgressBar : UIBase
    {
        #region Variables, Properties and Enums
        // Variables
        private Rectangle m_rRect;
        private Size m_sSize;

        private int m_iMaximum;
        private int m_iValue;

        private bool m_bHasShadow;
        private int m_iShadowOffset;

        private bool m_bShowBorder;
        private float m_fBorderThickness;

        private Orientation m_oOrientation;
        private bool m_bIsVisible;
        private bool m_bIsEnabled;
        private bool m_bIsMouseOver;
        private bool m_bIsSlider;
        private bool m_bMaximumReached;

        private object m_oTag;

        private Color m_cBorderColor;
        private Color m_cBackgroundColor;
        private Color m_cProgressColor;
        private Color m_cDisabledBackgroundColor;
        private Color m_cDisabledProgressColor;

        // Properties
        public bool Visible
        {
            get { return m_bIsVisible; }
            set { m_bIsVisible = value; }
        }
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
        public bool IsSlider
        {
            get { return m_bIsSlider; }
            set { m_bIsSlider = value; }
        }

        public Rectangle ProgressBarRectangle
        {
            get { return m_rRect; }
            private set { m_rRect = value; }
        }
        public Size Size
        {
            get { return m_sSize; }
            set { m_sSize = value; }
        }

        public int Maximum
        {
            get { return m_iMaximum; }
            set { m_iMaximum = value; }
        }
        public int Value
        {
            get { return m_iValue; }
            set { m_iValue = value; }
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

        public Orientation ProgressOrientation
        {
            get { return m_oOrientation; }
            set { m_oOrientation = value; }
        }

        public object Tag
        {
            get { return m_oTag; }
            set { m_oTag = value; }
        }

        public Color BorderColor
        {
            get { return m_cBorderColor; }
            set { m_cBorderColor = value; }
        }
        public Color BackgroundColor
        {
            get { return m_cBackgroundColor; }
            set { m_cBackgroundColor = value; }
        }
        public Color ProgressColor
        {
            get { return m_cProgressColor; }
            set { m_cProgressColor = value; }
        }
        public Color DisabledBackgroundColor
        {
            get { return m_cDisabledBackgroundColor; }
            set { m_cDisabledBackgroundColor = value; }
        }
        public Color DisabledProgressColor
        {
            get { return m_cDisabledProgressColor; }
            set { m_cDisabledProgressColor = value; }
        }

        // Enums
        public enum Orientation
        {
            Horizontal,
            Vertical
        }
        #endregion

        #region Events
        public delegate void ProgressBarDelegate(UIProgressBar sender);
        public delegate void SliderValueChangedDelegate(UIProgressBar sender, int value);

        public event ProgressBarDelegate OnMaximumReached;
        public event SliderValueChangedDelegate OnSliderValueChanged;
        #endregion

        #region Constructor
        public UIProgressBar(bool enabled, bool isSlider, Orientation orientation, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor, Color disabledProgressColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = orientation;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = disabledProgressColor;
        }
        public UIProgressBar(bool enabled, bool isSlider, Orientation orientation, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = orientation;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, bool isSlider, Orientation orientation, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor, Color progressColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = orientation;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, bool isSlider, Orientation orientation, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = orientation;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, bool isSlider, Orientation orientation, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = orientation;

            BorderColor = borderColor;
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, bool isSlider, Orientation orientation, Size size, int maximum, int value, bool showBorder, float borderThickness)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = orientation;

            BorderColor = Color.FromArgb(255, 255, 255, 255);
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }

        public UIProgressBar(bool enabled, bool isSlider, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor, Color disabledProgressColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = disabledProgressColor;
        }
        public UIProgressBar(bool enabled, bool isSlider, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, bool isSlider, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor, Color progressColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, bool isSlider, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, bool isSlider, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, bool isSlider, Size size, int maximum, int value, bool showBorder, float borderThickness)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = isSlider;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = Color.FromArgb(255, 255, 255, 255);
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }

        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor, Color disabledProgressColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = disabledProgressColor;
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor, Color progressColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor, Color backgroundColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, float borderThickness, Color borderColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, float borderThickness)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = borderThickness;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = Color.FromArgb(255, 255, 255, 255);
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }

        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor, Color disabledProgressColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = disabledProgressColor;
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, Color borderColor, Color backgroundColor, Color progressColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, Color borderColor, Color backgroundColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder, Color borderColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, bool showBorder)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = showBorder;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = Color.FromArgb(255, 255, 255, 255);
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }

        public UIProgressBar(bool enabled, Size size, int maximum, int value, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor, Color disabledProgressColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = disabledProgressColor;
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, Color borderColor, Color backgroundColor, Color progressColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, Color borderColor, Color backgroundColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value, Color borderColor)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(bool enabled, Size size, int maximum, int value)
        {
            Visible = true;
            Enabled = enabled;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = Color.FromArgb(255, 255, 255, 255);
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }

        public UIProgressBar(Size size, int maximum, int value, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor, Color disabledProgressColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = disabledProgressColor;
        }
        public UIProgressBar(Size size, int maximum, int value, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, int maximum, int value, Color borderColor, Color backgroundColor, Color progressColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, int maximum, int value, Color borderColor, Color backgroundColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, int maximum, int value, Color borderColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, int maximum, int value)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = value;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = Color.FromArgb(255, 255, 255, 255);
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }

        public UIProgressBar(Size size, int maximum, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor, Color disabledProgressColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = disabledProgressColor;
        }
        public UIProgressBar(Size size, int maximum, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, int maximum, Color borderColor, Color backgroundColor, Color progressColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, int maximum, Color borderColor, Color backgroundColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, int maximum, Color borderColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, int maximum)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = maximum;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = Color.FromArgb(255, 255, 255, 255);
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }

        public UIProgressBar(Size size, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor, Color disabledProgressColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = 100;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = disabledProgressColor;
        }
        public UIProgressBar(Size size, Color borderColor, Color backgroundColor, Color progressColor, Color disabledBackgroundColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = 100;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = disabledBackgroundColor;
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, Color borderColor, Color backgroundColor, Color progressColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = 100;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = progressColor;
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, Color borderColor, Color backgroundColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = 100;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = backgroundColor;
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size, Color borderColor)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = 100;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = borderColor;
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }
        public UIProgressBar(Size size)
        {
            Visible = true;
            Enabled = true;
            IsSlider = false;
            Size = size;
            Maximum = 100;
            Value = 0;
            HasShadow = false;
            ShadowOffset = 5;
            ShowBorder = false;
            BorderThickness = 5f;
            ProgressOrientation = Orientation.Horizontal;

            BorderColor = Color.FromArgb(255, 255, 255, 255);
            BackgroundColor = Color.FromArgb(255, 168, 168, 168);
            ProgressColor = Color.FromArgb(255, 13, 69, 255);
            DisabledBackgroundColor = Color.FromArgb(255, 168, 168, 168);
            DisabledProgressColor = Color.FromArgb(255, 100, 100, 100);
        }

        internal UIProgressBar()
        {

        }
        #endregion

        #region Statics
        public static UIProgressBar Placeholder()
        {
            return new UIProgressBar();
        }
        #endregion

        /// <inheritdoc/>
        public override void Draw(D3DGraphics gfx)
        {
            if (!Visible)
                return;

            Color backgroundColor = Enabled ? BackgroundColor : DisabledBackgroundColor;
            Color progressColor = Enabled ? ProgressColor : DisabledProgressColor;
            ProgressBarRectangle = new Rectangle(Position, Size);

            // Check if mouse is over
            IsMouseOver = CGame.Mouse.IntersectsWith(ProgressBarRectangle);

            // Calculate progressbar progress size based on selected Orientation and do slider calculations if set
            float progress = 0f;
            switch (ProgressOrientation)
            {
                case Orientation.Horizontal:

                    if (IsSlider)
                    {
                        if (CGame.Mouse.IntersectsWith(ProgressBarRectangle))
                        {
                            if (CGame.Mouse.LeftButtonDown)
                            {
                                int v = CGame.Mouse.Position.X - (ProgressBarRectangle.Left - 1);
                                Value = (int)Math.Floor((double)v * (double)Maximum / Size.Width);

                                if (Value > Maximum)
                                    Value = Maximum;
                                if (Value < 0)
                                    Value = 0;

                                OnSliderValueChanged?.Invoke(this, Value);
                            }
                        }
                    }

                    progress = (float)Math.Floor((double)Value / (double)Maximum * Size.Width); // Progress size width

                    // Maximum reached, pretend progressbar from overflowing and invoke event
                    if (progress >= Size.Width)
                    {
                        progress = Size.Width;

                        // Invoke OnMaximumReached event
                        if (!m_bMaximumReached)
                        {
                            OnMaximumReached?.Invoke(this);
                            m_bMaximumReached = true;
                        }
                    }
                    else
                    {
                        m_bMaximumReached = false;
                    }

                    break;
                case Orientation.Vertical:

                    if (IsSlider)
                    {
                        if (CGame.Mouse.IntersectsWith(ProgressBarRectangle))
                        {
                            if (CGame.Mouse.LeftButtonDown)
                            {
                                int v = ProgressBarRectangle.Bottom - CGame.Mouse.Position.Y;
                                Value = (int)Math.Floor((double)v * (double)Maximum / Size.Height);

                                if (Value > Maximum)
                                    Value = Maximum;
                                if (Value < 0)
                                    Value = 0;

                                OnSliderValueChanged?.Invoke(this, Value);
                            }
                        }
                    }

                    progress = (float)Math.Floor((double)Value / (double)Maximum * Size.Height); // Progress size height

                    // Maximum reached, pretend progressbar from overflowing and invoke event
                    if (progress >= Size.Height)
                    {
                        progress = Size.Height;

                        // Invoke OnMaximumReached event
                        if (!m_bMaximumReached)
                        {
                            OnMaximumReached?.Invoke(this);
                            m_bMaximumReached = true;
                        }
                    }
                    else
                    {
                        m_bMaximumReached = false;
                    }

                    break;
            }

            // Draw Shadow
            if (HasShadow)
            {
                if (BackgroundColor.A < 255)
                {

                    Color transparentShadowBrush = Color.Empty;

                    // Shadow for ProgressBar Progress
                    switch (ProgressOrientation)
                    {
                        case Orientation.Horizontal:
                            transparentShadowBrush = Color.FromArgb(ProgressColor.A, Color.Black);
                            gfx.DrawBoxFilled(new Vector2(ProgressBarRectangle.X + ShadowOffset, ProgressBarRectangle.Y + ShadowOffset), new SizeF(progress, ProgressBarRectangle.Height), transparentShadowBrush);
                            //gfx.FillRectangle(transparentShadowBrush, new RectangleF(ProgressBarRectangle.X + ShadowOffset, ProgressBarRectangle.Y + ShadowOffset, progress, ProgressBarRectangle.Height);
                            break;
                        case Orientation.Vertical:
                            transparentShadowBrush = Color.FromArgb(ProgressColor.A, Color.Black);
                            gfx.DrawBoxFilled(new Vector2(ProgressBarRectangle.X + ShadowOffset, (ProgressBarRectangle.Y + Size.Height) - progress + ShadowOffset), new SizeF(ProgressBarRectangle.Width, progress), transparentShadowBrush);
                            //gfx.FillRectangle(transparentShadowBrush, new RectangleF(ProgressBarRectangle.X + ShadowOffset, (ProgressBarRectangle.Y + Size.Height) - progress + ShadowOffset, ProgressBarRectangle.Width, progress);
                            break;
                    }

                    // Background shadow
                    transparentShadowBrush = Color.FromArgb(BackgroundColor.A, Color.Black);
                    gfx.DrawBoxFilled(new Vector2(ProgressBarRectangle.X + ShadowOffset, ProgressBarRectangle.Y + ShadowOffset), new SizeF(!ShowBorder ? ProgressBarRectangle.Width : ProgressBarRectangle.Width + BorderThickness, !ShowBorder ? ProgressBarRectangle.Height : ProgressBarRectangle.Height + BorderThickness), transparentShadowBrush);
                    //gfx.FillRectangle(transparentShadowBrush, new RectangleF(, , );

                }
                else
                {
                    // Background shadow only
                    gfx.DrawBoxFilled(new Vector2(ProgressBarRectangle.X + ShadowOffset, ProgressBarRectangle.Y + ShadowOffset), new SizeF(!ShowBorder ? ProgressBarRectangle.Width : ProgressBarRectangle.Width + BorderThickness, !ShowBorder ? ProgressBarRectangle.Height : ProgressBarRectangle.Height + BorderThickness), Color.Black);
                    //gfx.FillRectangle(Brushes.Black, new RectangleF(, );
                }
            }

            // Draw Border
            if (ShowBorder)
                gfx.DrawBox(new Vector2(ProgressBarRectangle.X, ProgressBarRectangle.Y), new SizeF(ProgressBarRectangle.Width, ProgressBarRectangle.Height), BorderThickness, BorderColor);

            // Draw ProgressBar
            gfx.DrawBoxFilled(new Vector2(ProgressBarRectangle.X, ProgressBarRectangle.Y), new SizeF(ProgressBarRectangle.Width, ProgressBarRectangle.Height), backgroundColor);

            // ProgressBar Progress
            switch (ProgressOrientation)
            {
                case Orientation.Horizontal:
                    gfx.DrawBoxFilled(new Vector2(ProgressBarRectangle.X, ProgressBarRectangle.Y), new SizeF(progress, ProgressBarRectangle.Height), progressColor);
                    break;
                case Orientation.Vertical:
                    gfx.DrawBoxFilled(new Vector2(ProgressBarRectangle.X, (ProgressBarRectangle.Y + Size.Height) - progress), new SizeF(ProgressBarRectangle.Width, progress), progressColor);
                    break;
            }
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
