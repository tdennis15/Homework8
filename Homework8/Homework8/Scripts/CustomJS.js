/**
 * This is the function from the index page to return all the genres found inside the models database

 * @param  genre is passed to help us know when the function is finished and to index our location
 *thanks to Jake Hatfield for explaining this
 */

function MakeAJAX_Call(genre) {

    //Resetting the div inside Index to make sure its blanks before we add anything to it (since we arent refreshing the page)
    document.getElementById('landing_zone').innerHTML = null;


    //ajax call
    $.ajax({
        //accessing the data inside our Genre view
        url: "/Home/Genre",

        //finding fields of genre
        data: { genre: genre },
        type: "POST",

        //returning the data by iterating over each item found, creating the function and then using the inner function to append it to the 
        //div found in index
        success: function (returnData) {
            returnData.arr.forEach(function (item) {
                $('#landing_zone').append(item);
            }
            )
        }
    });
}

