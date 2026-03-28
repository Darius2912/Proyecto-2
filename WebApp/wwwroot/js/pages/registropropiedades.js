document.addEventListener("DOMContentLoaded", function () {

    const formulario = document.getElementById("formRegistroPropiedad");

    formulario.addEventListener("submit", function (event) {
        let esValido = true;

        limpiarErrores();

        const nombreFinca = document.getElementById("nombreFinca").value.trim();
        const ubicacion = document.getElementById("ubicacion").value.trim();
        const tamano = document.getElementById("tamano").value.trim();
        const rios = document.getElementById("rios").value;
        const nacientes = document.getElementById("nacientes").value;
        const cantidadNacientes = document.getElementById("cantidadNacientes").value.trim();
        const vegetacion = document.getElementById("vegetacion").value;
        const usoSuelo = document.getElementById("usoSuelo").value;

        const superficieSeleccionada = document.querySelector('input[name="Superficie"]:checked');

        if (nombreFinca === "") {
            mostrarError("errorNombreFinca", "El nombre de la finca es obligatorio.");
            marcarInvalido("nombreFinca");
            esValido = false;
        } else {
            marcarValido("nombreFinca");
        }

        if (ubicacion === "") {
            mostrarError("errorUbicacion", "La ubicación es obligatoria.");
            marcarInvalido("ubicacion");
            esValido = false;
        } else {
            marcarValido("ubicacion");
        }

        if (tamano === "") {
            mostrarError("errorTamano", "El tamaño es obligatorio.");
            marcarInvalido("tamano");
            esValido = false;
        } else if (parseFloat(tamano) <= 0) {
            mostrarError("errorTamano", "Debe ser mayor a 0.");
            marcarInvalido("tamano");
            esValido = false;
        } else {
            marcarValido("tamano");
        }

        if (!superficieSeleccionada) {
            mostrarError("errorSuperficie", "Seleccione la superficie.");
            esValido = false;
        }

        if (rios === "") {
            mostrarError("errorRios", "Seleccione una opción.");
            marcarInvalido("rios");
            esValido = false;
        } else {
            marcarValido("rios");
        }

        if (nacientes === "") {
            mostrarError("errorNacientes", "Seleccione una opción.");
            marcarInvalido("nacientes");
            esValido = false;
        } else {
            marcarValido("nacientes");
        }

        if (nacientes === "Si") {
            if (cantidadNacientes === "") {
                mostrarError("errorCantidadNacientes", "Ingrese la cantidad.");
                marcarInvalido("cantidadNacientes");
                esValido = false;
            } else if (parseInt(cantidadNacientes) < 1) {
                mostrarError("errorCantidadNacientes", "Debe ser al menos 1.");
                marcarInvalido("cantidadNacientes");
                esValido = false;
            } else {
                marcarValido("cantidadNacientes");
            }
        }

        if (vegetacion === "") {
            mostrarError("errorVegetacion", "Seleccione una opción.");
            marcarInvalido("vegetacion");
            esValido = false;
        } else {
            marcarValido("vegetacion");
        }

        if (usoSuelo === "") {
            mostrarError("errorUsoSuelo", "Seleccione una opción.");
            marcarInvalido("usoSuelo");
            esValido = false;
        } else {
            marcarValido("usoSuelo");
        }

        if (!esValido) {
            event.preventDefault();
        }
    });

    function mostrarError(id, mensaje) {
        document.getElementById(id).textContent = mensaje;
    }

    function limpiarErrores() {
        document.querySelectorAll(".text-danger.small").forEach(e => e.textContent = "");
        document.querySelectorAll(".form-control, .form-select").forEach(e => {
            e.classList.remove("is-invalid", "is-valid");
        });
    }

    function marcarInvalido(id) {
        document.getElementById(id).classList.add("is-invalid");
    }

    function marcarValido(id) {
        document.getElementById(id).classList.remove("is-invalid");
        document.getElementById(id).classList.add("is-valid");
    }


    // ===== MAPA =====

    let mapa = L.map('map').setView([9.9281, -84.0907], 8); // Costa Rica

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(mapa);

    let marcador;

    mapa.on('click', function (e) {

        const lat = e.latlng.lat;
        const lng = e.latlng.lng;

        // Guardar coordenadas
        document.getElementById("latitud").value = lat;
        document.getElementById("longitud").value = lng;

        // Quitar marcador anterior
        if (marcador) {
            mapa.removeLayer(marcador);
        }

        // Agregar marcador nuevo
        marcador = L.marker([lat, lng]).addTo(mapa)
            .bindPopup("Ubicación seleccionada")
            .openPopup();
    });

    const latitud = document.getElementById("latitud").value;

    if (latitud === "") {
        mostrarError("errorMapa", "Debe seleccionar una ubicación en el mapa.");
        esValido = false;
    }

    // ===== PREVIEW DE IMÁGENES =====

    // ===== MANEJO DE IMÁGENES CON ELIMINAR =====

    const inputImagenes = document.getElementById("fotografias");
    const preview = document.getElementById("previewImagenes");

    let listaArchivos = [];

    inputImagenes.addEventListener("change", function () {

        limpiarErrorImagenes();

        const nuevosArchivos = Array.from(this.files);

        listaArchivos = listaArchivos.concat(nuevosArchivos);

        if (listaArchivos.length > 5) {
            mostrarError("errorImagenes", "Máximo 5 imágenes permitidas.");
            listaArchivos = listaArchivos.slice(0, 5);
        }

        renderizarImagenes();
        actualizarInput();
    });

    function renderizarImagenes() {
        preview.innerHTML = "";

        listaArchivos.forEach((archivo, index) => {

            if (!archivo.type.startsWith("image/")) {
                mostrarError("errorImagenes", "Solo se permiten imágenes.");
                return;
            }

            const reader = new FileReader();

            reader.onload = function (e) {

                const contenedor = document.createElement("div");
                contenedor.style.position = "relative";

                const img = document.createElement("img");
                img.src = e.target.result;

                img.style.width = "120px";
                img.style.height = "120px";
                img.style.objectFit = "cover";
                img.style.borderRadius = "10px";
                img.style.border = "1px solid #ccc";

                // BOTÓN ELIMINAR
                const boton = document.createElement("button");
                boton.innerHTML = "×";
                boton.type = "button";

                boton.style.position = "absolute";
                boton.style.top = "5px";
                boton.style.right = "5px";
                boton.style.background = "#dc3545"; // rojo bootstrap
                boton.style.color = "white";
                boton.style.border = "none";
                boton.style.borderRadius = "50%";
                boton.style.width = "28px";
                boton.style.height = "28px";
                boton.style.display = "flex";
                boton.style.alignItems = "center";
                boton.style.justifyContent = "center";
                boton.style.fontSize = "16px";
                boton.style.fontWeight = "bold";
                boton.style.cursor = "pointer";
                boton.style.boxShadow = "0 2px 5px rgba(0,0,0,0.3)";
                boton.style.transition = "0.2s";

                boton.onmouseover = () => boton.style.background = "#b02a37";
                boton.onmouseout = () => boton.style.background = "#dc3545";

                boton.onclick = function () {
                    eliminarImagen(index);
                };

                contenedor.appendChild(img);
                contenedor.appendChild(boton);

                preview.appendChild(contenedor);
            };

            reader.readAsDataURL(archivo);
        });
    }

    function eliminarImagen(index) {
        listaArchivos.splice(index, 1);
        renderizarImagenes();
        actualizarInput();
    }

    function actualizarInput() {
        const dataTransfer = new DataTransfer();

        listaArchivos.forEach(archivo => {
            dataTransfer.items.add(archivo);
        });

        inputImagenes.files = dataTransfer.files;
    }

    function limpiarErrorImagenes() {
        document.getElementById("errorImagenes").textContent = "";
    }

    if (listaArchivos.length === 0) {
        mostrarError("errorImagenes", "Debe subir al menos una imagen.");
        esValido = false;
    }

});