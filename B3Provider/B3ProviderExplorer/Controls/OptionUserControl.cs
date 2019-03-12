namespace B3ProviderExplorer.Controls
{
    using B3Provider.Records;
    using DevExpress.XtraBars;
    using DevExpress.XtraGrid.Views.Grid;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class OptionUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private B3Provider.B3ProviderConfig providerClientConfig = null;
        private B3Provider.B3ProviderClient providerClient = null;

        public OptionUserControl()
        {
            InitializeComponent();
            providerClientConfig = new B3Provider.B3ProviderConfig();
            providerClient = new B3Provider.B3ProviderClient(providerClientConfig);          
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

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl.DataSource = providerClient.OptionInstruments;
            bsiRecordsCount.Caption = "RECORDS : " + providerClient.EquityInstruments.Count;
        }

        private void InstrumentsUserControl_Load(object sender, EventArgs e)
        {
            //PreLoadData();
            //LoadData();
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
                loggingAction.BeginInvoke("Loading Instruments", null, null); //fire and forget
            providerClient.LoadInstruments();

            if (loggingAction != null)
                loggingAction.BeginInvoke("Loading Quotes", null, null); //fire and forget
            providerClient.LoadQuotes();

            if (loggingAction != null)
                loggingAction.BeginInvoke("Loading 2019", null, null); //fire and forget
            providerClient.LoadHistoricQuotes(2019);

            if (loggingAction != null)
                loggingAction.BeginInvoke("Loading 2018", null, null); //fire and forget
            providerClient.LoadHistoricQuotes(2018);

            if (loggingAction != null)
                loggingAction.BeginInvoke("Calculating changes", null, null); //fire and forget
            providerClient.CalculateHistoricChanges();


        }

        private void ChangeLoadingMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { ShowAndLogMessage(message); ; });
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

        private void mainEquityView_MasterRowExpanding(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowCanExpandEventArgs e)
        {
            e.Allow = true;
        }

        private void mainEquityView_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "";            
        }

        private void mainEquityView_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            if (e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                var masterView = sender as GridView;
                var masterRecord = masterView?.GetRow(e.RowHandle) as B3Provider.Records.B3EquityInfo;
                var options = providerClient.OptionInstruments.Where(o => o.B3IDUnderlying == masterRecord?.B3ID).ToList();

                e.ChildList = options;
            }
        }

        private void mainEquityView_MasterRowEmpty(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs e)
        {
            e.IsEmpty = false;
        }

        private void mainEquityView_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void bbiPreviousClose_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void bbiNextClose_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        internal void ShowOptions(List<B3OptionOnEquityInfo> options)
        {
            if (options == null || options.Count == 0)
                return;

            gridControl.DataSource = options;
            bsiRecordsCount.Caption = String.Format("RECORDS : {0}", options.Count);
        }
    }
}
