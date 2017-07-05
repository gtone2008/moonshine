//指定页面区域“单元格”内容导入Excel
function CellAreaExcel() {
    var oXL = new ActiveXObject("Excel.Application");
    var oWB = oXL.Workbooks.Add();
    var oSheet = oWB.ActiveSheet;
    var Lenr = MainContent_txtbase.rows.length;
    for (i = 0; i < Lenr; i++) {
        var Lenc = MainContent_txtbase.rows(i).cells.length;
        for (j = 0; j < Lenc; j++) {
            oSheet.Cells(i + 1, j + 1).value = MainContent_txtbase.rows(i).cells(j).innerText;
        }
    }
    oXL.Visible = true;
}