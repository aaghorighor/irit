
var suftnet_upload =
{
    init : function(url)
    {       
        $('#file_upload').uploadifive({
            'queueSizeLimit': 5,
            //'queueID':'uploads',
            'fileObjName': 'file',
            'method': 'post',
            'buttonText': $("#buttonText").text(),
            'removeCompleted': true,
            'dnd': false,
            'simuploadLimit': 1,
            'multi': false, 'auto': true, 'fileExt': '*.jpg;*.gif;*.png', 'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
            'uploadScript': url,
            'onUploadComplete': function (file, fileName)
            {              
                var obj = jQuery.parseJSON(fileName);

                if (obj.ok)
                {
                    $("#FileName").val(obj.FileName);
                    $("#ImageUrl").val(obj.FileName);                             
                                    
                    $("#imageSrc").attr('src', obj.FilePath);    
                    $('#btnSubmit').button({ disabled: false }); 
                }
            },
            'onUpload': function (file) {

                $('#btnSubmit').button({ disabled: true });
            },
            'onSelectOnce': function (event, data) {
               
            },

            'onFallback': function () {

            },
            'onClearQueue': function (queueItemCount) {
              

            },
            'onQueueComplete': function (queueData) {

                if (queueData.errors) {
                 
                } else {                 
                }
            }
           ,
            'onDestroy': function () {

            }
        });

    },
    init1: function (url) {
        $('#file_upload1').uploadifive({
            'queueSizeLimit': 1,
            //'queueID':'uploads',
            'fileObjName': 'file',
            'method': 'post',
            'buttonText': $("#buttonText").text(),
            'removeCompleted': true,
            'dnd': false,
            'simuploadLimit': 1,
            'multi': false, 'auto': true, 'fileExt': '*.jpg;*.gif;*.png', 'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
            'uploadScript': url,
            'onUploadComplete': function (file, fileName) {
                var obj = jQuery.parseJSON(fileName);

                if (obj.ok) {
                    $("#LogoUrl").val(obj.FileName);
                }
            },

            'onSelectOnce': function (event, data) {
                $('#upload_c').removeClass('disable').addClass('special');
                $('#uploadButtondisable').css({ 'display': 'none' });
                $('#uploadFile').removeClass('disable').addClass('uploadFilepics confirm');
                $('#status-message').html('Ready');
            },

            'onFallback': function () {

            },
            'onClearQueue': function (queueItemCount) {

                $('#upload_c').removeClass('special').addClass('disable');
                $('#uploadFile').removeClass('uploadFilepics confirm').addClass('disable');
                $('#status-message').html(' ');

            },
            'onQueueComplete': function (queueData) {

                if (queueData.errors) {

                } else {
                }
            }
            ,
            'onDestroy': function () {

            }
        });

    },
    init2: function (url) {
        $('#file_upload2').uploadifive({
            'queueSizeLimit': 1,
            //'queueID':'uploads',
            'fileObjName': 'file',
            'method': 'post',
            'buttonText': $("#buttonText").text(),
            'removeCompleted': true,
            'dnd': false,
            'simuploadLimit': 1,
            'multi': false, 'auto': true, 'fileExt': '*.jpg;*.gif;*.png', 'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
            'uploadScript': url,
            'onUploadComplete': function (file, fileName) {
                var obj = jQuery.parseJSON(fileName);

                if (obj.ok) {                   
                    $("#PastorUrl").val(obj.FileName);                   
                }
            },

            'onSelectOnce': function (event, data) {
                $('#upload_c').removeClass('disable').addClass('special');
                $('#uploadButtondisable').css({ 'display': 'none' });
                $('#uploadFile').removeClass('disable').addClass('uploadFilepics confirm');
                $('#status-message').html('Ready');
            },

            'onFallback': function () {

            },
            'onClearQueue': function (queueItemCount) {

                $('#upload_c').removeClass('special').addClass('disable');
                $('#uploadFile').removeClass('uploadFilepics confirm').addClass('disable');
                $('#status-message').html(' ');

            },
            'onQueueComplete': function (queueData) {

                if (queueData.errors) {

                } else {
                }
            }
            ,
            'onDestroy': function () {

            }
        });

    },
    init3: function (url) {
        $('#file_upload3').uploadifive({
            'queueSizeLimit':1,
            //'queueID':'uploads',
            'fileObjName': 'file',
            'method': 'post',
            'buttonText': $("#buttonText").text(),
            'removeCompleted': true,
            'dnd': false,
            'simuploadLimit': 1,
            'multi': false, 'auto': true, 'fileExt': '*.jpg;*.gif;*.png', 'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
            'uploadScript': url,
            'onUploadComplete': function (file, fileName) {
                var obj = jQuery.parseJSON(fileName);

                if (obj.ok) {
                   
                    $("#BackgroundUrl").val(obj.FileName);                 
                }
            },

            'onSelectOnce': function (event, data) {
                $('#upload_c').removeClass('disable').addClass('special');
                $('#uploadButtondisable').css({ 'display': 'none' });
                $('#uploadFile').removeClass('disable').addClass('uploadFilepics confirm');
                $('#status-message').html('Ready');
            },

            'onFallback': function () {

            },
            'onClearQueue': function (queueItemCount) {

                $('#upload_c').removeClass('special').addClass('disable');
                $('#uploadFile').removeClass('uploadFilepics confirm').addClass('disable');
                $('#status-message').html(' ');

            },
            'onQueueComplete': function (queueData) {

                if (queueData.errors) {

                } else {
                }
            }
            ,
            'onDestroy': function () {

            }
        });

    }
  
}