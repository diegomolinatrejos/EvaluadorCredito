// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// site.js

// Función para enviar la entrada a la API
function enviarEntradaACreditoAPI(formData) {
    // Llamar a la API con AJAX
    $.ajax({
        type: "POST",
        url: "http://localhost:5080/api/Credito/GetCredito",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function (salida) {
            // Llamar a la función para actualizar la sección right-section
            actualizarRightSection(salida);
        },
        error: function (error) {
            console.error("Error al llamar a la API:", error);
        }
    });
}

function actualizarRightSection(salida) {
    // Actualizar los campos span con los valores de salida
    $("#tipoCliente").text(salida.tipoCliente);
    $("#cuotaMensual").text(salida.cuotaMensual);
    $("#cantidadMeses").text(salida.cantidadMeses);
    $("#montoComision").text(salida.montoComision);
    $("#perfilRiesgo").text(salida.perfilRiesgo);
    $("#criterioRiesgo").text(salida.criterioRiesgo);

    // Cambiar el estilo del div "nivelRiesgo" según el nivel de riesgo
    var nivelRiesgoDiv = $("#nivelRiesgo");
    nivelRiesgoDiv.removeClass("alert-danger alert-success alert-info"); // Eliminar todas las clases de estilo
    switch (salida.perfilRiesgo.toLowerCase()) {
        case "alto":
            nivelRiesgoDiv.addClass("alert-danger");
            break;
        case "medio":
            nivelRiesgoDiv.addClass("alert-success");
            break;
        case "bajo":
            nivelRiesgoDiv.addClass("alert-info");
            break;
        default:
            // Si no se especifica un nivel de riesgo válido, puedes manejarlo de acuerdo a tus necesidades
            console.warn("Nivel de riesgo no reconocido:", salida.perfilRiesgo);
            break;
    }
}
