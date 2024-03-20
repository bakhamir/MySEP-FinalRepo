$(document).ready(function () {
    $("#theButton").click(function () {
        $.ajax
            ({
                type: "GET",
                url: "test/GetSongs",
                success: function (data) {
                    let html = '';
                    for (let i = 0; i < data.length; i++) {
                        html += '<div class = "alert alert-success">'
                        html += data[i].title + " "
                        html += data[i].duration
                        html += "</div>"
                    };
                    $("#main").html(html) 
                },
                error: function () {
                    console.log("error")
                }
            });
    })
    //$("#Add").click(function () {
    //    var MyData = {
    //        "id": 4,
    //        "name": "Aktobe"
    //    };
    //    $.ajax
    //        ({
    //            type: "POST",
    //            headers: {
    //                'Content-Type': 'application/json'
    //            },
    //            url: "test/AddSong",
    //            data: JSON.stringify(MyData),
    //            success: function (data) {
    //                console.log(data);
    //            },
    //            error: function () {
    //                console.log("error")
    //            }
    //        });
    $("#Add").click(function () {
        $("#FormAdd").show
    })



    })


})




       