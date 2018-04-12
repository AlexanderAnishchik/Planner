var data = [
{
    "Id": "sdsdfsad", "EducationForm": "Денна", "OrderNumber": 1,
    "Subject": "ТКП", "DSD": "КН, 6.050101, ЕІ", "Course": 1,
    "CountStudent": 11, "GroupCode": 11,
    "PlannedLectures": 11, "DoneLectures": 11,
    "PlannedPract": 11, "DonePract": 11,
    "PlannedLaboratory": 11, "DoneLaboratory": 11,
    "PlannedSeminar": 11, "DoneSeminar": 11,
    "PlannedIndividual": 11, "DoneIndividual": 11,
    "PlannedConsultation": 11, "DoneConsultation": 11,
    "PlannedExamConsultation": 11, "DoneExamConsultation": 11,
    "PlannedCheckControl": 11, "DoneCheckControl": 11,
    "PlannedCheckLectureControl": 11, "DoneCheckLectureControl": 11,
    "PlannedEAT": 11, "DoneEAT": 11,
    "PlannedCGS": 11, "DoneCGS": 11,
    "PlannedCoursework": 11, "DoneCoursework": 11,
    "PlannedOffsetting": 11, "DoneOffsetting": 11,
    "PlannedSemestrExam": 11, "DoneSemestrExam": 11,
    "PlannedTrainingPract": 11, "DoneTrainingPract": 11,
    "PlannedStateExam": 11, "DoneStateExam": 11,
    "PlannedDiploma": 11, "DoneDiploma": 11,
    "PlannedPostgraduates": 11, "DonePostgraduates": 11,
    "PlannedTotal": 12, "DoneTotal": 12
}];
var colNames = [
    'Id',
    'Форма навчання',
    '№ п/п',
    'Назва навчальних дисциплін і видів навчальної роботи',
    'Напрям, спеціальність, факультет',
    'Курс навчання',
    'Кількість студентів',
    'Шифр груп (потоків)',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.',
    'пл.', 'вик.'
];

