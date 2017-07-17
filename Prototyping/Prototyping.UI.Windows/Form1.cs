using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows;
using System.Windows.Media;

namespace Prototyping.UI.Windows
{
    public partial class Form1 : Form
    {
        private ElementHost ctrlHost;
        private Prototyping.UI.WPFControls.OLAPGrid  wpfAddressCtrl;
        System.Windows.FontWeight initFontWeight;
        double initFontSize;
        System.Windows.FontStyle initFontStyle;
        System.Windows.Media.SolidColorBrush initBackBrush;
        System.Windows.Media.SolidColorBrush initForeBrush;
        System.Windows.Media.FontFamily initFontFamily;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ctrlHost = new ElementHost();
            ctrlHost.Dock = DockStyle.Fill;
            panel1.Controls.Add(ctrlHost);
            wpfAddressCtrl = new Prototyping.UI.WPFControls.OLAPGrid();
            wpfAddressCtrl.InitializeComponent();
            ctrlHost.Child = wpfAddressCtrl;

            /*
              wpfAddressCtrl.OnButtonClick +=
                new Prototyping.UI.WPFControls.MyControlEventHandler (
                avAddressCtrl_OnButtonClick);
             */
            wpfAddressCtrl.Loaded += new RoutedEventHandler(
                avAddressCtrl_Loaded);
        }

        void avAddressCtrl_Loaded(object sender, EventArgs e)
        {
            //initBackBrush = (SolidColorBrush)wpfAddressCtrl.MyControl_Background;
            //initForeBrush = wpfAddressCtrl.MyControl_Foreground;
            //initFontFamily = wpfAddressCtrl.MyControl_FontFamily;
            //initFontSize = wpfAddressCtrl.MyControl_FontSize;
            //initFontWeight = wpfAddressCtrl.MyControl_FontWeight;
            //initFontStyle = wpfAddressCtrl.MyControl_FontStyle;
        }

        void avAddressCtrl_OnButtonClick(object sender, Prototyping.UI.WPFControls.MyControlEventArgs args)
        {
            if (args.IsOK)
            {
                lblAddress.Text = "Street Address: " + args.MyStreetAddress;
                lblCity.Text = "City: " + args.MyCity;
                lblName.Text = "Name: " + args.MyName;
                lblState.Text = "State: " + args.MyState;
                lblZip.Text = "Zip: " + args.MyZip;
            }
            else
            {
                lblAddress.Text = "Street Address: ";
                lblCity.Text = "City: ";
                lblName.Text = "Name: ";
                lblState.Text = "State: ";
                lblZip.Text = "Zip: ";
            }
        }        
    }
}
