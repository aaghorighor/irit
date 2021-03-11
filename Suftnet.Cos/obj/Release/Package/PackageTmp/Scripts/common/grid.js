
suftnet_grid = {

    currency: "",
    Url: "",
    iconUrl: '\/Content/zice-OneChurch/images/icon/color_18/',

    formatCurrency : function(value)
    {
        return  suftnet_grid.currency + '' + CommaFormatted(CurrencyFormatted(value));
    },
    
    refreshSettings: function (obj) {

        $('#tdlookups').dataTable().fnClearTable();

        $.each(obj, function (i, value) {
            var rowPos = $('#tdlookups').dataTable().fnAddData([value.Id, value.Title, value.Class, value.Active, "<a><span style=\"margin:10px;\" id=\"GroupLink\" GroupId=" + value.Id + " title=\"View group settings \"><img src=" + suftnet_grid.iconUrl + "list.png\ alt=\"\" /></span></a><a><span style=\"margin:10px;\" id=\"EditLink\" Editid=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a><span style=\"margin:10px;\" id=\"Delete\" Deleteid=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
            var tableRowElement = $('#tdlookups').dataTable().fnGetNodes(rowPos[0]);
            $(tableRowElement).attr('id', value.Id);
            $(tableRowElement).children('td').eq(0).attr('align', 'left');
            $(tableRowElement).children('td').eq(1).attr('align', 'left');
            $(tableRowElement).children('td').eq(2).attr('align', 'left');
            $(tableRowElement).children('td').eq(3).attr('align', 'center');
        });
    },

    //// add new settings
    addSettings: function (value) {
        var rowPos = $('#tdlookups').dataTable().fnAddData([value.Id, value.Title, value.Class, value.Active, "<a><span style=\"margin:10px;\" id=\"GroupLink\" GroupId=" + value.Id + " title=\"View group settings\"><img src=" + suftnet_grid.iconUrl + "list.png\ alt=\"\" /></span></a><a><span style=\"margin:10px;\" id=\"EditLink\" Editid=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a><span style=\"margin:10px;\" id=\"Delete\" Deleteid=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdlookups').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'center');
    },

    //// Update Setttings
    updateSettings: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.Title);
        $tableRowElement.children('td').eq(2).text(obj.Class);
        $tableRowElement.children('td').eq(3).text(obj.Active);
    },

    //// Refresh Common
    refreshCommon: function (obj) {
        $('#tdcommon').dataTable().fnClearTable();
        $.each(obj, function (i, value) {
            suftnet_grid.addCommon(value);
        });
    },

    //// Add Common
    addCommon: function (value) {
        var rowPos = $('#tdcommon').dataTable().fnAddData([value.Id, value.Indexno, value.Title, value.Active, "<a><span style=\"margin:15px;\" id=\"EditCommonLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a><span id=\"DeleteCommon\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdcommon').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'center');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'center');
    },

    //// Update Common
    updateCommon: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.Indexno);
        $tableRowElement.children('td').eq(2).text(obj.Title);
        $tableRowElement.children('td').eq(3).text(obj.Active);      
    },

    //// Refresh Common
    refreshCommonExtension: function (obj) {
        $('#tdcommon').dataTable().fnClearTable();
        $.each(obj, function (i, value) {
            suftnet_grid.addCommonExtension(value)
        });
    },

    //// Add Common
    addCommonExtension: function (value) {
        var rowPos = $('#tdcommon').dataTable().fnAddData([value.Id, value.Indexno, value.Title, value.code, value.Active, "<a><span id=\"EditCommonLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a><span style=\"margin:15px;\" id=\"DeleteCommon\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdcommon').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Update Common
    updateCommonExtension: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.Indexno);
        $tableRowElement.children('td').eq(2).text(obj.Title);
        $tableRowElement.children('td').eq(3).text(obj.code);
        $tableRowElement.children('td').eq(4).text(obj.Active);
    },

   
    //// Add Editor content
    addEditor: function (value) {
        var rowPos = $('#tdeditor').dataTable().fnAddData([value.Id, value.CreatedOn, value.Title, value.ContentType, value.Active, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete this row\" /></span></a>"], true);
        var tableRowElement = $('#tdeditor').dataTable().fnGetNodes(rowPos[0]); 
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Update Editor
    updateEditor: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.Title);
        $tableRowElement.children('td').eq(3).text(obj.ContentType);
        $tableRowElement.children('td').eq(4).text(obj.Active);
    },

    //// Add User
    addUser: function (value) {
        var rowPos = $('#tdUserAccount').dataTable().fnAddData([value.FirstName, value.LastName, value.Email, value.Area, value.Active, "<a><span style=\"margin:10px;\" id=\"EditLink\" Editid=" + value.UserId + " title=\"Edit this user\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this user\" /></span><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.UserId + " title=\"Delete this user\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a><a><span style=\"margin:10px;\" id=\"UserRole\" userId=" + value.UserId + " title=\"User role\"><img src=" + suftnet_grid.iconUrl + "pictures_folder.png\ alt=\"add user role\" /></span></a><a><span style=\"margin:10px;\" id=\"ResetPassword\" userId=" + value.UserId + " title=\"Reset Password\"><img src=" + suftnet_grid.iconUrl + "user.png\ alt=\"Reset user password\" /></span></a>"], true);
        var tableRowElement = $('#tdUserAccount').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just 
        $(tableRowElement).attr('id', value.UserId);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Update User
    updateUser: function (obj) {
        var $tableRowElement = $("#" + obj.UserId);
        $tableRowElement.children('td').eq(0).text(obj.FirstName);
        $tableRowElement.children('td').eq(1).text(obj.LastName);
        $tableRowElement.children('td').eq(2).text(obj.Email);
        $tableRowElement.children('td').eq(3).text(obj.Area);
        $tableRowElement.children('td').eq(4).text(obj.Active);
    },

    addCustomer: function (value) {
        var rowPos = $('#tdUserAccount').dataTable().fnAddData([value.UserId, value.FirstName, value.LastName, value.Email, value.Area, value.Active, "<a href=\"javascript:void(0)\"><span style=\"margin:5px;\" id=\"EditLink\" Editid=" + value.UserId + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span><\a>  <a href=\"javascript:void(0)\"><span style=\"margin:5px;\" id=\"Delete\" DeleteId=" + value.UserId + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span><\a><a href=\"javascript:void(0)\"><span id=\"UserRole\" style=\"margin:5px;\" userId=" + value.UserId + " title=\"View User permission\"><img src=" + suftnet_grid.iconUrl + "pictures_folder.png\ alt=\"Map user role\" /></span><span id=\"ResetPassword\" style=\"margin:5px;\" userId=" + value.UserId + " title=\"Reset User password\"><img src=" + suftnet_grid.iconUrl + "user.png\ alt=\"Reset user password\" /></span><\a>"], true);
        var tableRowElement = $('#tdUserAccount').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just 
        $(tableRowElement).attr('id', value.UserId);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');      
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Update User
    updateCustomer: function (obj) {
        var $tableRowElement = $("#" + obj.UserId);
        $tableRowElement.children('td').eq(1).text(obj.FirstName);
        $tableRowElement.children('td').eq(2).text(obj.LastName);
        $tableRowElement.children('td').eq(3).text(obj.Email);
        $tableRowElement.children('td').eq(4).text(obj.Area);
        $tableRowElement.children('td').eq(5).text(obj.Active);
    },  
   
    //// Add Venue Extension
    addVenueExtension: function (value) {

        var rowPos = $('#tdVenue').dataTable().fnAddData([value.Company, value.FirstName, value.LastName, value.Email, value.Phone, "</span><span id=\"customerFinder\" customerId=" + value.Id + " title=\"Select this Customer\"><img src=" + suftnet_grid.iconUrl + "pen.png\ alt=\"\" /></span>"], true);
        var tableRowElement = $('#tdVenue').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just added
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Add Venue
    addVenue: function (value) {

        var rowPos = $('#tdVenue').dataTable().fnAddData([value.Id, value.Company, value.Email, value.Phone, value.FullAddress, value.Active, "<a href=\"javascript:void(0)\"><span style=\"margin:15px;\" id=\"EditLink\" Editid=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span id=\"Delete\" Deleteid=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdVenue').dataTable().fnGetNodes(rowPos[0]); 
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
      
    },

    //// Update Venue
    updateVenue: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.Company);
        $tableRowElement.children('td').eq(2).text(obj.Email);
        $tableRowElement.children('td').eq(3).text(obj.Phone);
        $tableRowElement.children('td').eq(4).text(obj.FullAddress);
        $tableRowElement.children('td').eq(5).text(obj.Active);
    },

    //// Add Event
    addEvent: function (value) {

        var rowPos = $('#tdEvent').dataTable().fnAddData([value.Title, value.StartDt, value.EndDt, value.Status, "<span style=\"margin:5px;\" id=\"EventDetailsLink\" eventId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "list.png\ alt=\"Edit Content\" /></span><span style=\"margin:5px;\" id=\"EditLink\" Editid=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span><span id=\"Delete\" Deleteid=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span>"], true);
        var tableRowElement = $('#tdEvent').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just added
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
    },

    //// Update Event
    updateEvent: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Title);
        $tableRowElement.children('td').eq(1).text(obj.StartDt);
        $tableRowElement.children('td').eq(2).text(obj.EndDt);
        $tableRowElement.children('td').eq(3).text(obj.Status);
    },

    //// refresh  Event Register
    refreshEventRegister: function (objrow) {

        $('#tdEventRegister').dataTable().fnClearTable();

        $.each(objrow, function (i, value) {

            suftnet_grid.addEventRegister(value);
        });
    },

    //// Add Event Register
    addEventRegister: function (value) {

        var rowPos = $('#tdEventRegister').dataTable().fnAddData([value.Id, value.FirstName, value.LastName, value.Email, value.Mobile, "<a><span id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdEventRegister').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just added
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },  

  //// Add Asset
    addAsset: function (value) {
        var rowPos = $('#tdAsset').dataTable().fnAddData([value.CreatedOn, value.Reference, value.AssetType, value.Name, suftnet_grid.formatCurrency(value.Cost), value.Status, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.QueryId + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.QueryId + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdAsset').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just 
        $(tableRowElement).attr('QueryId', value.QueryId);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    //// Update Asset
    updateAsset: function (obj) {

        var $tableRowElement = $("#" + obj.QueryId);
        $tableRowElement.children('td').eq(0).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(1).text(obj.Reference);
        $tableRowElement.children('td').eq(2).text(obj.AssetType);
        $tableRowElement.children('td').eq(3).text(obj.Name);
        $tableRowElement.children('td').eq(4).text(suftnet_grid.formatCurrency(obj.Cost));
        $tableRowElement.children('td').eq(5).text(obj.Status);       
    },

    //// Add Attendance
    addAttendance: function (value) {
        var rowPos = $('#tdAttendance').dataTable().fnAddData([value.Id, value.CreatedOn, value.EventType, value.count, "<a href=\"javascript:void(0)\"><span id=\"GroupLink\" style=\"margin:10px;\" groupId=" + value.Id + " eventType=" + value.EventType + "title=\"View attender\"><img src=" + suftnet_grid.iconUrl + "list.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdAttendance').dataTable().fnGetNodes(rowPos[0]);  
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'center');
    },

    //// Update Attendance
    updateAttendance: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.EventType);       
        $tableRowElement.children('td').eq(3).text(obj.count);     
    },

    //// Add SmallGroup 
    addSmallGroup: function (value) {

        var rowPos = $('#tdsmallGroup').dataTable().fnAddData([value.Id, value.CreatedOn, value.FullNames, value.MinistryType, value.Role, value.Active, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"view\" smallGroupId=" + value.Id + " title=\"Task\"><img src=" + suftnet_grid.iconUrl + "file.png\ alt=\"Task\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a style=\"margin:10px;\" href=\"javascript:void(0)\"><span id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdsmallGroup').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    //// Update SmallGroup 
    updateSmallGroup: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.FullNames);
        $tableRowElement.children('td').eq(3).text(obj.MinistryType);
        $tableRowElement.children('td').eq(4).text(obj.Role);      
        $tableRowElement.children('td').eq(5).text(obj.Active);
    },

        //// Add Income
    addIncome: function (value) {

        var rowPos = $('#tdIncome').dataTable().fnAddData([value.Id, value.CreatedOn, value.Reference, value.MemberReference, value.IncomeType, suftnet_grid.formatCurrency(value.Amount), suftnet_grid.formatCurrency(value.GiftAidAmount), suftnet_grid.formatCurrency(value.Total), "<a href=\"javascript:void(0)\"><span style=\"margin:15px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdIncome').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'left');
        $(tableRowElement).children('td').eq(7).attr('align', 'left');
        $(tableRowElement).children('td').eq(8).attr('align', 'center');
    },

    //// Update Income
    updateIncome: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);    
        $tableRowElement.children('td').eq(2).text(obj.Reference);
        $tableRowElement.children('td').eq(3).text(obj.MemberReference);
        $tableRowElement.children('td').eq(4).text(obj.IncomeType);
        $tableRowElement.children('td').eq(5).text(suftnet_grid.formatCurrency(value.Amount));
        $tableRowElement.children('td').eq(6).text(suftnet_grid.formatCurrency(value.GiftAidAmount));
        $tableRowElement.children('td').eq(7).text(suftnet_grid.formatCurrency(value.Total));
    },   

     //// Add Notification
    addNotification: function (value) {
             
        var rowPos = $('#tdNotification').dataTable().fnAddData([value.Id, value.CreatedOn, value.MessageType, value.Status, "<a href=\"javascript:void(0)\"><span style=\"margin:15px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true); 
        var tableRowElement = $('#tdNotification').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');     
        $(tableRowElement).children('td').eq(4).attr('align', 'center');      
    },

    //// Update Notification
    updateNotification: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);       
        $tableRowElement.children('td').eq(2).text(obj.MessageType);    
        $tableRowElement.children('td').eq(3).text(obj.Status);
    },
 
    //// Add Media
    addMedia: function (value) {    

        var rowPos = $('#tdMedia').dataTable().fnAddData([value.Id, value.CreatedOn, value.MediaType, value.FormatType, value.Title, value.Publish, "<a href=\"javascript:void(0)\"><span style=\"margin:0px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);

        var tableRowElement = $('#tdMedia').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just 
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    //// Update Media
    updateMedia: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.MediaType);
        $tableRowElement.children('td').eq(3).text(obj.FormatType);
        $tableRowElement.children('td').eq(4).text(obj.Title);
        $tableRowElement.children('td').eq(5).text(obj.Publish);
    },

    //// Add Give
    addGive: function (value) {
        var rowPos = $('#tdGive').dataTable().fnAddData([value.CreatedOn, value.GiveType, value.FirstName, value.LastName, value.Email, suftnet_grid.currency + '' + CommaFormatted(CurrencyFormatted(value.Amount)), value.Status, "<a href=\"javascript:void(0)\"><span style=\"margin:15px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdGive').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just 
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'left');       
    },

    //// Update Give
    updateGive: function (obj) {
        var $tableRowElement = $("#" + obj.Id);       
        $tableRowElement.children('td').eq(0).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(1).text(obj.GiveType);
        $tableRowElement.children('td').eq(2).text(obj.FirstName);
        $tableRowElement.children('td').eq(3).text(obj.LastName);
        $tableRowElement.children('td').eq(4).text(obj.Email);
        $tableRowElement.children('td').eq(5).text(suftnet_grid.currency + '' + CommaFormatted(CurrencyFormatted(obj.Amount)));
        $tableRowElement.children('td').eq(6).text(obj.Status);
    },

    //// Add Article
    addArticle: function (value) {
        var rowPos = $('#tdArticle').dataTable().fnAddData([value.Id, value.CreatedOn, value.Author, value.Title, value.Active, "<a href=\"javascript:void(0)\"><span  id=\"Edit\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdArticle').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Update Article
    updateArticle: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.Author);
        $tableRowElement.children('td').eq(3).text(obj.Title);
        $tableRowElement.children('td').eq(4).text(obj.Active);
    },

    //// Add Devotion
    addDevotion: function (value) {
        var rowPos = $('#tdDevotion').dataTable().fnAddData([value.Id, value.CreatedOn, value.Author, value.Title, value.Active, "<a href=\"javascript:void(0)\"><span  id=\"Edit\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdDevotion').dataTable().fnGetNodes(rowPos[0]); 
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');     
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Update Devotion
    updateDevotion: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.Author);
        $tableRowElement.children('td').eq(3).text(obj.Title);
        $tableRowElement.children('td').eq(4).text(obj.Active);
    },

    //// Add Prayer
    addPrayer: function (value) {
        var rowPos = $('#tdPrayer').dataTable().fnAddData([value.Id, value.CreatedOn, value.PrayerType, value.Title, value.Active, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\"  id=\"View\" viewId=" + value.Id + " name=" + value.QueryString + " title=\"View this row\"><img src=" + suftnet_grid.iconUrl + "folder.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span  id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdPrayer').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just 
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');

    },   
    
    //// Update Prayer
    updatePrayer: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.PrayerType);
        $tableRowElement.children('td').eq(3).text(obj.Title);
        $tableRowElement.children('td').eq(4).text(obj.Active != null ? "true" : "false");
    },

    //// Refresh PrayerPoint
    refreshPrayerPoint: function (obj) {
        $('#tdPrayerPoint').dataTable().fnClearTable();
        $.each(obj, function (i, value) {
            suftnet_grid.addPrayerPoint(value)
        });
    },

    //// Add PrayerPoint
    addPrayerPoint: function (value) {
        var rowPos = $('#tdPrayerPoint').dataTable().fnAddData([value.Id, value.SequencyId, value.Description, value.Active, "<a><span id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a><span style=\"margin:15px;\" Id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdPrayerPoint').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');       
        $(tableRowElement).children('td').eq(4).attr('align', 'center');
    },

    //// Update PrayerPoint
    updatePrayerPoint: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.SequencyId);            
        $tableRowElement.children('td').eq(2).text(obj.Description);      
        $tableRowElement.children('td').eq(3).text(obj.Active);
    },
  
    //// Add Ticket Extension
    addTicketExtension: function (value) {

        var rowPos = $('#tdTicket').dataTable().fnAddData([value.Id, value.CreatedOn, value.Status, value.Priority, value.Subject, value.Department, "<a href=\"javascript:void(0)\"><span style=\"margin:1px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:1px;\" id=\"TicketThreadLink\" TicketId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "file.png\ alt=\"Show resolutions\" /></span></a>"], true);
        var tableRowElement = $('#tdTicket').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    //// Add Ticket
    addTicket: function (value) {

        var rowPos = $('#tdTicket').dataTable().fnAddData([value.Id, value.CreatedOn, value.Status, value.Priority, value.Subject, value.Department, "<a href=\"javascript:void(0)\"><span style=\"margin:1px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"TicketThreadLink\" TicketId=" + value.Id + " title=\"View this ticket\"><img src=" + suftnet_grid.iconUrl + "file.png\ alt=\"View this ticket\" /></span></a>"], true);
        var tableRowElement = $('#tdTicket').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    //// Update Ticket
    updateTicket: function (obj) {

        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.Status);
        $tableRowElement.children('td').eq(3).text(obj.Priority);
        $tableRowElement.children('td').eq(4).text(obj.Subject);
        $tableRowElement.children('td').eq(5).text(obj.Department);
    },

    //// Add Ticket Thread Extension
    addFrontOfficeTicketThreadExtension: function (value) {

        var rowPos = $('#tdTicketThread').dataTable().fnAddData([value.Id, value.CreatedOn, value.Comment], true);
        var tableRowElement = $('#tdTicketThread').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');       
    },

    refreshTicketThread: function (obj)
    {
        $('#tdTicketThread').dataTable().fnClearTable();

        $.each(obj, function (i, value) {
            suftnet_grid.addTicketThread(value);
        });
    },

    refreshFrontOfficeTicketThread: function (obj) {
        $('#tdTicketThread').dataTable().fnClearTable();

        $.each(obj, function (i, value) {
            suftnet_grid.addFrontOfficeTicketThreadExtension(value);
        });
    },

    //// Add Ticket
    addTicketThread: function (value) {

        var rowPos = $('#tdTicketThread').dataTable().fnAddData([value.Id, value.CreatedOn, value.CreatedBy, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditTicketThreadLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"DeleteTicketThread\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete this row\" /></span></a>"], true);
        var tableRowElement = $('#tdTicketThread').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');    
        $(tableRowElement).children('td').eq(3).attr('align', 'center');
    },

    //// Update Ticket
    updateTicketThread: function (obj) {

        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);      
    },        

    addTutorial: function (value) {

        var rowPos = $('#tdtutorial').dataTable().fnAddData([value.Id, value.Sequency, value.Action, value.TutorialGroup, value.VideoId, value.Active, "<a href=\"javascript:void(0)\"><span  id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + this.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:15px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + this.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdtutorial').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'center');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    //// Update Tutorial
    updateTutorial: function (obj) {

        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.Sequency);
        $tableRowElement.children('td').eq(2).text(obj.Action);
        $tableRowElement.children('td').eq(3).text(obj.TutorialGroup);
        $tableRowElement.children('td').eq(4).text(obj.VideoId);
        $tableRowElement.children('td').eq(5).text(obj.Active);
    },

    //// Add Case
    CaseAdd: function (value) {
        var rowPos = $('#tdcase').dataTable().fnAddData([value.Id, value.CreatedOn, value.Member, value.CaseType, value.Title, value.Status, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdcase').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just 
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
      
    },

    //// Update Case
    UpdateCase: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.Member);
        $tableRowElement.children('td').eq(3).text(obj.CaseType);
        $tableRowElement.children('td').eq(4).text(obj.Title);    
        $tableRowElement.children('td').eq(5).text(obj.Status);          
    },   
  

    //// Add Service Time
    AddServiceTime: function (value) {
        var rowPos = $('#tdServiceTime').dataTable().fnAddData([value.Id, value.Index, value.ServiceType, value.StartTime, value.EndTime, value.Status, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"View\" viewId=" + value.Id + " name=" + value.QueryString + " title=\"View this row\"><img src=" + suftnet_grid.iconUrl + "file.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdServiceTime').dataTable().fnGetNodes(rowPos[0]); 
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');       
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    //// Update Service Time
    UpdateServiceTime: function (obj) {
       
        var $tableRowElement = $("#" + obj.Id);      
        $tableRowElement.children('td').eq(1).text(obj.Index);
        $tableRowElement.children('td').eq(2).text(obj.ServiceType);     
        $tableRowElement.children('td').eq(3).text(obj.StartTime);
        $tableRowElement.children('td').eq(4).text(obj.EndTime);
        $tableRowElement.children('td').eq(5).text(obj.Status);
    },

    //// Add Service Time
    AddServiceTimeLine: function (value) {
        var rowPos = $('#tdServiceTimeLine').dataTable().fnAddData([value.Id, value.ServiceType, value.StartTime, value.EndTime, value.FullName, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdServiceTimeLine').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');         
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Update Service Time
    UpdateServiceTimeLine: function (obj) {

        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.ServiceType);
        $tableRowElement.children('td').eq(2).text(obj.StartTime);
        $tableRowElement.children('td').eq(3).text(obj.EndTime);
        $tableRowElement.children('td').eq(4).text(obj.FullName);       
    },

    //// Add Common Extension
    extendAddCommon: function (value) {

        var rowPos = $('#tdcommon').dataTable().fnAddData([value.Id, value.Indexno, value.Title, value.Active, "<a><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);

        var tableRowElement = $('#tdcommon').dataTable().fnGetNodes(rowPos[0]);

        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'center');
    },

    //// Update Common Extension
    extendUpdateCommon: function (obj) {

        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.Indexno);
        $tableRowElement.children('td').eq(2).text(obj.Title);
        $tableRowElement.children('td').eq(3).text(obj.Active);
    },

    //// Add Bible Verse
    addBibleVerse: function (value) {
        var rowPos = $('#tdBibleVerse').dataTable().fnAddData([value.Id, value.CreatedOn, value.Verse, value.Description, value.Active, "<a><span id=\"EditLink\" editid=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a><span style=\"margin:15px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdBibleVerse').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Update Bible Verse
    updateBibleVerse: function (obj) {
        var $tableRowElement = $("#" + obj.Id);      
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.Verse);
        $tableRowElement.children('td').eq(3).text(obj.Description);      
        $tableRowElement.children('td').eq(4).text(obj.Active);
    },
    
    //// Add Plan
    addPlan: function (value) {

        var rowPos = $('#tdPlan').dataTable().fnAddData([value.Id, value.Product, suftnet_grid.currency + '' + CommaFormatted(CurrencyFormatted(value.BasicPrice)), suftnet_grid.currency + '' + CommaFormatted(CurrencyFormatted(value.AdvancePrice)), suftnet_grid.currency + '' + CommaFormatted(CurrencyFormatted(value.ProfessionalPrice)), "<span style=\"margin:10px;\"><a href=\"javascript:void(0)\" id=\"PlanFeatureLink\" planId=" + value.Id + " title=\"Plan features\"><img src=" + suftnet_grid.iconUrl + "stats_lines.png\ alt=\"\" /><\a></span><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete this row\" /></span></a>"], true);
        var tableRowElement = $('#tdPlan').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'center');      

    },

    //// Update Plan
    updatePlan: function (obj) {

        var $tableRowElement = $("#" + obj.Id);     
        $tableRowElement.children('td').eq(1).text(suftnet_grid.currency + '' + CommaFormatted(CurrencyFormatted(obj.BasicPrice)));
        $tableRowElement.children('td').eq(2).text(suftnet_grid.currency + '' + CommaFormatted(CurrencyFormatted(obj.AdvancePrice)));
        $tableRowElement.children('td').eq(3).text(suftnet_grid.currency + '' + CommaFormatted(CurrencyFormatted(obj.ProfessionalPrice)));
    },

    //// Refresh Plan Feature
    refreshPlanFeature: function (obj) {

        $('#tdPlanFeature').dataTable().fnClearTable();
        $.each(obj, function (i, value) {

            suftnet_grid.addPlanFeature(value);

        });
    },

    //// Add Plan Feature
    addPlanFeature: function (value) {

        var rowPos = $('#tdPlanFeature').dataTable().fnAddData([value.Id, value.IndexNo, value.Feature, value.Basic, value.Advance, value.Professional, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditPlanFeatureLink\" EditId=" + value.Id + " title=\"Edit this content\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"DeletePlanFeature\" DeleteId=" + value.Id + " title=\"Delete this content\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete this Content\" /></span></a>"], true);
        var tableRowElement = $('#tdPlanFeature').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    //// Update Plan Feature
    updatePlanFeature: function (obj) {

        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.IndexNo);
        $tableRowElement.children('td').eq(2).text(obj.Feature);
        $tableRowElement.children('td').eq(3).text(obj.Basic);
        $tableRowElement.children('td').eq(4).text(obj.Advance);
        $tableRowElement.children('td').eq(5).text(obj.Professional);
    },
    //// refresh  Event Register
    refreshEventParticipant: function (objrow) {

        $('#tdParticipant').dataTable().fnClearTable();

        if (objrow != null)
        {
            $.each(objrow, function (i, value) {
                suftnet_grid.addEventParticipant(value);
            });
        }       
    },

    clearEventParticipant: function () {

        $('#tdParticipant').dataTable().fnClearTable();

    },

    //// Add Event Participant
    addEventParticipant: function (value) {

        var rowPos = $('#tdParticipant').dataTable().fnAddData([value.Id, value.FirstName, value.LastName, value.Email, value.Phone, "<a><span id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdParticipant').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Add Event Volunteer
    addEventMinister: function (value) {

        var rowPos = $('#__tdMinister').dataTable().fnAddData([value.Title, value.FirstName, value.LastName, value.Phone, value.RoleType, "<a><span id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#__tdMinister').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    refreshEventMinister: function (objrow) {

        $('#__tdMinister').dataTable().fnClearTable();

        if (objrow != null) {

            $.each(objrow, function (i, value) {
                suftnet_grid.addEventMinister(value);
            });
        }
    },
    clearEventMinister: function () {

        $('#__tdMinister').dataTable().fnClearTable();
    },   

    //// Add Permission
    addPermission: function (obj) {

        var Create, Edit, Remove, Get, GetAll, Custom;

        var permission = {
            Disable: 5130,
            Enable: 5129
        };

        if (obj.Create == permission.Enable)
        {
            Create = "YES";
        }
        else
        {
            Create = "NO";
        }

        if (obj.Edit == permission.Enable) {
            Edit = "YES";
        }
        else {
            Edit = "NO";
        }
        if (obj.Remove == permission.Enable) {
            Remove = "YES";
        }
        else {
            Remove = "NO";
        }
        if (obj.Get == permission.Enable) {
            Get = "YES";
        }
        else {
            Get = "NO";
        }
        if (obj.GetAll == permission.Enable) {
            GetAll = "YES";
        }
        else {
            GetAll = "NO";
        }

        var rowPos = $('#tdPermission').dataTable().fnAddData([obj.Id, obj.View, Create, Edit, Remove, Get, GetAll, "<a><span id=\"EditLink\" editid=" + obj.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a><span style=\"margin:15px;\" id=\"Delete\" DeleteId=" + obj.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdPermission').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', obj.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'left');
        $(tableRowElement).children('td').eq(7).attr('align', 'center');
    },

    //// Update Permission
    updatePermission: function (obj) {

        var Create, Edit, Remove, Get, GetAll, Custom;

        var permission = {
            Disable: 5130,
            Enable: 5129
        };     

        if (obj.Create == permission.Enable) {
            Create = "YES";
        }
        else {
            Create = "NO";
        }

        if (obj.Edit == permission.Enable) {
            Edit = "YES";
        }
        else {
            Edit = "NO";
        }
        if (obj.Remove == permission.Enable) {
            Remove = "YES";
        }
        else {
            Remove = "NO";
        }
        if (obj.Get == permission.Enable) {
            Get = "YES";
        }
        else {
            Get = "NO";
        }
        if (obj.GetAll == permission.Enable) {
            GetAll = "YES";
        }
        else {
            GetAll = "NO";
        }
      
        var $tableRowElement = $("#" + obj.Id);     
        $tableRowElement.children('td').eq(1).text(obj.View);
        $tableRowElement.children('td').eq(2).text(Create);
        $tableRowElement.children('td').eq(3).text(Edit);
        $tableRowElement.children('td').eq(4).text(Remove);
        $tableRowElement.children('td').eq(5).text(Get);
        $tableRowElement.children('td').eq(6).text(GetAll);     
    },

    //// Add Schedule
    addSchedule: function (value) {
        var rowPos = $('#tdSchedule').dataTable().fnAddData([value.Id, value.CreatedOn, value.Title, value.Frequency, value.Period, value.StartTime, value.EndTime, value.Venue, value.Active, "<a href=\"javascript:void(0)\"><span  id=\"view\" scheduleId=" + value.Id + " title=\"Send notification\"><img src=" + suftnet_grid.iconUrl + "messenger.png\ alt=\"Send Notification\" /></span></a><a href=\"javascript:void(0)\"><span  style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdSchedule').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just 
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'left');
        $(tableRowElement).children('td').eq(7).attr('align', 'left');
        $(tableRowElement).children('td').eq(8).attr('align', 'left');
        $(tableRowElement).children('td').eq(9).attr('align', 'center');
    },

    //// Update Schedule
    updateSchedule: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.Title);
        $tableRowElement.children('td').eq(3).text(obj.Frequency);
        $tableRowElement.children('td').eq(4).text(obj.Period);
        $tableRowElement.children('td').eq(5).text(obj.StartTime);
        $tableRowElement.children('td').eq(6).text(obj.EndTime);
        $tableRowElement.children('td').eq(7).text(obj.Venue);
        $tableRowElement.children('td').eq(8).text(obj.Active);
    },

    //// Add Slide
    addSlider: function (value) {
        var rowPos = $('#tdSlider').dataTable().fnAddData([value.Id, value.CreatedOn, value.SliderType, value.Title, value.Publish, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdSlider').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('Id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Update Slide
    updateSlider: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.SliderType);
        $tableRowElement.children('td').eq(3).text(obj.Title);
        $tableRowElement.children('td').eq(4).text(obj.Publish);
    },

    //// Add Faq
    addFaq: function (value) {
        var rowPos = $('#tdFaq').dataTable().fnAddData([value.Id, value.SortOrderId, value.Title, value.Publish, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdFaq').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'center');

    },

    //// Update Faq
    updateFaq: function (obj) {
        var $tableRowElement = $("#" + obj.Id);     
        $tableRowElement.children('td').eq(1).text(obj.SortOrderId);
        $tableRowElement.children('td').eq(2).text(obj.Title);
        $tableRowElement.children('td').eq(3).text(obj.Publish);
    },
    //// Add Tour 
    addTour: function (value) {
        var rowPos = $('#tdTour').dataTable().fnAddData([value.Id, value.SortOrder, value.Title, value.StyleType, value.Active, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete this row\" /></span></a>"], true);
        var tableRowElement = $('#tdTour').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just added
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    //// Update Tour
    updateTour: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.SortOrder);
        $tableRowElement.children('td').eq(2).text(obj.Title);
        $tableRowElement.children('td').eq(3).text(obj.StyleType);
        $tableRowElement.children('td').eq(4).text(obj.Active);
    },
    //// Add Family 
    addFamily: function (value) {
       
        var active = "NO";

        if (value.Active == true) {
            active = "YES"
        };

        var rowPos = $('#tdfamily').dataTable().fnAddData([value.Id, value.Title, value.FirstName, value.LastName, value.MaritalStatus, value.Status, "<a href=\"javascript:void(0)\"><span id=\"EditLink\" editid=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" deleteid=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete this row\" /></span></a>"], true);
        var tableRowElement = $('#tdfamily').dataTable().fnGetNodes(rowPos[0]); // get reference to <tr> element just added
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    //// Update Family
    updateFamily: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(0).text(obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.Title);
        $tableRowElement.children('td').eq(2).text(obj.FirstName);
        $tableRowElement.children('td').eq(3).text(obj.LastName);
        $tableRowElement.children('td').eq(4).text(obj.MaritalStatus);
        $tableRowElement.children('td').eq(5).text(obj.Status);
    },
         
    addEventTimeLime: function (value) {

        var rowPos = $('#tdEventTimeLine').dataTable().fnAddData([value.Id, value.StartDt, value.TimeLineType, value.StartTime, value.EndTime, value.FullName, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdEventTimeLine').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    updateEventTimeLine: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.StartDt);
        $tableRowElement.children('td').eq(2).text(obj.TimeLineType);
        $tableRowElement.children('td').eq(3).text(obj.StartTime);
        $tableRowElement.children('td').eq(4).text(obj.EndTime);
        $tableRowElement.children('td').eq(5).text(obj.FullName);
    },

    loadEventTimeLine: function (objrow) {
           
        $('#tdEventTimeLine').dataTable().fnClearTable();
                            
        if (objrow != null) {

            $.each(objrow, function (i, value)
            {             
                suftnet_grid.addEventTimeLime(value)
            });
        }
    },
    clearEventTimeLine: function () {

        $('#tdEventTimeLine').dataTable().fnClearTable();
    },

    addAttendanceMapping: function (value) {

        var rowPos = $('#tdAttendanceMapping').dataTable().fnAddData([value.Id, value.CreatedOn, value.Group, value.Count, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdAttendanceMapping').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');      
        $(tableRowElement).children('td').eq(4).attr('align', 'center');
    },

    addAttender: function (value) {

        var rowPos = $('#tdattender').dataTable().fnAddData([value.Id, value.CreatedOn, value.MemberId, value.TimeIn, value.TimeOut, value.FirstName, value.LastName, value.Status, "<a href=\"javascript:void(0)\"><span id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdattender').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'left');
        $(tableRowElement).children('td').eq(7).attr('align', 'left');
        $(tableRowElement).children('td').eq(8).attr('align', 'center');
    },

    updateAttender: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.MemberId);
        $tableRowElement.children('td').eq(3).text(obj.TimeIn);
        $tableRowElement.children('td').eq(4).text(obj.TimeOut);
        $tableRowElement.children('td').eq(5).text(obj.FirstName);
        $tableRowElement.children('td').eq(6).text(obj.LastName);
        $tableRowElement.children('td').eq(7).text(obj.Status);
    },

    //// Add Mobile Slider
    addMobileSlider: function (value) {
        var rowPos = $('#tdMobileSlider').dataTable().fnAddData([value.Id, value.CreatedOn, value.Title, value.Publish, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdMobileSlider').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('Id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');       
        $(tableRowElement).children('td').eq(4).attr('align', 'center');
    },

    //// Update Mobile Slider
    updateMobileSlider: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.Title);
        $tableRowElement.children('td').eq(3).text(obj.Publish);
    },

    //// Add Gallery
    addGallery: function (value) {
        var rowPos = $('#tdGallery').dataTable().fnAddData([value.Id, value.CreatedOn, value.Album, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdGallery').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('Id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');     
        $(tableRowElement).children('td').eq(3).attr('align', 'center');
    },

    //// Add Album
    addAlbum: function (value) {
        var rowPos = $('#tdAlbum').dataTable().fnAddData([value.Id, value.CreatedOn, value.Title, value.Publish, "<a href =\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"View\" ViewId=" + value.Id + " name=" + value.QueryString +  " title=\"View this row\"><img src=" + suftnet_grid.iconUrl + "folder.png\ alt=\"View this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdAlbum').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('Id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'center');
    },

    //// Update Album
    updateAlbum: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.Title);
        $tableRowElement.children('td').eq(3).text(obj.Publish);
    },
       
    //// Add Permission
    addMobilePermission: function (value) {

        var rowPos = $('#tdMobilePermission').dataTable().fnAddData([value.Id, value.CreatedOn, value.CreatedBy, value.Description, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdMobilePermission').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');     
        $(tableRowElement).children('td').eq(4).attr('align', 'center');

    },

    //// Update Permission
    updateMobilePermission: function (obj) {
        var $tableRowElement = $("#" + obj.Id);    
        $tableRowElement.children('td').eq(3).text(obj.Description);
    },

    loadFellowship: function (objrow) {

        $('#tdFellowship').dataTable().fnClearTable();

        if (objrow != null) {
            $.each(objrow, function (i, value) {
                suftnet_grid.addFellowship(value);
            });
        }
    },

    addFellowship: function (value) {

        var rowPos = $('#tdFellowship').dataTable().fnAddData([value.Id, value.CreatedOn, value.Name, value.Active == true ? "Yes" : "No", "<a href=\"javascript:void(0)\"><span id=\"view\" style=\"margin:10px;\" fellowshipId=" + value.Id + " title=\"View Fellowship Member\"><img src=" + suftnet_grid.iconUrl + "list.png\ alt=\"View Fellowship Member\" /></span></a><a href=\"javascript:void(0)\"><span id=\"viewSchedule\" style=\"margin:10px;\" name =" + value.Name + " fellowshipId=" + value.Id + " title=\"View Fellowship Schedule\"><img src=" + suftnet_grid.iconUrl + "file.png\ alt=\"View Fellowship Schedule\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdFellowship').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'center');
    },

    updateFellowship: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(2).text(obj.Name);
        $tableRowElement.children('td').eq(3).text(obj.Active == true ? "Yes" : "No");
    },
    addFellowshipMapping: function (value) {

        var rowPos = $('#tdFellowshipMapping').dataTable().fnAddData([value.Id, value.CreatedOn, value.FirstName, value.LastName, value.Role, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdFellowshipMapping').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    loadSmallGroup: function (objrow) {

        $('#tdSmallGroup').dataTable().fnClearTable();

        if (objrow != null) {
            $.each(objrow, function (i, value) {
                suftnet_grid.addSmallGroup(value);
            });
        }
    },

    addSmallGroup: function (value) {

        var rowPos = $('#tdSmallGroup').dataTable().fnAddData([value.Id, value.CreatedOn, value.Title, value.Active == true ? "Yes" : "No", "<a href=\"javascript:void(0)\"><span id=\"view\" style=\"margin:10px;\" name=" + value.Title + " smallGroupId=" + value.Id + " title=\"View SmallGroup Member\"><img src=" + suftnet_grid.iconUrl + "list.png\ alt=\"View smallGroup Member\" /></span></a><a href=\"javascript:void(0)\"><span id=\"viewSchedule\" style=\"margin:10px;\" name="+ value.Title + " smallGroupId=" + value.Id + " title=\"View Small Group Schedule\"><img src=" + suftnet_grid.iconUrl + "file.png\ alt=\"View Small Group Schedule\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdSmallGroup').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');     
        $(tableRowElement).children('td').eq(4).attr('align', 'center');
    },
   
    updateSmallGroup: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(2).text(obj.Title);      
        $tableRowElement.children('td').eq(3).text(obj.Active == true ? "Yes" : "No");        
    },
  
    addSmallGroupMapping: function (value) {

        var rowPos = $('#tdSmallGroupMapping').dataTable().fnAddData([value.Id, value.CreatedOn, value.FirstName, value.LastName, value.Role, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdSmallGroupMapping').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'center');
    },

    addSmallGroupSchedule: function (value) {

        var rowPos = $('#tdSmallGroupSchedule').dataTable().fnAddData([value.Id, value.WeekDay, value.Cycle, value.StartTime, value.EndTime, value.Active == true ? "Yes" : "No", "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdSmallGroupSchedule').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');    
        $(tableRowElement).children('td').eq(5).attr('align', 'left');  
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    updateSmallGroupSchedule: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.WeekDay);
        $tableRowElement.children('td').eq(2).text(obj.Cycle);
        $tableRowElement.children('td').eq(3).text(obj.StartTime);
        $tableRowElement.children('td').eq(4).text(obj.EndTime);
        $tableRowElement.children('td').eq(5).text(obj.Active == true ? "Yes" : "No");
    },

    addFellowshipSchedule: function (value) {

        var rowPos = $('#tdFellowshipSchedule').dataTable().fnAddData([value.Id, value.WeekDay, value.Cycle, value.StartTime, value.EndTime, value.Active == true ? "Yes" : "No", "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdFellowshipSchedule').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    updateFellowshipSchedule: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.WeekDay);
        $tableRowElement.children('td').eq(2).text(obj.Cycle);
        $tableRowElement.children('td').eq(3).text(obj.StartTime);
        $tableRowElement.children('td').eq(4).text(obj.EndTime);
        $tableRowElement.children('td').eq(5).text(obj.Active == true ? "Yes" : "No");
    },

    //// Add Chapter
    addChapter: function (value) {
               
        var rowPos = $('#tdChapter').dataTable().fnAddData([value.Id, value.CreatedOn, value.CreatedBy, value.Section, value.SubSection, value.Publish, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"View\" ViewId=" + value.Id + " name=" + value.QueryString + " title=\"View this row\"><img src=" + suftnet_grid.iconUrl + "folder.png\ alt=\"View this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Edit\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdChapter').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'center');
    },

    //// Update Chapter
    updateChapter: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(3).text(obj.Section);
        $tableRowElement.children('td').eq(4).text(obj.SubSection);
        $tableRowElement.children('td').eq(5).text(obj.Publish);
    },

    //// Add Topic
    addTopic: function (value) {

        var rowPos = $('#tdTopic').dataTable().fnAddData([value.Id, value.IndexNo, value.CreatedOn, value.CreatedBy, value.Topic, value.Publish, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"View\" ViewId=" + value.Id + " name=" + value.QueryString + " title=\"View this row\"><img src=" + suftnet_grid.iconUrl + "folder.png\ alt=\"View this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Edit\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdTopic').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');      
        $(tableRowElement).children('td').eq(5).attr('align', 'left');        
        $(tableRowElement).children('td').eq(6).attr('align', 'center');

    },

    //// Update Topic
    updateTopic: function (obj) {
        var $tableRowElement = $("#" + obj.Id);  
        $tableRowElement.children('td').eq(1).text(obj.IndexNo);
        $tableRowElement.children('td').eq(4).text(obj.Topic);
        $tableRowElement.children('td').eq(5).text(obj.Publish);      
    },

    //// Add Sub Topic
    addSubTopic: function (value) {

        var rowPos = $('#tdSubTopic').dataTable().fnAddData([value.Id, value.IndexNo, value.CreatedOn, value.CreatedBy, value.Title, "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Edit\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdSubTopic').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');      
        $(tableRowElement).children('td').eq(5).attr('align', 'center');

    },

    //// Update Sub Topic
    updateSubTopic: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.IndexNo);
        $tableRowElement.children('td').eq(4).text(obj.Title);      
    },
 
    //// Add Pledge
    addPlege: function (value) {
        
        var rowPos = $('#tdpledger').dataTable().fnAddData([value.Id, value.CreatedOn, value.FirstName, value.LastName, value.Email, value.Mobile, suftnet_grid.formatCurrency(value.Amount), "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Edit\" EditId=" + value.Id + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.Id + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>"], true);
        var tableRowElement = $('#tdpledger').dataTable().fnGetNodes(rowPos[0]);
        $(tableRowElement).attr('id', value.Id);
        $(tableRowElement).children('td').eq(0).attr('align', 'left');
        $(tableRowElement).children('td').eq(1).attr('align', 'left');
        $(tableRowElement).children('td').eq(2).attr('align', 'left');
        $(tableRowElement).children('td').eq(3).attr('align', 'left');
        $(tableRowElement).children('td').eq(4).attr('align', 'left');
        $(tableRowElement).children('td').eq(5).attr('align', 'left');
        $(tableRowElement).children('td').eq(6).attr('align', 'left');
        $(tableRowElement).children('td').eq(7).attr('align', 'center');

    },

    //// Update Pledge
    updatePledge: function (obj) {
        var $tableRowElement = $("#" + obj.Id);
        $tableRowElement.children('td').eq(1).text(obj.CreatedOn);
        $tableRowElement.children('td').eq(2).text(obj.FirstName);
        $tableRowElement.children('td').eq(3).text(obj.LastName);
        $tableRowElement.children('td').eq(4).text(obj.Email);
        $tableRowElement.children('td').eq(5).text(obj.Mobile);
        $tableRowElement.children('td').eq(6).text(suftnet_grid.formatCurrency(obj.Amount));       
    }
};
