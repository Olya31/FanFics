
    $(".editable-text").editable(function (value) {

        $.ajax({
            url: "/profile/EditNickName",
            type: "POST",
            data: { value: value },
            success: function (response) {
                if (response == true) {
                    console.log("Successful execution");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });

        return (value);
    });
    $(".editable-text-fullname").editable(function (value) {

        $.ajax({
            url: "/profile/EditFullName",
            type: "POST",
            data: { value: value },
            success: function (response) {
                if (response == true) {
                    console.log("Successful execution");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });

        return (value);
    });
    $(".datepicker").editable(function (value) {

        type: 'datepicker';
        submit: 'OK';
        datepicker: {
            format: "dd-mm-yy"
        };

        $.ajax({
            url: "/profile/EditBirthday",
            type: "POST",
            data: { value: value },
            success: function (response) {
                if (response == true) {
                    console.log("Successful execution");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });

        return (value);
    });
    
