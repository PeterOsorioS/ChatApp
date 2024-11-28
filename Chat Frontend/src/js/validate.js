export const moduloValidate = {

   getToken: function() {
    const token = sessionStorage.getItem('authToken');
        return token;
    }

};

