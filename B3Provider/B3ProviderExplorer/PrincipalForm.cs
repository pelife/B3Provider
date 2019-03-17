namespace B3ProviderExplorer
{
    using B3ProviderExplorer.Logging;
    using DevExpress.XtraBars;
    using DevExpress.XtraBars.Docking2010.Views;
    using DevExpress.XtraBars.Navigation;
    using DevExpress.XtraEditors;
    using NLog;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;

    public partial class PrincipalForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private static Logger logger;

        public static BindingList<LogEventInfo> LogCollection { get; set; } = new BindingList<LogEventInfo>();

        XtraUserControl employeesUserControl;
        XtraUserControl customersUserControl;
        public PrincipalForm()
        {
            InitializeComponent();
            employeesUserControl = CreateUserControl("Employees");
            customersUserControl = CreateUserControl("Customers");
            //accordionControl.SelectedElement = acgBovespa;
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
            //accordionControl.SelectedElement = mainAccordionGroup.Elements[barItemIndex];
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
                //if (e.Document.Caption == "Employees") accordionControl.SelectedElement = aceLoadInstruments;
                //else accordionControl.SelectedElement = acgBovespa;
            }
            else
            {
                //accordionControl.SelectedElement = null;
            }
        }
        void RecreateUserControls(DocumentEventArgs e)
        {
            if (e.Document.Caption == "Employees") employeesUserControl = CreateUserControl("Employees");
            else customersUserControl = CreateUserControl("Customers");
        }

        private void aceLoadInstruments_Click(object sender, EventArgs e)
        {
            var instrumentForm = new Controls.EquityUserControl(tabbedView);
            instrumentForm.Text = "Stocks";
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
            var memoryTarget = LogManager.Configuration.AllTargets.Where(t => t is MemoryEventTarget).FirstOrDefault() as MemoryEventTarget;

            if (memoryTarget != null)
                memoryTarget.EventReceived += MemoryTarget_EventReceived;

            loggingGridControl.DataSource = LogCollection;

            logger = LogManager.GetLogger("PrincipalForm");
            logger.Info("Started");

            MainDateNavigator.HighlightHolidays = true;
        }

        private void MemoryTarget_EventReceived(LogEventInfo message)
        {
            if (InvokeRequired)
                BeginInvoke(new Action(() => EventReceivedIternal(message)));
            else
                EventReceivedIternal(message);
        }

        private void EventReceivedIternal(LogEventInfo message)
        {
            if (LogCollection.Count >= 50) LogCollection.RemoveAt(LogCollection.Count - 1);
            LogCollection.Add(message);
        }

        private void MainDateNavigator_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {

        }

        private void loggingGridView_RowCountChanged(object sender, EventArgs e)
        {
            loggingGridView.MoveFirst();
        }

        private void loggingGridView_RowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {

        }

        private void ImportHollidaysButton_Click(object sender, EventArgs e)
        {
            if (HolidaysInputFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }

        private void aceStocks_Click(object sender, EventArgs e)
        {
            var instrumentForm = new Controls.EquityUserControl(tabbedView);
            instrumentForm.Text = "Stocks";
            tabbedView.AddDocument(instrumentForm);
            tabbedView.ActivateDocument(instrumentForm);
        }

        private void aceFuture_Click(object sender, EventArgs e)
        {
            var instrumentForm = new Controls.FutureUserControl();
            instrumentForm.Text = "Futures";
            tabbedView.AddDocument(instrumentForm);
            tabbedView.ActivateDocument(instrumentForm);
        }

        private void aceOption_Click(object sender, EventArgs e)
        {
            var instrumentForm = new Controls.OptionUserControl();
            instrumentForm.Text = "Options";
            tabbedView.AddDocument(instrumentForm);
            tabbedView.ActivateDocument(instrumentForm);

            instrumentForm.ShowOptions(null);
        }
    }
};