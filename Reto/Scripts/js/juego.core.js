var Reto = {
    GenerarNumero: function (max, lista) {
        var n;
        if (lista.length == 0) n = Math.floor(Math.random() * max) + 1;
        else {
            var r = [];

            for (var i = 1; i <= max; i++) {
                r.push(i);
            }

            var m = r.filter(function (i) { return lista.indexOf(i) < 0; });
            if (m.length > 0) {
                var i = Math.floor(Math.random() * m.length);
                n = m[i];
            }

            //console.log(lista, i, m, n);
        }

        return n;
    },
    ExtraerNumeroDistinto: function (a, pos, lista) {
        if (!lista) lista = [];
        var max = (a.length + 1) / 2;
        var n = Reto.GenerarNumero(max, lista);

        var x = pos.x, y = pos.y;

        //Si es la posicion 0,0 retorna cualquier numero aleatorio
        if (x == 0 && y == 0) return n;
        var existe = false, compare;

        if (x > 0) {
            for (var i = x - 1; i >= 0; i--) {
                if (a[i] && a[i].fila[y] && a[i].fila[y].col) {
                    compare = a[i].fila[y].col;
                    if (compare) existe = compare == n;
                    //console.log(compare, n, existe, i, y);

                    if (existe) break;
                }
            }
        }

        if (y > 0 && !existe) {
            for (var i = y - 1; i >= 0; i--) {
                if (a[x] && a[x].fila[i] && a[x].fila[i].col) {
                    compare = a[x].fila[i].col;
                    if (compare) existe = compare == n;
                    if (existe) break;
                }

            }
        }

        if (existe) {
            lista.push(n);
            return Reto.ExtraerNumeroDistinto(a, pos, lista);
        }

        return n;
    },
    CrearArreglo: function (t) {
        var x = 2 * t - 1;

        //creamos el arreglo
        var a = [];
        for (var i = 0; i < x; i++) {
            var fila = [];
            for (var j = 0; j < x; j++) {
                fila.push({ col: null });
            }
            a.push({ fila: fila });
        }

        for (var i = 0; i < x; i++) {
            for (var j = 0; j < x; j++) {
                if (i % 2 == 0 && j % 2 == 0) {
                    var w = Reto.ExtraerNumeroDistinto(a, { x: i, y: j });
                    //En caso se genere un undefined quiere decir que no hay solucion, creamos de nuevo
                    if (!w) {
                        return Reto.CrearArreglo(t);
                    }
                    a[i].fila[j].col = w;
                }
            }
        }

        //Creamos 4 signos
        Reto.CrearSignos(a, 4);

        return a;
    },
    CrearSignos: function (a, signos) {
        //var a = array.slice(0); //clone
        var x = a.length;//Longitud del arreglo

        var count = 0, s;
        var espacios = [];

        for (var i = 0; i < x; i++) {
            for (var j = 0; j < x; j++) {
                if (i != j && (i % 2 != 0 || j % 2 != 0)) {

                    //Vertical
                    if (i % 2 != 0 && (j + 1) % 2 != 0) {
                        if (a[i - 1].fila[j].col) {
                            if (!a[i].fila[j].col) {
                                s = a[i - 1].fila[j].col > a[i + 1].fila[j].col ? 'mayor' : 'menor';
                                espacios.push({ signo: s, pos: { x: i, y: j } });
                            }

                        }
                    }

                    //Horizontal
                    if (j % 2 != 0 && (i + 1) % 2 != 0) {
                        if (a[i].fila[j - 1].col) {
                            if (!a[i].fila[j].col) {
                                s = a[i].fila[j - 1].col > a[i].fila[j + 1].col ? 'mayor' : 'menor';
                                espacios.push({ signo: s, pos: { x: i, y: j } });
                            }
                        }
                    }
                }
            }
        }

        //obtenemos los espacios aleatorios donde se colocarán los signos
        var posiciones = Reto.EscogerPosicionesAzar(espacios, signos);

        for (var i = 0; i < posiciones.length; i++) {
            var obj = espacios[posiciones[i]];
            a[obj.pos.x].fila[obj.pos.y].col = obj.signo;
        }
        //console.log(posiciones);
    },
    EscogerPosicionesAzar: function (listaArray, cantidad, generados) {
        if (!generados) generados = [];
        if (cantidad == 0) return generados;

        var c = listaArray.length;
        var posicion;

        var existe;
        do {
            posicion = Math.floor(Math.random() * c);
            var m = generados.filter(function (x) { return x == posicion });
            existe = m.length > 0;
        } while (existe);

        generados.push(posicion);

        if (generados.length == cantidad) return generados;
        else return Reto.EscogerPosicionesAzar(listaArray, cantidad, generados);
    },
    ObtenerPosVal: function (a, cant) {
        var arr = [];
        var x = a.length;
        var posValidas = [];
        for (var i = 0; i < x; i++) {
            for (var j = 0; j < x; j++) {
                if (i % 2 == 0 && j % 2 == 0) {
                    posValidas.push({ x: i, y: j });
                }
            }
        }

        var pos = Reto.EscogerPosicionesAzar(posValidas, cant);
        for (var i = 0; i < pos.length; i++) {
            arr.push(posValidas[pos[i]]);
        }

        return arr;
    },
    ObtenerPosVaciasAzar: function (a, cant) {
        var arr = [];
        var x = a.length;
        var posValidas = [];
        for (var i = 0; i < x; i++) {
            for (var j = 0; j < x; j++) {
                if (i % 2 == 0 && j % 2 == 0 && !a[j].fila[i].col) { //Columna vacia
                    posValidas.push({ x: i, y: j });
                }
            }
        }

        var pos = Reto.EscogerPosicionesAzar(posValidas, cant);
        for (var i = 0; i < pos.length; i++) {
            arr.push(posValidas[pos[i]]);
        }

        return arr;
    },
    CrearHtmlCasillas: function (x) {
        var long_casillero = (x + 1) / 2;

        var box = $('.tabla');
        box.html('');
        for (var i = 0; i < x; i++) {
            var tr = $('<tr>');
            box.append(tr);
            for (var j = 0; j < x; j++) {
                var q = $('<td>', { 'class': 'q' });

                if (i % 2 != 0) q.addClass('v');
                if (j % 2 != 0) q.addClass('h');

                if (i % 2 == 0 && j % 2 == 0) {
                    var casilla = $('<div>', { 'class': 'casilla', 'contenteditable': true });
                    q.append(casilla);
                }

                tr.append(q);
            }
        }

        //Eventos
        $('.tabla .casilla').on({
            keypress: function (e) {
                if (!(e.keyCode >= 49 && e.keyCode <= 49 + long_casillero - 1)) {
                    return false;
                }

                $(this).html(e.key);

                var index = $('.tabla .casilla').index(this);
                $('.tabla .casilla').eq(index + 1).focus();

                return false;
            }
        });
    },
    LlenarCasillas: function (a) {
        var x = a.length;

        var box = $('.tabla');

        for (var i = 0; i < x; i++) {
            for (var j = 0; j < x; j++) {
                var q = box.find('tr').eq(i).find('td').eq(j);
                if (i % 2 == 0 && j % 2 == 0) {
                    var casilla = q.find('.casilla');
                    if (a[i].fila[j].col) {
                        casilla.html(a[i].fila[j].col);
                        casilla.removeAttr('contenteditable');
                    }
                } else {
                    q.addClass(a[i].fila[j].col);
                }
            }
        }
    },
    LlenarCasillasAzar: function (a) {
        var azar = Reto.ObtenerPosVal(a, 3);
        var box = $('.tabla');

        box.find('.casilla').html('');

        for (var i = 0; i < azar.length; i++) {
            var p = azar[i];
            box.find('tr').eq(p.y).find('td').eq(p.x).find('.casilla').html(a[p.y].fila[p.x].col).removeAttr('contenteditable');
        }

        //Signos
        var x = a.length;
        for (var i = 0; i < x; i++) {
            for (var j = 0; j < x; j++) {
                var q = box.find('tr').eq(i).find('td').eq(j);
                if (!(i % 2 == 0 && j % 2 == 0)) {
                    q.addClass(a[i].fila[j].col);
                }
            }
        }
    },
    Procesar: function () {
        var errores = [];
        $('.tabla .casilla').each(function (indice, casilla) {
            $(this).removeClass('error');

            var i = $(this).parents('.q').index(); // Eje x de la casilla actual
            var j = $('.tabla tr').index($(this).closest('.tabla tr')); // Eje y de la casilla actual

            var valor = $(this).text();
            var existe = false, correcto = true;

            //Fila            
            $(this).closest('tr').find('td').each(function (index, el) {
                var text = $(this).text();
                if (i != index) {
                    if (text == valor) {
                        existe = true;
                    }
                }

            });

            //console.clear();

            //Columna
            if (!existe) {
                $(this).closest('.tabla').find('tr').each(function (index, el) {
                    var celda = $(this).find('td').eq(i);
                    var text = celda.text();
                    //console.log(text, 'Actual: ' + valor, ' j= ' + j, 'index=' + index);
                    if (j != index) {
                        if (text == valor) {
                            existe = true;
                        }
                    }
                });
            }

            if (existe) {
                //$(this).html('');
                $(this).addClass('error');
                errores.push({ x: i, y: j, valor: valor, mensaje: 'Repetido en ' + i + ',' + j });
            }
        });

        //Vertical
        $('.tabla .q.v').each(function (index, el) {
            if ($(this).hasClass('mayor') || $(this).hasClass('menor')) {
                var i = $(this).index();
                var j = $('tr').index($(this).closest('.tabla tr'));

                //console.log(i, j);

                var casilla_anterior = $(this).parent().prev().find('td').eq(i);
                var casilla_posterior = $(this).parent().next().find('td').eq(i);

                var v_a = casilla_anterior.text();
                var v_p = casilla_posterior.text();

                if (!isNaN(parseInt(v_a)) && !isNaN(parseInt(v_p))) {
                    v_a = parseInt(v_a);
                    v_p = parseInt(v_p);

                    //console.log(v_a, v_p);

                    if ($(this).hasClass('mayor') && v_a < v_p) errores.push({ x: i, y: j - 1, valor: v_a, mensaje: 'Valor superior debe ser mayor' });
                    if ($(this).hasClass('menor') && v_a > v_p) errores.push({ x: i, y: j - 1, valor: v_a, mensaje: 'Valor superior debe ser mayor' });
                }
            }
        });

        //Horizontal
        $('.tabla .q.h').each(function (index, el) {
            if ($(this).hasClass('mayor') || $(this).hasClass('menor')) {
                var i = $(this).index();
                var j = $('tr').index($(this).closest('.tabla tr'));

                //console.log(i, j);

                var casilla_anterior = $(this).prev();
                var casilla_posterior = $(this).next();

                //console.log(casilla_anterior, casilla_posterior);

                var v_a = casilla_anterior.text();
                var v_p = casilla_posterior.text();

                if (!isNaN(parseInt(v_a)) && !isNaN(parseInt(v_p))) {
                    v_a = parseInt(v_a);
                    v_p = parseInt(v_p);

                    //console.log(v_a, v_p);

                    if ($(this).hasClass('mayor') && v_a < v_p) errores.push({ x: i - 1, y: j, valor: v_a, mensaje: 'Valor lateral debe ser mayor' });
                    if ($(this).hasClass('menor') && v_a > v_p) errores.push({ x: i - 1, y: j, valor: v_a, mensaje: 'Valor lateral debe ser mayor' });
                }
            }
        });

        if (errores.length == 0) {
            console.log('Ok correcto');
        } else {
            console.table(errores);
            //var mensajes = errores.map(function (x) { return x.mensaje }).join('\n');
            //alert(mensajes);
        }
    },
    PintarCasillasAzar: function (solucion, mostrar) {
        var colores = ['yellow', 'green', 'blue', 'red'];
        $('.tabla tr td').removeClass('color ' + colores.join(' '));        
        
        var posAzar = Reto.ObtenerPosVaciasAzar(mostrar, 4);

        for (var i = 0; i < posAzar.length; i++) {
            var casilla = $('.tabla tr:eq(' + posAzar[i].y + ') td:eq(' + posAzar[i].x + ')');
            $('.respuestas .rpta:eq(' + i + ')').attr('data-value', solucion[posAzar[i].y].fila[posAzar[i].x].col);
            casilla.addClass('color ' + colores[i]);
        }

        var long_casillero = $('.tabla tr:first .casilla').length;

        $('.respuestas .rpta').on({
            keypress: function (e) {
                if (!(e.keyCode >= 49 && e.keyCode <= 49 + long_casillero - 1)) {
                    return false;
                }

                $(this).html(e.key);

                var index = $('.respuestas .rpta').index(this);
                $('.respuestas .rpta').eq(index + 1).focus();

                return false;
            }
        });

        return posAzar;
    }
}