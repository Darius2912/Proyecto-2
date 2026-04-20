document.addEventListener("DOMContentLoaded", async function () {

    const sesion = await obtenerSesion();
    const idu = sesion ? sesion.idUsuario : 0; 

    const formulario = document.getElementById("formRegistroPropiedad");

    // ===== HELPERS =====
    function mostrarError(id, mensaje) {
        const el = document.getElementById(id);
        if (el) el.textContent = mensaje;
    }

    function limpiarErrores() {
        document.querySelectorAll(".text-danger.small").forEach(e => e.textContent = "");
        document.querySelectorAll(".form-control, .form-select").forEach(e => {
            e.classList.remove("is-invalid", "is-valid");
        });
    }

    function marcarInvalido(id) {
        const el = document.getElementById(id);
        if (el) el.classList.add("is-invalid");
    }

    function marcarValido(id) {
        const el = document.getElementById(id);
        if (el) { el.classList.remove("is-invalid"); el.classList.add("is-valid"); }
    }

    function limpiarErrorImagenes() {
        const el = document.getElementById("errorImagenes");
        if (el) el.textContent = "";
    }

    async function obtenerSesion() {
        try {
            const response = await fetch('/Acceso/ObtenerSesion', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Error al obtener el id de la sesión');
            }

            const data = await response.json();

            return data; // { idUsuario }
        } catch (error) {
            console.error('Error:', error);
            return null;
        }
    }

    // ===== MAPA =====
    let mapa = L.map('map').setView([9.9281, -84.0907], 8);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(mapa);

    let marcador;

    mapa.on('click', function (e) {
        const lat = e.latlng.lat;
        const lng = e.latlng.lng;

        document.getElementById("latitud").value = lat;
        document.getElementById("longitud").value = lng;

        if (marcador) mapa.removeLayer(marcador);

        marcador = L.marker([lat, lng]).addTo(mapa)
            .bindPopup("Ubicación seleccionada")
            .openPopup();

        llenarCampos(lat, lng);
    });

    async function reverseGeocode(lat, lon) {
        const response = await fetch(
            `https://localhost:7106/api/Propiedad/reverse?lat=${lat}&lon=${lon}`
        );
        if (!response.ok) {
            console.error("Error en reverse geocoding:", response.status);
            return null;
        }
        return await response.json();
    }

    async function llenarCampos(lat, lon) {
        try {
            const data = await reverseGeocode(lat, lon);
            if (!data || !data.address) return;

            const address = data.address;
            document.getElementById("txtProvincia").value = address.province || "";
            document.getElementById("txtCanton").value = address.city || address.town || address.county || "";
            document.getElementById("txtDistrito").value =
                address.city_district || address.suburb || address.village || address.neighbourhood || "";
        } catch (err) {
            console.warn("No se pudo obtener la dirección:", err.message);
        }
    }

    // ===== IMÁGENES =====
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
                contenedor.style.display = "inline-block";
                contenedor.style.margin = "5px";

                const img = document.createElement("img");
                img.src = e.target.result;
                img.style.cssText = "width:120px;height:120px;object-fit:cover;border-radius:10px;border:1px solid #ccc;display:block";

                const boton = document.createElement("button");
                boton.innerHTML = "&times;";
                boton.type = "button";
                boton.style.cssText = `
                    position:absolute;top:5px;right:5px;background:#dc3545;color:white;
                    border:none;border-radius:50%;width:28px;height:28px;
                    display:flex;align-items:center;justify-content:center;
                    font-size:16px;font-weight:bold;cursor:pointer;
                    box-shadow:0 2px 5px rgba(0,0,0,0.3);transition:background 0.2s
                `;
                boton.onmouseover = () => boton.style.background = "#b02a37";
                boton.onmouseout = () => boton.style.background = "#dc3545";
                boton.onclick = () => { listaArchivos.splice(index, 1); renderizarImagenes(); actualizarInput(); };

                contenedor.appendChild(img);
                contenedor.appendChild(boton);
                preview.appendChild(contenedor);
            };
            reader.readAsDataURL(archivo);
        });
    }

    function actualizarInput() {
        const dataTransfer = new DataTransfer();
        listaArchivos.forEach(f => dataTransfer.items.add(f));
        inputImagenes.files = dataTransfer.files;
    }

    // ===== SUBMIT  =====
    formulario.addEventListener("submit", async function (event) {
        event.preventDefault();
      

        
        limpiarErrores();
        let esValido = true;

        const nombreFinca = document.getElementById("nombreFinca").value.trim();
        const tamano = document.getElementById("tamano").value.trim();
        const rios = document.getElementById("rios").value;
        const nacientes = document.getElementById("nacientes").value;
        const cantidadNacientes = document.getElementById("cantidadNacientes").value.trim();
        const vegetacion = document.getElementById("vegetacion").value;
        const usoSuelo = document.getElementById("usoSuelo").value;
        const superficieSeleccionada = document.querySelector('input[name="Superficie"]:checked');

        // Validaciones
        if (nombreFinca === "") {
            mostrarError("errorNombreFinca", "El nombre de la finca es obligatorio.");
            marcarInvalido("nombreFinca"); esValido = false;
        } else marcarValido("nombreFinca");

        if (tamano === "") {
            mostrarError("errorTamano", "El tamaño es obligatorio.");
            marcarInvalido("tamano"); esValido = false;
        } else if (parseFloat(tamano) <= 0) {
            mostrarError("errorTamano", "Debe ser mayor a 0.");
            marcarInvalido("tamano"); esValido = false;
        } else marcarValido("tamano");

        if (!superficieSeleccionada) {
            mostrarError("errorSuperficie", "Seleccione la superficie."); esValido = false;
        }

        if (rios === "") {
            mostrarError("errorRios", "Seleccione una opción.");
            marcarInvalido("rios"); esValido = false;
        } else marcarValido("rios");

        if (nacientes === "") {
            mostrarError("errorNacientes", "Seleccione una opción.");
            marcarInvalido("nacientes"); esValido = false;
        } else {
            marcarValido("nacientes");
            if (nacientes === "Si") {
                if (cantidadNacientes === "") {
                    mostrarError("errorCantidadNacientes", "Ingrese la cantidad.");
                    marcarInvalido("cantidadNacientes"); esValido = false;
                } else if (parseInt(cantidadNacientes) < 1) {
                    mostrarError("errorCantidadNacientes", "Debe ser al menos 1.");
                    marcarInvalido("cantidadNacientes"); esValido = false;
                } else marcarValido("cantidadNacientes");
            }
        }

        if (vegetacion === "") {
            mostrarError("errorVegetacion", "Seleccione una opción.");
            marcarInvalido("vegetacion"); esValido = false;
        } else marcarValido("vegetacion");

        if (usoSuelo === "") {
            mostrarError("errorUsoSuelo", "Seleccione una opción.");
            marcarInvalido("usoSuelo"); esValido = false;
        } else marcarValido("usoSuelo");

        if (listaArchivos.length === 0) {
            mostrarError("errorImagenes", "Debe subir al menos una imagen."); esValido = false;
        }

        if (!esValido) return;

        // Construir FormData

       




        const formData = new FormData();
        
        formData.append("IdUsuario", idu);

        formData.append("NombreFinca", nombreFinca);
        formData.append("Latitud", document.getElementById("latitud").value || "0");
        formData.append("Longitud", document.getElementById("longitud").value || "0");

        const provincia = document.getElementById("txtProvincia").value;
        const canton = document.getElementById("txtCanton").value;
        const distrito = document.getElementById("txtDistrito").value;
        formData.append("Ubicacion", `${provincia}, ${canton}, ${distrito}`);

        formData.append("Tamano", tamano);
        formData.append("TipoSuperficie", superficieSeleccionada.value);
        formData.append("TieneRio", rios === "Si" ? 1 : 0 );
        formData.append("Nacientes", nacientes);
        formData.append("CantidadNacientes", nacientes === "Si" ? cantidadNacientes : "0");
        formData.append("TipoVegetacion", vegetacion);
        formData.append("UsoSuelo", usoSuelo);

        // Imágenes — mismo key repetido para List<IFormFile>
        listaArchivos.forEach(archivo => formData.append("Fotografias", archivo));

        try {
            const response = await fetch("https://localhost:7106/api/Propiedad", {
                method: "POST",
                body: formData
              
            });

            if (!response.ok) {
                const errorTexto = await response.text();
                throw new Error(errorTexto || `Error ${response.status}`);
            }

            const resultado = await response.json();

            Swal.fire({
                icon: 'success',
                title: 'Éxito',
                text: 'Propiedad registrada correctamente'
            });

            console.log("Respuesta API:", resultado);

        } catch (err) {
            console.error("Error al enviar:", err);
            Swal.fire({
                icon: 'error',
                title: 'Error al registrar',
                text: err.message || 'Ocurrió un problema al guardar la propiedad.'
            });
        }
    });

});