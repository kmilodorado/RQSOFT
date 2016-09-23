//** Funcion para Abrir Reportes en Windows Emergentes
function reportPDF(url, nameFile) {
    window.open(url, nameFile);
}

/** Funciones de Filtro  **/
/* Filtro */
function _Filter(gridPanel, getFilter) {
    var store = gridPanel.getStore();
    store.filterBy(getFilter);
    store.applyPaging();
};

/* Tipado de Filtros */
var filterString = function (value, dataIndex, record) {
    var val = record.get(dataIndex);

    if (typeof val != "string") {  
        return value.length == 0;
    }

    return val.toLowerCase().indexOf(value.toLowerCase()) > -1;
};

var filterDate = function (value, dataIndex, record) {
    var val = Ext.Date.clearTime(record.get(dataIndex), true).getTime();

    if (!Ext.isEmpty(value, false) && val != Ext.Date.clearTime(value, true).getTime()) {
        return false;
    }
    return true;
};

var filterNumber = function (value, dataIndex, record) {
    var val = record.get(dataIndex);

    if (!Ext.isEmpty(value, false) && val != value) {
        return false;
    }

    return true;
};

/*Function String.Format */
String.prototype.format = function () {
    var content = this;
    for (var i = 0; i < arguments.length; i++) {
        var replacement = '{' + i + '}';
        content = content.replace(replacement, arguments[i]);
    }
    return content;
};
