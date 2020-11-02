
var Suftnet_Utility =
{
    CurrencyFormatted: function (amount) {
        var i = parseFloat(amount);
        if (isNaN(i)) { i = 0.00; }
        var minus = '';
        if (i < 0) { minus = '-'; }
        i = Math.abs(i);
        i = parseInt((i + .005) * 100);
        i = i / 100;
        s = new String(i);
        if (s.indexOf('.') < 0) { s += '.00'; }
        if (s.indexOf('.') == (s.length - 2)) { s += '0'; }
        s = minus + s;
        return s;
    },

    CommaFormatted: function (amount) {
        var delimiter = ","; // replace comma if desired
        var a = amount.split('.', 2)
        var d = a[1];
        var i = parseInt(a[0]);
        if (isNaN(i)) { return ''; }
        var minus = '';
        if (i < 0) { minus = '-'; }
        i = Math.abs(i);
        var n = new String(i);
        var a = [];
        while (n.length > 3) {
            var nn = n.substr(n.length - 3);
            a.unshift(nn);
            n = n.substr(0, n.length - 3);
        }
        if (n.length > 0) { a.unshift(n); }
        n = a.join(delimiter);
        if (d.length < 1) { amount = n; }
        else { amount = n + '.' + d; }
        amount = minus + amount;
        return amount;
    },

    treatAsUTC: function (date) {
        var result = new Date(date);
        result.setMinutes(result.getMinutes() - result.getTimezoneOffset());
        return result;
    },

    DaysBetween: function (startDate, endDate) {
        var millisecondsPerDay = 24 * 60 * 60 * 1000;
        return (Suftnet_Utility.treatAsUTC(endDate) - Suftnet_Utility.treatAsUTC(startDate)) / millisecondsPerDay;
    }
        ,
    parseFloatHTML: function (element) {
        return parseFloat(element.innerHTML.replace(/[^\d\.\-]+/g, '')) || 0;
    },

    parsePrice: function (number) {
        return number.toFixed(2).replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1,');
    },

    numonly: function (root) {
        var reet = root.value;
        var arr1 = reet.length;
        var ruut = reet.charAt(arr1 - 1);
        if (reet.length > 0) {
            var regex = /[0-9]|\./;
            if (!ruut.match(regex)) {
                var reet = reet.slice(0, -1);
                $(root).val(reet);
            }
        }
    },

   imposeMaxLength : function(Event, Object, MaxLen) {
        return (Object.value.length <= MaxLen) || (Event.keyCode == 8 || Event.keyCode == 46 || (Event.keyCode >= 35 && Event.keyCode <= 40))
    },

   isValidEmailAddress : function (emailAddress) {
        var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
        return pattern.test(emailAddress);
    },
    seoUrl: function (url) {

        var encodedUrl = url.toString().toLowerCase();           
        encodedUrl = encodedUrl.split(/\&+/).join("-and-");     
        encodedUrl = encodedUrl.split(/[^a-z0-9]/).join("-");       
        encodedUrl = encodedUrl.split(/-+/).join("-"); 
        encodedUrl = encodedUrl.trim('-');

        return encodedUrl; 
    },

    toSeoUrl: function (url) {

        return url.toString()               // Convert to string
            .normalize('NFD')               // Change diacritics
            .replace(/[\u0300-\u036f]/g, '') // Remove illegal characters
            .replace(/\s+/g, '-')            // Change whitespace to dashes
            .toLowerCase()                  // Change to lowercase
            .replace(/&/g, '-and-')          // Replace ampersand
            .replace(/[^a-z0-9\-]/g, '')     // Remove anything that is not a letter, number or dash
            .replace(/-+/g, '-')             // Remove duplicate dashes
            .replace(/^-*/, '')              // Remove starting dashes
            .replace(/-*$/, '');  
    }
};

