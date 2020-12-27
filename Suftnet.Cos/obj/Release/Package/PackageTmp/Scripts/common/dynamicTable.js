var ministerFinderTable;
var ministerTable;
var lookupTable;
var oPledge;
var oPledger;
var contactusTable;
var logTable;

var suftnet_dynamicTable = {
        
    initPledges : function(url)
    {      
        if (oPledge == null)
        {
            oPledge = $('#tdpledge').DataTable(
              {
                  "bProcessing": true,
                  "bServerSide": true,
                  "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
                  "iDisplayLength": 5,
                  "sPaginationType": "full_numbers",
                  "sAjaxSource": url,
                  "aoColumns": [
                    { "sName": "Id", "bSortable": false, },
                    { "sName": "CreatedOn", "bSortable": false, },
                    { "sName": "Title", "bSortable": false, },
                    { "sName": "Expected", "bSortable": false, },
                    { "sName": "Donated", "bSortable": false, },
                    { "sName": "Remaining", "bSortable": false, "bSearchable": false, },
                    { "sName": "Status", "bSortable": false, "bSearchable": false, },
                    {
                        "mData": "Id",
                        "bSearchable": false,
                        "bAutoWidth": false,
                        "bSortable": false,
                        "fnRender": function (value) {

                            var seoUrl = Suftnet_Utility.toSeoUrl(value.aData[2]);                          

                            return "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"View\" viewId=" + value.aData[0] + " name=" + seoUrl + " title=\"Open pledger viewer\"><img src=" + suftnet_grid.iconUrl + "file.png\ alt=\"Open pledger viewer\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.aData[0] + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.aData[0] + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete this row\" /></span></a>";
                        }
                    }
                  ]
              });
        }      

    },
      
    initContactus : function(url)
    {       
        if (contactusTable == null)
        {
             contactusTable = $('#tdContactus').DataTable(
             {
                 "bProcessing": true,
                 "bServerSide": true,
                 "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
                 "iDisplayLength": 5,
                 "sPaginationType": "full_numbers",
                 "sAjaxSource": url,
                 "aoColumns": [
                   { "sName": "Id", "bSortable": false, },
                   { "sName": "SequencyId", "bSortable": false, },                 
                   { "sName": "FirstName", "bSortable": false, },
                   { "sName": "LastName", "bSortable": false, },
                   { "sName": "Role", "bSortable": false, },
                   { "sName": "Active", "bSortable": false, },
                   {
                       "mData": "Id",
                       "bSearchable": false,
                       "bAutoWidth": false,
                       "bSortable": false,
                       "fnRender": function (value) {
                           return "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.aData[0] + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.aData[0] + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>";
                       }
                   }
                 ]
             });
        }       
    },
      
    initIncome : function(url)
    {
        incomeTable = $('#tdIncome').DataTable(
      {
          "bProcessing": true,
          "bServerSide": true,
          "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
          "iDisplayLength": 5,
          "sPaginationType": "full_numbers",
          "sAjaxSource": url,
          "aoColumns": [
            { "sName": "Id", "bSortable": false, },
            { "sName": "CreatedOn", "bSortable": false, },
            { "sName": "StripeReference", "bSortable": false, },
            { "sName": "MemberReference", "bSortable": false, },
            { "sName": "IncomeType", "bSortable": false, },          
            { "sName": "Amount", "bSortable": false, },
            { "sName": "GiftAidAmount", "bSortable": false, },
            { "sName": "Total", "bSortable": false, },
            {
                "mData": "Id",
                "bSearchable": false,
                "bAutoWidth": true,
                "bSortable": false,
                "fnRender": function (value) {
                    return "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditIncomeLink\" EditId=" + value.aData[0] + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"DeleteIncome\" DeleteId=" + value.aData[0] + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>";
                }
            }
          ]
      });
    },

    initlookUp :function(url)
    {
        if (lookupTable == null) {
            lookupTable = $("#tdlookups").DataTable(
         {
             "bProcessing": true,
             "bServerSide": true,
             "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
             "iDisplayLength": 5,
             "sPaginationType": "full_numbers",
             "sAjaxSource": url,
             "aoColumns": [
               { "sName": "Id", "bSortable": false, },
               { "sName": "Title", "bSortable": false, },
               { "sName": "FirstName", "bSortable": false, },
               { "sName": "LastName", "bSortable": false, },
               { "sName": "Active", "bSortable": false, "bSearchable": false, },
               {
                   "mData": "",
                   "bSearchable": false,
                   "bAutoWidth": false,
                   "bSortable": false,
                   "fnRender": function (value) {
                       return "<a href=\"javascript:void(0)\"><a href=\"javascript:void(0)\"><span id=\"memberFinder\" memberId=" + value.aData[0] + " title=\"Select this content\"><img src=" + suftnet_grid.iconUrl + "pencil.png\ alt=\"\" /></span></a>";
                   }
               }
             ]
         });
        }
    },

    initMember : function(url)
    {
        memberTable = $('#tdMember').DataTable(
      {
          "bProcessing": true,
          "bServerSide": true,
          "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
          "iDisplayLength": 5,
          "sPaginationType": "full_numbers",
          "sAjaxSource": url,
          "aoColumns": [
            { "sName": "Id", "bSortable": false, },         
            { "sName": "FirstName", "bSortable": false, },
            { "sName": "LastName", "bSortable": false, },
            { "sName": "Email", "bSortable": false, },
            { "sName": "Mobile", "bSortable": false, "bSearchable": false, },
            { "sName": "Status", "bSortable": false, "bSearchable": false, },
            {
                "mData": "Id",
                "bSearchable": false,
                "bAutoWidth": false,
                "bSortable": false,
                "fnRender": function (value) {
                    return "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"EditLink\" EditId=" + value.aData[0] + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit Content\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"Delete\" DeleteId=" + value.aData[0] + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete\" /></span></a>";
                }
            }
          ]
      });
    },
  
    initHymn: function (url) {
        hymnTable = $('#tdHymn').DataTable(
            {
                "bProcessing": true,
                "bServerSide": true,
                "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
                "iDisplayLength": 5,
                "sPaginationType": "full_numbers",
                "sAjaxSource": url,
                "aoColumns": [
                    { "sName": "Id", "bSortable": false, },
                    { "sName": "Number", "bSortable": false, },
                    { "sName": "Title", "bSortable": false, },
                    { "sName": "Status", "bSortable": false, "bSearchable": false, },
                    {
                        "mData": "Id",
                        "bSearchable": false,
                        "bAutoWidth": false,
                        "bSortable": false,
                        "fnRender": function (value) {
                            return "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"edit\" editId=" + value.aData[0] + " title=\"Edit this row\"><img src=" + suftnet_grid.iconUrl + "edit.png\ alt=\"Edit this row\" /></span></a><a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"delete\" deleteId=" + value.aData[0] + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete this row\" /></span></a>";
                        }
                    }
                ]
            });
    },

    initLog: function (url) {
        logTable = $('#tdLogViewer').DataTable(
            {
                "bProcessing": true,
                "bServerSide": true,
                "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
                "iDisplayLength": 5,
                "sPaginationType": "full_numbers",
                "sAjaxSource": url,
                "timeout": 300000,
                "aoColumns": [
                    { "sName": "Id", "bSortable": false, },
                    { "sName": "CreatedDt", "bSortable": false, },
                    { "sName": "CreatedBy", "bSortable": false, },
                    { "sName": "Description", "bSortable": false, "bSearchable": false, },
                    {
                        "mData": "Id",
                        "bSearchable": false,
                        "bAutoWidth": false,
                        "bSortable": false,
                        "fnRender": function (value) {
                            return "<a href=\"javascript:void(0)\"><span style=\"margin:10px;\" id=\"delete\" deleteId=" + value.aData[0] + " title=\"Delete this row\"><img src=" + suftnet_grid.iconUrl + "delete.png\ alt=\"Delete this row\" /></span></a>";
                        }
                    }
                ]
            });
    },

}