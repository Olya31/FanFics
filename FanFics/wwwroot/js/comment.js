function SubmitComment() {
    if ($("#RatingCounter").val() == "0") {
        alert("Please rate this service provider.");
        return false;
    }
    else {
        return true;
    }
}
function CRate(r) {
    $("#RatingCounter").val(r);
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).attr('class', 'starGlow');
    }
    for (var i = r + 1; i <= 5; i++) {
        $("#Rate" + i).attr('class', 'starFade');
    }
}

function CRateOver(r) {
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).attr('class', 'starGlow');
    }
}

function CRateOut(r) {
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).attr('class', 'starFade');
    }
}

function CRateSelected() {
    var setRating = $("#RatingCounter").val();
    for (var i = 1; i <= setRating; i++) {
        $("#Rate" + i).attr('class', 'starGlow');
    }
}