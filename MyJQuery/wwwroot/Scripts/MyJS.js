$(document).ready(function () {
    $("#myButton").click(function () {


        $.ajax
            ({
                type: "GET",
                url: "test/getCityAll",
                success: function (data) {
                    $("#cbCity").empty();
                    for (let i = 0; i < data.length; i++) {
                        $("#cbCity").append('<option value=' + data[i].id + '>' + data[i].name + '</option >');
                    };
                    let html = '';
                    html += "<table border='1' cellpadding='1' cellspacing='1' width='500'>";
                    html += "<tr bgcolor='#ffd400'>";
                    html += "<td class='text-center'>id</td>"
                    html += "<td class='text-center'>name</td>";
                    html += "</tr>";
                    $.each(data, function (i, item) {
                        html += "<tr><td>" + item.id + "</td>" +
                            "<td>" + item.name + "</td></tr>";
                    });
                    html += "</table >";
                    $("#divTest").html(html);
                },
                error: function () {
                    console.log("error")
                }
            });

    });

    $("#myButton2").click(function () {
        var st = `<table border='1'>`;
        st += '<tr bgcolor="#ffd400"><td class="text-center">id</td><td class="text-center">name</td></tr>';
        st += '<tr><td>1</td><td>Алматы</td></tr>';
        st += '<tr><td>2</td><td>Астана</td></tr>';
        st += '</table>';
        $("#divTest").html(st);

    });
})
function F1() {
    alert('hello step');
}

function showModal() {
    $("#myModal").modal("show");
}

function createCity() {
    var MyData = {
        "id": 4,
        "name": "Aktobe"
    };
    $.ajax
        ({
            type: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            url: "test/createCity",
            data: JSON.stringify(MyData),
            success: function (data) {
                console.log(data);
            },
            error: function () {
                console.log("error")
            }
        });
}
function confirm() {

    $.confirm({
        title: 'Are you sure to delete book!',
        content: 'Deletion!',
        buttons: {
            confirm: function () {
                //$.alert('go to delete');

            },
            cancel: function () {
                //$.alert('cancel delete!');
            }
        }
    });
}