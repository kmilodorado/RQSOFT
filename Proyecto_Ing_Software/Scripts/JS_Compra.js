//** Varible que almacena el filtro del Store
var filterStore = [];

//** Filtro campos data_Proveedor */
var getRecordFilterProveedor = function () {
    var f = [];
    filterStore = [];

    f.push({
        filter: function (record) {
            return filterNumber(App.txt_ProveedorNit.getValue(), "codproveedor", record);
        }
    });

    f.push({
        filter: function (record) {
            return filterString(App.txt_Proveedor.getValue(), "proveedor", record);
        }
    });

    f.push({
        filter: function (record) {
            return filterString(App.txt_Establecimiento.getValue(), "establecimiento", record);
        }
    });

    var len = f.length;

    return function (record) {
        for (var i = 0; i < len; i++) {
            if (!f[i].filter(record)) {
                return false;
            }
        }
        filterStore.push(record.data);
        return true;
    };
};

//** Clear Filter data_Proveedor */
function clearFilterProveedor() {
    App.txt_ProveedorNit.reset();
    App.txt_Proveedor.reset();
    App.txt_Establecimiento.reset();

    App.data_Proveedor.getStore().clearFilter();
}

/** cbxTipoDcto: Validación del Tipo de Descuento */
function getTipoDescuento(value) {
    App.txtDctoTotal.setValue('0');
    App.txtDctoUnidad.setValue('0');

    if (Ext.isEmpty(value)) {
        App.txtDctoTotal.setDisabled(true);
        App.txtDctoUnidad.setDisabled(true);
    }
    else if (parseInt(value) > 1) {
        App.txtDctoTotal.setDisabled(true);
        App.txtDctoUnidad.setDisabled(false);
    }
    else {
        App.txtDctoTotal.setDisabled(false);
        App.txtDctoUnidad.setDisabled(true);
    }

    switch (parseInt(value)) {
        case 0:
            App.txtDctoTotal.setMaxValue(Number.MAX_VALUE);
            break;
        case 1:
            App.txtDctoTotal.setMaxValue(100);
            break;
        case 2:
            App.txtDctoUnidad.setMaxValue(Number.MAX_VALUE);
            break;
        case 3:
            App.txtDctoUnidad.setMaxValue(100);
            break;
    }
}

/** btnEntrada_Click */
function btnEntrada_Click() {
    if (App.FormPanel1.isValid() && App.FormPanel2.isValid()) {
        var record = App.data_Articulo.getStore().getById(App.txtCodArticulo.getValue());

        if (!Ext.isEmpty(record))
            App.data_Articulo.getStore().remove(record);

        var _rProveedor = App.storeProveedor.getById(App.txtProveedorNit.getValue());
        var _rArticulo = App.cbxArticulo.getStore().getById(Ext.util.Format.uppercase(App.txtCodArticulo.getValue()));

        App.direct.btnEntrada_Click(_rProveedor.data, _rArticulo.data);
    }
}
