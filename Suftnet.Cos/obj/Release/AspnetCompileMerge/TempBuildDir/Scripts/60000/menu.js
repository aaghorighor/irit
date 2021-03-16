
var menu = {

    init: function () {
        $(document).ready(function () {
          
            setTimeout(function () {
                menu.pageEvent();
            }, 0);
        
        });
    },

    pageEvent : function()
    {
        $('#main_menu').removeClass('main_menu').addClass('iconmenu');
        $('#main_menu li a').find('b').hide();
        
        $(document).on("click", "#toggle_menu", function () {

            var margin = $('#toggle_menu').css("margin-left");          
           
            if (margin == "170px") {

                $('body').addClass('nobg');
                $('#content').css({ marginLeft: "70px" });
                $('#toggle_menu').css({ marginLeft: "0px" });
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
            else if (margin == "0px") {

                $('#toggle_menu').css({ marginLeft: "170px" });
                $('body').removeClass('nobg').addClass('dashborad');
                $('#content').css({ marginLeft: "240px" });
                $('#main_menu').removeClass('iconmenu ').addClass('main_menu');
                $('#main_menu li a').find('b').show();

                $('body.dashborad').css('background-image', 'url("' + $("#bigbg").attr("data-bigbg") + '")');
                $('body.dashborad').css("background-repeat", "repeat-y");
            }

        });

        $(document).on("click", ".limenu", function () {

            var margin = $('#toggle_menu').css("margin-left");

            //$('body').addClass('nobg');
            //$('#content').css({ marginLeft: "70px" });
            //$('#toggle_menu').css({ marginLeft: "0px" });
            //$('#main_menu').removeClass('main_menu').addClass('iconmenu');
            //$('#main_menu li').each(function () {
            //    var title = $(this).find('b').text();
            //    $(this).find('a').attr('title', title);
            //});

            //$('#main_menu li a').find('b').hide();
            //$('#main_menu li ').find('ul').hide();

            //$('.iconmenu').css("background-color", "#1f1f1f");
            //$('.iconmenu').css("top", "0px");
            //$('.iconmenu').css("position", "absolute");
            //$('.iconmenu').css("float", "left");
            //$('.iconmenu').css("z-index", "48");

            //$('body.dashborad').css('background-image', 'url("' + $("#smallbg").attr("data-smallbg") + '")');
            //$('body.dashborad').css("background-repeat", "repeat-y");

        });
    }
}