var colNames = [
    'Змiст',
    'Пiдсумковий результат',
    'Термiн виконання',
    'Плановий обсяг',
    'Фактичний обсяг'
];

var editruleInteger = { required: true, integer: true, minValue: 0, maxValue: 500 };

var editruleLetters = { required: true, custom: true, custom_func: checkOnlyLetters };

var colModel = [
    { name: 'Content', index: 'Content', resizable: false, width: 600, editrules: editruleLetters },
    { name: 'Result', index: 'Result', width: 150, resizable: false, editable: true, editrules: editruleLetters },
    { name: 'DurationTime', index: 'DurationTime', formatter: 'integer', sorttype: 'integer', align: 'right', width: 150, resizable: false, editable: true, editrules: editruleLetters },
    { name: 'PlannedVolume', index: 'PlannedVolume', formatter: 'integer', sorttype: 'integer', align: 'right', width: 150, resizable: false, editable: true, editrules: editruleLetters },
    { name: 'ActualVolume', index: 'ActualVolume', formatter: 'integer', sorttype: 'integer', align: 'right', width: 150, resizable: false, editable: true, editrules: editruleLetters }
];


jQuery("#JQTable").jqGrid({
    url: gridSettings.loadurl,
    datatype: "json",
    method: 'POST',
    width: 1100,
    colNames: colNames,
    colModel: colModel,
    rowNum: 50,
    rowTotal: 2000,
    rowList: [20, 30, 50],
    scroll: 1,
    loadonce: true,
    rownumbers: true,
    rownumWidth: 40,
    gridview: true,
    sortname: 'Id',
    autowidth: false,
    shrinkToFit: false,
    forceFit: true,
    pager: "#JQPager",
    viewrecords: true,
    sortorder: "desc",
    caption: gridSettings.caption,
    editurl: gridSettings.editurl,
    ondblClickRow: function(id) {
        $("#JQTable").jqGrid('editRow', id, {
            keys: true,
            oneditfunc: function() {},
            successfunc: function(response, postdata) {
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                return [true, "", 0];
            }
        });
    }
});

$("#JQTable").jqGrid('navGrid', '#JQPager', {
    search: true,
    searchtext: "Пошук",
    refresh: false,
    add: true,
    del: true,
    edit: false,
    view: false,
    viewtitle: "Вибір",
    addtext: "Додати",
    deltext: "Видалити"
},
  update("edit"),
  update("add"),
  update("del")
);

function update(act) {
    return {
        closeAfterAdd: true,
        closeAfterEdit: true,
        width: '100%',
        reloadAfterSubmit: true,
        drag: true,
        onclickSubmit: function (params) {
            var list = $("#JQTable");
            var selectedRow = list.getGridParam("selrow");
            var rowData = list.getRowData(selectedRow);
            if (act === "add")
                params.url = gridSettings.addurl;
            else if (act === "del") {
                params.url = gridSettings.delurl;
            }
        },
        afterSubmit: function (response, postdata) {
            $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
            return [true, "", 0];
        }
    };
};

function checkOnlyLetters(value, colname) {
    var reg = new RegExp("^[а-яА-ЯёЁa-zA-Z\ \.\,\_\-]{2,}$");
    if (!reg.test(value))
        return [false, colname + ". Введіть тільки букви. Не менше 2-х."];
    else
        return [true, ""];
}

function checkGroupCode(value, colname) {
    var reg = new RegExp("^[0-9]{1,2}\.[0-9]{1,2}\.[0-9]{1,2}\.[0-9]{1,2}\.[0-9]{1,2}$");
    if (!reg.test(value))
        return [false, colname + ". Введіть коректно шифр групи. (Наприклад: 6.04.52.15.01)"];
    else
        return [true, ""];
}