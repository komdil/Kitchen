$(document).ready(function () {
    $("body").on('click', '#accept', function (e) {
        e.preventDefault();
        selectedMenu = $("#todaysMenu").val();
        message = $("#messageTextarea").val();
        console.log(selectedMenu)
        console.log(message);
        $.ajax({
            type: 'POST',
            url: '/Admin/SelectMenuForToday',
            data: { 'selectedMenu': selectedMenu, 'message': message },
            cache: false,
            success: function (result) {
                alert(result);                
            },
            error: function () {
                alert('rrr');
            }
        });
    });

}