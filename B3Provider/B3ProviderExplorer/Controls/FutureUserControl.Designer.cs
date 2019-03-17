namespace B3ProviderExplorer.Controls
{
    partial class FutureUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FutureUserControl));
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.bdsFuture = new System.Windows.Forms.BindingSource(this.components);
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiPrintPreview = new DevExpress.XtraBars.BarButtonItem();
            this.bsiRecordsCount = new DevExpress.XtraBars.BarStaticItem();
            this.bbiNew = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.bbiFuture = new DevExpress.XtraBars.BarButtonItem();
            this.bbiOptions = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClosingDate = new DevExpress.XtraBars.BarEditItem();
            this.ridClosingDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.bbiNextClose = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPreviousClose = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgFuture = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.MainSplashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::B3ProviderExplorer.Controls.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl), true);
            this.FuturesGridControl = new DevExpress.XtraGrid.GridControl();
            this.mainFutureView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colB3ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colISIN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSecurityCategoryCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpiration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpirationCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeStart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValueTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDaycountBase = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConversionCriteriaCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaturityContractValueInPoints = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequiredConversionIndicator = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFICategoryCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryNoticeStart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryNoticeEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaymentTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractMultiplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetQuotationQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSettlementIndexInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAllocationRoundLot = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnderlyingIntrument = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWithdrawalDays = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkingDays = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCalendarDays = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoadDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bdsFuture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ridClosingDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ridClosingDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FuturesGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFutureView)).BeginInit();
            this.SuspendLayout();
            // 
            // bdsFuture
            // 
            this.bdsFuture.DataSource = typeof(B3Provider.Records.B3FutureInfo);
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
            this.bbiFuture,
            this.bbiOptions,
            this.bbiClosingDate,
            this.bbiNextClose,
            this.bbiPreviousClose});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 29;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ridClosingDate});
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
            // bbiFuture
            // 
            this.bbiFuture.Caption = "Futures";
            this.bbiFuture.Id = 23;
            this.bbiFuture.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiFuture.ImageOptions.Image")));
            this.bbiFuture.Name = "bbiFuture";
            this.bbiFuture.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.bbiFuture.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiFuture_ItemClick);
            // 
            // bbiOptions
            // 
            this.bbiOptions.Caption = "Options";
            this.bbiOptions.Id = 24;
            this.bbiOptions.ImageOptions.ImageUri.Uri = "Superscript;Size32x32;Colored";
            this.bbiOptions.Name = "bbiOptions";            
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
            toolTipTitleItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Text = "Close session date";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Specify the close session reference of the calculations.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.bbiClosingDate.SuperTip = superToolTip2;
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
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.rpgFuture});
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
            // rpgFuture
            // 
            this.rpgFuture.ItemLinks.Add(this.bbiFuture);
            this.rpgFuture.ItemLinks.Add(this.bbiOptions);
            this.rpgFuture.Name = "rpgFuture";
            this.rpgFuture.Text = "Future";
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
            // FuturesGridControl
            // 
            this.FuturesGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FuturesGridControl.Location = new System.Drawing.Point(0, 132);
            this.FuturesGridControl.MainView = this.mainFutureView;
            this.FuturesGridControl.MenuManager = this.ribbonControl;
            this.FuturesGridControl.Name = "FuturesGridControl";
            this.FuturesGridControl.Size = new System.Drawing.Size(800, 442);
            this.FuturesGridControl.TabIndex = 8;
            this.FuturesGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mainFutureView});
            // 
            // mainFutureView
            // 
            this.mainFutureView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colB3ID,
            this.colISIN,
            this.colAssetName,
            this.colAssetDescription,
            this.colDescription,
            this.colSecurityCategoryCode,
            this.colTicker,
            this.colExpiration,
            this.colExpirationCode,
            this.colTradeStart,
            this.colTradeEnd,
            this.colValueTypeCode,
            this.colDaycountBase,
            this.colConversionCriteriaCode,
            this.colMaturityContractValueInPoints,
            this.colRequiredConversionIndicator,
            this.colCFICategoryCode,
            this.colDeliveryNoticeStart,
            this.colDeliveryNoticeEnd,
            this.colDeliveryTypeCode,
            this.colPaymentTypeCode,
            this.colContractMultiplier,
            this.colAssetQuotationQuantity,
            this.colSettlementIndexInfo,
            this.colAllocationRoundLot,
            this.colCurrency,
            this.colUnderlyingIntrument,
            this.colWithdrawalDays,
            this.colWorkingDays,
            this.colCalendarDays,
            this.colLoadDate});
            this.mainFutureView.GridControl = this.FuturesGridControl;
            this.mainFutureView.Name = "mainFutureView";
            this.mainFutureView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.mainFutureView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.mainFutureView.OptionsBehavior.Editable = false;
            this.mainFutureView.OptionsBehavior.ReadOnly = true;
            // 
            // colB3ID
            // 
            this.colB3ID.FieldName = "B3ID";
            this.colB3ID.Name = "colB3ID";
            this.colB3ID.Visible = true;
            this.colB3ID.VisibleIndex = 0;
            // 
            // colISIN
            // 
            this.colISIN.Caption = "ISIN";
            this.colISIN.FieldName = "ISIN";
            this.colISIN.Name = "colISIN";
            this.colISIN.Visible = true;
            this.colISIN.VisibleIndex = 1;
            // 
            // colAssetName
            // 
            this.colAssetName.Caption = "Asset Name";
            this.colAssetName.FieldName = "AssetName";
            this.colAssetName.Name = "colAssetName";
            this.colAssetName.Visible = true;
            this.colAssetName.VisibleIndex = 2;
            // 
            // colAssetDescription
            // 
            this.colAssetDescription.Caption = "Asset Description";
            this.colAssetDescription.FieldName = "AssetDescription";
            this.colAssetDescription.Name = "colAssetDescription";
            this.colAssetDescription.Visible = true;
            this.colAssetDescription.VisibleIndex = 3;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 4;
            // 
            // colSecurityCategoryCode
            // 
            this.colSecurityCategoryCode.Caption = "Security Category";
            this.colSecurityCategoryCode.FieldName = "SecurityCategoryCode";
            this.colSecurityCategoryCode.Name = "colSecurityCategoryCode";
            this.colSecurityCategoryCode.Visible = true;
            this.colSecurityCategoryCode.VisibleIndex = 5;
            // 
            // colTicker
            // 
            this.colTicker.Caption = "Ticker";
            this.colTicker.FieldName = "Ticker";
            this.colTicker.Name = "colTicker";
            this.colTicker.OptionsColumn.AllowEdit = false;
            this.colTicker.Visible = true;
            this.colTicker.VisibleIndex = 6;
            // 
            // colExpiration
            // 
            this.colExpiration.Caption = "Expiration";
            this.colExpiration.FieldName = "Expiration";
            this.colExpiration.Name = "colExpiration";
            this.colExpiration.Visible = true;
            this.colExpiration.VisibleIndex = 7;
            // 
            // colExpirationCode
            // 
            this.colExpirationCode.Caption = "Expiration Code";
            this.colExpirationCode.FieldName = "ExpirationCode";
            this.colExpirationCode.Name = "colExpirationCode";
            this.colExpirationCode.Visible = true;
            this.colExpirationCode.VisibleIndex = 8;
            // 
            // colTradeStart
            // 
            this.colTradeStart.Caption = "Trade Start";
            this.colTradeStart.FieldName = "TradeStart";
            this.colTradeStart.Name = "colTradeStart";
            this.colTradeStart.Visible = true;
            this.colTradeStart.VisibleIndex = 9;
            // 
            // colTradeEnd
            // 
            this.colTradeEnd.Caption = "Trade End";
            this.colTradeEnd.FieldName = "TradeEnd";
            this.colTradeEnd.Name = "colTradeEnd";
            this.colTradeEnd.Visible = true;
            this.colTradeEnd.VisibleIndex = 10;
            // 
            // colValueTypeCode
            // 
            this.colValueTypeCode.Caption = "Value Type";
            this.colValueTypeCode.FieldName = "ValueTypeCode";
            this.colValueTypeCode.Name = "colValueTypeCode";
            this.colValueTypeCode.Visible = true;
            this.colValueTypeCode.VisibleIndex = 11;
            // 
            // colDaycountBase
            // 
            this.colDaycountBase.Caption = "Daycount";
            this.colDaycountBase.FieldName = "DaycountBase";
            this.colDaycountBase.Name = "colDaycountBase";
            this.colDaycountBase.ToolTip = "Daycount Base";
            this.colDaycountBase.Visible = true;
            this.colDaycountBase.VisibleIndex = 12;
            // 
            // colConversionCriteriaCode
            // 
            this.colConversionCriteriaCode.Caption = "Conv. Criteria";
            this.colConversionCriteriaCode.FieldName = "ConversionCriteriaCode";
            this.colConversionCriteriaCode.Name = "colConversionCriteriaCode";
            this.colConversionCriteriaCode.ToolTip = "Conversion Criteria Code";
            this.colConversionCriteriaCode.Visible = true;
            this.colConversionCriteriaCode.VisibleIndex = 13;
            // 
            // colMaturityContractValueInPoints
            // 
            this.colMaturityContractValueInPoints.Caption = "M. Cont. Value Points";
            this.colMaturityContractValueInPoints.FieldName = "MaturityContractValueInPoints";
            this.colMaturityContractValueInPoints.Name = "colMaturityContractValueInPoints";
            this.colMaturityContractValueInPoints.ToolTip = "Maturity Contract Value In Points";
            this.colMaturityContractValueInPoints.Visible = true;
            this.colMaturityContractValueInPoints.VisibleIndex = 14;
            // 
            // colRequiredConversionIndicator
            // 
            this.colRequiredConversionIndicator.FieldName = "RequiredConversionIndicator";
            this.colRequiredConversionIndicator.Name = "colRequiredConversionIndicator";
            this.colRequiredConversionIndicator.Visible = true;
            this.colRequiredConversionIndicator.VisibleIndex = 15;
            // 
            // colCFICategoryCode
            // 
            this.colCFICategoryCode.FieldName = "CFICategoryCode";
            this.colCFICategoryCode.Name = "colCFICategoryCode";
            this.colCFICategoryCode.Visible = true;
            this.colCFICategoryCode.VisibleIndex = 16;
            // 
            // colDeliveryNoticeStart
            // 
            this.colDeliveryNoticeStart.FieldName = "DeliveryNoticeStart";
            this.colDeliveryNoticeStart.Name = "colDeliveryNoticeStart";
            this.colDeliveryNoticeStart.Visible = true;
            this.colDeliveryNoticeStart.VisibleIndex = 17;
            // 
            // colDeliveryNoticeEnd
            // 
            this.colDeliveryNoticeEnd.FieldName = "DeliveryNoticeEnd";
            this.colDeliveryNoticeEnd.Name = "colDeliveryNoticeEnd";
            this.colDeliveryNoticeEnd.Visible = true;
            this.colDeliveryNoticeEnd.VisibleIndex = 18;
            // 
            // colDeliveryTypeCode
            // 
            this.colDeliveryTypeCode.FieldName = "DeliveryTypeCode";
            this.colDeliveryTypeCode.Name = "colDeliveryTypeCode";
            this.colDeliveryTypeCode.Visible = true;
            this.colDeliveryTypeCode.VisibleIndex = 19;
            // 
            // colPaymentTypeCode
            // 
            this.colPaymentTypeCode.FieldName = "PaymentTypeCode";
            this.colPaymentTypeCode.Name = "colPaymentTypeCode";
            this.colPaymentTypeCode.Visible = true;
            this.colPaymentTypeCode.VisibleIndex = 20;
            // 
            // colContractMultiplier
            // 
            this.colContractMultiplier.FieldName = "ContractMultiplier";
            this.colContractMultiplier.Name = "colContractMultiplier";
            this.colContractMultiplier.Visible = true;
            this.colContractMultiplier.VisibleIndex = 21;
            // 
            // colAssetQuotationQuantity
            // 
            this.colAssetQuotationQuantity.FieldName = "AssetQuotationQuantity";
            this.colAssetQuotationQuantity.Name = "colAssetQuotationQuantity";
            this.colAssetQuotationQuantity.Visible = true;
            this.colAssetQuotationQuantity.VisibleIndex = 22;
            // 
            // colSettlementIndexInfo
            // 
            this.colSettlementIndexInfo.FieldName = "SettlementIndexInfo";
            this.colSettlementIndexInfo.Name = "colSettlementIndexInfo";
            this.colSettlementIndexInfo.Visible = true;
            this.colSettlementIndexInfo.VisibleIndex = 23;
            // 
            // colAllocationRoundLot
            // 
            this.colAllocationRoundLot.FieldName = "AllocationRoundLot";
            this.colAllocationRoundLot.Name = "colAllocationRoundLot";
            this.colAllocationRoundLot.Visible = true;
            this.colAllocationRoundLot.VisibleIndex = 24;
            // 
            // colCurrency
            // 
            this.colCurrency.FieldName = "Currency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 25;
            // 
            // colUnderlyingIntrument
            // 
            this.colUnderlyingIntrument.FieldName = "UnderlyingIntrument";
            this.colUnderlyingIntrument.Name = "colUnderlyingIntrument";
            this.colUnderlyingIntrument.Visible = true;
            this.colUnderlyingIntrument.VisibleIndex = 26;
            // 
            // colWithdrawalDays
            // 
            this.colWithdrawalDays.FieldName = "WithdrawalDays";
            this.colWithdrawalDays.Name = "colWithdrawalDays";
            this.colWithdrawalDays.Visible = true;
            this.colWithdrawalDays.VisibleIndex = 27;
            // 
            // colWorkingDays
            // 
            this.colWorkingDays.FieldName = "WorkingDays";
            this.colWorkingDays.Name = "colWorkingDays";
            this.colWorkingDays.Visible = true;
            this.colWorkingDays.VisibleIndex = 28;
            // 
            // colCalendarDays
            // 
            this.colCalendarDays.FieldName = "CalendarDays";
            this.colCalendarDays.Name = "colCalendarDays";
            this.colCalendarDays.Visible = true;
            this.colCalendarDays.VisibleIndex = 29;
            // 
            // colLoadDate
            // 
            this.colLoadDate.FieldName = "LoadDate";
            this.colLoadDate.Name = "colLoadDate";
            this.colLoadDate.Visible = true;
            this.colLoadDate.VisibleIndex = 30;
            // 
            // FutureUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FuturesGridControl);
            this.Controls.Add(this.ribbonControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Name = "FutureUserControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.InstrumentsUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bdsFuture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ridClosingDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ridClosingDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FuturesGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFutureView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgFuture;        
        private DevExpress.XtraBars.BarButtonItem bbiFuture;
        private DevExpress.XtraBars.BarButtonItem bbiOptions;
        private System.Windows.Forms.BindingSource bdsFuture;
        private DevExpress.XtraSplashScreen.SplashScreenManager MainSplashScreenManager;
        private DevExpress.XtraBars.BarEditItem bbiClosingDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit ridClosingDate;
        private DevExpress.XtraBars.BarButtonItem bbiNextClose;
        private DevExpress.XtraBars.BarButtonItem bbiPreviousClose;
        private DevExpress.XtraGrid.GridControl FuturesGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView mainFutureView;
        private DevExpress.XtraGrid.Columns.GridColumn colB3ID;
        private DevExpress.XtraGrid.Columns.GridColumn colISIN;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetName;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colSecurityCategoryCode;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colExpiration;
        private DevExpress.XtraGrid.Columns.GridColumn colExpirationCode;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeStart;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeEnd;
        private DevExpress.XtraGrid.Columns.GridColumn colValueTypeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDaycountBase;
        private DevExpress.XtraGrid.Columns.GridColumn colConversionCriteriaCode;
        private DevExpress.XtraGrid.Columns.GridColumn colMaturityContractValueInPoints;
        private DevExpress.XtraGrid.Columns.GridColumn colRequiredConversionIndicator;
        private DevExpress.XtraGrid.Columns.GridColumn colCFICategoryCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryNoticeStart;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryNoticeEnd;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryTypeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPaymentTypeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colContractMultiplier;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetQuotationQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colSettlementIndexInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colAllocationRoundLot;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colUnderlyingIntrument;
        private DevExpress.XtraGrid.Columns.GridColumn colWithdrawalDays;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkingDays;
        private DevExpress.XtraGrid.Columns.GridColumn colCalendarDays;
        private DevExpress.XtraGrid.Columns.GridColumn colLoadDate;
    }
}
