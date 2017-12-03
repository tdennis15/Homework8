

function JavaAJAX_Call(genreID) {
    document.getElementById('landing_zone').innerHTML = null;
    console.log(genreID);
    var source = "/Home/GenreResult?id=" + genreID;
    console.log(source);
    $.ajax({
        type: "GET",
        dataType: "json",
        url: source,
        success: function (data) {
            returnToHTML(data);
        },
        error: function (data) {
            alert("There was an error. Try again please!");
        }
    });
}

function returnToHTML(data) {
    $("#landing_zone").empty();
    $.each(data, function (i, field) {
        $("#landing_zone").append("<p>" + field["Title"] + " by " + field["ArtistName"] + "</p>");
    });
    $("#landing_zone").css("display", "block");
}



function validateForm() {
    // Find the date for today and assign the proper variables each part, for ease of checking
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //A "1off" bug was found here, January at 0?
    var yyyy = today.getFullYear();


    var DOB = document.getElementById("birthDate").value;
    DOB = DOB.split("-");
    //DOB[0] = Year //DOB[1] = Month //DOB[2] = Day

    // Confirm birthdate is not in the future
    if (DOB[0] > yyyy) {
        alert("Birth Date can't be in the future!");
        return false;
    }
    else if (DOB[0] === yyyy) {
        if (DOB[1] > mm || ((DOB[1] === mm) && (DOB[2] > dd))) {
            alert("Birth Date can't be in the future!");
            return false;
        }

        else {
            return true;
        }
    }
}
