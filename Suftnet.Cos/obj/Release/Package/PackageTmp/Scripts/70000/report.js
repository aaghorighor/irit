
var report = {

    init : function()
    {     
        setTimeout(function () {
            $("#report").show();
        }, 0);

        $("#MemberSection").hide();
        $("#DateSection").hide();
        $("#EventSection").hide();
        $("#IncomeSection").hide();
        $("#ExpenseSection").hide();
        $("#AssetSection").hide();
        
        //// COOKIE SECTION
        $("#ReportTypeId").val($.cookie("ReportTypeId"));

        $("#GenderId").val($.cookie("GenderId"));
        $("#MaritalStatusId").val($.cookie("MaritalStatusId"));
        $("#MemberTypeId").val($.cookie("MemberTypeId"));
        $("#ProfessionId").val($.cookie("ProfessionId"));

        $("#EventTypeId").val($.cookie("EventTypeId"));
        $("#StatusId").val($.cookie("StatusId"));

        $("#RoleId").val($.cookie("RoleId"));

        $("#IncomeTypeId").val($.cookie("IncomeTypeId"));
        $("#ExpenseTypeId").val($.cookie("ExpenseTypeId"));

        $("#AssetTypeId").val($.cookie("AssetTypeId"));

        $("#StartDate").val($.cookie("StartDate"));
        $("#FinishDate").val($.cookie("FinishDate"));

        $("#BaptizeId").val($.cookie("BaptizeId"));
       
        //// 

        $("#StartDate").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        $("#FinishDate").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        $("#ReportTypeId").val($.cookie("ReportTypeId"));

        $("#GenderId").val($.cookie("GenderId"));
        $("#MaritalStatusId").val($.cookie("MaritalStatusId"));
        $("#GroupId").val($.cookie("GroupId"));
        $("#ProfessionId").val($.cookie("ProfessionId"));

        $("#EventTypeId").val($.cookie("EventTypeId"));
        $("#StatusId").val($.cookie("StatusId"));

        $("#RoleId").val($.cookie("RoleId"));

        $("#IncomeTypeId").val($.cookie("IncomeTypeId"));
        $("#ExpenseTypeId").val($.cookie("ExpenseTypeId"));

        $("#AssetTypeId").val($.cookie("AssetTypeId"));

        $("#StartDate").val($.cookie("StartDate"));
        $("#FinishDate").val($.cookie("FinishDate"));

        $("#BaptizeId").val($.cookie("BaptizeId"));
             
        $("#btnSubmit").click(function () {

            $.cookie("ReportTypeId", $("#ReportTypeId").val(), { expires: 7, path: "/" });

            $.cookie("StartDate", $("#StartDate").val(), { expires: 7, path: "/" });
            $.cookie("FinishDate", $("#FinishDate").val(), { expires: 7, path: "/" });

            $.cookie("GenderId", $("#GenderId").val(), { expires: 7, path: "/" });
            $.cookie("MaritalStatusId", $("#MaritalStatusId").val(), { expires: 7, path: "/" });
            $.cookie("GroupId", $("#GroupId").val(), { expires: 7, path: "/" });
            $.cookie("ProfessionId", $("#ProfessionId").val(), { expires: 7, path: "/" });

            $.cookie("VenueId", $("#VenueId").val(), { expires: 7, path: "/" });
            $.cookie("StatusId", $("#StatusId").val(), { expires: 7, path: "/" });
                     
            $.cookie("RoleId", $("#RoleId").val(), { expires: 7, path: "/" });

            $.cookie("IncomeTypeId", $("#IncomeTypeId").val(), { expires: 7, path: "/" });
            $.cookie("ExpenseTypeId", $("#ExpenseTypeId").val(), { expires: 7, path: "/" });

            $.cookie("AssetTypeId", $("#AssetTypeId").val(), { expires: 7, path: "/" });
            $.cookie("BaptizeId", $("#BaptizeId").val(), { expires: 7, path: "/" });
                       
            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#GiftAid').is(':checked')) {
                $("#GiftAid").val(true);
            } else {
                $("#GiftAid").val(false);
            }

            $("#form").append($('<input type="hidden" id="CurrencySymbol" name="CurrencySymbol" />').val($("#currencySymbol").text()));
            $("#form").submit();

        });

        $("#ReportTypeId").change(function () {

            CurrentReport($("#ReportTypeId").val());
        });

        if ($("#ReportTypeId").val() > 0) {

            CurrentReport($("#ReportTypeId").val());
        }

        $("#btnReset").click(function () {
            $("#ReportTypeId").val(0);

            $("#MemberSection").hide();
            $("#DateSection").hide();
            $("#EventSection").hide();
            $("#MinistrySection").hide();
            $("#IncomeSection").hide();
            $("#ExpenseSection").hide();
            $("#AssetSection").hide();
            $("#TaskStatusSection").hide();
            $("#MinistryScheduleSection").hide();

            $.cookie("ReportTypeId", '', { expires: 7, path: "/" });

            $.cookie("StartDate", '', { expires: 7, path: "/" });
            $.cookie("FinishDate", '', { expires: 7, path: "/" });

            $.cookie("GenderId", '', { expires: 7, path: "/" });
            $.cookie("MaritalStatusId", '', { expires: 7, path: "/" });
            $.cookie("GroupId", '', { expires: 7, path: "/" });
            $.cookie("ProfessionId", '', { expires: 7, path: "/" });

            $.cookie("EventTypeId", '', { expires: 7, path: "/" });
            $.cookie("StatusId", '', { expires: 7, path: "/" });

            $.cookie("MinistryTypeId", '', { expires: 7, path: "/" });
            $.cookie("RoleId", '', { expires: 7, path: "/" });

            $.cookie("IncomeTypeId", '', { expires: 7, path: "/" });
            $.cookie("ExpenseTypeId", '', { expires: 7, path: "/" });

            $.cookie("AssetTypeId", '', { expires: 7, path: "/" });
            $.cookie("BaptizeId", '', { expires: 7, path: "/" });

            $.cookie("TaskStatusId", '', { expires: 7, path: "/" });
            $.cookie("MinistryGroupId", '', { expires: 7, path: "/" });

        });

        function CurrentReport(Id) {

            switch (parseInt(Id)) {

                case 3035: //// Event                   

                    $("#MemberSection").hide();
                    $("#DateSection").show();
                    $("#EventSection").hide();
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").hide();
                 
                    break;

                case 275: //// Event                   

                    $("#MemberSection").hide();
                    $("#DateSection").show();
                    $("#EventSection").show();
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").hide();
                  
                    break;

                case 276: //// Member              

                    $("#MemberSection").show();
                    $("#DateSection").hide();
                    $("#EventSection").hide();
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").hide();
                
                    break;

                case 277: //// Assets                

                    $("#MemberSection").hide();
                    $("#DateSection").hide();
                    $("#EventSection").hide();
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").show();
                 
                    break;

                case 445: ////  Ministry  section

                    $("#MemberSection").hide();
                    $("#DateSection").hide();
                    $("#EventSection").hide();
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").hide();
                
                    break;

                case 353: //// Training  section 

                    $("#MemberSection").hide();
                    $("#DateSection").hide();
                    $("#EventSection").hide();
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").hide();
                   
                    break;

                case 417: //// Expense section

                    $("#MemberSection").hide();
                    $("#DateSection").show();
                    $("#EventSection").hide();
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").show();
                    $("#AssetSection").hide();
                   
                    break;

                case 279: //// Income section

                    $("#MemberSection").hide();
                    $("#DateSection").show();
                    $("#EventSection").hide();
                    $("#IncomeSection").show();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").hide();
                   
                    break;

                case 278: //// Attendance section

                    $("#MemberSection").hide();
                    $("#DateSection").show();
                    $("#EventSection").hide();               
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").hide();
                   
                    break;

                case 3112:
                case 3113:

                    $("#MemberSection").hide();
                    $("#DateSection").show();
                    $("#EventSection").hide();
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").hide();
                   
                    break;

                case 3115:
                case 3114:

                    $("#MemberSection").hide();
                    $("#DateSection").hide();
                    $("#EventSection").hide();
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").hide();
                  
                    break;
                case 5179:

                    $("#MemberSection").hide();
                    $("#DateSection").hide();
                    $("#EventSection").hide();
                    $("#IncomeSection").hide();
                    $("#ExpenseSection").hide();
                    $("#AssetSection").hide();
                   
                    break;

                default:
                    break;
            }
        }
    }
}