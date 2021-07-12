    $("#tag").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/composition/search",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { value: item.tagName, id: item.id };
                    }));
                },
                error: function (xhr, status, error) {
                    alert("Error");
                }
            });
        },
        select: function (e, i) {
            var count = $('#tags .tag').length;
            $("#tags").append('<div class="tag">' +
                '<input name = "Tags[' + count + '].TagName" value = "' + i.item.value + '" readonly />' +
                '<input name = "Tags[' + count + '].Id" value = "' + i.item.id + '" type = "hidden" />' +
                '</div > ');
        },
        minLength: 1
    });


$(function () {
    SCRate();
});

$("#favorite").click(function () {

    var compositionId = $("#compositionId").val();
    var userId = $("#userId").val();

    $.ajax({
        url: "/Favorite/AddFavorite",
        type: "POST",
        data: { compositionId: compositionId, userId: userId },
        success: function (response) {
            if (response == true) {
                console.log("Good");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }
    });

});





