//$(document).ready(function () {
//    console.log("niggers")
//    $("#Add").click(function () {
//        $(".newSongForm").show()
//    })
//    $("#theButton").click(function () {
//        $.ajax
//            ({
//                type: "GET",
//                url: "test/GetSongs",
//                success: function (data) {
//                    console.log(data)
//                    let html = '';
//                    for (let i = 0; i < data.length; i++) {
//                        html += '<div class = "alert alert-success songs">'
//                        html += i + " "
//                        html += data[i].title + " "
//                        html += data[i].duration + " seconds"
//                        //html += ''
//                        //html += ''
//                        html += "</div>"
//                    };
//                    $("#main").html(html) 
//                },
//                error: function () {
//                    console.log("error")
//                }
//            });
//    })
//    $("#DelButton").click(function ()
//    {

//        var MyData = {
//            "id": document.getElementById("DelId").valueAsNumber
//        };
//        $.ajax
//            ({
//                type: "POST",
//                headers: {
//                    'Content-Type': 'application/json'
//                },
//                url: "test/DelSong",
//                data: JSON.stringify(MyData),
//                success: function (data) {
//                    alert(document.getElementById("DelId").valueAsNumber)
                     
//                }
//            })

//    })






//})
$(document).ready(function () {
    // Функция для получения всех категорий музыки
    function getAllCategories() {
        $.ajax({
            type: "GET",
            url: "test/GetAllCategories",
            success: function (data) {
                let options = '<option selected disabled>Select category</option>';
                data.forEach(function (category) {
                    options += `<option value="${category.id}">${category.genre}</option>`;
                });
                $("#categorySelect").html(options);
            },
            error: function () {
                console.log("Error occurred while fetching categories.");
            }
        });
    }

    // Функция для получения всех музыкальных треков
    function getAllSongs() {
        $.ajax({
            type: "GET",
            url: "test/GetSongs",
            success: function (data) {
                let html = '';
                data.forEach(function (song, index) {
                    html += `<div class="alert alert-success songs">
                                ${index + 1} ${song.title} ${song.duration} seconds
                            </div>`;
                });
                $("#main").html(html);
            },
            error: function () {
                console.log("Error occurred while fetching songs.");
            }
        });
    }

    // Вызываем функции для получения всех категорий и треков при загрузке страницы
    getAllCategories();
    getAllSongs();

    // Удаление музыкального трека
    $("#DelButton").click(function () {
        var songId = $("#DelId").val();
        $.ajax({
            type: "POST",
            url: "test/DelSong",
            data: { id: songId },
            success: function (data) {
                console.log("Song deleted successfully.");
                getAllSongs(); // После удаления обновляем список треков
            },
            error: function () {
                console.log("Error occurred while deleting song.");
            }
        });
    });

    // Добавление музыкального трека
    $("#FormAdd").submit(function (event) {
        event.preventDefault();
        var formData = {
            title: $("#inputTitle").val(),
            catId: $("#categorySelect").val(),
            duration: $("#exampleInputDuration").val()
        };
        $.ajax({
            type: "POST",
            url: "test/AddSong",
            data: JSON.stringify(formData),
            contentType: 'application/json',
            success: function (data) {
                console.log("Song added successfully.");
                $("#FormAdd")[0].reset(); // Сбрасываем форму после успешного добавления
                getAllSongs(); // После добавления обновляем список треков
            },
            error: function () {
                console.log("Error occurred while adding song.");
            }
        });
    });
});
