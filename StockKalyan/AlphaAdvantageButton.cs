using Microsoft.Office.Tools.Ribbon;
using Excel = Microsoft.Office.Interop.Excel;

namespace StockKalyan
{
    public partial class AlphaAdvantageButton
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            //
        }

        private void Button1_Click(object sender, RibbonControlEventArgs e)
        {

            Excel.Worksheet activeWorkSheet = Globals.ThisAddIn.Application.ActiveSheet;
            if (activeWorkSheet == null) return;

            Excel.Range activeCell = Globals.ThisAddIn.Application.ActiveCell;

            // restrict to only first column = ticker column
            if (activeCell.Column != 1) return;

            if (activeCell.Value2 != null)
            {
                var command = new GetStockPriceCommand(activeCell.Text);
                string results = command.Handle();

                Excel.Range nextCell = activeWorkSheet.Cells[activeCell.Row, activeCell.Column+1];
                nextCell.Value = results;
            }

        }
    }
}
