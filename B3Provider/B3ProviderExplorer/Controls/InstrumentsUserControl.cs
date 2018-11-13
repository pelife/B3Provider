namespace B3ProviderExplorer.Controls
{
    using DevExpress.XtraBars;
    using DevExpress.XtraGrid.Views.Grid;
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class InstrumentsUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private B3Provider.B3ProviderConfig providerClientConfig = null;
        private B3Provider.B3ProviderClient providerClient = null;

        public InstrumentsUserControl()
        {
            InitializeComponent();
            providerClientConfig = new B3Provider.B3ProviderConfig();
            providerClient = new B3Provider.B3ProviderClient(providerClientConfig);
            //gridControl.DataSource = dataSource;
            //bsiRecordsCount.Caption = "RECORDS : " + dataSource.Count;
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
                loggingAction.BeginInvoke("Loading Instruments", null, null); //fire and forget
            providerClient.LoadInstruments();

            if (loggingAction != null)
                loggingAction.BeginInvoke("Loading Quotes", null, null); //fire and forget
            providerClient.LoadQuotes();
        }

        private void ChangeLoadingMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { MainSplashScreenManager.SetWaitFormDescription(message); });
            }
            else
            {
                MainSplashScreenManager.SetWaitFormDescription(message);
            }
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
    }
}
