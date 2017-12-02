

function JavaAJAX_Call(genre) {
    document.getElementById('landing_zone').innerHTML = null;

    $.ajax({
        url: "/Home/Genre/",
        data: { genre: genre },
        type: "POST",
        //datatype: "json",
        success: function (returnData) { 
            returnData.arr.forEach(function (item) {
                $('#landing_zone').append(item);
            }
            );
        }
    });
}

