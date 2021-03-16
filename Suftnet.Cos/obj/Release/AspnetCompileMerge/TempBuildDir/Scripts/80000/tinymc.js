
var tinymc = {

    start: function (e, height) {

        tinyMCE.init({
            mode: "exact",
            elements: e,
            width: "100%",
            height: height
        });
    }
}