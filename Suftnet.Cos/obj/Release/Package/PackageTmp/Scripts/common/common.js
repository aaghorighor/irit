function formatDate(ms) {

    var date = new Date(parseInt(ms));
    var hour = date.getHours();
    var mins = date.getMinutes() + '';
    var time = "AM";

    // find time 
    if (hour >= 12) {
        time = "PM";
    }
    // fix hours format
    if (hour > 12) {
        hour -= 12;
    }
    else if (hour == 0) {
        hour = 12;
    }
    // fix minutes format
    if (mins.length == 1) {
        mins = "0" + mins;
    }
    // return formatted date time string
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    return month + "/" + day + "/" + year;
}

function clear_form_elements(ele) {
    $(ele).find(':input').each(function () {
        alert(this.type);
        switch (this.type) {

            case 'password':
            case 'select-multiple':
            case 'select-one':
            case 'text':
            case 'textarea':
                $(this).val('');
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
        }
    }); 
}


function Reset() {
    $(':text, textarea , :password, file', '#form').val('');  
    // De-select any checkboxes, radios and drop-down menus
    $(':input, select option', '#form').removeAttr('checked').removeAttr('selected');
    //this is for selecting the first entry of the select     
    $("select option[value='0']").attr('selected', 'selected');
}


function CommaFormatted(amount) {
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
}


function CurrencyFormatted(amount) {
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
}

//function for validating emails
function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
    return pattern.test(emailAddress);
};

function numonly(root) {
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
}

function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));

    if (suftnet_Settings.dateTimeFormat = "dd/mm/yy")
    {
        return dt.getDate() + "/" + dt.getMonth()  + "/" + dt.getFullYear();
    }

    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
}

function imposeMaxLength(Event, Object, MaxLen) {
    return (Object.value.length <= MaxLen) || (Event.keyCode == 8 || Event.keyCode == 46 || (Event.keyCode >= 35 && Event.keyCode <= 40))
}

var MathHelper = (function () {
    this.round = function (number, numberOfDecimals) {
        var aux = Math.pow(10, numberOfDecimals);
        return Math.round(number * aux) / aux;
    };
    this.floor = function (number, numberOfDecimals) {
        var aux = Math.pow(10, numberOfDecimals);
        return Math.floor(number * aux) / aux;
    };
    this.ceil = function (number, numberOfDecimals) {
        var aux = Math.pow(10, numberOfDecimals);
        return Math.ceil(number * aux) / aux;
    };

    return {
        round: round,
        floor: floor,
        ceil: ceil
    }
})();

var suftnet = {

    tab: function (id) {
        $("ul.tabs li").removeClass("active");
        $("ul.tabs li:eq(" + id + ")").addClass("active");
        var activeTab = $("ul.tabs li:eq(" + id + ")").find("a").attr("href");
        $('.tab_content').fadeOut();
        $(activeTab).delay(400).fadeIn();
    }
}

 

