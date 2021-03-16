var _mapMakerIcon;

var map = {

    init: function (mapMakerIcon) {
        _mapMakerIcon = mapMakerIcon     
    },
    initMap : function() {
        simpleMap(48.47292127, 4.28672791, "map-item", true, _mapMakerIcon);
    },
    edit: function (model) {
        if (model.Longitude != null && model.Latitude != null) {
            simpleMap(model.Latitude, model.Longitude, "map-item", true, _mapMakerIcon);
        }
    }
}