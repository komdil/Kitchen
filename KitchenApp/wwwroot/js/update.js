$(document).ready(function () {
    $("#deleteuser").on('click', function (e) {
        e.preventDefault();
        let answer = confirm("Do you want delete?");
        id = $(this).data("id");

        if (answer) {
            deleteUser(id);
        }
    })

    function deleteUser(id) {
        
        $.ajax({
            type: "Post",
            url: "/Admin/DeleteUser",
            data: { "id": id },
            cache: false,
            success: function () {
                alert("You successfully delete user!")
            },
            error: function () {
                alert("rrr")
            }
        });
    }
});