var editruleInteger = { required: true, integer: true, minValue: 0, maxValue: 500 };
var editruleLetters = { required: true, custom: true, custom_func: checkOnlyLetters };
var colModel = [
    { name: 'Id', index: 'Id', key: true, resizable: false , width: 25, hidden: true },
    { name: 'EducationForm', editable: true, index: 'EducationForm', resizable: false , width: 35, editrules: editruleLetters },
    { name: 'OrderNumber', index: 'OrderNumber', resizable: false , width: 40, editable: true, editrules: editruleInteger },
    { name: 'Subject', index: 'Subject', resizable: false , width: 35, editable: true, editrules: editruleLetters },
    { name: 'DSD', index: 'DSD', resizable: false , width: 60, editable: true, editrules: {required: true} },
    { name: 'Course', index: 'Course', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, edittype: 'select', editoptions: { value: { 1: '1', 2: '2', 3: '3', 4: '4', 5: '5', 6: '6' } } },
    { name: 'CountStudent', index: 'CountStudent', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, editrules: editruleInteger },
    { name: 'GroupCode', index: 'GroupCode', resizable: false , width: 40, editable: true, editrules: { required: true, custom: true, custom_func: checkGroupCode } },
    { name: 'PlannedLectures', index: 'PlannedLectures', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Читання лекцій (пл.)' }, editrules: editruleInteger },
    { name: 'DoneLectures', index: 'DoneLectures', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Читання лекцій (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedPract', index: 'PlannedPract', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення практичних (семінарських) занять (пл.)' }, editrules: editruleInteger },
    { name: 'DonePract', index: 'DonePract', resizable: false, width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення практичних (семінарських) занять (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedLaboratory', index: 'PlannedLaboratory', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення лабораторних занять (пл.)' }, editrules: editruleInteger },
    { name: 'DoneLaboratory', index: 'DoneLaboratory', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення лабораторних занять (вик.)' }, editrules: editruleInteger },
    //{ name: 'PlannedSeminar', index: 'PlannedSeminar', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення семінарських занять (пл.)' }, editrules: editruleInteger },
    //{ name: 'DoneSeminar', index: 'DoneSeminar', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення семінарських занять (вик.)' }, editrules: editruleInteger },
    //{ name: 'PlannedIndividual', index: 'PlannedIndividual', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення індивідуальних занять (пл.)' }, editrules: editruleInteger },
    //{ name: 'DoneIndividual', index: 'DoneIndividual', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення індивідуальних занять (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedConsultation', index: 'PlannedConsultation', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення консультацій протягом семестру (пл.)' }, editrules: editruleInteger },
    { name: 'DoneConsultation', index: 'DoneConsultation', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення консультацій протягом семестру (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedExamConsultation', index: 'PlannedExamConsultation', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення екзаменаційних консультацій (пл.)' }, editrules: editruleInteger },
    { name: 'DoneExamConsultation', index: 'DoneExamConsultation', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення екзаменаційних консультацій (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedCheckControl', index: 'PlannedCheckControl', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення контрольних робіт (пл.)' }, editrules: editruleInteger },
    { name: 'DoneCheckControl', index: 'DoneCheckControl', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення контрольних робіт (вик.)' }, editrules: editruleInteger },
    //{ name: 'PlannedCheckLectureControl', index: 'PlannedCheckLectureControl', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення контрольних робіт, що виконують під час самостійної роботи (пл.)' }, editrules: editruleInteger },
    //{ name: 'DoneCheckLectureControl', index: 'DoneCheckLectureControl', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення контрольних робіт, що виконують під час самостійної роботи (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedEAT', index: 'PlannedEAT', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Реферати, аналітичні огляди, переклади (пл.)' }, editrules: editruleInteger },
    { name: 'DoneEAT', index: 'DoneEAT', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Реферати, аналітичні огляди, переклади (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedCGS', index: 'PlannedCGS', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Розрахункові, графічні, розрахунково-графічні роботи (пл.)' }, editrules: editruleInteger },
    { name: 'DoneCGS', index: 'DoneCGS', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Розрахункові, графічні, розрахунково-графічні роботи (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedCoursework', index: 'PlannedCoursework', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Курсові проекти, роботи (пл.)' }, editrules: editruleInteger },
    { name: 'DoneCoursework', index: 'DoneCoursework', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Курсові проекти, роботи (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedOffsetting', index: 'PlannedOffsetting', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення заліку (пл.)' }, editrules: editruleInteger },
    { name: 'DoneOffsetting', index: 'DoneOffsetting', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення заліку (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedSemestrExam', index: 'PlannedSemestrExam', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення семестрових екзаменів (пл.)' }, editrules: editruleInteger },
    { name: 'DoneSemestrExam', index: 'DoneSemestrExam', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення семестрових екзаменів (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedTrainingPract', index: 'PlannedTrainingPract', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Керівництво навчальною і виробничою практикою (пл.)' }, editrules: editruleInteger },
    { name: 'DoneTrainingPract', index: 'DoneTrainingPract', resizable: false, width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Керівництво навчальною і виробничою практикою (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedDEK', index: 'PlannedDEK', resizable: false, width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Участь в ДЕК (пл.)' }, editrules: editruleInteger },
    { name: 'DoneDEK', index: 'DoneDEK', resizable: false, width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Участь в ДЕК (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedStateExam', index: 'PlannedStateExam', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення державних екзаменів (пл.)' }, editrules: editruleInteger },
    { name: 'DoneStateExam', index: 'DoneStateExam', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Проведення державних екзаменів (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedDiploma', index: 'PlannedDiploma', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Керівництво, консультування, рецензування та проведення захисту дипломних проектів (робіт) (пл.)' }, editrules: editruleInteger },
    { name: 'DoneDiploma', index: 'DoneDiploma', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Керівництво, консультування, рецензування та проведення захисту дипломних проектів (робіт) (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedPostgraduates', index: 'PlannedPostgraduates', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Керівництво аспірантами, здобувачами та стажуванням викладачів (пл.)' }, editrules: editruleInteger },
    { name: 'DonePostgraduates', index: 'DonePostgraduates', resizable: false , width: 40, formatter: 'integer', sorttype: 'integer', align: 'right', editable: true, formoptions: { label: 'Керівництво аспірантами, здобувачами та стажуванням викладачів (вик.)' }, editrules: editruleInteger },
    { name: 'PlannedTotal', index: 'PlannedTotal', resizable: false , width: 40,  sorttype: 'integer', align: 'right', formoptions: { label: 'Усього (пл.)' }, formatter: function (cellvalue, options, rowObject) {
        return rowObject["PlannedLectures"] + rowObject["PlannedPract"] + rowObject["PlannedLaboratory"] + rowObject["PlannedSeminar"] + rowObject["PlannedIndividual"] + rowObject["PlannedConsultation"] + rowObject["PlannedExamConsultation"] + rowObject["PlannedCheckControl"] + rowObject["PlannedCheckLectureControl"] + rowObject["PlannedEAT"] + rowObject["PlannedCGS"] + rowObject["PlannedCoursework"] + rowObject["PlannedOffsetting"] + rowObject["PlannedSemestrExam"] + rowObject["PlannedTrainingPract"] + rowObject["PlannedStateExam"] + rowObject["PlannedDiploma"] + rowObject["PlannedPostgraduates"];} },
    {name: 'DoneTotal', index: 'DoneTotal', resizable: false , width: 40, sorttype: 'integer', align: 'right', formoptions: { label: 'Усього (вик.)' }, formatter: function (cellvalue, options, rowObject) {
        return rowObject["DoneLectures"] + rowObject["DonePract"] + rowObject["DoneLaboratory"] + rowObject["DoneSeminar"] + rowObject["DoneIndividual"] + rowObject["DoneConsultation"] + rowObject["DoneExamConsultation"] + rowObject["DoneCheckControl"] + rowObject["DoneCheckLectureControl"] + rowObject["DoneEAT"] + rowObject["DoneCGS"] + rowObject["DoneCoursework"] + rowObject["DoneOffsetting"] + rowObject["DoneSemestrExam"] + rowObject["DoneTrainingPract"] + rowObject["DoneStateExam"] + rowObject["DoneDiploma"] + rowObject["DonePostgraduates"];} }];

jQuery("#Training").jqGrid({
    url: 'GetPlanTrainingJobs',
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
    pager: "#TrainingJob",
    viewrecords: true,
    sortorder: "asc",
    caption: "Навчальна робота",
    editurl: "EditPlanTrainingJobs",
    ondblClickRow: function (id) { 
        $("#Training").jqGrid('editRow', id, { 
            keys: true, 
            oneditfunc: function () { }, 
            successfunc: function (response, postdata) {
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                return [true, "", 0];
            } 
        }); 
    },
});

  $("#Training").jqGrid('navGrid', '#TrainingJob', {
    search: true,
    searchtext: "Пошук",
    refresh: false,
    add: true,
    width: 1140,
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
            var list = $("#Training");
            var selectedRow = list.getGridParam("selrow");
            var rowData = list.getRowData(selectedRow);
            if (act === "add") {
                if (jQuery("#Training").getGridParam("reccount") < 1)
                    params.url = 'SavePlanTrainingJobs';
                else[false, tablename + ". Не може мати быльше одного запису."];
                //else return [false, jQuery("#Training").getGridParam("caption") + ". Не може мати быльше одного запису."];
            }
            else if (act === "del") {
                params.url = 'DeletePlanTrainingJobs';
                params.url = 'DeletePlanTrainingJobs';
            }
        },
        afterSubmit: function (response, postdata) {
            $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
            return [true, "", 0]
        }
    };
};

jQuery("#Training").jqGrid('setGroupHeaders', {
    useColSpanStyle: true,
    groupHeaders: [
     { startColumnName: 'PlannedLectures', numberOfColumns: 2, titleText: '<div class="groupHeader">Читання лекцій</div>' },
     { startColumnName: 'PlannedPract', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення практичних (семінарських) занять</div>' },
     { startColumnName: 'PlannedLaboratory', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення лабораторних занять</div>' },
     //{ startColumnName: 'PlannedSeminar', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення семінарських занять</div>' },
     //{ startColumnName: 'PlannedIndividual', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення індивідуальних занять</div>' },
     { startColumnName: 'PlannedConsultation', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення консультацій протягом семестру</div>' },
     { startColumnName: 'PlannedExamConsultation', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення екзаменаційних консультацій</div>' },
     { startColumnName: 'PlannedCheckControl', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення контрольних робіт</div>' },
     //{ startColumnName: 'PlannedCheckLectureControl', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення контрольних робіт, що виконують під час самостійної роботи</div>' },
     { startColumnName: 'PlannedEAT', numberOfColumns: 2, titleText: '<div class="groupHeader">Реферати, аналітичні огляди, переклади</div>' },
     { startColumnName: 'PlannedCGS', numberOfColumns: 2, titleText: '<div class="groupHeader">Розрахункові, графічні, розрахунково-графічні роботи</div>' },
     { startColumnName: 'PlannedCoursework', numberOfColumns: 2, titleText: '<div class="groupHeader">Курсові проекти, роботи</div>' },
     { startColumnName: 'PlannedOffsetting', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення заліку</div>' },
     { startColumnName: 'PlannedSemestrExam', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення семестрових екзаменів</div>' },
     { startColumnName: 'PlannedTrainingPract', numberOfColumns: 2, titleText: '<div class="groupHeader">Керівництво навчальною і виробничою практикою</div>' },
     { startColumnName: 'PlannedDEK', numberOfColumns: 2, titleText: '<div class="groupHeader">Участь в ДЕК</div>' },
     { startColumnName: 'PlannedStateExam', numberOfColumns: 2, titleText: '<div class="groupHeader">Проведення державних екзаменів</div>' },
     { startColumnName: 'PlannedDiploma', numberOfColumns: 2, titleText: '<div class="groupHeader">Керівництво, консультування, рецензування та проведення захисту дипломних проектів (робіт)</div>' },
     { startColumnName: 'PlannedPostgraduates', numberOfColumns: 2, titleText: '<div class="groupHeader">Керівництво аспірантами, здобувачами та стажуванням викладачів</div>' },
    { startColumnName: 'PlannedTotal', numberOfColumns: 2, titleText: '<div class="groupHeader">Усього</div>' }
    ]
});
var grid = jQuery('#Training');
var thd = $("thead:first", grid.hDiv).get(0);
for (var i = 48; i < 55; i++) {
    $("tr th:eq(" + (i + 1) + ") div", thd).addClass("rotate");
}
$(".ui-th-column-header").height("300px");


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