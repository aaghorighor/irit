
var js = {

    ajaxPost: function (url, param, datatype) {
        return tools.promise(function (resolve, reject) {

            var promise;

            promise = $.ajax({
                type: "POST",
                url: url,              
                data: param,
                cache: false,
                dataType: datatype != null ? datatype : 'json'
            });

            promise.done(function (data) {

                resolve(data);
            });

            promise.fail(function (xhr, status, error) {              
                reject(xhr);
            });

        });
    },

    post: function (url, param, datatype) {
        return tools.promise(function (resolve, reject) {

            var promise;       

            promise = $.ajax({
                type: "POST",
                url: url,
                data: param,
                cache: false,
                dataType: datatype != null ? datatype : 'json'
            });

            promise.done(function (data) {            

                resolve(data);
            });

            promise.fail(function (xhr, status, error) {               
                reject(xhr);
            });

        });
    },

    ajaxGet: function (url, param, datatype) {

        return tools.promise(function (resolve, reject) {

            var promise;

            promise = $.ajax({
                type: "GET",
                url: url,
                data: param,
                cache: false,
                dataType: datatype != null ? datatype : 'json'
            });

            promise.done(function (data) {

                resolve(data);
            });

            promise.fail(function (xhr, status, error) {
            
                reject(xhr);
            });
        });
    },

    ajaxSyncPost: function (url, param, datatype) {
        var promise;

        promise = $.ajax({
            type: "POST",
            url: url,
            data: param,
            cache: false,
            async: false,
            dataType: datatype != null ? datatype : 'json'
        });

        return promise;
    }
 
}

var iuHelper = {

    addressDropdown: function (data, element, Id, title)
    {
        $(element).empty();
        $(element).append('<option value="">' + title + '</option>');

        $(data).each(function ()
        {
            var $option = $("<option />");
            $option.attr("value", this.Value).text(this.Title);
            $(element).append($option);
        });

        if (Id != null) {
            $(element).val(Id);
        }
    }    
}