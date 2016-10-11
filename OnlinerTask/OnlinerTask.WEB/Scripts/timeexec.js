$(document).ready(function () {
    $('#timepicker').timepicker();
    $('#submitbutton').hide();

    $('#timepicker').on('changeTime.timepicker', function (e) {
        $('#timeDisplay').text(e.time.value);
        $('#submitbutton').show();
    });
});