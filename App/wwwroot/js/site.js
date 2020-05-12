// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

$("#ckbox").on('change', () => {
    console.log("Mudou")
    if ($("#ckbox").is(':checked'))
        setLocalStorage($("#codigoPacote").val())
    else
        clearLocalStorage()
})