function initMap() {
    var uluru = { lat: 10.824736, lng: 106.765711 };
    var map = new google.maps.Map(document.getElementById('googleMap'), {
        zoom: 14,
        center: uluru
    });
    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });
}
