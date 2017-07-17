using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace Prototyping.UI.Windows
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void treeViewAdv1_MouseHover(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("teste");
            Point p = this.treeViewAdv1.PointToClient(new Point(MousePosition.X, MousePosition.Y));
            Syncfusion.Windows.Forms.Tools.TreeNodeAdv node = this.treeViewAdv1.PointToNode(p);
            if ((node != null) && (node.TextBounds.Contains(p)))
            {
                ToolTipInfo t1 = new ToolTipInfo();
                t1.Body.Text = node.Text;
                Point mouseLoc = Control.MousePosition;
                mouseLoc.Offset(0, 0);
                this.superToolTip1.Show(t1, mouseLoc);
            }
        }

        private void treeViewAdv1_MouseHover_1(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("teste");
        }

        private void treeView1_MouseHover(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("teste");
        }
    }
}
