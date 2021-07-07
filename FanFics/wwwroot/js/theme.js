$(function () {
    const defaultThemeUrl = '/lib/bootstrap/dist/css/dark.css';

    $('#theme').change(function () {
        var themeUrl = $('#theme').find(":selected").val();        

        $('#stylesheet').attr({ href: themeUrl });

        setCookieToServer(themeUrl);
    }); 

    function setCookieToServer(themeUrl) {
        $.ajax({
            url: "/settings/SetThemeToCookie",
            type: "POST",
            data: { themeUrl: themeUrl },
            success: function (response) {
                if (response == true) {
                    console.log('theme was set to cookie.')
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    }

    function getCookie(name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
    }

    $(document).ready(function () {
        var cookie = getCookie('theme');

        var themeUrl = cookie == undefined ? defaultThemeUrl : cookie;

        $('#theme').val(themeUrl);
    });
});