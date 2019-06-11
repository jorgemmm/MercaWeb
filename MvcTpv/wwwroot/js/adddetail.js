jQuery(function ($) {
    alert('Seriously its ready!');

    // Use $() with out fear of conflicts
});


jQuery(document).ready(function () {
    alert('DOM is ready!');
    $('#bt_add').click(function ($) {
        agregar();
    });
});



//jQuery(function ($) {

//    $('#bt_add').click(function ($) {

//        agregar();
//    });

//});


var cont = 0;
var cantidad = 0;
var PrecioCompra = 0.0;
var PrecioVentaUnitario = 0.0;
var total = 0.0;
var subtotal = [];
//inputdetails=[];


//var fila = '<tbody><tr class="selected" id="fila' + cont + '" >   <td class="col-lg-2">  <button type="button" class="btn btn-warning" onclick="eliminar(' + cont + ');">X</button> </td>       <td class="col-lg-2">  <input  type="hidden"  asp -for="ProductID[]" class="form-control"  value="' + ProductID + '" />   ' + $('#pidarticulo option:selected').text() + '  </td><td class="col-lg-2"> <input type="hidden" asp -for="Cantidad[]" class="form-control" value="' + Cantidad + '" /> ' + Cantidad + '  </td><td class="col-lg-2"> <input type="hidden" asp -for="PrecioCompra[]" class="form-control" value="' + PrecioCompra + '" /> ' + PrecioCompra + '</td > <td class="col-lg-2"> <input type="hidden" asp -for="PrecioVentaUnitario[]" class="form-control" value="' + PrecioVentaUnitario + '" /> ' + PrecioVentaUnitario + ' </td >     <td class="col-lg-2">  ' + subtotal[cont] + ' </td></tr ></tbody>';

