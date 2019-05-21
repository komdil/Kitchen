$(document).ready(function () {
    $(".deleteMenu").on('click', function (e) {
        e.preventDefault();
        let answer = confirm("Do you want delete?");
        id = $(this).data("id");
        name = $(this).data("name");
        alert(name);
        if (answer) {
           // deleteUser(id);
        }
    })

    function deleteUser(id) {

        $.ajax({
            type: "Post",
            url: "/Admin/DeleteMenu",
            data: { "id": id },
            cache: false,
            success: function () {
                alert("You successfully delete Menu!")
            },
            error: function () {
                alert("rrr")
            }
        });
    }
});