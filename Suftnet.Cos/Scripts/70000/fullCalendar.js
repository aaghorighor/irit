var calendar;
var fullCalendar = function ()
{
      var calendarEl = document.getElementById('calendar');

      calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: [ 'interaction', 'dayGrid', 'timeGrid' ],
        header: {
            left: 'prev,next today',
            center: 'title',
            right: ''
        },   
        navLinks: true, 
        selectable: true,
        selectMirror: true,
        eventLimit: true, 
        views: {
            timeGrid: {
                eventLimit: 4
            }
        },
        select: function (arg) {
         
            switch ($("#dateTimeFormat").attr("data-dateTimeFormat"))
            {
                case "dd/mm/yy":

                    $("#__StartDate").val($.fullCalendar.formatDate(arg.start, "dd-MM-yyyy"));
                    $("#__EndDate").val($.fullCalendar.formatDate(arg.end, "dd-MM-yyyy"));

                    break;

                case "mm/dd/yy":

                    $("#__StartDate").val($.fullCalendar.formatDate(arg.start, "MM-dd-yyyy"));
                    $("#__EndDate").val($.fullCalendar.formatDate(arg.end, "MM-dd-yyyy"));

                    break;
            }                 

            calendar.unselect();

            window.location.href = $("#createUrl").attr("data-createUrl") + "/" + $("#__StartDate").val() + "/00" + "/" + $("#__EndDate").val();
        },
        eventClick: function(arg) {

            window.location.href = $("#editUrl").attr("data-editUrl") + "/" + Suftnet_Utility.seoUrl(arg.event.title) + "/" + arg.event.id;         
        },
        editable: true,
        events: function (info, successCallback, failureCallback) {

            var params = {
                StartDate: info.startStr,
                EndDate: info.endStr              
            };
        
            js.ajaxGet($('#getAllUrl').attr('data-getAllUrl'), params).then(
            function (data) {
                        
                successCallback(
                    data.dataobject.map(function (msg)
                    {
                        return {
                            id: msg.QueryId,
                            title: msg.Title,
                            start: msg.StartDt,
                            end: msg.EndDt,                        
                            allDay: false,
                            backgroundColor: msg.StatusId == 3069 ? '#000000' : '#0077b3',
                            borderColor: msg.StatusId == 3069 ? '#f2f2f2' : '#0077b3' ,
                            textColor: msg.StatusId == 3069 ? '#ffffff' : '#ffffff'
                        };
                
                    }));

            }).catch(function (err)
            {
                failureCallback(err);
            });          

        }}),    

    calendar.render();   
}