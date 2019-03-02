namespace B3ProviderExplorer.Controls
{
    using B3Provider.Records;
    using DevExpress.XtraBars;
    using DevExpress.XtraBars.Docking2010.Views.Tabbed;
    using DevExpress.XtraGrid.Views.Grid;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class EquityUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private B3Provider.B3ProviderConfig providerClientConfig = null;
        private B3Provider.B3ProviderClient providerClient = null;
        private List<B3OptionOnEquityInfo> selectedOptions = null;

        public TabbedView ParentView { get; set; }

        public EquityUserControl(TabbedView parentView)
        {
            InitializeComponent();
            providerClientConfig = new B3Provider.B3ProviderConfig();
            providerClient = new B3Provider.B3ProviderClient(providerClientConfig);
            ParentView = parentView;
        }

        private void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl.ShowRibbonPrintPreview();
        }


        private void bbiRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void bbiStocks_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl.DataSource = providerClient.EquityInstruments;
            bsiRecordsCount.Caption = "RECORDS : " + providerClient.EquityInstruments.Count;
        }

        private void InstrumentsUserControl_Load(object sender, EventArgs e)
        {
            PreLoadData();
            LoadData();
        }

        private void PreLoadData()
        {
            MainSplashScreenManager.ShowWaitForm();
        }

        private void LoadData()
        {
            Task.Factory.StartNew(() => { FetchData(new Action<string>(ChangeLoadingMessage)); }).ContinueWith((task) => { PostLoadData(task); });
        }

        private void FetchData(Action<string> loggingAction)
        {
            if (loggingAction != null)
            {
                loggingAction.BeginInvoke("Loading Instruments", null, null); //fire and forget
            }

            providerClient.LoadInstruments();

            if (loggingAction != null)
            {
                loggingAction.BeginInvoke("Loading Quotes", null, null); //fire and forget
            }

            providerClient.LoadQuotes();

            if (loggingAction != null)
            {
                loggingAction.BeginInvoke("Loading 2019", null, null); //fire and forget
            }

            providerClient.LoadHistoricQuotes(2019);

            if (loggingAction != null)
            {
                loggingAction.BeginInvoke("Loading 2018", null, null); //fire and forget
            }

            providerClient.LoadHistoricQuotes(2018);

            if (loggingAction != null)
            {
                loggingAction.BeginInvoke("Calculating changes", null, null); //fire and forget
            }

            providerClient.CalculateHistoricChanges();
        }

        private void ChangeLoadingMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { ShowAndLogMessage(message); });
            }
            else
            {
                ShowAndLogMessage(message);
            }
        }

        private void ShowAndLogMessage(string message)
        {
            MainSplashScreenManager.SetWaitFormDescription(message);
            _logger.Info(message);
        }

        private void PostLoadData(Task tarefa)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { MainSplashScreenManager.CloseWaitForm(); });
            }
            else
            {
                MainSplashScreenManager.CloseWaitForm();
            }
        }

        private void mainEquityView_MasterRowGetChildList(object sender, MasterRowGetChildListEventArgs e)
        {
            if (e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                var masterView = sender as GridView;
                var masterRecord = masterView?.GetRow(e.RowHandle) as B3Provider.Records.B3EquityInfo;
                var options = providerClient.OptionInstruments.Where(o => o.B3IDUnderlying == masterRecord?.B3ID).ToList();

                e.ChildList = options;
            }
        }

        private void bbiPreviousClose_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void bbiNextClose_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void mainEquityView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.ControllerRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                var masterView = sender as GridView;
                var masterRecord = masterView?.GetRow(e.ControllerRow) as B3EquityInfo;
                var options = providerClient.OptionInstruments.Where(o => o.B3IDUnderlying == masterRecord?.B3ID).ToList();
                ShowOptions(options);
            }
        }

        private void mainEquityView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                var masterView = sender as GridView;
                var masterRecord = masterView?.GetRow(e.FocusedRowHandle) as B3EquityInfo;
                var options = providerClient.OptionInstruments.Where(o => o.B3IDUnderlying == masterRecord?.B3ID).ToList();

                //bbiOptions.Enabled = (options != null && options.Count > 0) ? true : false;
                //selectedOptions = (options != null && options.Count > 0) ? options : null;
            }
        }

        private void ShowOptions(List<B3OptionOnEquityInfo> options)
        {
            if (options == null || options.Count == 0)
            {
                return;
            }
            //todo: 
            // 1 -check if the options view está aberta
            // 1.1 - se fechada, abra e defina o ativo
            // 1.2 - se aberta, mude o ativo e mande atualizar
        }

        private void bbiOptions_ItemClick(object sender, ItemClickEventArgs e)
        {
            DocumentGroup optionsGroup = null;

            if (ParentView == null)            
                return;
            

            if (ParentView.DocumentGroups.Count == 1)
            {
                ParentView.Orientation = Orientation.Vertical;
                optionsGroup = new DocumentGroup();
                ParentView.DocumentGroups.Add(optionsGroup);
            }
            else
            {
                optionsGroup = ParentView.DocumentGroups[1];
            }

            var instrumentForm = new Controls.OptionUserControl();
            instrumentForm.Text = "Options";
            var document = (Document)ParentView.AddDocument(instrumentForm);
            ParentView.Controller.Dock(document, optionsGroup);
        }
    }
}
