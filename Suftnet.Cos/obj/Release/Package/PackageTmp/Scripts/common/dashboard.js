
var dashBoard = function () {

    function getItemCount(_response) {       

        var len = 0, key;
        for (key in _response) {          
           
            if (_response.hasOwnProperty(key)) len++;
        }
        return len;
    }

    function getPartDate(y) {
        var d = new Date(y || Date.now()),
            month = '' + (d.getMonth() + 1),
            day = '' + (d.getDate() + 1),
            year = d.getFullYear();

        return [year, month, day].join('-');
    }

   function getBasket(_response) {

        var items = [];
        var itemCount = getItemCount(_response);

        while (itemCount--) {

            var item =
             {
                 a: _response[itemCount].a,                
                 y: getPartDate(_response[itemCount].y)
             };          

            items.push(item);
        }

        return items;
    }

    return {
        getBasket: getBasket
    }
}();