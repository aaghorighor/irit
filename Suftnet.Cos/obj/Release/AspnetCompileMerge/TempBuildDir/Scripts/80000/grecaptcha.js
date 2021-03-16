var widgetId;

var _grecaptcha = function()
{
    document.getElementById("term").setCustomValidity("Please indicate that you accept the Terms and Conditions");

    $("#_submit").attr("disabled", true);

    widgetId = grecaptcha.render('ReCaptchContainer', {
        'sitekey': '6LdDcWoUAAAAAE0fD9WmluwVjluPSh_z52YIKczL',
        'callback': reCaptchaCallback,
        theme: 'light', 
        type: 'image',
        size: 'normal'
    });

}

var reCaptchaCallback= function (_response)
{
    if (_response !== '')
    {
        $("#_submit").attr("disabled", false);
    }  
}