using Prototyping.UI.WPFControls.Model;
using Syncfusion.Olap.Manager;
using Syncfusion.Olap.Reports;

namespace Prototyping.UI.WPFControls.ViewModel
{
    public class PrimeiroCuboViewModel
    {
        private PrimeiroCuboModel _model;
        private OlapDataManager _olapDataManager;

        public PrimeiroCuboViewModel()
        {
            _model = new PrimeiroCuboModel("teste");
            _olapDataManager = new OlapDataManager(_model.ConnectionString);
            _olapDataManager.SetCurrentReport(CreateReport());
        }

        private OlapReport CreateReport()
        {
            OlapReport olapReport = new OlapReport();
            olapReport.CurrentCubeName = "Stress Cenario";
            //Defining the categorical dimension element
            DimensionElement dimensionElementColumn = new DimensionElement();
            dimensionElementColumn.Name = "Calendario";
            dimensionElementColumn.AddLevel("Geography Hierarchy", "Country");
            ////Specifying the measure element
            MeasureElements measureElementColumn = new MeasureElements();
            measureElementColumn.Elements.Add(new MeasureElement { Name = "Quantity" });
            //Specifying the series dimension element
            DimensionElement dimensionElementRow = new DimensionElement();
            dimensionElementRow.Name = "Sales Transaction";
            dimensionElementRow.AddLevel("Fiscal Year", "Year");
            // Adding column members to the manager
            olapReport.CategoricalElements.Add(dimensionElementColumn);
            //Adding measure elements to the manager in the column axis
            olapReport.CategoricalElements.Add(measureElementColumn);
            //Adding row members to the manager
            olapReport.SeriesElements.Add(dimensionElementRow);
            return olapReport;
        }
    }
}
