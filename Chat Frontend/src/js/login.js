import { moduloAuth } from "./auth";

const moduloLogin = (() =>{
    
    moduloAuth.initialize(false);


    const correo = document.querySelector('#email');
    const clave = document.querySelector('#password');
    const errorDiv = document.getElementById('error-message');
    const validaciones = document.querySelectorAll('.validacion');

    const aplicarValidaciones = (input) =>{
        const placeholderOriginal = input.getAttribute('placeholder');
        if (input.value === '') {
            input.setAttribute('placeholder', 'El campo es obligatorio');
            input.classList.add('border-1', 'border-red-500', 'bg-red-100');
            return false;
        } else {
            input.classList.remove('border-1', 'border-red-500', 'bg-red-100');
            input.setAttribute('placeholder', placeholderOriginal);
            return true;
        }
    }

    validaciones.forEach(input => {  
        input.addEventListener('blur', function () {
            aplicarValidaciones(this);
        });
    });


    document.querySelector('#loginForm').addEventListener('submit', async function(event) {
        
        event.preventDefault();

        let valido = true;

        validaciones.forEach(input => {
            if (!aplicarValidaciones(input)) {
                valido = false; // Si algún campo no es válido, establece valido a false
            }
        });

        if(!valido) {
            errorDiv.textContent = 'Todos los campos son obligatorios';
            errorDiv.style.display = 'block';
            return;
        };

        // Creacion del DTO
        const loginDTO = {
            Correo: correo.value,
            Clave: clave.value
        };

        try {
    
            const response = await fetch('https://localhost:7225/api/autenticacion/iniciar-sesion', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(loginDTO),
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error( errorData.message || 'Ocurrió un error');
            }

            const data = await response.text();
            const token = data;
            moduloAuth.login(token);

        } catch (error) {
            errorDiv.textContent = error.message;
            errorDiv.style.display = 'block';
        }
    });

})();