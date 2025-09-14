console.log("✅ JS CARGADO: swagger-custom.js");
window.addEventListener("load", function () {
    console.log("✅ swagger-custom.js cargado");

    const oldFetch = window.fetch;

    window.fetch = async function () {
        const response = await oldFetch.apply(this, arguments);
        const url = arguments[0];

        // ✅ Logout detectado
        if (url.includes("/api/Auth/logout") && response.status === 200) {
            console.log("🚪 Logout exitoso. Borrando token...");
            
            waitForUI(() => {
                const system = window.ui.getSystem();
                const auth = system.authSelectors.getConfigs();

                Object.keys(auth).forEach(name => {
                    system.authActions.logout(name); // 👈 Esto elimina el candado visual
                });
                window.ui.preauthorizeApiKey("Bearer", null); //Elimina el candado del token, visualmente
                window.localStorage.removeItem("swagger_access_token");
                window.location.reload();
            });

        }

        // ✅ Login detectado
        if (url.includes("/api/Auth/login") && response.status === 200) {
            const data = await response.clone().json();
            if (data.token) {
                console.log("🔐 Token recibido:", data.token);
                window.ui.preauthorizeApiKey("Bearer", data.token); //Aplica token en swagger
            }
        }

        return response;
    };

    function waitForUI(callback) {
        const check = setInterval(() => {
            if (window.ui && window.ui.getSystem) {
                clearInterval(check);
                callback();
            }
        }, 100);
    }

});
