﻿//** Funcion que Agregar nuevos tabs a un TabPanel
var addTab = function (tabPanel, menuPanel, url) {
    var item = menuPanel.items.items[0].activeItem;
    var tab = tabPanel.getComponent("id" + item.text);

    if (!tab) {
        tab = tabPanel.add({
            id: "id" + item.text,
            title: "Informe: " + Ext.util.Format.uppercase(item.text),
            closable: true,
            menuItem: item,
            loader: {
                url: url,
                renderer: "frame",
                loadMask: {
                    showMask: true,
                    msg: "Cargando ..."
                }
            }
        });

        tab.on("activate", function (tab) {
            menuPanel.setSelection(tab.menuItem);
        });
    }

    tabPanel.setActiveTab(tab);
}

//** Funcion para Filtrado de los Grid
function FilterGrid(grid) {
    filterStore = [];
    getFilter = getRecordFilterCompra();

    _Filter(grid, getFilter);
}
