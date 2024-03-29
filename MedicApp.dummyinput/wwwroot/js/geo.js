﻿function initMap() {

    // Create the map.
    const map = new google.maps.Map(document.getElementById('map'), {
        zoom: 7,
        center: { lat: 6.5244, lng: 3.3792 }
    });

    // Load the stores GeoJSON onto the map.
    map.data.loadGeoJson('locations.json');

    const infoWindow = new google.maps.InfoWindow();

    // Show the information for a store when its marker is clicked.
    map.data.addListener('click', event => {

        let name = event.feature.getProperty('name');
        //let description = event.feature.getProperty('description');
        //let hours = event.feature.getProperty('hours');
        //let phone = event.feature.getProperty('phone');
        let position = event.feature.getGeometry().get();
        let content = 
      <h2>${name}</h2>//<p>${description}</p>
      //<p><b>Open:</b> ${hours}<br/><b>Phone:</b> ${phone}</p>
    
        infoWindow.setContent(content);
        infoWindow.setPosition(position);
        infoWindow.setOptions({ pixelOffset: new google.maps.Size(0, -30) });
        infoWindow.open(map);
    });
}