namespace B3ProviderExplorer.Controls
{
    using DevExpress.XtraBars;
    using DevExpress.XtraGrid.Views.Grid;
    using System;
    using System.Data;
    using System.Linq;

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
        void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
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
            providerClient.LoadInstruments();
        }

        private void mainEquityView_MasterRowExpanding(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowCanExpandEventArgs e)
        {
            e.Allow = true;
        }

        private void mainEquityView_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "Options";
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
    }
}
