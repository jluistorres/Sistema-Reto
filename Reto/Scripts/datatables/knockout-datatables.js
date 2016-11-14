/* ********************

Datatables Custom Binding for Knockout.js

******************** */
var local = [];
ko.bindingHandlers.dataTable = {
    'init': function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

        if ($.data(element, isInitialisedKey) === true)
            return;

        var binding = ko.utils.unwrapObservable(valueAccessor());
        var isInitialisedKey = "ko.bindingHandlers.dataTable.isInitialised";
        var options = {};

        // ** Initialise the DataTables options object with the data-bind settings **

        // Clone the options object found in the data bindings.  This object will form the base for the DataTable initialisation object.
        if (binding.options)
            options = $.extend(options, binding.options);

        // Define the tables columns.
        if (binding.columns && binding.columns.length) {
            options.aoColumns = [];
            ko.utils.arrayForEach(binding.columns, function (col) {
                options.aoColumns.push({ mDataProp: col });

            })
        }

        // Define column data attributes
        if (binding.columns && binding.columns.length) {
            options.aoColumns = [];
            ko.utils.arrayForEach(binding.columns, function (col) {

                options.aoColumns.push({ mDataProp: col.name });

                theIndex = binding.columns.indexOf(col);

                if (col.dataSort) {
                    options.aoColumns[theIndex].iDataSort = col.dataSort;
                }

                if (col.sortType) {
                    options.aoColumns[theIndex].sType = col.sortType;
                }

                if (col.sortable == false) {
                    options.aoColumns[theIndex].bSortable = col.sortable;
                }

                if (col.orderable == false) {
                    options.aoColumns[theIndex].bOrderable = col.orderable;
                }

                if (col.visible == false) {
                    options.aoColumns[theIndex].bVisible = col.visible;
                }

                //Implementado por jtorres para añadir los encabezados de busqueda a cada columna
                if (options.enableSearchInColumn && col.name) {
                    options.aoColumns[theIndex].bSearch = (typeof col.bSearch !== 'undefined') ? col.bSearch : true;
                    options.aoColumns[theIndex].sSearchType = col.sSearchType;
                    options.aoColumns[theIndex].sType = col.sType;
                }
            })
        }

        //console.log(options);

        if (binding.sortingFixed && binding.sortingFixed.length) {
            options.aaSortingFixed = [];
            ko.utils.arrayForEach(binding.sortingFixed, function (item) {
                options.aaSortingFixed.push(
            		[item.column, item.direction]
            	);

            })
        }

        if (binding.autoWidth) {
            options.bAutoWidth = binding.autoWidth;
        } else {
            options.bAutoWidth = false;
        }

        if (binding.sDom) {
            options.sDom = binding.sDom;
        }

        if (binding.iDisplayLength) {
            options.iDisplayLength = binding.iDisplayLength;
        }

        if (binding.lengthMenu) {
            options.lengthMenu = binding.lengthMenu;
        }

        if (binding.sPaginationType) {
            options.sPaginationType = binding.sPaginationType;
        }

        if (binding.bPaginate) {
            options.bPaginate = binding.bPaginate;
        }

        // Register the row template to be used with the DataTable.
        if (binding.rowTemplate && binding.rowTemplate != '') {
            options.fnRowCallback = function (row, data, displayIndex, displayIndexFull) {
                // Render the row template for this row.
                ko.renderTemplate(binding.rowTemplate, data, null, row, "replaceChildren");
                return row;
            }
        }

        // Set the data source of the DataTable.
        if (binding.dataSource) {
            var dataSource = ko.utils.unwrapObservable(binding.dataSource);

            if (dataSource instanceof Array) {
                // Set the initial datasource of the table.
                options.aaData = ko.utils.unwrapObservable(binding.dataSource);

                // If the data source is a knockout observable array...
                if (ko.isObservable(binding.dataSource)) {
                    // Subscribe to the dataSource observable.  This callback will fire whenever items are added to 
                    // and removed from the data source.
                    binding.dataSource.subscribe(function (newItems) {
                        // ** Redraw table **
                        var dataTable = $(element).dataTable();

                        // Get a list of rows in the DataTable.
                        var tableNodes = dataTable.fnGetNodes();

                        // If the table contains rows...
                        if (tableNodes.length) {
                            // Unregister each of the table rows from knockout.
                            ko.utils.arrayForEach(tableNodes, function (node) { ko.cleanNode(node); });
                            // Clear the datatable of rows.
                            dataTable.fnClearTable();
                        }

                        if (newItems.length == 0) return;

                        // Unwrap the items in the data source if required.
                        var unwrappedItems = [];
                        ko.utils.arrayForEach(newItems, function (item) {
                            unwrappedItems.push(ko.utils.unwrapObservable(item));
                        });

                        // Add the new data back into the data table.
                        dataTable.fnAddData(unwrappedItems);
                    });
                }

            }
                // If the dataSource was not a function that retrieves data, or a javascript object array containing data.
            else {
                throw 'The dataSource defined must a javascript object array';
            }
        }

        // If no fnRowCallback has been registered in the DataTable's options, then register the default fnRowCallback.
        // This default fnRowCallback function is called for every row in the data source.  The intention of this callback
        // is to build a table row that is bound it's associated record in the data source via knockout js.
        if (!options.fnRowCallback) {
            options.fnRowCallback = function (row, srcData, displayIndex, displayIndexFull) {
                var columns = this.fnSettings().aoColumns

                // Empty the row that has been build by the DataTable of any child elements.
                var destRow = $(row);
                destRow.empty();

                // For each column in the data table...
                ko.utils.arrayForEach(columns, function (column) {

                    var columnName = column.mDataProp;

                    var newCell = $("<td></td>");

                    // bind the cell to the observable in the current data row.
                    var accesor = eval("srcData['" + columnName.replace(".", "']['") + "']");

                    destRow.append(newCell);
                    if (columnName == 'action') {
                        ko.applyBindingsToNode(newCell[0], { html: accesor }, srcData);
                    } else {
                        ko.applyBindingsToNode(newCell[0], { text: accesor }, srcData);
                    }
                });

                return destRow[0];
            }
        }

        // If no fnDrawCallback has been registered in the DataTable's options, then register the default here. 
        // This default callback is called every time the table is drawn (for example, when the pagination is clicked). 

        if (!options.fnDrawCallback) {
            options.fnDrawCallback = function () {

                // There are some assumptions here that need to be better abstracted
                $(binding.expandIcon).click(function () {
                    var theRow = $(this).parent().parent()[0]; //defined by the relationship between the clickable expand icon and the row. assumes that the icon (the trigger) is in a td which is in a tr. 
                    rowContent = $(theRow).find(".hiddenRow").html();

                    tableId = local[binding.gridId];

                    if (tableId.fnIsOpen(theRow)) {
                        $(this).removeClass('icon-contract ' + binding.expandIcon);
                        $(this).addClass('icon-expand ' + binding.expandIcon);
                        tableId.fnClose(theRow);
                    } else {
                        $(this).removeClass('icon-expand ' + binding.expandIcon);
                        $(this).addClass('icon-contract ' + binding.expandIcon);
                        tableId.fnOpen(theRow, rowContent, 'info_row');
                    }
                });

                // bootstrap tooltip definition
                $("[rel=dataGridTooltip]").tooltip(
					{
					    placement: 'top',
					    trigger: 'hover',
					    animation: true,
					    delay:
							{
							    show: 1000,
							    hide: 10
							}
					}
				);

            }
        }

        local[binding.gridId] = $(element).dataTable(options);

        //var oSettings = local[binding.gridId].fnSettings();
        //oSettings.aoPreSearchCols[1].sSearch = "^" + '1' + "$";
        //oSettings.aoPreSearchCols[1].bRegex = true;
        //oSettings.aoPreSearchCols[1].bSmart = false;
        //console.log(oSettings);

        // Apply the search footer
        if (options.enableSearchInColumn) {
            var _head = $('<tr class="head-search">');
            local[binding.gridId].find('thead').append(_head);

            /*function _buscar(that, texto, col) {                
                setTimeout(function () { that.draw(); }, 500);                

                if (col.sSearchType == 'equals' || col.sSearchType == 'start-with') {
                    value = $.fn.dataTable.util.escapeRegex(texto);
                    regExSearch = '^' + value + (col.sSearchType == 'equals' ? '$' : '');
                    that.search((value != '') ? regExSearch : value, true, false).draw();
                }
                else if (col.sSearchType == 'smart') {
                    that.search(texto).draw();
                }
                else {
                    that.draw();
                }
            }*/

            local[binding.gridId].api().columns().every(function (index) {
                var that = this;
                var col = that.context[0].aoColumns[index];
                //console.log(col);

                if (col.bSearch) {
                    var _input = $('<input type="text" class="input-sm" placeholder="Buscar..." />');
                    var _key = $('<i class="fa fa-key"></i>');
                    var _menu = $('<div>', {
                        'class': 'table-header-menu',
                        'html': '<div data-type="smart">Contiene</div>' +
                                '<div data-type="equals">Igual</div>' +
                                '<div data-type="start-with">Comienza con</div>' +
                                (col.sType == 'num' ?
                                    (
                                        '<div data-type="greater">Mayor que</div>' +
                                        '<div data-type="greater-equals">Mayor o igual que</div>' +
                                        '<div data-type="less">Menor que</div>' +
                                        '<div data-type="less-equals">Menor o igual que</div>'
                                     ) : '')
                    });
                    col.sSearchType = (col.sSearchType ? col.sSearchType : 'smart');
                    $('<td>').append(_input).append(_key).append(_menu).appendTo(_head);

                    //Búsqueda personalizada
                    $.fn.dataTable.ext.search.push(
                        function (settings, data, dataIndex) {
                            //console.log(settings.aoColumns[index].sType);
                            var row_data = data[index];
                            var value = _input.val().toString().trim();
                            var comparator = col.sSearchType;
                            //console.log(comparator, row_data, value);

                            if (value != '') {
                                switch (comparator) {
                                    case 'smart': return new RegExp(value, 'gi').test(row_data); break;                                    
                                    //case 'smart':
                                    //    var s = value.split(' ');
                                    //    var r = row_data.match(new RegExp($.fn.dataTable.util.escapeRegex(s.join('|')), 'gi'));
                                    //    //Eliminamos el match duplicado para comparar los arrays
                                    //    return ((r || []).filter(function (value, index, self) {
                                    //        return self.indexOf(value) === index
                                    //    }).length == s.length);
                                    //    break;
                                    case 'start-with': return (new RegExp("^" + $.fn.dataTable.util.escapeRegex(value), 'i').test(row_data)); break;
                                    case 'equals': return row_data == value; break;
                                    case 'greater': return parseFloat(row_data) > parseFloat(value); break;
                                    case 'greater-equals': return parseFloat(row_data) >= parseFloat(value); break;
                                    case 'less': return parseFloat(row_data) < parseFloat(value); break;
                                    case 'less-equals': return parseFloat(row_data) <= parseFloat(value); break;
                                }
                            }

                            return true;
                        });

                    _input.on('keyup change', function (ev) {
                        //_buscar(that, this.value, col);
                        //that.draw();                        
                        setTimeout(function () { that.draw(); }, 250);
                    });

                    _key.on('click', function (e) {
                        $('.table .table-header-menu').not(this).hide();
                        _menu.find('div[data-type="' + col.sSearchType + '"]').addClass('selected');
                        _menu.toggle();
                        e.stopPropagation();
                    });

                    _menu.on('click', 'div', function (e) {
                        _menu.find('div').removeClass('selected');
                        $(this).addClass('selected');
                        //Actualizamos el tipo de busqueda
                        col.sSearchType = $(this).data('type');

                        //Actualizar busqueda
                        //_buscar(that, _input.val(), col);
                        that.draw();                        
                    });
                }
                else {
                    _head.append('<td></td>');
                }
            });

            //Ocultamos el menu
            $('body').click(function (e) {
                $('.table .table-header-menu').hide();
            });
        }

        $.data(element, isInitialisedKey, true);

        // Tell knockout that the control rendered by this binding is capable of managing the binding of it's descendent elements.
        // This is crutial, otherwise knockout will attempt to rebind elements that have been printed by the row template.
        return { controlsDescendantBindings: true };

    },
    convertDataCriteria: function (srcOptions) {
        var getColIndex = function (name) {
            var matches = name.match("\\d+");

            if (matches && matches.length)
                return matches[0];

            return null;
        }

        var destOptions = { Columns: [] };

        // Figure out how many columns in in the data table.
        for (var i = 0; i < srcOptions.length; i++) {
            if (srcOptions[i].name == "iColumns") {
                for (var j = 0; j < srcOptions[i].value; j++)
                    destOptions.Columns.push(new Object());
                break;
            }
        }

        ko.utils.arrayForEach(srcOptions, function (item) {
            var colIndex = getColIndex(item.name);

            if (item.name == "iDisplayStart")
                destOptions.RecordsToSkip = item.value;
            else if (item.name == "iDisplayLength")
                destOptions.RecordsToTake = item.value;
            else if (item.name == "sSearch")
                destOptions.GlobalSearchText = item.value;
            else if (cog.string.startsWith(item.name, "bSearchable_"))
                destOptions.Columns[colIndex].IsSearchable = item.value;
            else if (cog.string.startsWith(item.name, "sSearch_"))
                destOptions.Columns[colIndex].SearchText = item.value;
            else if (cog.string.startsWith(item.name, "mDataProp_"))
                destOptions.Columns[colIndex].ColumnName = item.value;
            else if (cog.string.startsWith(item.name, "iSortCol_")) {
                destOptions.Columns[item.value].IsSorted = true;
                destOptions.Columns[item.value].SortOrder = colIndex;

                var sortOrder = ko.utils.arrayFilter(srcOptions, function (item) {
                    return item.name == "sSortDir_" + colIndex;
                });

                if (sortOrder.length && sortOrder[0].value == "desc")
                    destOptions.Columns[item.value].SortDirection = "Descending";
                else
                    destOptions.Columns[item.value].SortDirection = "Ascending";
            }
        });

        return destOptions;
    }
};