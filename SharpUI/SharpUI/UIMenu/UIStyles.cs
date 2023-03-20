using System;
using System.Drawing;

namespace SharpUI.UIMenu
{

    /// <summary>
    /// Styling class for the subtitle item of the <see cref="UIMenu"/>.
    /// </summary>
    public class UISubtitleItemStyle
    {
        #region Variables and Properties
        // Variables
        private Color _bgColor;
        private Color _fgColor;

        // Properties
        /// <summary>Gets or sets the background color of this item.</summary>
        public Color BackgroundColor
        {
            get { return _bgColor; }
            set { _bgColor = value; }
        }
        /// <summary>Gets or sets the foreground color of this item.</summary>
        public Color ForegroundColor
        {
            get { return _fgColor; }
            set { _fgColor = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="UISubtitleItemStyle"/> styling class.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        /// <param name="foregroundColor">The foreground color.</param>
        public UISubtitleItemStyle(Color backgroundColor, Color foregroundColor)
        {
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
        }
        #endregion

        /// <summary>
        /// Gets the default style for the subtitle item of the <see cref="UIMenu"/>.
        /// </summary>
        /// <returns>The default <see cref="UISubtitleItemStyle"/>.</returns>
        public static UISubtitleItemStyle Default()
        {
            return new UISubtitleItemStyle(Color.Black, Color.White);
        }
    }

    /// <summary>
    /// Styling class for the <see cref="UIItem"/>.
    /// </summary>
    public class UIItemStyle
    {
        #region Variables and Properties
        // Variables
        private Color _bgColor;
        private Color _fgColor;

        private Color _selectedBGColor;
        private Color _selectedFGColor;

        private Color _disabledFGColor;

        // Properties
        /// <summary>Gets or sets the background color of this item.</summary>
        public Color BackgroundColor
        {
            get { return _bgColor; }
            set { _bgColor = value; }
        }
        /// <summary>Gets or sets the foreground color of this item.</summary>
        public Color ForegroundColor
        {
            get { return _fgColor; }
            set { _fgColor = value; }
        }

        /// <summary>Gets or sets the selected background color of this item.</summary>
        public Color SelectedBackgroundColor
        {
            get { return _selectedBGColor; }
            set { _selectedBGColor = value; }
        }
        /// <summary>Gets or sets the selected foreground color of this item.</summary>
        public Color SelectedForegroundColor
        {
            get { return _selectedFGColor; }
            set { _selectedFGColor = value; }
        }

        /// <summary>Gets or sets the disabled foreground color of this item.</summary>
        public Color DisabledForegroundColor
        {
            get { return _disabledFGColor; }
            set { _disabledFGColor = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="UIItemStyle"/> styling class.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        /// <param name="foregroundColor">The foreground color.</param>
        /// <param name="selectedBackgroundColor">The selected background color.</param>
        /// <param name="selectedForegroundColor">The selected foreground color.</param>
        /// <param name="disabledForegroundColor">The disabled foreground color.</param>
        public UIItemStyle(Color backgroundColor,
            Color foregroundColor,
            Color selectedBackgroundColor,
            Color selectedForegroundColor,
            Color disabledForegroundColor)
        {
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            SelectedBackgroundColor = selectedBackgroundColor;
            SelectedForegroundColor = selectedForegroundColor;
            DisabledForegroundColor = disabledForegroundColor;
        }
        #endregion

        /// <summary>
        /// Gets the default style for the <see cref="UIItem"/>.
        /// </summary>
        /// <returns>The default <see cref="UIItemStyle"/>.</returns>
        public static UIItemStyle Default()
        {
            return new UIItemStyle(Color.FromArgb(170, 10, 10, 10),    // Background
                                    Color.White,                       // Foreground
                                    Color.FromArgb(245, 245, 245),     // Selected Background
                                    Color.FromArgb(5, 5, 5),           // Selected Foreground
                                    Color.FromArgb(150, 150, 150));    // Disabled Foreground
        }
    }

    /// <summary>
    /// Styling class for the <see cref="UICheckboxItem"/>.
    /// </summary>
    public class UICheckboxItemStyle
    {
        #region Variables and Properties
        // Variables
        private Color _bgColor;
        private Color _fgColor;
        private Color _cbColor;

        private Color _selectedBGColor;
        private Color _selectedFGColor;
        private Color _selectedCBColor;

        private Color _disabledFGColor;
        private Color _disabledCBColor;

        // Properties
        /// <summary>Gets or sets the background color of this item.</summary>
        public Color BackgroundColor
        {
            get { return _bgColor; }
            set { _bgColor = value; }
        }
        /// <summary>Gets or sets the foreground color of this item.</summary>
        public Color ForegroundColor
        {
            get { return _fgColor; }
            set { _fgColor = value; }
        }
        /// <summary>Gets or sets the checkbox color of this item.</summary>
        public Color CheckboxColor
        {
            get { return _cbColor; }
            set { _cbColor = value; }
        }

        /// <summary>Gets or sets the selected background color of this item.</summary>
        public Color SelectedBackgroundColor
        {
            get { return _selectedBGColor; }
            set { _selectedBGColor = value; }
        }
        /// <summary>Gets or sets the selected foreground color of this item.</summary>
        public Color SelectedForegroundColor
        {
            get { return _selectedFGColor; }
            set { _selectedFGColor = value; }
        }
        /// <summary>Gets or sets the selected checkbox color of this item.</summary>
        public Color SelectedCheckboxColor
        {
            get { return _selectedCBColor; }
            set { _selectedCBColor = value; }
        }

        /// <summary>Gets or sets the disabled foreground color of this item.</summary>
        public Color DisabledForegroundColor
        {
            get { return _disabledFGColor; }
            set { _disabledFGColor = value; }
        }
        /// <summary>Gets or sets the disabled checkbox color of this item.</summary>
        public Color DisabledCheckboxColor
        {
            get { return _disabledCBColor; }
            set { _disabledCBColor = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="UICheckboxItemStyle"/> styling class.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        /// <param name="foregroundColor">The foreground color.</param>
        /// <param name="checkboxColor">The checkbox color.</param>
        /// <param name="selectedBackgroundColor">The selected background color.</param>
        /// <param name="selectedForegroundColor">The selected foreground color.</param>
        /// <param name="selectedCheckboxColor">The selected checkbox color.</param>
        /// <param name="disabledForegroundColor">The disabled foreground color.</param>
        /// <param name="disabledCheckboxColor">The disabled checkbox color.</param>
        public UICheckboxItemStyle(Color backgroundColor,
            Color foregroundColor,
            Color checkboxColor,
            Color selectedBackgroundColor,
            Color selectedForegroundColor,
            Color selectedCheckboxColor,
            Color disabledForegroundColor,
            Color disabledCheckboxColor)
        {
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            CheckboxColor = checkboxColor;
            SelectedBackgroundColor = selectedBackgroundColor;
            SelectedForegroundColor = selectedForegroundColor;
            SelectedCheckboxColor = selectedCheckboxColor;
            DisabledForegroundColor = disabledForegroundColor;
            DisabledCheckboxColor = disabledCheckboxColor;
        }
        #endregion

        /// <summary>
        /// Gets the default style for the <see cref="UICheckboxItemStyle"/>.
        /// </summary>
        /// <returns>The default <see cref="UICheckboxItemStyle"/>.</returns>
        public static UICheckboxItemStyle Default()
        {
            return new UICheckboxItemStyle(Color.FromArgb(170, 10, 10, 10),         // Background
                                            Color.White,                            // Foreground
                                            Color.White,                            // Checkbox
                                            Color.FromArgb(255, 245, 245, 245),     // Selected Background
                                            Color.FromArgb(255, 5, 5, 5),           // Selected Foreground
                                            Color.FromArgb(255, 5, 5, 5),           // Selected Checkbox
                                            Color.FromArgb(255, 150, 150, 150),     // Disabled Foreground
                                            Color.FromArgb(255, 150, 150, 150));    // Disabled Checkbox
        }
    }

    /// <summary>
    /// Styling class for the <see cref="UIItem"/>.
    /// </summary>
    public class UISliderItemStyle
    {
        #region Variables and Properties
        // Variables
        private Color _bgColor;
        private Color _fgColor;
        private Color _bgSliderColor;
        private Color _fgSliderColor;
        private Color _sliderBorderColor;

        private Color _selectedBGColor;
        private Color _selectedFGColor;
        private Color _selectedBGSliderColor;
        private Color _selectedFGSliderColor;

        private Color _disabledFGColor;
        private Color _disabledFGSliderColor;
        private Color _disabledSliderBorderColor;

        // Properties
        /// <summary>Gets or sets the background color of this item.</summary>
        public Color BackgroundColor
        {
            get { return _bgColor; }
            set { _bgColor = value; }
        }
        /// <summary>Gets or sets the foreground color of this item.</summary>
        public Color ForegroundColor
        {
            get { return _fgColor; }
            set { _fgColor = value; }
        }
        /// <summary>Gets or sets the background color of the slider for this item.</summary>
        public Color SliderBackgroundColor
        {
            get { return _bgSliderColor; }
            set { _bgSliderColor = value; }
        }
        /// <summary>Gets or sets the foreground color of the slider for this item.</summary>
        public Color SliderForegroundColor
        {
            get { return _fgSliderColor; }
            set { _fgSliderColor = value; }
        }
        /// <summary>Gets or sets the border color of the slider for this item.</summary>
        public Color SliderBorderColor
        {
            get { return _sliderBorderColor; }
            set { _sliderBorderColor = value; }
        }

        /// <summary>Gets or sets the selected background color of this item.</summary>
        public Color SelectedBackgroundColor
        {
            get { return _selectedBGColor; }
            set { _selectedBGColor = value; }
        }
        /// <summary>Gets or sets the selected foreground color of this item.</summary>
        public Color SelectedForegroundColor
        {
            get { return _selectedFGColor; }
            set { _selectedFGColor = value; }
        }
        /// <summary>Gets or sets the selected background color of the slider for this item.</summary>
        public Color SelectedSliderBackgroundColor
        {
            get { return _selectedBGSliderColor; }
            set { _selectedBGSliderColor = value; }
        }
        /// <summary>Gets or sets the selected foreground color of the slider for this item.</summary>
        public Color SelectedSliderForegroundColor
        {
            get { return _selectedFGSliderColor; }
            set { _selectedFGSliderColor = value; }
        }

        /// <summary>Gets or sets the disabled foreground color of this item.</summary>
        public Color DisabledForegroundColor
        {
            get { return _disabledFGColor; }
            set { _disabledFGColor = value; }
        }
        /// <summary>Gets or sets the disabled foreground color of the slider for this item.</summary>
        public Color DisabledSliderForegroundColor
        {
            get { return _disabledFGSliderColor; }
            set { _disabledFGSliderColor = value; }
        }
        /// <summary>Gets or sets the disabled border color of the slider for this item.</summary>
        public Color DisabledSliderBorderColor
        {
            get { return _disabledSliderBorderColor; }
            set { _disabledSliderBorderColor = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="UISliderItemStyle"/> styling class.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        /// <param name="foregroundColor">The foreground color.</param>
        /// <param name="sliderBackgroundColor">The background color of the slider.</param>
        /// <param name="sliderForegroundColor">The foreground color of the slider.</param>
        /// <param name="sliderBorderColor">The border color of the slider.</param>
        /// <param name="selectedBackgroundColor">The selected background color.</param>
        /// <param name="selectedForegroundColor">The selected foreground color.</param>
        /// <param name="selectedSliderBackgroundColor">The selected background color of the slider.</param>
        /// <param name="selectedSliderForegroundColor">The selected foreground color of the slider.</param>
        /// <param name="disabledForegroundColor">The disabled foreground color.</param>
        /// <param name="disabledSliderForegroundColor">The disabled foreground color of the slider.</param>
        /// <param name="disabledSliderBorderColor">The disabled border color of the slider.</param>
        public UISliderItemStyle(Color backgroundColor,
            Color foregroundColor,
            Color sliderBackgroundColor,
            Color sliderForegroundColor,
            Color sliderBorderColor,
            Color selectedBackgroundColor,
            Color selectedForegroundColor,
            Color selectedSliderBackgroundColor,
            Color selectedSliderForegroundColor,
            Color disabledForegroundColor,
            Color disabledSliderForegroundColor,
            Color disabledSliderBorderColor)
        {
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            SliderBackgroundColor = sliderBackgroundColor;
            SliderForegroundColor = sliderForegroundColor;
            SliderBorderColor = sliderBorderColor;
            SelectedBackgroundColor = selectedBackgroundColor;
            SelectedForegroundColor = selectedForegroundColor;
            SelectedSliderBackgroundColor = selectedSliderBackgroundColor;
            SelectedSliderForegroundColor = selectedSliderForegroundColor;
            DisabledForegroundColor = disabledForegroundColor;
            DisabledSliderForegroundColor = disabledSliderForegroundColor;
            DisabledSliderBorderColor = disabledSliderBorderColor;
        }
        #endregion

        /// <summary>
        /// Gets the default style for the <see cref="UISliderItem"/>.
        /// </summary>
        /// <returns>The default <see cref="UISliderItemStyle"/>.</returns>
        public static UISliderItemStyle Default()
        {
            return new UISliderItemStyle(Color.FromArgb(170, 10, 10, 10),   // Background
                                    Color.White,                            // Foreground
                                    Color.FromArgb(40, 40, 40),             // Slider Background
                                    Color.FromArgb(140, 140, 140),          // Slider Foreground
                                    Color.Black,                            // Slider Border
                                    Color.FromArgb(255, 245, 245, 245),     // Selected Background
                                    Color.FromArgb(255, 5, 5, 5),           // Selected Foreground
                                    Color.FromArgb(40, 40, 40),             // Selected Slider Background
                                    Color.FromArgb(240, 160, 0),            // Selected Slider Foreground
                                    Color.FromArgb(255, 150, 150, 150),     // Disabled Foreground
                                    Color.FromArgb(100, 100, 100),          // Disabled Slider Foreground
                                    Color.Black);                           // Disabled Border
        }
    }

}
