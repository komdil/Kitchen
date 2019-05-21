function DeleteMenu(name) {
    let answer = confirm("Do you want delete menu " + name + " ?");
    if (answer) {
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
}