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

var cont = 0;
total = 0;
subtotal = [];
saledetails=[];

function agregar() {
    
        //Vbles de ViewIngresoYdetallesViewModel
         ProductID = $('#pidarticulo').val();
         Cantidad = $('#pcantidad').val();
         Descuento = $('#pdescuento').val();
         PrecioVentaUnitario = $('#pprecio_venta').val();
         var detail = { id:ProductID, cant:Cantidad, dto:Descuento, pvp:PrecioVentaUnitario };
        
         if (ProductID !== "") {
            
                    if (Cantidad !== "" && Cantidad > 0 && Descuento !== "" && Descuento > 0 &&   PrecioVentaUnitario !== "" &&   PrecioVentaUnitario>0  ) {
            
                        subtotal[cont] = ( Cantidad * (PrecioVentaUnitario-Descuento) );
                        
                        saledetails[cont] = detail; 
                        
                        total = total + subtotal[cont];
                        jQuery('<tbody><tr class="selected" id="fila' + cont + '" >   <td class="col-lg-2"> <button type="button" class="btn btn-warning" onclick="eliminar(' + cont + ');">X</button> </td>       <td class="col-lg-2"><div class="form-group">   <input class="form-control" type="hidden"  name="selectedProducts" value="' + ProductID + '" />   ' + $('#pidarticulo option:selected').text() + ' </div> </td><td class="col-lg-2"><div class="form-group"><input type="hidden" class="form-control" name="selectedCants" value="' + Cantidad + '" /> ' + Cantidad + '   </div></td><td class="col-lg-2"><div class="form-group"> <input type="hidden" class="form-control" name="selectedDTOs" value="' + Descuento + '" /> ' + Descuento + '</div></td > <td class="col-lg-2"><div class="form-group"> <input type="hidden" class="form-control" name="selectedPVPs" value="' + PrecioVentaUnitario + '" /> ' + PrecioVentaUnitario + ' </div></td >     <td class="col-lg-2">  ' + subtotal[cont] + ' </td></tr ></tbody >').appendTo("#detalles");

                        cont++;
                      
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
                $("#pdescuento").val("");
                $("#pprecio_venta").val("");
            }
            
            
            function eliminar(index) {
                total = total - subtotal[index];
                $("#total").html("S/. " + total);
                $("#fila" + index).remove();
                evaluar();
            
            }