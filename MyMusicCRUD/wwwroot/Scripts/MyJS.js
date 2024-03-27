$(document).ready(function () {
    console.log("niggers")
    $("#Add").click(function () {
        $(".newSongForm").show()
    })
    $("#theButton").click(function () {
        $.ajax
            ({
                type: "GET",
                url: "test/GetSongs",
                success: function (data) {
                    console.log(data)
                    let html = '';
                    for (let i = 0; i < data.length; i++) {
                        html += '<div class = "alert alert-success songs">'
                        html += i + " "
                        html += data[i].title + " "
                        html += data[i].duration + " seconds"
                        //html += ''
                        //html += ''
                        html += "</div>"
                    };
                    $("#main").html(html) 
                },
                error: function () {
                    console.log("error")
                }
            });
    })
    $("#DelButton").click(function ()
    {

        var MyData = {
            "id": document.getElementById("DelId").valueAsNumber
        };
        $.ajax
            ({
                type: "POST",
                headers: {
                    'Content-Type': 'application/json'
                },
                url: "test/DelSong",
                data: JSON.stringify(MyData),
                success: function (data) {
                    alert(document.getElementById("DelId").valueAsNumber)
                     
                }
            })

    })






})
   