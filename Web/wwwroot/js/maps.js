const { type } = require("jquery");

//$(document).ready(function () {
function initMap() {
    const stockholm = { lat: 59.3293, lng: 18.0686 };
    var allMarkerData = document.querySelectorAll("[data-marker]");
    var mapDiv = document.querySelector("#map");

    const map = new google.maps.Map(mapDiv, {
        zoom: 8,
        center: stockholm,
    });

    const unviewedListing = {
        path: google.maps.SymbolPath.CIRCLE,
        strokeColor: "Blue",
        strokeWeight: 1,
        fillColor: "DodgerBlue",
        fillOpacity: 0.9,
        scale: 10,
    };

    const viewedListing = {
        path: google.maps.SymbolPath.CIRCLE,
        strokeColor: "DarkSlateGrey",
        strokeWeight: 1,
        fillColor: "Grey",
        fillOpacity: 0.9,
        scale: 10,
    };

    var bounds = new google.maps.LatLngBounds();

    allMarkerData.forEach(function (markerData) {
        var markerDataObj = JSON.parse(markerData.dataset.marker)
        const markerLatLong = { lat: markerDataObj.Latitude, lng: markerDataObj.Longitude }
        var location = new google.maps.LatLng(markerDataObj.Latitude, markerDataObj.Longitude);

        var markerHeading = '<h6><a href="/Home/RealEstateObject/' + markerDataObj.Id + '" target="_blank">' + markerDataObj.Address + '</a></h6>';
        var markerDescription = '<p>' + markerDataObj.Description + '</p>';
        var markerPrice = '<p>' + markerDataObj.Price + ' kr</p>';
        var markerNrOfRooms = '<p>' + markerDataObj.NrOfRooms + ' rum</p>';
        var markerArea = '<p>' + markerDataObj.LivingArea + ' kvadrat</p>';
        var contentString = markerHeading + markerDescription + markerPrice + markerNrOfRooms + markerArea;

        var infowindow = new google.maps.InfoWindow({
            content: contentString,
            minWidth: 200,
            maxWidth: 300,
        });

        const marker = new google.maps.Marker({
            position: markerLatLong,
            title: markerDataObj.Address,
            map: map,
            icon: unviewedListing,
            realEstateObjectId: markerDataObj.Id,
        });

        bounds.extend(location);

        marker.addListener("click", () => {
            infowindow.open({
                anchor: marker,
                map,
                shouldFocus: false,
            });
        });

        marker.addListener("click", () => {
            marker.setIcon(viewedListing);
        });
    });

    if (allMarkerData.length > 0) {
        map.fitBounds(bounds);
        map.setCenter(bounds.getCenter());
    }    
}