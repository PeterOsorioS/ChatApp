import { moduloAuth } from "./auth";

const moduloIndex = (() => {
    moduloAuth.authentication();

    const inputLogout = document.querySelector('#logout');

    inputLogout.addEventListener('click', function (event) {
        moduloAuth.logout();
        });

})();