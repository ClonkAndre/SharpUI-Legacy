using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using IVSDKDotNet.Direct3D9;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace SharpUI.UIMenu {
    /// <summary>
    /// Used to create a new Rockstar-like menu.
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
        /// Gets or sets the size of the items in this <see cref="UIMenu"/>.
        /// <para>Default X: 306, Y: 34</para>
        /// </summary>
        public Size ItemSize
        {
            get { return _itemSize; }
            set { _itemSize = value; }
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
        }
        public UIMenu(string title, string subtitle, UIMenuOptions options, Point pos, UIItemBase[] items)
        {
            Title = title;
            subtitleItem = new UISubtitleItem(subtitle, UISubtitleItemStyle.Default());
            Options = options;
            Position = pos;

            Items = new List<UIItemBase>();
            if (items != null) Items.AddRange(items);
        }

        public UIMenu(string title, string subtitle, UIMenuOptions options, Point pos, D3DResource image)
        {
            Title = title;
            subtitleItem = new UISubtitleItem(subtitle, UISubtitleItemStyle.Default());
            Options = options;
            Position = pos;
            Image = image;

            Items = new List<UIItemBase>();
        }
        public UIMenu(string title, string subtitle, UIMenuOptions options, Point pos)
        {
            Title = title;
            subtitleItem = new UISubtitleItem(subtitle, UISubtitleItemStyle.Default());
            Options = options;
            Position = pos;

            Items = new List<UIItemBase>();
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
        #endregion

        #region Functions
        /// <summary>
        /// Gets a <see cref="UIItemBase"/> from the <see cref="Items"/> list by this tag.
        /// </summary>
        /// <param name="tag">The tag of the <see cref="UIItemBase"/> you search for.</param>
        /// <returns>The <see cref="UIItemBase"/> if found. Otherwise, null.</returns>
        public UIItemBase GetItemByThisTag(string tag)
        {
            return Items.Where(x => x.Tag != null && x.Tag.ToString() == tag).FirstOrDefault();
        }
        #endregion

        /// <summary>
        /// Responsible for drawing this <see cref="UIMenu"/> and all items.
        /// </summary>
        /// <param name="gfx">The <see cref="D3DGraphics"/> object needed for drawing.</param>
        public override void Draw(D3DGraphics gfx)
        {
            if (gfx == null)
                return;
            if (!IsVisible)
                return;

            // Draw only header image, subtitle item and no items item when there are no items in the menu.
            if (Items.Count == 0)
            {
                Point p = Position;

                if (Image != null && Image.DXType == eD3D9ResourceType.Texture)
                {
                    // Draw menu header image
                    gfx.DrawTexture(gfx.Device, Image, new RectangleF(p.X, p.Y, ItemSize.Width, 108f));

                    // Draw menu title
                    gfx.DrawString(gfx.Device, TitleFontOverride, Title, new Rectangle(p.X, p.Y, ItemSize.Width, 108), eD3DFontDrawFlags.Center | eD3DFontDrawFlags.VerticalCenter, Color.White);

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
                    // Draw menu subtitle item
                    subtitleItem.Draw(this, gfx, p);
                    subtitleItem.SelectedIndex = SelectedIndex + 1;
                    subtitleItem.ItemCount = Items.Count;

                    // Draw no items item
                    p = new Point(p.X, p.Y + ItemSize.Height);
                    noItemsItem.Draw(this, gfx, p);
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
                        gfx.DrawTexture(gfx.Device, Image, new RectangleF(p.X, p.Y, ItemSize.Width, 108f));

                        // Draw menu title
                        gfx.DrawString(gfx.Device, TitleFontOverride, Title, new Rectangle(p.X, p.Y, ItemSize.Width, 108), eD3DFontDrawFlags.Center | eD3DFontDrawFlags.VerticalCenter, Color.White);

                        // Draw menu subtitle item
                        subtitleItem.Draw(this, gfx, new Point(p.X, p.Y + 108));
                        subtitleItem.SelectedIndex = SelectedIndex + 1;
                        subtitleItem.ItemCount = Items.Count;

                        // Set position for item
                        p = new Point(p.X, (p.Y + 108 + ItemSize.Height) + i * ItemSize.Height);
                    }
                    else
                    {
                        // Draw menu subtitle item
                        subtitleItem.Draw(this, gfx, p);
                        subtitleItem.SelectedIndex = SelectedIndex + 1;
                        subtitleItem.ItemCount = Items.Count;

                        // Set position for item
                        p = new Point(p.X, (p.Y + ItemSize.Height) + i * ItemSize.Height);
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
                        gfx.DrawTexture(gfx.Device, Image, new RectangleF(p.X, p.Y, ItemSize.Width, 108f));

                        // Draw menu title
                        gfx.DrawString(gfx.Device, TitleFontOverride, Title, new Rectangle(p.X, p.Y, ItemSize.Width, 108), eD3DFontDrawFlags.Center | eD3DFontDrawFlags.VerticalCenter, Color.White);

                        // Draw menu subtitle item
                        subtitleItem.Draw(this, gfx, new Point(p.X, p.Y + 108));
                        subtitleItem.SelectedIndex = SelectedIndex + 1;
                        subtitleItem.ItemCount = Items.Count;

                        // Set position for item
                        p = new Point(p.X, (p.Y + 108 + ItemSize.Height) + (i - ViewRangeStart) * ItemSize.Height);
                    }
                    else
                    {
                        // Draw menu subtitle item
                        subtitleItem.Draw(this, gfx, p);
                        subtitleItem.SelectedIndex = SelectedIndex + 1;
                        subtitleItem.ItemCount = Items.Count;

                        // Set position for item
                        p = new Point(p.X, (p.Y + ItemSize.Height) + (i - ViewRangeStart) * ItemSize.Height);
                    }

                    // Draw item
                    item.Draw(this, gfx, p);
                }
            }





            // Draw selected item description
            //UIItemBase selectedItem = Items[SelectedIndex];
            //if (!string.IsNullOrWhiteSpace(selectedItem.Description))
            //{
            //    Point p2 = Position;
            //    if (Image != null && Image.DXType == eD3D9ResourceType.Texture)
            //    {
            //        // Set position for item description
            //        p2 = new Point(p2.X, (p2.Y + 108 + ItemSize.Height) + MaxItemsVisibleAtOnce * ItemSize.Height);
            //    }
            //    else
            //    {
            //        // Set position for item description
            //        p2 = new Point(p2.X, (p2.Y + ItemSize.Height) + MaxItemsVisibleAtOnce * ItemSize.Height);
            //    }

            //    //Rectangle textRect = gfx.MeasureText(null, selectedItem.Description, new Rectangle(p2.X, p2.Y + 10, ItemSize.Width, ItemSize.Height), eD3DFontDrawFlags.Left);
            //    SizeF size = GetTextSize(selectedItem.Description, ItemSize, out int linesFitted);

            //    linesFitted = ItemSize.Height + linesFitted * ItemSize.Height;

            //    gfx.DrawBoxFilled(gfx.Device, new Vector2(p2.X, p2.Y + 10), new Size(ItemSize.Width, linesFitted), Color.Black);
            //    gfx.DrawString(gfx.Device, selectedItem.Description, new Rectangle(new Point(p2.X, p2.Y + 10), new Size(ItemSize.Width, linesFitted)), eD3DFontDrawFlags.Left | eD3DFontDrawFlags.WordBreak, Color.White);
            //}
        }

        /// <summary>
        /// Responsible for navigating through this <see cref="UIMenu"/>.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> from either the <see cref="IVSDKDotNet.Script.KeyDown"/> or <see cref="IVSDKDotNet.Script.KeyUp"/> method.</param>
        public override void KeyPress(KeyEventArgs args)
        {
            if (args == null)
                return;
            if (!IsVisible)
                return;
            if (!HasFocus)
                return;
            if (Items.Count == 0)
                return;

            // Process navigation
            if (args.KeyCode == Options.NavigateUp) // Navigate Up
            {
                NavigateUp();
            }
            if (args.KeyCode == Options.NavigateDown) // Navigate Down
            {
                NavigateDown();
            }

            Items[SelectedIndex].KeyPress(this, args);
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
