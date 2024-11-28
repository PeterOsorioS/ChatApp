import { jwtDecode } from "jwt-decode";
import { moduloValidate } from "./validate.js";
import { moduloAuth } from "./auth";

const moduloApp = (() => {

    moduloAuth.initialize();

    const token = moduloValidate.getToken();
    const chats = document.querySelectorAll('.chats');
    const inputMensaje = document.querySelector('#mensaje');
    const botonEnviar = document.querySelector('#enviar');
    const ListaMensajes = document.querySelector(`#ListaMensaje`);

    let salaActual;

    // Inicializar conexión con SignalR
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7225/signalR")
        .build();

    const usuarioDelToken = () => {
        if (token) {
            try {
                const decodedToken = jwtDecode(token);
                return Number(decodedToken.userId);
            } catch (error) {
                console.error('Error al decodificar el token:', error);
            }
        } else {
            console.log('No hay token de autenticación.');
        }
        return null;
    };

    const idUsuario = usuarioDelToken();

    // Conectar a SignalR
    connection.start()
        .then(() => console.log("Conectado a SignalR"))
        .catch(error => console.error("Error al conectar:", error));

    connection.on("RecibirMensaje", (sala, userId, mensaje) => {
        if (mensaje) {

            guardarMensajeLocalStorage(sala, userId, mensaje);
            if(sala == salaActual){
                const mensajeDiv = document.createElement('div');
        
                mensajeDiv.classList.add('break-words', 'whitespace-pre-wrap', 'max-w-xs', 'p-3', 'rounded-lg', 'px-4', 'py-2', 'mb-2', 'rounded-lg', 'max-w-xs');
        
                if (userId == idUsuario) {
                    mensajeDiv.classList.add('bg-green-200', 'text-green-900', 'self-end', 'ml-auto');  
                } else {
                    mensajeDiv.classList.add('bg-gray-200', 'text-gray-900', 'self-start', 'mr-auto');  
                }
                mensajeDiv.textContent = mensaje;
                ListaMensajes.appendChild(mensajeDiv);
            }
        }
        
    });
    

    // API Fetch para obtener usuario(s)
    const ObtenerUsuario = (id) => {
        return fetch(`https://localhost:7225/api/usuarios/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        }).then(response => {
            if (!response.ok) {
                throw new Error(`Error en la solicitud: ${response.status}`);
            }
            return response.json();
        }).catch(error => console.error('Error en la solicitud:', error));
    };

    const ObtenerUsuarios = () => {
        return fetch('https://localhost:7225/api/usuarios', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        }).then(response => {
            if (!response.ok) {
                throw new Error(`Error en la solicitud: ${response.status}`);
            }
            return response.json();
        }).then(usuarios => imprimirUsuarios(usuarios))
        .catch(error => console.error('Error:', error));
    };

    const UnirseSala = (button) => {
        salaActual = button.getAttribute('data-sala');
        ListaMensajes.innerHTML = ''
        ListaMensajes.id = `ListaMensaje-${salaActual}`;
        
        connection.invoke("UnirseSala", salaActual)
            .then(() => {
                console.log('Unido al chat:', salaActual);
                
                // Mostrar los mensajes correspondientes a la sala actual
                const mensajes = document.querySelector(`#contenedorMensajes-${salaActual}`);
                const inicio = document.querySelector('#mensajeInicial');
                contenedorMensajes.style.display = 'flex';
                mensajeInicial.style.display = 'none';
    
                const mensajesGuardados = JSON.parse(localStorage.getItem(salaActual)) || [];

    
                // Mostrar los mensajes guardados desde localStorage
                mensajesGuardados.forEach(({ userId, mensaje }) => {
                    const mensajeEnviado = document.createElement('div');
                    mensajeEnviado.classList.add('break-words', 'whitespace-pre-wrap', 'max-w-xs', 'p-3', 'rounded-lg', 'px-4', 'py-2', 'mb-2', 'rounded-lg', 'max-w-xs');
    
                    // Verificar si el mensaje es del usuario actual
                    if (userId === idUsuario) {
                        mensajeEnviado.classList.add('bg-green-200', 'text-green-900', 'self-end', 'ml-auto');  // Alineado a la derecha
                    } else {
                        mensajeEnviado.classList.add('bg-gray-200', 'text-gray-900', 'self-start', 'mr-auto');  // Alineado a la izquierda
                    }
    
                    mensajeEnviado.textContent = mensaje;
                    ListaMensajes.appendChild(mensajeEnviado);
                });
            })
            .catch(err => console.error('Error al unirse al chat', err));
    };
    const imprimirUsuarios = (usuarios) => {
        const listaUsuarios = document.getElementById('lista-usuarios');
        listaUsuarios.innerHTML = '';
        usuarios.forEach(usuario => {
            const li = document.createElement('li');
            li.textContent = `${usuario.nombreUsuario}`;
            li.dataset.id = usuario.id;
            li.style.cursor = "pointer";
            li.addEventListener("click", () => crearSala(usuario.id));
            listaUsuarios.appendChild(li);
        });
    };

    const EnviarMensaje = async (mensaje) => {
        if (!mensaje.trim()) return;

        const usuario = await ObtenerUsuario(idUsuario);
        connection.invoke("EnviarMensajeSala", salaActual, idUsuario, `${usuario.nombre}: ${mensaje}`)
        .then(() => {
            guardarMensajeLocalStorage(salaActual, idUsuario, `${usuario.nombre}: ${mensaje}`);
        })
        .catch(err => console.error("Error al enviar el mensaje:", err));
    };

    const guardarMensajeLocalStorage = (sala, userId, mensaje) => {
        let mensajesGuardados = JSON.parse(localStorage.getItem(sala)) || [];

        mensajesGuardados.push({ userId, mensaje });
        localStorage.setItem(sala, JSON.stringify(mensajesGuardados));
    };

    chats.forEach(button => {
        button.addEventListener('click', function () {
            UnirseSala(this);
        });
    });

    botonEnviar.addEventListener("click", () => {
        if (inputMensaje.value.trim() !== '') {
            EnviarMensaje(inputMensaje.value);
            inputMensaje.value = '';
        }
    });

    inputMensaje.addEventListener("keydown", (event) => {
        if (event.key === "Enter") {
            event.preventDefault(); 
            if (inputMensaje.value.trim() !== '') {
                EnviarMensaje(inputMensaje.value); 
                inputMensaje.value = ''; 
            }
        }
    });


    document.addEventListener("keydown", (event) =>{
        if (event.key === "Escape") {
            const activeElement = document.activeElement;
            activeElement.style.outline = 'none';

            contenedorMensajes.style.display = 'none';
            mensajeInicial.style.display = 'flex';
        }
    });


})();