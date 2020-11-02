$(function () {

    /* ---------- Add class .active to current link  ---------- */
    $('li.limenu a').each(function () {        

        if ($($(this))[0].href == String(window.location)) {

            $(this).parent().addClass('select');
        }

    });

    $('li.limenu ul li a').each(function () {

        if ($($(this))[0].href == String(window.location)) {

            $(this).parent().parent().parent().addClass('select');
        }

    });

    /* ---------- Submenu  ---------- */

    $('.limenu').click(function (e) {

        $(this).find('ul').slideToggle();

    });

    LResize();

    $(window).resize(function () { LResize(); });
    $(window).scroll(function () { scrollmenu(); });

    $('.data_table').dataTable({
        "sDom": 'f<"clear">rt<"clear">',
        "aaSorting": [],
        "aoColumns": [{ "bSortable": false }, { "bSortable": false}]
    });

    $('.data_messages').dataTable({
        "sDom": 'fCl<"clear">rtip',
        "aaSorting": [],
        "aoColumns": [{ "bSortable": false }, null, { "bSortable": false }, { "bSortable": false}]
    });

    $('.data_table').dataTable({
        "dom": 'f<"clear">rt<"clear">',
        "ordering": [],
        "aoColumns": [{ "bSortable": false }, { "bSortable": false }]
    });

    $('.data_messages').dataTable({
        "dom": 'fCl<"clear">rtip',
        "ordering": []     
    });

    $('.static').dataTable({
        "dom": '',
        "ordering": []       
    });

    $('.data_table2').dataTable({
        "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
        "iDisplayLength": 5,
        "sPaginationType": "full_numbers"
    });

    $('.data_table3').dataTable({
        "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
        "iDisplayLength": 5
    });

    // Form validationEngine
    $('form#validation').validationEngine();

  
    // Check browser fixbug
    var mybrowser = navigator.userAgent;
    if (mybrowser.indexOf('MSIE') > 0) {
        $(function () {
            $('.formEl_b fieldset').css('padding-top', '0');
            $('div.section label small').css('font-size', '10px');
            $('div.section  div .select_box').css({ 'margin-left': '-5px' });
            $('.iPhoneCheckContainer label').css({ 'padding-top': '6px' });
            $('.uibutton').css({ 'padding-top': '6px' });
            $('.uibutton.icon:before').css({ 'top': '1px' });
            $('.dataTables_wrapper .dataTables_length ').css({ 'margin-bottom': '10px' });
        });
    }

    if (mybrowser.indexOf('Firefox') > 0) {
        $(function () {
            $('.formEl_b fieldset  legend').css('margin-bottom', '0px');
            $('table .custom-checkbox label').css('left', '3px');
        });
    }

    if (mybrowser.indexOf('Presto') > 0) {
        $('select').css('padding-top', '8px');
    }

    if (mybrowser.indexOf('Chrome') > 0) {
        $(function () {
            $('div.tab_content  ul.uibutton-group').css('margin-top', '-40px');
            $('div.section  div .select_box').css({ 'margin-top': '0px', 'margin-left': '-2px' });
            $('select').css('padding', '6px');
            $('table .custom-checkbox label').css('left', '3px');
        });
    }

    if (mybrowser.indexOf('Safari') > 0) { }

});

function showError(str, delay) {
    if (delay) {
        $('#alertMessage').removeClass('success info warning').addClass('error').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500, function () {
            $(this).delay(delay).animate({ opacity: 0, right: '-20' }, 500, function () { $(this).hide(); });
        });
        return false;
    }
    $('#alertMessage').addClass('error').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500);
}
function showSuccess(str, delay) {
    if (delay) {
        $('#alertMessage').removeClass('error info warning').addClass('success').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500, function () {
            $(this).delay(delay).animate({ opacity: 0, right: '-20' }, 500, function () { $(this).hide(); });
        });
        return false;
    }
    $('#alertMessage').addClass('success').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500);
}
function showWarning(str, delay) {
    if (delay) {
        $('#alertMessage').removeClass('error success  info').addClass('warning').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500, function () {
            $(this).delay(delay).animate({ opacity: 0, right: '-20' }, 500, function () { $(this).hide(); });
        });
        return false;
    }
    $('#alertMessage').addClass('warning').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500);
}
function showInfo(str, delay) {
    if (delay) {
        $('#alertMessage').removeClass('error success  warning').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500, function () {
            $(this).delay(delay).animate({ opacity: 0, right: '-20' }, 500, function () { $(this).hide(); });
        });
        return false;
    }
    $('#alertMessage').html(str).stop(true, true).show().animate({ opacity: 1, right: '10' }, 500);
}

function loading(name, overlay) {
    $('body').append('<div id="overlay"></div><div id="preloader">' + name + '..</div>');
    if (overlay == 1) {
        $('#overlay').css('opacity', 0.4).fadeIn(400, function () { $('#preloader').fadeIn(400); });
        return false;
    }
    $('#preloader').fadeIn();
}

function unloading() {
    $('#preloader').fadeOut(400, function () { $('#overlay').fadeOut(); }).remove();
}

function LResize() {

    scrollmenu();
    $("#shadowhead").show();
    $('body').addClass('nobg');
    $('#content').css({ marginLeft: "70px" });
    $('#main_menu').removeClass('main_menu').addClass('iconmenu');
    $('#main_menu li').each(function () {
        var title = $(this).find('b').text();
        $(this).find('a').attr('title', title);
    });
    $('#main_menu li a').find('b').hide();
    $('#main_menu li ').find('ul').hide();

    $('.iconmenu').css("background-color", "#1f1f1f");
    $('.iconmenu').css("top", "0px");
    $('.iconmenu').css("position", "absolute");
    $('.iconmenu').css("float", "left");
    $('.iconmenu').css("z-index", "48");

    $('body.dashborad').css('background-image', 'url("' + $("#smallbg").attr("data-smallbg") + '")');
    $('body.dashborad').css("background-repeat", "repeat-y");    
}

function scrollmenu() {
    if ($(window).scrollTop() >= 1) {
        $("#header ").css("z-index", "50");
    } else {
        $("#header ").css("z-index", "47");
    }
}
		
	 