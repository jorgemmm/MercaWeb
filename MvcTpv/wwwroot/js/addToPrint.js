
jQuery(function ($) {
    alert('Seriously its ready!');
    
    // Use $() with out fear of conflicts
});



function CallPrint(strid) {
    var prtContent = document.getElementById(strid);
    var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');

    WinPrint.document.write(prtContent.innerHTML);
    WinPrint.document.close();
    WinPrint.focus();
    WinPrint.print();
    WinPrint.close();

    prtContent.innerHTML = strOldOne;
}

jQuery(document).ready(function () {
    alert('DOM is ready!');//
    $("#bt_add").click(function ($) {
        imprimir();
    });
});

function imprimir() {

     //var prtContent = $(strid).val();
     //var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');

    $("#bt_add").click(function () {
        window.open("@Url.Action('Details','Sales')", ,"menubar=no, toolbar=no, resizable=no, top=100, left=200, width=500, height=500");
        //window.print();

    });
}
//"_blank"