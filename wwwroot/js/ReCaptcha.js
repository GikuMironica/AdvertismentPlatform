function ActivateRecaptcha(siteKey){
    grecaptcha.ready(function () {
        grecaptcha.execute(siteKey, { action: 'homepage' }).then(function (token) {
            console.log(token);
        });
    });
};

