
var tools =
{    
    formatCurrency : function (amount)
    {        
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

    commaFormatted : function(amount)
    {       

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

    treatAsUTC:function (date) {
                var result = new Date(date);
                result.setMinutes(result.getMinutes() - result.getTimezoneOffset());
                return result;
            },

    daysBetween:function (startDate, endDate) {
            var millisecondsPerDay = 24 * 60 * 60 * 1000;
            return (tools.treatAsUTC(endDate) - tools.treatAsUTC(startDate)) / millisecondsPerDay;
        }
        ,
    parseFloatHTML: function (element) {
	        return parseFloat(element.innerHTML.replace(/[^\d\.\-]+/g, '')) || 0;
        },

    parsePrice : function (number) {
	        return number.toFixed(2).replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1,');
    },

    uniqueId : null,

    getUniqueName : function () {
        if (!tools.uniqueId) tools.uniqueId = (new Date()).getTime();
        var result = tools.uniqueId++
        return result.toString().substring(5);
    },

    percentage: function (percentage, value) {

        if (percentage <= 0)
        {
            return 0;
        }

        var tem3 = percentage / 100;     
        var temp2 = tem3 * value;
        return temp2;
    },

    round: function (number, numberOfDecimals) {
        var aux = Math.pow(10, numberOfDecimals);
        return Math.round(number * aux) / aux;
    },

    floor: function (number, numberOfDecimals) {
        var aux = Math.pow(10, numberOfDecimals);
        return Math.floor(number * aux) / aux;
    },

    ceil: function (number, numberOfDecimals) {
        var aux = Math.pow(10, numberOfDecimals);
        return Math.ceil(number * aux) / aux;
    },

    formattedDate: function (date, dateformat) {

        var d = new Date(date || Date.now()),
        month = '' + (d.getMonth() + 1),
        day = '' + (d.getDate() + 1),
        year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        if ("dd/mm/yy" == dateformat) {
            return [day, month, year].join('/');
        }

        if ("mm/dd/yy" == dateformat) {
            return [month, day, year].join('/');
        }
    },

    log: {
        /** Debugging messages */
        trace: function () { console.log.apply(console, arguments); },
        /** General messages */
        info: function () { console.log.apply(console, arguments); },
        /** Debugging errors */
        warn: function () { console.log.apply(console, arguments); },
        /** General errors */
        error: function () { console.error.apply(console, arguments); }
    },

    promise: function (resolver) {
        return new RSVP.Promise(resolver);
    },

    stringify: function (object) {
        //old versions of prototype affect stringify
        var pjson = Array.prototype.toJSON;
        delete Array.prototype.toJSON;

        var result = JSON.stringify(object);

        Array.prototype.toJSON = pjson;

        return result;
    },    

    absolute: function (loc) {
        if (document && typeof document.createElement === 'function') {
            var a = document.createElement("a");
            a.href = loc;
            return a.href;
        }
        return loc;
    }
    
  };

