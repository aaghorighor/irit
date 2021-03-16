
var js = {

    ajaxPost: function (url, param, datatype, token)
    {      
        return tools.promise(function (resolve, reject) {
                       
            var __requestVerificationToken = $("[name='__RequestVerificationToken']").val();
           
            var promise;   
            
            $.preloader.start({
                position: 'center',
                modal: true,
                src: $("#spriteUrl").attr("data-spriteUrl")
            }); 

            promise = $.ajax({
                type: "POST",
                url: url,
                headers: {'__RequestVerificationToken': __requestVerificationToken},
                data: param,
                cache: false,
                dataType: datatype != null ? datatype : 'json'
            });

            promise.done(function (data) {

                $.preloader.stop();

                if (data.ok) {

                    setTimeout("showSuccess('Success',5000);", 1000);

                    resolve(data);
                } else {
                   
                    if (data.isValid != undefined)
                    {
                        $.each(data.errors, function (i, value) {
                            $("#" + value.PropertyName).addClass("error");                            
                        });

                    } else
                    {
                        showError(data.msg, 5000);
                        reject(data);
                    }                   
                }
            });

            promise.fail(function (xhr, status, error)
            {             
                $.preloader.stop();
                reject(xhr);               
            });
           
        });
     
    },

    post: function (url, param, datatype)
    {     
        return tools.promise(function (resolve, reject) {

            var promise;

            $.preloader.start({
                position: 'center',
                modal: true,
                src: $("#spriteUrl").attr("data-spriteUrl")
            }); 

            promise = $.ajax({
                type: "POST",
                url: url,            
                data: param,
                cache: false,
                dataType: datatype != null ? datatype : 'json'
            });

            promise.done(function (data) {

                $.preloader.stop();

                if (data.ok) {
                   
                    resolve(data);
                } else {                 

                    if (data.isValid != undefined) {
                        $.each(data.errors, function (i, value) {
                            $("#" + value.PropertyName).addClass("error");
                        });

                    } else {                  
                        reject(data);
                    }

                    reject(data);
                }
            });

            promise.fail(function (xhr, status, error)
            {          
                $.preloader.stop();

                reject(xhr);
            });         

        });      
    },

    ajaxGet: function (url, param, datatype) {        
      
        return tools.promise(function (resolve, reject) {          

            var promise;

            $.preloader.start({
                position: 'center',
                modal: true,
                src: $("#spriteUrl").attr("data-spriteUrl")
            }); 

            promise = $.ajax({
                type: "GET",
                url: url,
                data: param,
                cache: false,
                dataType: datatype != null ? datatype : 'json'
            });

            promise.done(function (data) {

                $.preloader.stop();

                if (data.ok) {
                    resolve(data);
                } else {

                    showError(data.msg, 5000);
                    reject(data);
                }
            });

            promise.fail(function (xhr, status, error) {

                $.preloader.stop();

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
    },

    confirm: function (url, param, index, tableId)
    {
        return tools.promise(function (resolve, reject) {

            var promise;

            $.confirm.show({
                "message": "Do you want to delete this row?",
                "type": "danger",
                "yes": function () {

                    $.preloader.start({
                        position: 'center',
                        modal: true,
                        src: $("#spriteUrl").attr("data-spriteUrl")
                    }); 

                    promise = $.ajax({
                        type: "POST",
                        url: url,
                        data: param,
                        cache: false,
                        dataType: 'json'
                    });

                    promise.done(function (data) {

                        $.preloader.stop();

                        if (data.ok) {
                           
                            $(tableId).find("#" + index).fadeOut("normal", function () {
                                $(this).remove();
                            });

                            resolve(data);
                        } else {
                           
                            reject(data);
                        }
                    
                    });

                    promise.fail(function (xhr, status, error) {
                        reject(xhr);

                        $.preloader.stop();
                    });
                   
                },
                "no": function () {

                }
            });

        });
    },

    dyconfirm: function (url, param, index, tableId)
{
        return tools.promise(function (resolve, reject) {

            var promise;

            $.confirm.show({
                "message": "Do you want to delete this row?",
                "type": "danger",
                "yes": function () {

                    $.preloader.start({
                        position: 'center',
                        modal: true,
                        src: $("#spriteUrl").attr("data-spriteUrl")
                    }); 

                    promise = $.ajax({
                        type: "POST",
                        url: url,
                        data: param,
                        cache: false,
                        dataType: 'json'
                    });

                    promise.done(function (data) {

                        $.preloader.stop();

                        if (data.ok) {

                            resolve(data);
                        } else {

                            reject(data);
                        }                      
                    });

                    promise.fail(function (xhr, status, error) {    

                        $.preloader.stop();
                        reject(xhr);
                    });                   
                },
                "no": function () {

                }
            });

        });

    }
}

var iuHelper = {
  
   dropdown: function (data, element, Id) {
        $(element).empty();
        $(element).append('<option value=""> --SELECT-- </option>');
        $(data).each(function ()
        {          
            var $option = $("<option />");                  
            $option.attr("value", this.Id).text(this.Title);                 
            $(element).append($option);
        });

        if (Id != null)
        {
            $(element).val(Id);
        }     
    },
   postcodeDropdown: function (data, element, Id) {
        $(element).empty();
        $(element).append('<option value=""> --SELECT-- </option>');
        $(data).each(function () {
            var $option = $("<option />");
            $option.attr("value", this.Title).text(this.Title);
            $(element).append($option);
        });

        if (Id != null) {
            $(element).val(Id);
        }

    },
   roledropdown: function (data, element) {
        $(element).empty();
        $.each(data, function (i, value) {
            $(element).append('<option value="' + value + '">' + value + '</option>');
        });
    },
   lookupDropdown: function (data, element) {
        $(element).empty();
      
        $(data).each(function ()
        {
            var $option = $("<option />");                  
            $option.attr("value", this.ID).text(this.Title);                
            $(element).append($option);
        });
    },   
   stripeDropdown: function (data, element) {

        $(element).empty();
        $(data).each(function () {
            // Create option
            var $option = $("<option />");
            // Add value and text to option          
            $option.attr("value", this.amount).text(this.Title);
            // Add option to drop down list          
            $(element).append($option);
        });

        $(element).prepend('<option selected ="selected" value="">--SELECT--</option>');

   },
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
   },
   resetForm: function (element) {
        $(element).get(0).reset();

        suftnet_Settings.ClearFormErrorMessages(element);
    },
   submit: function (element) {
        $(element).submit();
    }
}