function agregar() {
    
        //Vbles de ViewIngresoYdetallesViewModel
         ProductID = $('#pidarticulo').val();
         Cantidad = $('#pcantidad').val();
         PrecioCompra = $('#pprecio_compra').val();
         PrecioVentaUnitario = $('#pprecio_venta').val();
         //var detail = { id:ProductID, cant:Cantidad, pneto:PrecioCompra, pvp:PrecioVentaUnitario };
        
        //Vbles de ViewIngresoYdetallesViewModel
    
        if (ProductID !== "") {
    
            if (Cantidad !== "" && Cantidad > 0 && PrecioCompra !== "" && PrecioCompra > 0 &&   PrecioVentaUnitario !== "" &&   PrecioVentaUnitario>0  ) {
    
                //subtotal[cont] = (Cantidad * PrecioVentaUnitario);
                subtotal[cont] = PrecioCompra * Cantidad;
                //inputdetails[cont] = detail; //inputdetails.push(inputdetail)  // la podemos enviar por ajax para asegurar
                
                total = total + subtotal[cont];

                
    
                //jQuery('<tbody><tr><td class="col-lg-2">Test</td><td class="col-lg-2">Test</td><td class="col-lg-2">Test</td><td class="col-lg-2">Test</td><td class="col-lg-2">Test</td><td class="col-lg-2">Test</td></tr></tbody>').appendTo("#detalles");
    
                //jQuery('<tbody><tr class="selected" id="fila' + cont + '" >   <td class="col-lg-2">  Button </td>       <td class="col-lg-2">  <input  type="hidden"  name="ProductID[]" value="' + ProductID + '" />   ' + $('#pidarticulo option:selected').text() + '  </td><td class="col-lg-2"> <input type="hidden" name="Cantidad[]" value="' + Cantidad + '" /> ' + Cantidad + '  </td><td class="col-lg-2"> <input type="hidden" name="PrecioCompra[]" value="' + PrecioCompra + '" /> ' + PrecioCompra + '</td > <td class="col-lg-2"> <input type="hidden" name="PrecioVentaUnitario[]" value="' + PrecioVentaUnitario + '" /> ' + PrecioVentaUnitario + ' </td >     <td class="col-lg-2">  ' + subtotal[cont] + ' </td></tr ></tbody >').appendTo("#detalles");
    
                // jQuery('<tbody><tr class="selected" id="fila' + cont + '" >   <td class="col-lg-2">  <button type="button" class="btn btn-warning" onclick="eliminar(' + cont + ');">X</button> </td>       <td class="col-lg-2">  <input  type="hidden"  name="ProductID[]" value="' + ProductID + '" />   ' + $('#pidarticulo option:selected').text() + '  </td><td class="col-lg-2"> <input type="hidden" name="Cantidad[]" value="' + Cantidad + '" /> ' + Cantidad + '  </td><td class="col-lg-2"> <input type="hidden" name="PrecioCompra[]" value="' + PrecioCompra + '" /> ' + PrecioCompra + '</td > <td class="col-lg-2"> <input type="hidden" name="PrecioVentaUnitario[]" value="' + PrecioVentaUnitario + '" /> ' + PrecioVentaUnitario + ' </td >     <td class="col-lg-2">  ' + subtotal[cont] + ' </td></tr ></tbody >').appendTo("#detalles");
    
               // jQuery('<tbody><tr class="selected" id="fila' + cont + '" >   <td class="col-lg-2"> <button type="button" class="btn btn-warning" onclick="eliminar(' + cont + ');">X</button> </td>       <td class="col-lg-2"><div class="form-group">   <input class="form-control" type="hidden"  name="detail["id"]" value="' + ProductID + '" />   ' + $('#pidarticulo option:selected').text() + ' </div> </td><td class="col-lg-2"><div class="form-group"><input type="hidden" class="form-control" name="detail["cant"]" value="' + Cantidad + '" /> ' + Cantidad + '   </div></td><td class="col-lg-2"><div class="form-group"> <input type="hidden" class="form-control" name="detail["pneto"]" value="' + PrecioCompra + '" /> ' + PrecioCompra + '</div></td > <td class="col-lg-2"><div class="form-group"> <input type="hidden" class="form-control" name="detail["pvp"]" value="' + PrecioVentaUnitario + '" /> ' + PrecioVentaUnitario + ' </div></td >     <td class="col-lg-2">  ' + subtotal[cont] + ' </td></tr ></tbody >').appendTo("#detalles");
    
              //  jQuery('<tbody><tr class="selected" id="fila' + cont + '" >   <td class="col-lg-2"> <button type="button" class="btn btn-warning" onclick="eliminar(' + cont + ');">X</button> </td>       <td class="col-lg-2"><div class="form-group">   <input class="form-control" type="hidden"  name="ProductID[]" value="' + ProductID + '" />   ' + $('#pidarticulo option:selected').text() + ' </div> </td><td class="col-lg-2"><div class="form-group"><input type="hidden" class="form-control" name="Cantidad[]" value="' + Cantidad + '" /> ' + Cantidad + '   </div></td><td class="col-lg-2"><div class="form-group"> <input type="hidden" class="form-control" name="PrecioCompra[]" value="' + PrecioCompra + '" /> ' + PrecioCompra + '</div></td > <td class="col-lg-2"><div class="form-group"> <input type="hidden" class="form-control" name="PrecioVentaUnitario[]" value="' + PrecioVentaUnitario + '" /> ' + PrecioVentaUnitario + ' </div></td >     <td class="col-lg-2">  ' + subtotal[cont] + ' </td></tr ></tbody >').appendTo("#detalles");
               
                jQuery('<tbody><tr class="selected" id="fila' + cont + '" >   <td class="col-lg-2"> <button type="button" class="btn btn-warning" onclick="eliminar(' + cont + ');">X</button> </td>       <td class="col-lg-2"><div class="form-group">   <input class="form-control" type="hidden"  name="selectedProducts" value="' + ProductID + '" />   ' + $('#pidarticulo option:selected').text() + ' </div> </td><td class="col-lg-2"><div class="form-group"><input type="hidden" class="form-control" name="selectedCants" value="' + Cantidad + '" /> ' + Cantidad + '   </div></td><td class="col-lg-2"><div class="form-group"> <input type="hidden" class="form-control" name="selectedPNETOs" value="' + PrecioCompra + '" /> ' + PrecioCompra + '</div></td > <td class="col-lg-2"><div class="form-group"> <input type="hidden" class="form-control" name="selectedPVPs" value="' + PrecioVentaUnitario + '" /> ' + PrecioVentaUnitario + ' </div></td >     <td class="col-lg-2">  ' + subtotal[cont] + ' </td></tr ></tbody >').appendTo("#detalles");
            
                //jQuery('<td class="col-lg-2">  <button type="button" class="btn btn-warning" onclick="eliminar(' + cont + ');">X</button> </td>       <td class="col-lg-2">  <input  type="hidden"  id="ppid"  value="' + ProductID + '" />   ' + $('#pidarticulo option:selected').text() + '  </td><td class="col-lg-2"> <input type="hidden" id="pcant" value="' + Cantidad + '" /> ' + Cantidad + '  </td><td class="col-lg-2"> <input type="hidden" asp -for="PrecioCompra" class="form-control" value="' + PrecioCompra + '" /> ' + PrecioCompra + '</td > <td class="col-lg-2"> <input type="hidden" asp -for="PrecioVentaUnitario" class="form-control" value="' + PrecioVentaUnitario + '" /> ' + PrecioVentaUnitario + ' </td >     <td class="col-lg-2">  ' + subtotal[cont] + ' </td>').appendTo("#detallesRow");
    
                //JQuery(fila).appendTo("#detalles");
                
                cont++;
               // updateserver(inputdetails[cont]);
                limpiar();
                $("#total").html("S/." + total);
        
            } else {
                alert('introduce valores correctos');
            }
    
    
    
        }
        else {
            alert('no se ha introducido el id de articulo');
    
    
    
        }
    
    }
    
    function limpiar() {
        $("#pcantidad").val("");
        $("#pprecio_compra").val("");
        $("#pprecio_venta").val("");
    }
    
    
    function eliminar(index) {
        total = total - subtotal[index];
        $("#total").html("S/. " + total);
        $("#fila" + index).remove();
        evaluar();
    
    }

 