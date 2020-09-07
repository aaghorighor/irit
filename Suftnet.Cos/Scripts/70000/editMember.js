
var editMember = {

    init: function (Id) {

        editMember.eventPage(Id);
    },

    eventPage: function (Id)
    {
        var tr = $(this).closest('tr');
        var rowIndex = tr.index();

        $("#rowindex").val(rowIndex);

        js.ajaxGet($("#editMemberUrl").attr("data-editMemberUrl"), { Id: Id }).then(
         function (data) {

             var dataobject = data.dataobject;
            
             $("#Id").val(dataobject.Id);
             $("#GenderId").val(dataobject.GenderId);

             $("#FirstName").val(dataobject.FirstName);
             $("#LastName").val(dataobject.LastName);

             $("#Email").val(dataobject.Email);
             $("#Mobile").val(dataobject.Mobile);

             if (dataobject.DateOfBirthDT != null)
             {                                                                     
                 $("#DateOfBirth").val(dataobject.DateOfBirthDT);
             }            
                                   
             $("#IsEmail").attr("checked", dataobject.IsEmail);
             $("#IsSms").attr("checked", dataobject.IsSms);
           
             $("#MemberTypeId").val(dataobject.MemberTypeId);            
             $("#StatusId").val(dataobject.StatusId);

             $("#JoinDate").val(dataobject.MembershipDT);

             $("#AddressId").val(dataobject.AddressId);
             $("#AddressLine1").val(dataobject.AddressLine1);
             $("#AddressLine2").val(dataobject.AddressLine2);
             $("#AddressLine3").val(dataobject.AddressLine3);

             $("#PostCode").val(dataobject.PostCode);
             $("#Country").val(dataobject.Country);

             var imageUrl = "/Content/Photo/Member/216X196/";
                      
             if (dataobject.FileName != null && dataobject.FileName.length != 0) {              
                 $("#FileName").val(dataobject.FileName);                
                 $("#TempUrl").attr('src', imageUrl + dataobject.FileName);
             };

         });
    }
}

