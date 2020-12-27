var suftnet_validation = {

    // Used for validating generic form elements
    IsValid: function (element) {
        var isValid = true;
        var $element = $(element);
        var id = $element.attr("id");
        var name = $element.attr("name");
        var value = $element.val();
        var type = $element[0].type.toLowerCase();

        switch (type) {
            case 'text':
            case 'textarea':
            case 'password':
                if (value.length == 0 || value.replace(/\s/g, '').length == 0) { isValid = false; }
                break;
            case 'select-one':
            case 'select-mutiple':              
                if (!value) { isValid = false; }
                break;
            case 'checkbox':
            case 'radio':
                if ($('input[name"' + name + '"]:checked').length == 0) { isValid = false; };
                break;
        }

        if (isValid == true) {
            $element.removeClass("error");
        } else {
            $element.addClass("error");
        }

        return isValid;
    },

    // used for validating email address
    IsEmail: function (element) {

        var isValid = true;
        var $element = $(element);
        var id = $element.attr("id");
        var name = $element.attr("name");
        var value = $element.val();

        var filter = /^[a-zA-Z0-9]+[a-zA-Z0-9_.-]+[a-zA-Z0-9_-]+@[a-zA-Z0-9]+[a-zA-Z0-9.-]+[a-zA-Z0-9]+.[a-z]{2,4}$/;
        //if it's valid email
        if (!filter.test(value)) {
            isValid = false; $element.addClass("error");
        }

        return isValid;

    },

    // used for validating data
    IsDate: function (element, dateformat) {

        var isValid = true;
        var $element = $(element);
        var id = $element.attr("id");
        var name = $element.attr("name");
        var value = $element.val();
        var filter;

        switch(dateformat)
        {
            case 'mm/dd/yy':
            case 'mm/dd/yyyy' :
                filter = /^((0?[13578]|10|12)(-|\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(-|\/)((19)([2-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1}))|(0?[2469]|11)(-|\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(-|\/)((19)([2-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1})))$/;
                break;
            case 'dd/mm/yy':
            case 'dd/mm/yyyy':
                filter = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
                break;

        }     
         
        if (!filter.test(value)) {
            isValid = false; $element.addClass("error");
        }

        return isValid;

    },

    // used for validating number only
    IsNumber: function (element) {

    },

    // used for validating decimal only
    IsDecimal: function (element) {

    },

    EventListener: function () {
        $('input.required, input.personnelrequired, input.addressrequired, select.personnelrequired, input.emailrequired, select.required, textarea.required').change(function () {
            if ($(this).hasClass('error')) {
                $(this).removeClass("error");
            }
        });

        $('input.required, input.emailrequired, input.addressrequired, select.required, select.personnelrequired, input.personnelrequired,textarea.required').focus(function () {
            if ($(this).hasClass('error')) {
                $(this).removeClass("error");
            }
        });
    },

    listener: function () {
        $('input, select, textarea').change(function () {
            if ($(this).hasClass('error')) {
                $(this).removeClass("error");
            }
        });
        $('input, select, textarea').focus(function () {
            if ($(this).hasClass('error')) {
                $(this).removeClass("error");
            }
        });       
    },

    clearFormErrorMessages: function (form) {
        jQuery(form).validationEngine('hideAll');
    },

    clearErrorMessages: function (form) {
        $('input, select, textarea').focus(function () {          

            jQuery(form).validationEngine('hideAll');
            
        });
    },

    isValid: function (form) {
        $("#" + form).validationEngine('hideAll');
        if (!$("#" + form).validationEngine('validate')) {
            return false;
        }
        return true;
    }
};