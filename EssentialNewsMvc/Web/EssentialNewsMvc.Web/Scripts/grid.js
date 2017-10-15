$(function () {
    $("#jqGrid").jqGrid({
        url: "/Administration/Articles/GetArticles",
        datatype: 'json',
        mtype: 'Get',
        //loadonce: true,
        sortable: true,
        colNames: ['ID', 'Title', 'Content', 'Author', 'Deleted', 'Created On', 'Deleted On'],
        colModel: [
            { key: true, hidden: true, name: 'Id', index: 'Id', editable: true },
            { key: false, name: 'Title', index: 'Title', editable: true, sortable: true, sorttype: 'text' },
            { key: false, name: 'Content', index: 'Content', editable: true },
            { key: false, name: 'UserName', index: 'UserName', editable: false },
            { key: false, name: 'IsDeleted', index: 'IsDeleted', editable: true, edittype: 'select', editoptions: { value: { 'true': 'Deleted', 'false': 'Not Deleted' } } },
            { key: false, name: 'CreatedOn', index: 'CreatedOn', editable: true, formatter: 'date', formatoptions: { newformat: 'm/d/Y' }, editoptions: { dataInit: function (el) { setTimeout(function () { $(el).datepicker(); }, 200); } } },
            { key: false, name: 'DeletedOn', index: 'DeletedOn', editable: true, formatter: 'date', formatoptions: { newformat: 'm/d/Y' }, editoptions: { dataInit: function (el) { setTimeout(function () { $(el).datepicker(); }, 200); } } }],
        pager: jQuery('#jqControls'),
        rowNum: 10,
        rowList: [10, 20, 30, 40, 50],
        height: '100%',
        viewrecords: true,
        caption: 'Articles Records',
        emptyrecords: 'No Articles Records are Available to Display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#jqControls', { edit: true, add: false, del: true, search: false, refresh: true },
        {
            zIndex: 100,
            url: '/Administration/Articles/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            url: "/Administration/Articles/Delete",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        }, 
        {
            zIndex: 100,
            url: "/Administration/Articles/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure ? ",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
});
  
