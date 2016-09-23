/** Select nodes */
function getFormularios(record) {
    var chk = record.data.checked;

    record.data.checked = !chk;
    record.commit(false);

    if (!record.isLeaf()) {
        this.setNodesChildCheked(record);
    }

    if (!record.parentNode.data.root) {
        record.parentNode.data.checked = this.getNodesChildCheked(record.parentNode);
        record.parentNode.commit(false);
    }
}

function setNodesChildCheked(node) {
    var nodeChild = node.childNodes;
    var chk = node.data.checked;

    if (chk) node.expand();
    else node.collapse();

    for (var n in nodeChild) {
        nodeChild[n].data.checked = chk;
        nodeChild[n].commit(false);

        if (!nodeChild[n].isLeaf()) {
            this.setNodesChildCheked(nodeChild[n]);
        }
    }
}

function getNodesChildCheked(nodeParent) {
    var nodeChild = nodeParent.childNodes;
    var n = 0;

    for (n in nodeChild) {
        if (!nodeChild[n].data.checked) {
            return false;
        }
    }

    return true;
}

//** carga User
function filter(array, value) {
    for (var n in array) {
        if (array[n]["codpage"] == value) {
            return true;
        }
    }
    return false;
}

function UserLoad(a) {
    var select = App.data_Modulo.getSelectionModel().getSelection();

    if (select.length > 0) {
        var user = App.data_User.getSelectionModel().getSelection()[0];
        App.TreePanel1.clearChecked(App.TreePanel1.getRootNode());
        var _n = App.TreePanel1.getRootNode().firstChild.childNodes;
        this.getChildren(_n, a);
        App.Panel3.setTitle("Usuario: " + user.data["nombre"] + " - " + user.data["login"]);
    }
    else {
        Ext.Msg.show({
            title: 'Sigc.Net',
            msg: 'Seleccione un Modulo para visualizar los Formularios.',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO,
            closable: false
        });
        App.data_User.getSelectionModel().deselectAll(false);
    }
}

function getChildren(child, array) {
    var _n = child;

    for (var i in _n) {
        if (!_n[i].data.leaf) {
            this.getChildren(_n[i].childNodes, array);
        }
        else {
            if (this.filter(array, _n[i].data.id)) {
                _n[i].data.checked = true;
                _n[i].commit(false);
            }

            if (!_n[i].parentNode.data.root) {
                _n[i].parentNode.data.checked = this.getNodesChildCheked(_n[i].parentNode);
                _n[i].parentNode.commit(false);
            }
        }
    }
}

//** guardar
function detectedCkeckeds() {
    var mod = App.data_Modulo.getSelectionModel().getSelection();
    var user = App.data_User.getSelectionModel().getSelection();

    if (user.length < 1) {
        Ext.Msg.show({
            title: 'Sigc.Net',
            msg: 'Seleccione un Usuario para guardar los cambios.',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO,
            closable: false
        });
        return;
    }

    var n = App.TreePanel1.getChecked();
    var getNode = [];

    for (var i in n) {
        getNode.push(n[i].data);
    }

    App.direct.btnGuardar_Click(getNode, mod[0].data);
}

//** Varible que almacena el filtro del Store
var filterStore = [];

//** Filtro campos data_Modulo
function getRecordFilter() {
    var f = [];
    filterStore = [];

    f.push({
        filter: function (record) {
            return filterString(App.txtUsuario.getValue(), "nombre", record);
        }
    });

    f.push({
        filter: function (record) {
            return filterString(App.txtLogin.getValue(), "login", record);
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

//** Clear Filter data_Modulo
function clearFilter() {
    App.txtUsuario.reset();
    App.txtLogin.reset();

    App.data_Modulo.getStore().clearFilter();
}
