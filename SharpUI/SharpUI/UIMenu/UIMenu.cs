using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using IVSDKDotNet;
using IVSDKDotNet.Direct3D9;
using IVSDKDotNet.Native;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace SharpUI.UIMenu {
    /// <summary>
    /// Used to create a new Rockstar-like menu as can be seen in GTA V.
    /// </summary>
    public class UIMenu : UIBase {

        private static readonly UIItem noItemsItem = new UIItem("There are no items in this menu.", UIItemStyle.Default(), null);

        #region Variables and Properties
        // Variables
        private string _title;

        private UISubtitleItem subtitleItem;
        private UIMenuOptions _menuOptions;
        private D3DResource _menuImage;

        private int _viewRangeStart = 0;
        private int _viewRangeEnd = 6;
        private int _maxItemsVisibleAtOnce = 6;
        private int _selectedIndex;
        private int _descriptionTextHeight = 60;

        private bool _doNotDrawSubtitlePart;

        /// <summary>Collection of all items in this <see cref="UIMenu"/>.</summary>
        public List<UIItemBase> Items;
        private Size _itemSize = new Size(432, 34);
        private D3DResource _menuTitleFontOverride;
        private D3DResource _menuFontOverride;

        // Properties
        /// <summary>Gets or sets the title of this <see cref="UIMenu"/>.</summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>Gets or sets the subtitle of this <see cref="UIMenu"/>.</summary>
        public string Subtitle
        {
            get { return subtitleItem.Subtitle; }
            set { subtitleItem.Subtitle = value; }
        }
        /// <summary>Gets or sets the options this <see cref="UIMenu"/>.</summary>
        public UIMenuOptions Options
        {
            get { return _menuOptions; }
            set { _menuOptions = value; }
        }
        /// <summary>Gets or sets the header image of this <see cref="UIMenu"/>.</summary>
        public D3DResource Image
        {
            get { return _menuImage; }
            set { _menuImage = value; }
        }

        /// <summary>Gets the start value of the viewrange.</summary>
        public int ViewRangeStart
        {
            get { return _viewRangeStart; }
            private set { _viewRangeStart = value; }
        }
        /// <summary>Gets the end value of the viewrange.</summary>
        public int ViewRangeEnd
        {
            get { return _viewRangeEnd; }
            private set { _viewRangeEnd = value; }
        }
        /// <summary>
        /// Gets or sets how much items this <see cref="UIMenu"/> can display at once.
        /// <para>Default value: 6<br/>Minimum value: 2</para>
        /// </summary>
        public int MaxItemsVisibleAtOnce
        {
            get { return _maxItemsVisibleAtOnce; }
            set {
                if (value < 2)
                {
                    _maxItemsVisibleAtOnce = 6;
                    _viewRangeEnd = 6;
                }
                else
                {
                    _maxItemsVisibleAtOnce = value;
                    _viewRangeEnd = value;
                }
            }
        }
        /// <summary>Gets or sets the selected index of this <see cref="UIMenu"/>.</summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }

        /// <summary>
        /// Sets the height of the description text at the bottom of this <see cref="UIMenu"/>.
        /// <para>Default value: 60</para>
        /// </summary>
        public int DescriptionTextHeight
        {
            get { return _descriptionTextHeight; }
            set { _descriptionTextHeight = value; }
        }

        /// <summary>
        /// Sets if the part with the <see cref="Subtitle"/> and the currently <see cref="SelectedIndex"/> should not be drawn.
        /// <para>Default value: <see langword="false"/></para>
        /// </summary>
        public bool DoNotDrawSubtitlePart
        {
            get { return _doNotDrawSubtitlePart; }
            set { _doNotDrawSubtitlePart = value; }
        }

        /// <summary>
        /// Gets or sets the size of the items in this <see cref="UIMenu"/>.
        /// <para>Default X: 306, Y: 34</para>
        /// </summary>
        public Size ItemSize
        {
            get { return _itemSize; }
            set { _itemSize = value; }
        }

        /// <summary>
        /// Gets the full menu rectangle of this <see cref="UIMenu"/>.
        /// </summary>
        public Rectangle MenuRectangle
        {
            get {
                Rectangle rect = Rectangle.Empty;

                if (Items.Count == 0)
                {

                    if (Image != null && Image.DXType == eD3D9ResourceType.Texture)
                    {
                        if (!DoNotDrawSubtitlePart)
                        {
                            rect = new Rectangle(Position, new Size(ItemSize.Width, 108 + ItemSize.Height * 2));
                        }
                        else
                        {
                            rect = new Rectangle(Position, new Size(ItemSize.Width, 108 + ItemSize.Height));
                        }
                    }
                    else
                    {
                        if (!DoNotDrawSubtitlePart)
                        {
                            rect = new Rectangle(Position, new Size(ItemSize.Width, ItemSize.Height * 2));
                        }
                        else
                        {
                            rect = new Rectangle(Position, new Size(ItemSize.Width, ItemSize.Height));
                        }
                    }

                    return rect;
                }

                if (Image != null && Image.DXType == eD3D9ResourceType.Texture)
                {
                    if (Items.Count < MaxItemsVisibleAtOnce)
                    {
                        if (!DoNotDrawSubtitlePart)
                            rect = new Rectangle(Position, new Size(ItemSize.Width, (108 + ItemSize.Height) + Items.Count * ItemSize.Height));
                        else
                            rect = new Rectangle(Position, new Size(ItemSize.Width, 108 + Items.Count * ItemSize.Height));
                    }
                    else
                    {
                        if (!DoNotDrawSubtitlePart)
                            rect = new Rectangle(Position, new Size(ItemSize.Width, (108 + ItemSize.Height) + MaxItemsVisibleAtOnce * ItemSize.Height));
                        else
                            rect = new Rectangle(Position, new Size(ItemSize.Width, 108 + MaxItemsVisibleAtOnce * ItemSize.Height));
                    }
                }
                else
                {
                    if (Items.Count < MaxItemsVisibleAtOnce)
                    {
                        if (!DoNotDrawSubtitlePart)
                            rect = new Rectangle(Position, new Size(ItemSize.Width, ItemSize.Height + Items.Count * ItemSize.Height));
                        else
                            rect = new Rectangle(Position, new Size(ItemSize.Width, Items.Count * ItemSize.Height));
                    }
                    else
                    {
                        if (!DoNotDrawSubtitlePart)
                            rect = new Rectangle(Position, new Size(ItemSize.Width, ItemSize.Height + MaxItemsVisibleAtOnce * ItemSize.Height));
                        else
                            rect = new Rectangle(Position, new Size(ItemSize.Width, MaxItemsVisibleAtOnce * ItemSize.Height));
                    }
                }

                return rect;
            }
        }

        /// <summary>
        /// Gets or sets the overriden font for this <see cref="UIMenu"/> title.<br/>
        /// If null, the default font of IV-SDK .NET will be used.
        /// </summary>
        public D3DResource TitleFontOverride
        {
            get { return _menuTitleFontOverride; }
            set { _menuTitleFontOverride = value; }
        }

        /// <summary>
        /// Gets or sets the overriden font for this <see cref="UIMenu"/>.<br/>
        /// If null, the default font of IV-SDK .NET will be used.
        /// </summary>
        public D3DResource FontOverride
        {
            get { return _menuFontOverride; }
            set { _menuFontOverride = value; }
        }

        /// <summary>
        /// Gets or sets the style for the subtitle of this <see cref="UIMenu"/>.
        /// </summary>
        public UISubtitleItemStyle SubtitleStyle
        {
            get { return subtitleItem.Style; }
            set { subtitleItem.Style = value; }
        }
        #endregion

        #region Events
        private void UIMenu_VisibilityChanged(UIBase sender, bool visible)
        {
            if (!visible)
                SelectedIndex = 0;
        }
        #endregion

        #region Constructor
        public UIMenu(string title, string subtitle, UIMenuOptions options, Point pos, D3DResource image, UIItemBase[] items)
        {
            Title = title;
            subtitleItem = new UISubtitleItem(subtitle, UISubtitleItemStyle.Default());
            Options = options;
            Position = pos;
            Image = image;

            Items = new List<UIItemBase>();
            if (items != null) Items.AddRange(items);

            VisibilityChanged += UIMenu_VisibilityChanged;
        }
        public UIMenu(string title, string subtitle, UIMenuOptions options, Point pos, UIItemBase[] items)
        {
            Title = title;
            subtitleItem = new UISubtitleItem(subtitle, UISubtitleItemStyle.Default());
            Options = options;
            Position = pos;

            Items = new List<UIItemBase>();
            if (items != null) Items.AddRange(items);

            VisibilityChanged += UIMenu_VisibilityChanged;
        }

        public UIMenu(string title, string subtitle, UIMenuOptions options, Point pos, D3DResource image)
        {
            Title = title;
            subtitleItem = new UISubtitleItem(subtitle, UISubtitleItemStyle.Default());
            Options = options;
            Position = pos;
            Image = image;

            Items = new List<UIItemBase>();

            VisibilityChanged += UIMenu_VisibilityChanged;
        }
        public UIMenu(string title, string subtitle, UIMenuOptions options, Point pos)
        {
            Title = title;
            subtitleItem = new UISubtitleItem(subtitle, UISubtitleItemStyle.Default());
            Options = options;
            Position = pos;

            Items = new List<UIItemBase>();

            VisibilityChanged += UIMenu_VisibilityChanged;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Navigates up in this <see cref="UIMenu"/>.
        /// </summary>
        public void NavigateUp()
        {
            if (Items.Count != 0)
            {
                if (SelectedIndex == 0)
                {
                    SelectedIndex = (Items.Count - 1);
                    ViewRangeStart = (Items.Count - 1) - (MaxItemsVisibleAtOnce - 1);
                    ViewRangeEnd = Items.Count;
                }
                else
                {
                    SelectedIndex--;
                    if (SelectedIndex < ViewRangeStart)
                    {
                        ViewRangeStart--;
                        ViewRangeEnd--;
                    }
                }
            }
        }

        /// <summary>
        /// Navigates down in this <see cref="UIMenu"/>.
        /// </summary>
        public void NavigateDown()
        {
            if (Items.Count != 0)
            {
                if (SelectedIndex == (Items.Count - 1))
                {
                    SelectedIndex = 0;
                    ViewRangeStart = 0;
                    ViewRangeEnd = MaxItemsVisibleAtOnce;
                }
                else
                {
                    SelectedIndex++;
                    if (SelectedIndex >= ViewRangeEnd)
                    {
                        ViewRangeStart++;
                        ViewRangeEnd++;
                    }
                }
            }
        }

        private void PlayNavigationSound()
        {
            if (Options.EnableNavigationSounds)
            {
                CTheScripts.SetDummyThread();
                Natives.PLAY_SOUND_FRONTEND(-1, "FRONTEND_MENU_HIGHLIGHT_DOWN_UP");
                CTheScripts.RestorePreviousThread();
            }
        }
        internal void PlaySound(string name)
        {
            if (Options.EnableNavigationSounds)
            {
                CTheScripts.SetDummyThread();
                Natives.PLAY_SOUND_FRONTEND(-1, name);
                CTheScripts.RestorePreviousThread();
            }
        }
        internal void PlayErrorSound()
        {
            if (Options.EnableNavigationSounds)
            {
                CTheScripts.SetDummyThread();
                Natives.PLAY_SOUND_FRONTEND(-1, "FRONTEND_MENU_ERROR");
                CTheScripts.RestorePreviousThread();
            }
        }
        internal void PlaySelectSound()
        {
            if (Options.EnableNavigationSounds)
            {
                CTheScripts.SetDummyThread();
                Natives.PLAY_SOUND_FRONTEND(-1, "FRONTEND_MENU_SELECT");
                CTheScripts.RestorePreviousThread();
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Gets an item from the <see cref="Items"/> list by the given tag and converts it to the given type T.
        /// </summary>
        /// <typeparam name="T">The new type to convert the found item to.</typeparam>
        /// <param name="tag">The tag of the <see cref="UIItemBase"/> you search for.</param>
        /// <returns>The target item of type T if found. Otherwise, null.</returns>
        public T GetItemByThisTag<T>(string tag)
        {
            try
            {
                UIItemBase item = Items.Where(x => x.Tag != null && x.Tag.ToString() == tag).FirstOrDefault();

                if (item != null)
                    return (T)Convert.ChangeType(item, typeof(T));

                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// Gets a <see cref="UIItemBase"/> from the <see cref="Items"/> list by this tag.
        /// </summary>
        /// <param name="tag">The tag of the <see cref="UIItemBase"/> you search for.</param>
        /// <returns>The <see cref="UIItemBase"/> if found. Otherwise, null.</returns>
        public UIItemBase GetItemByThisTag(string tag)
        {
            return Items.Where(x => x.Tag != null && x.Tag.ToString() == tag).FirstOrDefault();
        }

        /// <summary>
        /// Gets all items of this type from the <see cref="Items"/> list of this <see cref="UIMenu"/>.
        /// </summary>
        /// <typeparam name="T">The type you want to target.</typeparam>
        /// <returns>An array of all items in this <see cref="UIMenu"/> that match the given type.</returns>
        public UIItemBase[] GetItemsOfType<T>()
        {
            return Items.Where(x => x.GetType() == typeof(T)).ToArray();
        }

        /// <summary>
        /// Peeks to the next item in this <see cref="UIMenu"/>.
        /// </summary>
        /// <param name="repeat">Sets if the function should begin peeking from the beginning of the <see cref="Items"/> list if the peek index was greater then the <see cref="Items"/> count.</param>
        /// <returns>The next item in this <see cref="UIMenu"/> from the current <see cref="SelectedIndex"/>. <see langword="null"/> if: The <see cref="Items"/> list is empty, the peek index is less then 0 or when the peek index was greater then the <see cref="Items"/> count and 'repeat' was not set to true.</returns>
        public UIItemBase Peek(bool repeat = true)
        {
            if (Items.Count == 0)
                return null;

            int index = SelectedIndex + 1;

            if (repeat)
            {
                if (index > (Items.Count - 1))
                    index = 0;
            }
            else
            {
                if (index > (Items.Count - 1))
                    return null;
            }

            if (index < 0)
                return null;

            return Items[index];
        }

        /// <summary>
        /// Gets if the current item at the given index is currently visible in the <see cref="UIMenu"/>.
        /// </summary>
        /// <param name="index">The index of an item.</param>
        /// <returns>True if the item is visible in the <see cref="UIMenu"/>. False if the item is out of the view range, the <see cref="UIMenu"/> does not have any items or when the given index is smaller then the <see cref="Items"/> count.</returns>
        public bool IsItemVisibleInMenu(int index)
        {
            if (Items.Count == 0)
                return false;
            if (Items.Count < index)
                return false;

            return index < ViewRangeEnd && index >= ViewRangeStart;
        }
        #endregion

        /// <inheritdoc/>
        public override void Draw(D3DGraphics gfx)
        {
            if (gfx == null)
                return;
            if (!IsVisible)
                return;
            if (Options.DoNotShowInPauseMenu && Natives.IS_PAUSE_MENU_ACTIVE())
                return;

            // Draw only header image, subtitle item and no items item when there are no items in the menu.
            if (Items.Count == 0)
            {
                Point p = Position;

                if (Image != null && Image.DXType == eD3D9ResourceType.Texture)
                {
                    // Draw menu header image
                    gfx.DrawTexture(Image, new RectangleF(p.X, p.Y, ItemSize.Width, 108f));

                    // Draw menu title
                    gfx.DrawString(TitleFontOverride, Title, new Rectangle(p.X, p.Y, ItemSize.Width, 108), eD3DFontDrawFlags.Center | eD3DFontDrawFlags.VerticalCenter, Color.White);

                    if (!DoNotDrawSubtitlePart)
                    {
                        // Draw menu subtitle item
                        subtitleItem.Draw(this, gfx, new Point(p.X, p.Y + 108));
                        subtitleItem.SelectedIndex = SelectedIndex + 1;
                        subtitleItem.ItemCount = Items.Count;

                        // Draw no items item
                        p = new Point(p.X, p.Y + 108 + ItemSize.Height);
                        noItemsItem.Draw(this, gfx, p);
                    }
                    else
                    {
                        // Draw no items item
                        p = new Point(p.X, p.Y + 108);
                        noItemsItem.Draw(this, gfx, p);
                    }
                }
                else
                {
                    if (!DoNotDrawSubtitlePart)
                    {
                        // Draw menu subtitle item
                        subtitleItem.Draw(this, gfx, p);
                        subtitleItem.SelectedIndex = SelectedIndex + 1;
                        subtitleItem.ItemCount = Items.Count;

                        // Draw no items item
                        p = new Point(p.X, p.Y + ItemSize.Height);
                        noItemsItem.Draw(this, gfx, p);
                    }
                    else
                    {
                        // Draw no items item
                        p = new Point(p.X, p.Y);
                        noItemsItem.Draw(this, gfx, p);
                    }
                }

                return;
            }

            // Draw items
            if (Items.Count < MaxItemsVisibleAtOnce)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    UIItemBase item = Items[i];
                    item.IsSelected = SelectedIndex == i;

                    Point p = Position;
                    if (Image != null && Image.DXType == eD3D9ResourceType.Texture)
                    {
                        // Draw menu header image
                        gfx.DrawTexture(Image, new RectangleF(p.X, p.Y, ItemSize.Width, 108f));

                        // Draw menu title
                        gfx.DrawString(TitleFontOverride, Title, new Rectangle(p.X, p.Y, ItemSize.Width, 108), eD3DFontDrawFlags.Center | eD3DFontDrawFlags.VerticalCenter, Color.White);

                        if (!DoNotDrawSubtitlePart)
                        {
                            // Draw menu subtitle item
                            subtitleItem.Draw(this, gfx, new Point(p.X, p.Y + 108));
                            subtitleItem.SelectedIndex = SelectedIndex + 1;
                            subtitleItem.ItemCount = Items.Count;

                            // Set position for item
                            p = new Point(p.X, (p.Y + 108 + ItemSize.Height) + i * ItemSize.Height);
                        }
                        else
                        {
                            // Set position for item
                            p = new Point(p.X, (p.Y + 108) + i * ItemSize.Height);
                        }
                    }
                    else
                    {
                        if (!DoNotDrawSubtitlePart)
                        {
                            // Draw menu subtitle item
                            subtitleItem.Draw(this, gfx, p);
                            subtitleItem.SelectedIndex = SelectedIndex + 1;
                            subtitleItem.ItemCount = Items.Count;

                            // Set position for item
                            p = new Point(p.X, (p.Y + ItemSize.Height) + i * ItemSize.Height);
                        }
                        else
                        {
                            // Set position for item
                            p = new Point(p.X, p.Y + i * ItemSize.Height);
                        }
                    }

                    // Draw item
                    item.Draw(this, gfx, p);
                }
            }
            else
            {
                for (int i = ViewRangeStart; i < ViewRangeEnd; i++)
                {
                    UIItemBase item = Items[i];
                    item.IsSelected = SelectedIndex == i;

                    Point p = Position;
                    if (Image != null && Image.DXType == eD3D9ResourceType.Texture)
                    {
                        // Draw menu header image
                        gfx.DrawTexture(Image, new RectangleF(p.X, p.Y, ItemSize.Width, 108f));

                        // Draw menu title
                        gfx.DrawString(TitleFontOverride, Title, new Rectangle(p.X, p.Y, ItemSize.Width, 108), eD3DFontDrawFlags.Center | eD3DFontDrawFlags.VerticalCenter, Color.White);

                        if (!DoNotDrawSubtitlePart)
                        {
                            // Draw menu subtitle item
                            subtitleItem.Draw(this, gfx, new Point(p.X, p.Y + 108));
                            subtitleItem.SelectedIndex = SelectedIndex + 1;
                            subtitleItem.ItemCount = Items.Count;

                            // Set position for item
                            p = new Point(p.X, (p.Y + 108 + ItemSize.Height) + (i - ViewRangeStart) * ItemSize.Height);
                        }
                        else
                        {
                            // Set position for item
                            p = new Point(p.X, (p.Y + 108) + (i - ViewRangeStart) * ItemSize.Height);
                        }
                    }
                    else
                    {
                        if (!DoNotDrawSubtitlePart)
                        {
                            // Draw menu subtitle item
                            subtitleItem.Draw(this, gfx, p);
                            subtitleItem.SelectedIndex = SelectedIndex + 1;
                            subtitleItem.ItemCount = Items.Count;

                            // Set position for item
                            p = new Point(p.X, (p.Y + ItemSize.Height) + (i - ViewRangeStart) * ItemSize.Height);
                        }
                        else
                        {
                            // Set position for item
                            p = new Point(p.X, p.Y + (i - ViewRangeStart) * ItemSize.Height);
                        }
                    }

                    // Draw item
                    item.Draw(this, gfx, p);
                }
            }

            // Draw selected item description
            UIItemBase selectedItem = Items[SelectedIndex];
            if (!string.IsNullOrWhiteSpace(selectedItem.Description))
            {
                Point p2 = Position;
                if (Image != null && Image.DXType == eD3D9ResourceType.Texture)
                {
                    // Set position for item description
                    if (Items.Count < MaxItemsVisibleAtOnce)
                    {
                        if (!DoNotDrawSubtitlePart)
                            p2 = new Point(p2.X, (p2.Y + 108 + ItemSize.Height) + Items.Count * ItemSize.Height);
                        else
                            p2 = new Point(p2.X, (p2.Y + 108) + Items.Count * ItemSize.Height);
                    }
                    else
                    {
                        if (!DoNotDrawSubtitlePart)
                            p2 = new Point(p2.X, (p2.Y + 108 + ItemSize.Height) + MaxItemsVisibleAtOnce * ItemSize.Height);
                        else
                            p2 = new Point(p2.X, (p2.Y + 108) + MaxItemsVisibleAtOnce * ItemSize.Height);
                    }
                }
                else
                {
                    // Set position for item description
                    if (Items.Count < MaxItemsVisibleAtOnce)
                    {
                        if (!DoNotDrawSubtitlePart)
                            p2 = new Point(p2.X, (p2.Y + ItemSize.Height) + Items.Count * ItemSize.Height);
                        else
                            p2 = new Point(p2.X, p2.Y + Items.Count * ItemSize.Height);
                    }
                    else
                    {
                        if (!DoNotDrawSubtitlePart)
                            p2 = new Point(p2.X, (p2.Y + ItemSize.Height) + MaxItemsVisibleAtOnce * ItemSize.Height);
                        else
                            p2 = new Point(p2.X, p2.Y + MaxItemsVisibleAtOnce * ItemSize.Height);
                    }
                }

                // Shadow
                gfx.DrawString(selectedItem.Description, new Rectangle(new Point(p2.X + 2, p2.Y + 10), new Size(ItemSize.Width, ItemSize.Height + DescriptionTextHeight)), eD3DFontDrawFlags.Left | eD3DFontDrawFlags.WordBreak, Color.Black);

                gfx.DrawString(selectedItem.Description, new Rectangle(new Point(p2.X, p2.Y + 8), new Size(ItemSize.Width, ItemSize.Height + DescriptionTextHeight)), eD3DFontDrawFlags.Left | eD3DFontDrawFlags.WordBreak, Color.White);
            }
        }

        /// <inheritdoc/>
        public override void KeyPress(KeyEventArgs args, bool isKeyUpEvent, bool shouldBeUsedForNavigation)
        {
            if (args == null)
                return;
            if (!IsVisible)
                return;
            if (!HasFocus)
                return;
            if (Items.Count == 0)
                return;
            if (Options.DoNotShowInPauseMenu && Natives.IS_PAUSE_MENU_ACTIVE())
                return;

            // Process navigation
            if (shouldBeUsedForNavigation)
            {
                if (args.KeyCode == Options.NavigateUp) // Navigate Up
                {
                    PlayNavigationSound();
                    NavigateUp();
                }
                if (args.KeyCode == Options.NavigateDown) // Navigate Down
                {
                    PlayNavigationSound();
                    NavigateDown();
                }
            }

            // Process the keypress
            Items[SelectedIndex].KeyPress(this, args, isKeyUpEvent, shouldBeUsedForNavigation);
        }

        /// <inheritdoc/>
        public override void Cleanup()
        {
            if (Items.Count == 0)
                return;

            Items.ForEach(x => x.Cleanup());
            Items.Clear();
        }

    }
}
