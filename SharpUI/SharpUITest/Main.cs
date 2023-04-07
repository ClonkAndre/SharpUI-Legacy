using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;

using SharpUI;
using SharpUI.UI;
using SharpUI.UIForms;
using SharpUI.UIMenu;

using IVSDKDotNet;
using IVSDKDotNet.Direct3D9;
using IVSDKDotNet.Native;

namespace SharpUITest {
    public class Main : Script {

        #region Variables
        private D3DGraphics gfx;

        private UIPool pool;

        private UIList testUIList1;
        private UIMenu testMenu1, testMenu2, testMenu3, testMenu4;
        private D3DResource menuImage;
        private D3DResource icon;
        #endregion

        #region Constructor
        public Main()
        {
            Initialized += Main_Initialized;
            KeyDown += Main_KeyDown;
            KeyUp += Main_KeyUp;
        }
        #endregion

        private UIButton btn;

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
            //

            icon = (D3DResource)gfx.CreateD3D9Texture(Properties.Resources._lock).DXObject;

            // Create UIPool
            pool = new UIPool();


            // Create a new UIButton
            btn = new UIButton("Click me!", null, new Size(300, 100), (UIButton button) => { ShowSubtitleMessage("Button clicked!"); });
            btn.Position = new Point(10, 10);
            pool.Items.Add(btn); // Add btn to UIPool

            // Activate the IVSDK .NET mouse for the button
            CGame.Mouse.IsVisible = true;


            // Create a new UIList
            testUIList1 = new UIList(new Point(10, 500), new UIList.Entry[] {
                new UIList.Entry("Test123"),
                new UIList.Entry(Color.Red, "Red item!"),
                new UIList.Entry((UIList.Entry entry) => { return "Mouse Sensitivity: " + Natives.GET_MOUSE_SENSITIVITY(); }),
            });


            // Creates a new UIMenu
            testMenu1 = new UIMenu("Title", "Subtitle", UIMenuOptions.Default(), new Point(100, 100), menuImage, new UIItemBase[] {
                new UIItem("test item 1 mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm", "Just a test to test if the text will fit properly, and will go to the new-line if cut-off. will go to the new-line if cut-off. test to test if the text will fit properly. Just a test to test if the text will fit properly", UIItemStyle.Default(), null),
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
                new UIItem("ItemTest", icon, "Decrease item height", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { menu.ItemSize = new Size(menu.ItemSize.Width, menu.ItemSize.Height - 1); menu.Subtitle = menu.ItemSize.ToString(); }),
                new UIItem("Peek to next item", "Peeks to the next item which would be the 'Show / Hide subtitle part' checkbox item.", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { ShowSubtitleMessage(((UICheckboxItem)testMenu1.Peek()).Text); }),
                new UICheckboxItem(true, false, "Show / Hide subtitle part", UICheckboxItemStyle.Default(), (UIMenu menu, UICheckboxItem item) => { testMenu1.DoNotDrawSubtitlePart = !testMenu1.DoNotDrawSubtitlePart; }),
                new UISliderItem("Slider item", 5, UISliderItemStyle.Default(), (UIMenu menu, UISliderItem item) => { ShowSubtitleMessage("Value: " + item.Value.ToString()); }),
                new UIListItem<string>((object)"TestList1", "test list", UIItemStyle.Default(), (UIMenu menu, UIListItem<string> item) => { ShowSubtitleMessage(item.SelectedText); }, new string[] { "Test Item 1", "Test Item 2", "Test Item 3" }),
                new UIListItem<int>("test int list", UIItemStyle.Default(), (UIMenu menu, UIListItem<int> item) => { ShowSubtitleMessage(item.SelectedText); }, new int[] { 1, 2, 3 }),
                new UIIntegerUpDownItem("test int up/down item", UIItemStyle.Default(), (UIMenu menu, UIIntegerUpDownItem item) => { ShowSubtitleMessage(item.Value.ToString()); }),
                new UIDoubleUpDownItem("test double up/down item", 25, 0.5, UIItemStyle.Default(), (UIMenu menu, UIDoubleUpDownItem item) => { ShowSubtitleMessage(item.Value.ToString()); }),
                new UINumberUpDownItem<float>("test generic number up/down item", 25f, 0.1f, UIItemStyle.Default(), (UIMenu menu, UINumberUpDownItem<float> item) => { ShowSubtitleMessage(item.Value.ToString()); }),
                new UIItem("Set focus to testMenu2", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { pool.SetFocus(testMenu2, true); }),
                new UIItem("Close all menus", "Closes all menus", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { pool.ChangeVisibilityOfEveryElementOfType<UIMenu>(false); }),
                new UIItem("Close menu", "Closes this menu", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { menu.SetVisibility(false); })
            });
            testMenu1.GetItemByThisTag<UIItem>("ItemTest").RightText = "Test text!!!";
            testMenu1.SetVisibility(true);

            // Tries to get the "TestList1" item inside the "testMenu1" by its tag and add a new item to the list.
            UIListItem<string> itemFound = testMenu1.GetItemByThisTag<UIListItem<string>>("TestList1");
            if (itemFound != null)
            {
                //UIListItem<string> theItem = (itemFound as UIListItem<string>);
                itemFound.Items.Add("I'm new");
            }


            // Creates another UIMenu
            testMenu2 = new UIMenu("TEST MENU 2!", "Subtitle", UIMenuOptions.Default(), new Point(1000, 500), menuImage, new UIItemBase[] {
                new UIItem("Test item", "Test!", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { ShowSubtitleMessage("123"); }),
                new UIItem("Show / Hide subtitle part", "Test!", UIItemStyle.Default(), (UIMenu menu, UIItem item) => { testMenu2.DoNotDrawSubtitlePart = !testMenu2.DoNotDrawSubtitlePart; }),
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


            // Creates another UIMenu which is empty
            testMenu4 = new UIMenu("Empty menu!", "This is empty", UIMenuOptions.Default(), new Point(800, 400), menuImage);
            testMenu4.SetVisibility(true);


            // Adds the 4 UIMenus to the UIPool.
            pool.Items.Add(testMenu1);
            pool.Items.Add(testMenu2);
            pool.Items.Add(testMenu3);
            pool.Items.Add(testMenu4);

            // Adds the UIList to the UIPool.
            pool.Items.Add(testUIList1);

            // Set the focus to the testMenu so it can receive key inputs.
            pool.SetFocus(testMenu1, true);
        }

        private void Gfx_OnInit(IntPtr device)
        {
            // Create menu header image and set them to all 3 UIMenus if texture got created successfully.
            D3DResult r = gfx.CreateD3D9Texture(Properties.Resources.menuImage);
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
                testMenu4.Image = menuImage;
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
            pool.ProcessKeys(e, true, true);
        }
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            // Process key presses for all elements that are in the pool.
            pool.ProcessKeys(e, false, false);
        }

    }
}
