//** ORDENES DE SUMINISTRO
/** btnEntrada */
function btnEntrada_Click() {
    var record = App.data_Articulo.getStore().getById(App.txtCodArticulo.getValue());

    if (!Ext.isEmpty(record))
        App.data_Articulo.getStore().remove(record);

    App.direct.btnEntrada_Click(App.data_Articulo.getRowsValues());
}

function txtCantidad_Valid() {
    if (Number(App.txtCantidad.getValue()) > Number(App.txtSaldo.getValue())) {
        Ext.Msg.show({ title: 'Sigc. App.', msg: 'Cantidad para suministro es mayor al saldo.', buttons: Ext.Msg.OK, icon: Ext.MessageBox.WARNING, closable: false });
        App.txtCantidad.setValue('');
    }
}