using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using NLog;

namespace B3ProviderExplorer
{
    public partial class PrincipalForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private static Logger logger;

        XtraUserControl employeesUserControl;
        XtraUserControl customersUserControl;
        public PrincipalForm()
        {
            InitializeComponent();
            employeesUserControl = CreateUserControl("Employees");
            customersUserControl = CreateUserControl("Customers");
            accordionControl.SelectedElement = acgBovespa;
        }
        XtraUserControl CreateUserControl(string text)
        {
            XtraUserControl result = new XtraUserControl();
            result.Name = text.ToLower() + "UserControl";
            result.Text = text;
            LabelControl label = new LabelControl();
            label.Parent = result;
            label.Appearance.Font = new Font("Tahoma", 25.25F);
            label.Appearance.ForeColor = Color.Gray;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.AutoSizeMode = LabelAutoSizeMode.None;
            label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label.Text = text;
            return result;
        }
        void accordionControl_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {
            if (e.Element == null) return;
            XtraUserControl userControl = e.Element.Text == "Employees" ? employeesUserControl : customersUserControl;
            tabbedView.AddDocument(userControl);
            tabbedView.ActivateDocument(userControl);
        }
        void barButtonNavigation_ItemClick(object sender, ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            accordionControl.SelectedElement = mainAccordionGroup.Elements[barItemIndex];
        }
        void tabbedView_DocumentClosed(object sender, DocumentEventArgs e)
        {
            RecreateUserControls(e);
            SetAccordionSelectedElement(e);
        }
        void SetAccordionSelectedElement(DocumentEventArgs e)
        {
            if (tabbedView.Documents.Count != 0)
            {
                if (e.Document.Caption == "Employees") accordionControl.SelectedElement = aceLoadInstruments;
                else accordionControl.SelectedElement = acgBovespa;
            }
            else
            {
                accordionControl.SelectedElement = null;
            }
        }
        void RecreateUserControls(DocumentEventArgs e)
        {
            if (e.Document.Caption == "Employees") employeesUserControl = CreateUserControl("Employees");
            else customersUserControl = CreateUserControl("Customers");
        }

        private void aceLoadInstruments_Click(object sender, EventArgs e)
        {
            var instrumentForm = new Controls.InstrumentsUserControl();
            instrumentForm.Text = "Instruments";
            tabbedView.AddDocument(instrumentForm);
            tabbedView.ActivateDocument(instrumentForm);
        }

        private void aceLoadCurrentMarketData_Click(object sender, EventArgs e)
        {
            var quotesForm = new Controls.QuotesUserControl();
            quotesForm.Text = "Quotes";
            tabbedView.AddDocument(quotesForm);
            tabbedView.ActivateDocument(quotesForm);
        }

        private void aceLoadHistory_Click(object sender, EventArgs e)
        {

        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            logger = LogManager.GetLogger("PrincipalForm");
            logger.Info("Started");
        }
    }
};