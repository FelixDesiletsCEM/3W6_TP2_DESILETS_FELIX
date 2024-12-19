// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function filtrerNom(pNom) {
    $("tbody").find("tr:contains("pNom")").addClass("highlight");
    alert('hello world');
}
function filtrerDate(pListeDate, pDate)
{
    foreach(item i in pListeDate)
    {
        if (i.text > pDate)
        {
            i.hide("fast");
        }
    }
}
//$("tbody").find("tr:contains('$(document).find(#date).val()')").addClass("highlight");
//Marche pas, trop de guillemets. 
function highlightRows(pNom) {

    // Rechercher les lignes contenant le texte et ajouter la classe highlight

    $('tbody').find('tr').filter(function () {
        return $(this).text().includes(pNom);
    }).addClass('highlight');
}
$("tbody").find(tr:contains($(document).find("#date").val()).addClass("highlight");
