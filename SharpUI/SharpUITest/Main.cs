using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpUI;
using SharpUI.UIMenu;

using IVSDKDotNet;
using IVSDKDotNet.Direct3D9;
using SharpUI.UIMenu.Items;

namespace SharpUITest {
    public class Main : Script {

        #region Variables
        private D3DGraphics gfx;

        private UIPool pool;

        private UIMenu testMenu1, testMenu2, testMenu3;
        private D3DResource menuImage;
        #endregion

        #region Constructor
        public Main()
        {
            Initialized += Main_Initialized;
            KeyDown += Main_KeyDown;
            KeyUp += Main_KeyUp;
        }
        #endregion

        private void Main_Initialized(object sender, EventArgs e)
        {
            // Create new D3D9Graphics object
            gfx = new D3DGraphics(this);
            gfx.OnInit += Gfx_OnInit;
            gfx.OnDeviceEndScene += Gfx_OnDeviceEndScene;

            // - - - THIS WILL WORK IN VERSION 0.7 OF IV-SDK .NET - - -
            // For now you will have to create the image in the D3DGraphics.OnInit event.
            //// Create menu header image
            //D3DResult r = gfx.CreateD3D9Texture(gfx.Device, Properties.Resources.menuImage);
            //if (r.Error != null)
            //{
            //    CGame.Console.PrintError(r.Error.ToString());
            //}
            //else
            //{
            //    menuImage = (D3DResource)r.DXObject;
            //}


            // Create UIPool
            pool = new UIPool();


            // Creates a new UIMenu
            testMenu1 = new UIMenu("Title", "Subtitle", UIMenuOptions.Default(), new Point(100, 100), menuImage, new UIItemBase[] {
                new UIItem("test item 1 mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm", "Just a test to test if the text will fit properly, and will go to the new-line if cut-off.", UIItemStyle.Default(), null),
                new UIItem("Show / Hide Image", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { 
                    if (menu.Image == null)
                    {
                        menu.Image = menuImage;
                    }
                    else
                    {
                        menu.Image = null;
                    }
                }),
                new UIItem("Increase item height", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { menu.ItemSize = new Size(menu.ItemSize.Width, menu.ItemSize.Height + 1); menu.Subtitle = menu.ItemSize.ToString(); }),
                new UIItem("Decrease item height", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { menu.ItemSize = new Size(menu.ItemSize.Width, menu.ItemSize.Height - 1); menu.Subtitle = menu.ItemSize.ToString(); }),
                new UICheckboxItem(false, false, "test checkbox", UICheckboxItemStyle.Default(), (UIMenu menu, UICheckboxItem item) => { ShowSubtitleMessage("COOL!"); }),
                new UISliderItem("Slider item", 5, UISliderItemStyle.Default(), (UIMenu menu, UISliderItem item) => { ShowSubtitleMessage("Value: " + item.Value.ToString()); }),
                new UIListItem<string>((object)"TestList1", "test list", UIItemStyle.Default(), (UIMenu menu, UIListItem<string> item) => { ShowSubtitleMessage(item.SelectedText); }, new string[] { "Test Item 1", "Test Item 2", "Test Item 3" }),
                new UIListItem<int>("test int list", UIItemStyle.Default(), (UIMenu menu, UIListItem<int> item) => { ShowSubtitleMessage(item.SelectedText); }, new int[] { 1, 2, 3 }),
                new UIItem("Set focus to testMenu2", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { pool.SetFocus(testMenu2, true); }),
                new UIItem("Close all menus", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { pool.ChangeVisibilityOfEveryElementOfType<UIMenu>(false); }),
                new UIItem("Close menu", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { menu.SetVisibility(false); })
            });
            testMenu1.SetVisibility(true);

            // Tries to get the "TestList1" item inside the "testMenu1" by its tag and add a new item to the list.
            UIItemBase itemFound = testMenu1.GetItemByThisTag("TestList1");
            if (itemFound != null)
            {
                UIListItem<string> theItem = (itemFound as UIListItem<string>);
                theItem.Items.Add("I'm new");
            }


            // Creates another UIMenu
            testMenu2 = new UIMenu("TEST MENU 2!", "Subtitle", UIMenuOptions.Default(), new Point(1000, 500), menuImage, new UIItemBase[] {
                new UIItem("Test item", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { ShowSubtitleMessage("123"); }),
                new UIItem("Set focus to testMenu1", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { pool.SetFocus(testMenu1, true); }),
                new UIItem("Set focus to testMenu3", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { pool.SetFocus(testMenu3, true); })
            });
            testMenu2.SetVisibility(true);


            // Creates yet another UIMenu
            testMenu3 = new UIMenu("TEST MENU 3!", "Subtitle lol", UIMenuOptions.Default(), new Point(500, 800), menuImage, new UIItemBase[] {
                new UIItem("Test item", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { ShowSubtitleMessage("123"); }),
                new UIItem("Set focus to testMenu1", UIItemStyle.Default(), (UIMenu menu, UIItem item) => {
                    pool.SetFocus(testMenu1, true);
                })
            });
            testMenu3.SetVisibility(true);
            
            
            // Adds the 3 UIMenus to the UIPool.
            pool.Items.Add(testMenu1);
            pool.Items.Add(testMenu2);
            pool.Items.Add(testMenu3);


            // Set the focus to the testMenu so it can receive key inputs.
            pool.SetFocus(testMenu1, true);
        }

        private void Gfx_OnInit(IntPtr device)
        {
            // Create menu header image and set them to all 3 UIMenus if texture got created successfully.
            D3DResult r = gfx.CreateD3D9Texture(device, Properties.Resources.menuImage);
            if (r.Error != null)
            {
                CGame.Console.PrintError(r.Error.ToString());
            }
            else
            {
                menuImage = (D3DResource)r.DXObject;
                testMenu1.Image = menuImage;
                testMenu2.Image = menuImage;
                testMenu3.Image = menuImage;
            }
        }
        private void Gfx_OnDeviceEndScene(IntPtr device)
        {
            // Draw all elements that are in the pool.
            pool.ProcessDrawing(gfx);
        }
        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            // Process key presses for all elements that are in the pool.
            pool.ProcessKeys(e);
        }
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            // Process key presses for all elements that are in the pool.
            //pool.ProcessKeys(e);
        }

    }
}
