$(document).ready(function () {
    $("#desired").on("mouseenter", swipe);
    $("#undesired").on("mouseenter", swipe);
    $("#desired").on("mouseleave", unswipe);
    $("#undesired").on("mouseleave", unswipe);

    $("#viewpassword").on("click", function () {
        $("#password").show();
        $("#viewpassword").hide();
    });

    function swipe() {
        $(this).animate({ "width": "70%" });
        $(this).siblings().animate({ "width": "30%" });
        $(this).siblings().find("h3").hide();
    }
    function unswipe() {
        $(this).animate({ "width": "50%" });
        $(this).siblings().animate({ "width": "50%" });
        $(this).siblings().find("h3").show();
    }
});