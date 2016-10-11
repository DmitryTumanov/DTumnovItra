$(document).ready(function () {
    $('#timepicker').timepicker();
    hide();

    $('#timepicker').on('changeTime.timepicker', function (e) {
        $('#timeDisplay').text(e.time.value);
        $('#oldtimeDisplay').hide();
        $('#submitbutton').show();
        $('#cancelbutton').show();
        $('#timeDisplay').show();
    });

    $('#cancelbutton').on('click', function () {
        hide();
    });

    function hide() {
        $('#oldtimeDisplay').show();
        $('#timeDisplay').hide();
        $('#submitbutton').hide();
        $('#cancelbutton').hide();
    }
});