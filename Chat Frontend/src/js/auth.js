export const moduloAuth = {

    authentication: function () {
        const token = sessionStorage.getItem('authToken');
        if (!token) return false;

        try {
            // Decodifica el payload del token (asume formato JWT)
            const payload = JSON.parse(atob(token.split('.')[1]));
            const isExpired = Date.now() >= payload.exp * 1000; // Verifica si ha expirado
            return !isExpired;
        } catch (error) {
            console.error("Error al decodificar el token:", error);
            return false;
        }
    },

    login: function(token) {
        sessionStorage.setItem('authToken', token);
        window.location.replace('/index.html');
    },

    logout: function() {
        sessionStorage.removeItem('authToken');
        window.location.replace('/login.html');
    },

    monitorToken: function () {
        setInterval(() => {
            if (!this.authentication()) {
                console.warn("Sesión expirada. Cerrando sesión...");
                this.logout();
            }
        }, 60000);
    },
    
    initialize: function (rutaProtegida = true) {
        if (rutaProtegida && !this.authentication()) {
            console.warn("No autenticado. Redirigiendo al login...");
            this.logout();
        } else {
            console.log("Usuario autenticado. Inicializando módulo...");
        }
    }
};