function validateIPAddress(e) { return /^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(e) ? !0 : !1 } function getGeoCoordinates() { function e(e) { latitude = e.coords.latitude, long = e.coords.longitude } function a() { alert("Unable to retrieve your location") } return navigator.geolocation ? void navigator.geolocation.getCurrentPosition(e, a) : void alert("<p>Geolocation is not supported by your browser</p>") } function getIPDetails() { var e = $("#txtIPAddress").val().trim(); return validateIPAddress(e) ? ($("#divIPData").css("color", "black"), addRemoveBusyImage("divIPData", !0), void $.ajax({ type: "GET", url: GetIPDetailsUrl, data: { IPAddress: e }, dataType: "html", success: function (e) { addRemoveBusyImage("divIPData", !1), $("#divIPData").html(e) }, error: function (e) { addRemoveBusyImage("divIPData", !1), $("#divIPData").html("Error occurred! Cannot load the content") } })) : ($("#divIPData").html("Invalid IP Address. Please check again!").css("color", "red"), !1) } function addRemoveBusyImage(e, a) { $("#" + e).empty(), a ? $("#" + e).prepend($("<img>", { id: "busyImg", src: "/Content/Images/loadingAnimation.gif", height: "100" })) : $("#" + e).removeClass("text-center") } function getNearbyPlaces() { addRemoveBusyImage("divPlaces", !0), $.ajax({ type: "GET", url: GetNearbyPlacesUrl, data: { lat: latitude, longi: long, place: $("#PlaceKey").val(), radius: $("#RadiusKey").val() }, dataType: "html", success: function (e) { addRemoveBusyImage("divPlaces", !1), $("#divPlaces").html(e) }, error: function (e) { addRemoveBusyImage("", !1), $("#divPlaces").html("Error occurred! Cannot load the content") } }) } $(function () { getGeoCoordinates() }); var latitude = "0", long = "0";

//$(function () {
//    //$('.partialContent').each(function (index, item) {
//    //    var url = $(item).data('url');
//    //    if (url && url.length > 0) {
//    //        $(item).load(url);
//    //    }
//    //});

//    getGeoCoordinates();

//});

//function validateIPAddress(inputText) {
//    if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(inputText)) {
//        return (true)
//    }
//    return false;
//}
//var latitude = "0";
//var long = "0";
//function getGeoCoordinates() {
//    if (!navigator.geolocation) {
//        alert("<p>Geolocation is not supported by your browser</p>");
//        return;
//    }

//    function success(position) {
//        latitude = position.coords.latitude;
//        long = position.coords.longitude;
//    };

//    function error() {
//        alert("Unable to retrieve your location");
//    };
//    navigator.geolocation.getCurrentPosition(success, error);
//}


//function getIPDetails() {
//    var ip = $("#txtIPAddress").val().trim();
//    if (!validateIPAddress(ip)) {
//        $('#divIPData').html('Invalid IP Address. Please check again!').css('color','red');
//        return false;
//    }
//    $('#divIPData').css('color', 'black');
//    addRemoveBusyImage('divIPData', true);
//    $.ajax({
//        type: "GET",
//        url: GetIPDetailsUrl,
//        data: { IPAddress: ip },
//        dataType: "html",
//        success: function (data) {
//            addRemoveBusyImage('divIPData', false);
//            $('#divIPData').html(data);

//        },
//        error: function (data) {
//            addRemoveBusyImage('divIPData', false);
//            $('#divIPData').html('Error occurred! Cannot load the content');
//        }
//    });

//}

//function addRemoveBusyImage(divElement, isAdd) {
//    $('#' + divElement).empty();
//    if (isAdd) {
//        // $('#' + divElement).addClass('text-center');
//        $('#' + divElement).prepend($('<img>', { id: 'busyImg', src: '/Content/Images/loadingAnimation.gif', height: '100' }));
//    }
//    else {
//        $('#' + divElement).removeClass('text-center');
//    }
//}

//function getNearbyPlaces() {
//    debugger;
//    addRemoveBusyImage('divPlaces', true);
//    $.ajax({
//        type: "GET",
//        url: GetNearbyPlacesUrl,
//        data: { lat: latitude, longi: long, place: $('#PlaceKey').val(), radius: $('#RadiusKey').val() },
//        dataType: "html",
//        success: function (data) {
//            addRemoveBusyImage('divPlaces', false);
//            $('#divPlaces').html(data);

//        },
//        error: function (data) {
//            addRemoveBusyImage('', false);
//            $('#divPlaces').html('Error occurred! Cannot load the content');
//        }
//    });
//}