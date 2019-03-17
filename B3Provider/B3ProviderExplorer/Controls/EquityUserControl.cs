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
        private OptionUserControl optionsControl = null;

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
            Initialize();
        }

        private void Initialize()
        {
            SetupYearSpin();
        }

        private void SetupYearSpin()
        {
            repositoryYearHistoryDownload.MaxValue = DateTime.Today.Year;
            repositoryYearHistoryDownload.MinValue = DateTime.Today.Year - 10;
            YearHistoryDownload.EditValue = DateTime.Today.Year;
        }

        private void LoadAllData()
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
            loggingAction?.BeginInvoke("Loading Instruments", null, null); //fire and forget
            providerClient.LoadInstruments();

            loggingAction?.BeginInvoke("Loading Quotes", null, null); //fire and forget
            providerClient.LoadQuotes();


            loggingAction?.BeginInvoke("Loading 2019", null, null); //fire and forget
            providerClient.LoadHistoricQuotes(2019);

            loggingAction?.BeginInvoke("Loading 2018", null, null); //fire and forget
            providerClient.LoadHistoricQuotes(2018);

            //loggingAction?.BeginInvoke("Calculating changes", null, null); //fire and forget
            //providerClient.CalculateHistoricChanges();
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
                if (masterView != null)
                {
                    var masterRecord = masterView.IsDataRow(e.FocusedRowHandle) ? masterView.GetRow(e.FocusedRowHandle) as B3EquityInfo : null;
                    var options = (masterRecord != null) ? providerClient.OptionInstruments.Where(o => o.B3IDUnderlying == masterRecord?.B3ID).ToList() : null;

                    bbiOptions.Enabled = (options != null && options.Count > 0) ? true : false;
                    selectedOptions = (options != null && options.Count > 0) ? options : null;
                    ShowOptions(selectedOptions);
                }
            }
        }

        private void ShowOptions(List<B3OptionOnEquityInfo> options)
        {
            if (options == null || options.Count == 0)
            {
                return;
            }

            if (optionsControl == null)
            {
                return;
            }

            optionsControl.ShowOptions(options);
        }

        private void bbiOptions_ItemClick(object sender, ItemClickEventArgs e)
        {
            DocumentGroup optionsGroup = null;

            if (ParentView == null)
            {
                return;
            }

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

            if (optionsControl == null)
            {
                optionsControl = new OptionUserControl();
                optionsControl.Text = "Options";
                var document = (Document)ParentView.AddDocument(optionsControl);
                ParentView.Controller.Dock(document, optionsGroup);
                document.Disposed += OptioDocument_Disposed;
            }

            ShowOptions(selectedOptions);
        }

        private void OptioDocument_Disposed(object sender, EventArgs e)
        {
            optionsControl = null;
        }

        private void DownloadAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.LoadAllData();
        }
    }
}
