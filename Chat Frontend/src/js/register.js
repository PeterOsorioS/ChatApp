import { moduloAuth } from "./auth";

const moduloRegister = (() =>{

    moduloAuth.initialize(false);

    const nombre = document.querySelector('#name');
    const nombreUsuario = document.querySelector('#userName');
    const correo = document.querySelector('#email');
    const clave = document.querySelector('#password');
    const confirmarClave = document.querySelector('#confirmPassword');
    const divError = document.querySelector('#error-message');

    const validaciones = document.querySelectorAll('.validacion');
    const aplicarValicaciones = (input) =>{
        const placeholderOriginal = input.getAttribute('placeholder');
            if (input.value === '') {
                input.setAttribute('placeholder', 'El campo es obligatorio');
                input.classList.add('border-1', 'border-red-500', 'bg-red-100');
            } else {
                input.classList.remove('border-1', 'border-red-500', 'bg-red-100');
                input.setAttribute('placeholder', placeholderOriginal);
            }
        };
    
    validaciones.forEach(input => {  

        input.addEventListener('blur', function () {
            aplicarValicaciones(this);
        });
    });


   
    document.querySelector('#registerForm').addEventListener('submit', async function (event) {

        event.preventDefault();

        let validacion = true;

        validaciones.forEach(input => {
            aplicarValicaciones(input);
            if (input.value === '') {
                validacion = false;
            }
        });

        if(!validacion){
            divError.textContent = 'Todos los campos son obligatorios';
            divError.style.display = 'block';
            return;
        }

        const CrearUsuarioDTO = {
            Nombre : nombre.value,
            NombreUsuario : nombreUsuario.value,
            Correo : correo.value,
            clave : clave.value,
            ConfirmarClave : confirmarClave.value
        }
        try{

            const response = await fetch('https://localhost:7225/api/autenticacion/registro',{
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(CrearUsuarioDTO),
                keepalive: true
            });
    
            if(!response.ok){
                const errorData = await response.json();
                throw new Error( errorData.message || 'Ocurrió un error');
            }

            const data = await response;
            const token = data.token;

            moduloValidation.login(token);

            window.location.href = '/index.html'; 

        }catch(error){
            divError.textContent = error.message;
            divError.style.display = 'block';
        }
        
    });

})();