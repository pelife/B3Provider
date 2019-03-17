namespace B3ProviderExplorer.Controls
{
    partial class EquityUserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EquityUserControl));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.bdsEquity = new System.Windows.Forms.BindingSource(this.components);
            this.mainEquityView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colB3ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCapitalization = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSectorClassificationSetorEconomico = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSectorClassificationSubsetorEconomico = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSectorClassificationSegmentoEconomico = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoadDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colISIN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiPrintPreview = new DevExpress.XtraBars.BarButtonItem();
            this.bsiRecordsCount = new DevExpress.XtraBars.BarStaticItem();
            this.bbiNew = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.bbiStocks = new DevExpress.XtraBars.BarButtonItem();
            this.bbiOptions = new DevExpress.XtraBars.BarButtonItem();
            this.bbiFilter = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClosingDate = new DevExpress.XtraBars.BarEditItem();
            this.ridClosingDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.bbiNextClose = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPreviousClose = new DevExpress.XtraBars.BarButtonItem();
            this.DownloadStaticData = new DevExpress.XtraBars.BarButtonItem();
            this.DownloadMarketData = new DevExpress.XtraBars.BarButtonItem();
            this.DownloadHistory = new DevExpress.XtraBars.BarButtonItem();
            this.YearHistoryDownload = new DevExpress.XtraBars.BarEditItem();
            this.repositoryYearHistoryDownload = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.DownloadAll = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgEquity = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ToolsRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.MainSplashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::B3ProviderExplorer.Controls.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl), true);
            this.gridSplitContainer1 = new DevExpress.XtraGrid.GridSplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsEquity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainEquityView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ridClosingDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ridClosingDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryYearHistoryDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).BeginInit();
            this.gridSplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.DataSource = this.bdsEquity;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.mainEquityView;
            this.gridControl.MenuManager = this.ribbonControl;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(800, 442);
            this.gridControl.TabIndex = 2;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mainEquityView});
            // 
            // bdsEquity
            // 
            this.bdsEquity.DataSource = typeof(B3Provider.DTO.EquityPriceInfo);
            // 
            // mainEquityView
            // 
            this.mainEquityView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mainEquityView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCompanyName,
            this.colTicker,
            this.colLastPrice,
            this.colB3ID,
            this.colMarketCapitalization,
            this.colSectorClassificationSetorEconomico,
            this.colSectorClassificationSubsetorEconomico,
            this.colSectorClassificationSegmentoEconomico,
            this.colCurrency,
            this.colDescription,
            this.colLoadDate,
            this.colISIN});
            this.mainEquityView.GridControl = this.gridControl;
            this.mainEquityView.Name = "mainEquityView";
            this.mainEquityView.OptionsBehavior.Editable = false;
            this.mainEquityView.OptionsBehavior.ReadOnly = true;
            this.mainEquityView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.mainEquityView_FocusedRowChanged);
            // 
            // colCompanyName
            // 
            this.colCompanyName.Caption = "Company Name";
            this.colCompanyName.FieldName = "CompanyName";
            this.colCompanyName.Name = "colCompanyName";
            this.colCompanyName.Visible = true;
            this.colCompanyName.VisibleIndex = 2;
            // 
            // colTicker
            // 
            this.colTicker.Caption = "Ticker";
            this.colTicker.FieldName = "Ticker";
            this.colTicker.Name = "colTicker";
            this.colTicker.Visible = true;
            this.colTicker.VisibleIndex = 4;
            // 
            // colLastPrice
            // 
            this.colLastPrice.DisplayFormat.FormatString = "C2";
            this.colLastPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLastPrice.FieldName = "LastPrice";
            this.colLastPrice.GroupFormat.FormatString = "C2";
            this.colLastPrice.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLastPrice.Name = "colLastPrice";
            this.colLastPrice.Visible = true;
            this.colLastPrice.VisibleIndex = 7;
            // 
            // colB3ID
            // 
            this.colB3ID.Caption = "B3 ID";
            this.colB3ID.FieldName = "B3ID";
            this.colB3ID.Name = "colB3ID";
            this.colB3ID.Visible = true;
            this.colB3ID.VisibleIndex = 0;
            // 
            // colMarketCapitalization
            // 
            this.colMarketCapitalization.Caption = "Market Cap.";
            this.colMarketCapitalization.DisplayFormat.FormatString = "N";
            this.colMarketCapitalization.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMarketCapitalization.FieldName = "MarketCapitalization";
            this.colMarketCapitalization.GroupFormat.FormatString = "N";
            this.colMarketCapitalization.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMarketCapitalization.Name = "colMarketCapitalization";
            this.colMarketCapitalization.Visible = true;
            this.colMarketCapitalization.VisibleIndex = 6;
            // 
            // colSectorClassificationSetorEconomico
            // 
            this.colSectorClassificationSetorEconomico.Caption = "Economic Sector";
            this.colSectorClassificationSetorEconomico.FieldName = "SectorClassification.EconomicSector";
            this.colSectorClassificationSetorEconomico.Name = "colSectorClassificationSetorEconomico";
            this.colSectorClassificationSetorEconomico.Visible = true;
            this.colSectorClassificationSetorEconomico.VisibleIndex = 9;
            // 
            // colSectorClassificationSubsetorEconomico
            // 
            this.colSectorClassificationSubsetorEconomico.Caption = "Econ. Subsector";
            this.colSectorClassificationSubsetorEconomico.FieldName = "SectorClassification.EconomicSubSector";
            this.colSectorClassificationSubsetorEconomico.Name = "colSectorClassificationSubsetorEconomico";
            this.colSectorClassificationSubsetorEconomico.Visible = true;
            this.colSectorClassificationSubsetorEconomico.VisibleIndex = 10;
            // 
            // colSectorClassificationSegmentoEconomico
            // 
            this.colSectorClassificationSegmentoEconomico.Caption = "Segmnet";
            this.colSectorClassificationSegmentoEconomico.FieldName = "SectorClassification.EconomicSegment";
            this.colSectorClassificationSegmentoEconomico.Name = "colSectorClassificationSegmentoEconomico";
            this.colSectorClassificationSegmentoEconomico.Visible = true;
            this.colSectorClassificationSegmentoEconomico.VisibleIndex = 11;
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "Ccy";
            this.colCurrency.FieldName = "Currency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 5;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 3;
            // 
            // colLoadDate
            // 
            this.colLoadDate.Caption = "Load";
            this.colLoadDate.DisplayFormat.FormatString = "d";
            this.colLoadDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLoadDate.FieldName = "LoadDate";
            this.colLoadDate.GroupFormat.FormatString = "d";
            this.colLoadDate.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLoadDate.Name = "colLoadDate";
            this.colLoadDate.Visible = true;
            this.colLoadDate.VisibleIndex = 8;
            // 
            // colISIN
            // 
            this.colISIN.Caption = "ISIN";
            this.colISIN.FieldName = "ISIN";
            this.colISIN.Name = "colISIN";
            this.colISIN.Visible = true;
            this.colISIN.VisibleIndex = 1;
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.bbiPrintPreview,
            this.bsiRecordsCount,
            this.bbiNew,
            this.bbiEdit,
            this.bbiDelete,
            this.bbiRefresh,
            this.bbiStocks,
            this.bbiOptions,
            this.bbiFilter,
            this.bbiClosingDate,
            this.bbiNextClose,
            this.bbiPreviousClose,
            this.DownloadStaticData,
            this.DownloadMarketData,
            this.DownloadHistory,
            this.YearHistoryDownload,
            this.DownloadAll});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 35;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ridClosingDate,
            this.repositoryYearHistoryDownload});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.Size = new System.Drawing.Size(800, 132);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // bbiPrintPreview
            // 
            this.bbiPrintPreview.Caption = "Print Preview";
            this.bbiPrintPreview.Id = 14;
            this.bbiPrintPreview.ImageOptions.ImageUri.Uri = "Preview";
            this.bbiPrintPreview.Name = "bbiPrintPreview";
            this.bbiPrintPreview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPrintPreview_ItemClick);
            // 
            // bsiRecordsCount
            // 
            this.bsiRecordsCount.Caption = "RECORDS : 0";
            this.bsiRecordsCount.Id = 15;
            this.bsiRecordsCount.Name = "bsiRecordsCount";
            // 
            // bbiNew
            // 
            this.bbiNew.Caption = "New";
            this.bbiNew.Id = 16;
            this.bbiNew.ImageOptions.ImageUri.Uri = "New";
            this.bbiNew.Name = "bbiNew";
            this.bbiNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bbiEdit
            // 
            this.bbiEdit.Caption = "Edit";
            this.bbiEdit.Id = 17;
            this.bbiEdit.ImageOptions.ImageUri.Uri = "Edit";
            this.bbiEdit.Name = "bbiEdit";
            this.bbiEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bbiDelete
            // 
            this.bbiDelete.Caption = "Delete";
            this.bbiDelete.Id = 18;
            this.bbiDelete.ImageOptions.ImageUri.Uri = "Delete";
            this.bbiDelete.Name = "bbiDelete";
            this.bbiDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bbiRefresh
            // 
            this.bbiRefresh.Caption = "Refresh";
            this.bbiRefresh.Id = 19;
            this.bbiRefresh.ImageOptions.ImageUri.Uri = "Refresh";
            this.bbiRefresh.Name = "bbiRefresh";
            this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
            // 
            // bbiStocks
            // 
            this.bbiStocks.Caption = "Stocks";
            this.bbiStocks.Id = 23;
            this.bbiStocks.ImageOptions.ImageUri.Uri = "Pie;Size32x32;Colored";
            this.bbiStocks.Name = "bbiStocks";
            this.bbiStocks.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiStocks_ItemClick);
            // 
            // bbiOptions
            // 
            this.bbiOptions.Caption = "Options";
            this.bbiOptions.Enabled = false;
            this.bbiOptions.Id = 23;
            this.bbiOptions.ImageOptions.ImageUri.Uri = "Superscript;Size32x32";
            this.bbiOptions.Name = "bbiOptions";
            this.bbiOptions.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiOptions_ItemClick);
            // 
            // bbiFilter
            // 
            this.bbiFilter.Caption = "Filter";
            this.bbiFilter.Id = 25;
            this.bbiFilter.ImageOptions.ImageUri.Uri = "Filter";
            this.bbiFilter.Name = "bbiFilter";
            // 
            // bbiClosingDate
            // 
            this.bbiClosingDate.Caption = "Close";
            this.bbiClosingDate.Edit = this.ridClosingDate;
            this.bbiClosingDate.EditWidth = 120;
            this.bbiClosingDate.Hint = "Close date you want results";
            this.bbiClosingDate.Id = 26;
            this.bbiClosingDate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiClosingDate.ImageOptions.Image")));
            this.bbiClosingDate.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiClosingDate.ImageOptions.LargeImage")));
            this.bbiClosingDate.Name = "bbiClosingDate";
            this.bbiClosingDate.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            toolTipTitleItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem1.Text = "Close session date";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Specify the close session reference of the calculations.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.bbiClosingDate.SuperTip = superToolTip1;
            // 
            // ridClosingDate
            // 
            this.ridClosingDate.AutoHeight = false;
            this.ridClosingDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ridClosingDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ridClosingDate.Name = "ridClosingDate";
            // 
            // bbiNextClose
            // 
            this.bbiNextClose.Hint = "Next";
            this.bbiNextClose.Id = 27;
            this.bbiNextClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiNextClose.ImageOptions.Image")));
            this.bbiNextClose.Name = "bbiNextClose";
            this.bbiNextClose.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.bbiNextClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNextClose_ItemClick);
            // 
            // bbiPreviousClose
            // 
            this.bbiPreviousClose.Hint = "Previous";
            this.bbiPreviousClose.Id = 28;
            this.bbiPreviousClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiPreviousClose.ImageOptions.Image")));
            this.bbiPreviousClose.Name = "bbiPreviousClose";
            this.bbiPreviousClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPreviousClose_ItemClick);
            // 
            // DownloadStaticData
            // 
            this.DownloadStaticData.Caption = "Static";
            this.DownloadStaticData.Id = 30;
            this.DownloadStaticData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("DownloadStaticData.ImageOptions.Image")));
            this.DownloadStaticData.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("DownloadStaticData.ImageOptions.LargeImage")));
            this.DownloadStaticData.Name = "DownloadStaticData";
            this.DownloadStaticData.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // DownloadMarketData
            // 
            this.DownloadMarketData.Caption = "Market";
            this.DownloadMarketData.Id = 31;
            this.DownloadMarketData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("DownloadMarketData.ImageOptions.Image")));
            this.DownloadMarketData.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("DownloadMarketData.ImageOptions.LargeImage")));
            this.DownloadMarketData.Name = "DownloadMarketData";
            this.DownloadMarketData.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // DownloadHistory
            // 
            this.DownloadHistory.Caption = "History";
            this.DownloadHistory.Id = 32;
            this.DownloadHistory.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("DownloadHistory.ImageOptions.Image")));
            this.DownloadHistory.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("DownloadHistory.ImageOptions.LargeImage")));
            this.DownloadHistory.Name = "DownloadHistory";
            this.DownloadHistory.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // YearHistoryDownload
            // 
            this.YearHistoryDownload.Caption = "Year";
            this.YearHistoryDownload.Edit = this.repositoryYearHistoryDownload;
            this.YearHistoryDownload.EditWidth = 70;
            this.YearHistoryDownload.Id = 33;
            this.YearHistoryDownload.Name = "YearHistoryDownload";
            // 
            // repositoryYearHistoryDownload
            // 
            this.repositoryYearHistoryDownload.AutoHeight = false;
            this.repositoryYearHistoryDownload.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryYearHistoryDownload.IsFloatValue = false;
            this.repositoryYearHistoryDownload.Mask.EditMask = "N00";
            this.repositoryYearHistoryDownload.Name = "repositoryYearHistoryDownload";
            // 
            // DownloadAll
            // 
            this.DownloadAll.Caption = "All";
            this.DownloadAll.Id = 34;
            this.DownloadAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("DownloadAll.ImageOptions.Image")));
            this.DownloadAll.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("DownloadAll.ImageOptions.LargeImage")));
            this.DownloadAll.Name = "DownloadAll";
            this.DownloadAll.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.DownloadAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DownloadAll_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.rpgEquity,
            this.ToolsRibbonPageGroup});
            this.ribbonPage1.MergeOrder = 1;
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Home";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiNew);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiEdit);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiDelete);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiRefresh);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Tasks";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.AllowTextClipping = false;
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiPrintPreview);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.ShowCaptionButton = false;
            this.ribbonPageGroup2.Text = "Print and Export";
            // 
            // rpgEquity
            // 
            this.rpgEquity.ItemLinks.Add(this.bbiStocks);
            this.rpgEquity.ItemLinks.Add(this.bbiOptions);
            this.rpgEquity.ItemLinks.Add(this.bbiFilter);
            this.rpgEquity.Name = "rpgEquity";
            this.rpgEquity.Text = "Equity";
            // 
            // ToolsRibbonPageGroup
            // 
            this.ToolsRibbonPageGroup.ItemLinks.Add(this.DownloadAll);
            this.ToolsRibbonPageGroup.ItemLinks.Add(this.DownloadStaticData);
            this.ToolsRibbonPageGroup.ItemLinks.Add(this.DownloadMarketData);
            this.ToolsRibbonPageGroup.ItemLinks.Add(this.DownloadHistory);
            this.ToolsRibbonPageGroup.ItemLinks.Add(this.YearHistoryDownload);
            this.ToolsRibbonPageGroup.ItemsLayout = DevExpress.XtraBars.Ribbon.RibbonPageGroupItemsLayout.TwoRows;
            this.ToolsRibbonPageGroup.Name = "ToolsRibbonPageGroup";
            toolTipTitleItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            toolTipTitleItem2.Text = "Tools";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Import files, load into the provider, save to database";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ToolsRibbonPageGroup.SuperTip = superToolTip2;
            this.ToolsRibbonPageGroup.Text = "Tools";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.bsiRecordsCount);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 574);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(800, 26);
            // 
            // MainSplashScreenManager
            // 
            this.MainSplashScreenManager.ClosingDelay = 500;
            // 
            // gridSplitContainer1
            // 
            this.gridSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSplitContainer1.Grid = this.gridControl;
            this.gridSplitContainer1.Location = new System.Drawing.Point(0, 132);
            this.gridSplitContainer1.Name = "gridSplitContainer1";
            this.gridSplitContainer1.Panel1.Controls.Add(this.gridControl);
            this.gridSplitContainer1.Size = new System.Drawing.Size(800, 442);
            this.gridSplitContainer1.TabIndex = 5;
            // 
            // EquityUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridSplitContainer1);
            this.Controls.Add(this.ribbonControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Name = "EquityUserControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.InstrumentsUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsEquity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainEquityView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ridClosingDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ridClosingDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryYearHistoryDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).EndInit();
            this.gridSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView mainEquityView;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem bbiPrintPreview;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarStaticItem bsiRecordsCount;
        private DevExpress.XtraBars.BarButtonItem bbiNew;
        private DevExpress.XtraBars.BarButtonItem bbiEdit;
        private DevExpress.XtraBars.BarButtonItem bbiDelete;
        private DevExpress.XtraBars.BarButtonItem bbiRefresh;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgEquity;        
        private DevExpress.XtraBars.BarButtonItem bbiStocks;
        private DevExpress.XtraBars.BarButtonItem bbiOptions;
        private DevExpress.XtraBars.BarButtonItem bbiFilter;
        private System.Windows.Forms.BindingSource bdsEquity;
        private DevExpress.XtraGrid.Columns.GridColumn colB3ID;
        private DevExpress.XtraGrid.Columns.GridColumn colISIN;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCapitalization;
        private DevExpress.XtraGrid.Columns.GridColumn colLastPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colLoadDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSectorClassificationSetorEconomico;
        private DevExpress.XtraGrid.Columns.GridColumn colSectorClassificationSubsetorEconomico;
        private DevExpress.XtraGrid.Columns.GridColumn colSectorClassificationSegmentoEconomico;
        private DevExpress.XtraSplashScreen.SplashScreenManager MainSplashScreenManager;
        private DevExpress.XtraBars.BarEditItem bbiClosingDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit ridClosingDate;
        private DevExpress.XtraBars.BarButtonItem bbiNextClose;
        private DevExpress.XtraBars.BarButtonItem bbiPreviousClose;
        private DevExpress.XtraGrid.GridSplitContainer gridSplitContainer1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ToolsRibbonPageGroup;
        private DevExpress.XtraBars.BarButtonItem DownloadStaticData;
        private DevExpress.XtraBars.BarButtonItem DownloadMarketData;
        private DevExpress.XtraBars.BarButtonItem DownloadHistory;
        private DevExpress.XtraBars.BarEditItem YearHistoryDownload;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryYearHistoryDownload;
        private DevExpress.XtraBars.BarButtonItem DownloadAll;
    }
}
