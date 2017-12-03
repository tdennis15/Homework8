

function JavaAJAX_Call(genre) {
    document.getElementById('landing_zone').innerHTML = null;

    $.ajax({
        url: "/Home/Genre/",
        data: { genre: genre },
        type: "POST",
        datatype: "json",
        success: function (returnData) { 
            returnData.arr.forEach(function (item) {
                $('#landing_zone').append(item);
            }
            );
        }
    });
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
        if (DOB[1] > mm || (DOB[1] === mm && DOB[2] > dd)) {
            alert("Birth Date can't be in the future!");
            return false;
        }

        else {
            return true;
        }
    }
}
