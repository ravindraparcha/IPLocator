$(function () {
    //$('.partialContent').each(function (index, item) {
    //    var url = $(item).data('url');
    //    if (url && url.length > 0) {
    //        $(item).load(url);
    //    }
    //});

    getGeoCoordinates();

});

function validateIPAddress(inputText) {
    if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(inputText)) {
        return (true)
    }
    return false;
}
var latitude = "0";
var long = "0";
function getGeoCoordinates() {
    if (!navigator.geolocation) {
        alert("<p>Geolocation is not supported by your browser</p>");
        return;
    }

    function success(position) {
        latitude = position.coords.latitude;
        long = position.coords.longitude;
    };

    function error() {
        alert("Unable to retrieve your location");
    };
    navigator.geolocation.getCurrentPosition(success, error);
}


function getIPDetails() {
    var ip = $("#txtIPAddress").val().trim();
    if (!validateIPAddress(ip)) {
        $('#divIPData').html('Invalid IP Address. Please check again!').css('color','red');
        return false;
    }
    $('#divIPData').css('color', 'black');
    addRemoveBusyImage('divIPData', true);
    $.ajax({
        type: "GET",
        url: GetIPDetailsUrl,
        data: { IPAddress: ip },
        dataType: "html",
        success: function (data) {
            addRemoveBusyImage('divIPData', false);
            $('#divIPData').html(data);

        },
        error: function (data) {
            addRemoveBusyImage('divIPData', false);
            $('#divIPData').html('Error occurred! Cannot load the content');
        }
    });

}

function addRemoveBusyImage(divElement, isAdd) {
    $('#' + divElement).empty();
    if (isAdd) {
        // $('#' + divElement).addClass('text-center');
        $('#' + divElement).prepend($('<img>', { id: 'busyImg', src: '/Content/Images/loadingAnimation.gif', height: '100' }));
    }
    else {
        $('#' + divElement).removeClass('text-center');
    }
}

function getNearbyPlaces() {
    var ip = $("#txtIPAddress").val().trim();
    if (ip != "") {
        if (!validateIPAddress(ip)) {
            $('#divPlaces').html('Invalid IP Address. Please check again!').css('color', 'red');
            return false;
        }
    }
    $('#divPlaces').css('color', 'black');
    addRemoveBusyImage('divPlaces', true);
    $.ajax({
        type: "GET",
        url: GetNearbyPlacesUrl,
        data: { lat: latitude, longi: long, place: $('#PlaceKey').val(), radius: $('#RadiusKey').val(), ipAddress: ip },
        dataType: "html",
        success: function (data) {
            addRemoveBusyImage('divPlaces', false);
            $('#divPlaces').html(data);

        },
        error: function (data) {
            addRemoveBusyImage('', false);
            $('#divPlaces').html('Error occurred! Cannot load the content');
        }
    });
}