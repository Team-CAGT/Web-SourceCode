$("input[value='true']").click(function () {
    $("#abc").slideDown();
});
$("input[value='false']").click(function () {
    $("#abc").slideUp();
});
if ($("#hospitalTypes-Content1").length > 0) {
    $('#hospitalTypes-Content1').mixItUp({
        load: {
            filter: 'all'
        }
    });
}
//$(function () {
//    $('#hospitalTypes-Content').mixItUp();
//});
$(function() {
    $('#hospitalTypes-Content').mixItUp({
      load: {
        filter: '.type1'
      }
    });
    var parentWidth = $(".js-fullWidth").first().parent().width();
    // If absolute URL from the remote server is provided, configure the COR
    $(".js-fullWidth").each(function (index) {
        $(this).attr("width", parentWidth);
    });
    $("#myTabs").on("show.bs.tab",
        function(e) {
            var paneId = $(e.target).attr('href');
            var src = $(paneId).attr('data-src');
            // if the iframe hasn't already been loaded once
            if (!$(paneId + " iframe").attr("src")) {
                $(paneId + " iframe").attr("src", src);
            }
        });
});
function file_change(f) {

    var reader = new FileReader();

    reader.onload = function (e) {

        var img = document.getElementById("img");

        img.src = e.target.result;

        img.style.display = "inline";

    };

    reader.readAsDataURL(f.files[0]);

};
function AjaxSubmitDoctorForm() {
    $("#butSaveDoctor").attr("disabled", "disabled");
    $(".loading-spinner").removeClass("hidden");
    var formData = new FormData($('#CreateOrUpdateDoctor')[0]);
    console.log('formData', formData);
    $.ajax({
        url: '/Doctor/CreateDoctor',
        type: 'POST',
        data: formData, // serializes the form's elements.
        contentType: false,
        processData: false,
        success: function (response) {

            if (response != null) {
                if (!response.Result) {
                    //$("#Register-dangky").attr("disabled", "");
                    $('#butSaveDoctor').removeAttr("disabled");
                    swal("Tạo Profile Bác sĩ", response.Message, "warning");
                    $(".loading-spinner").addClass("hidden");
                } else {

                    swal("Tạo Profile Bác sĩ ", response.Message, "success");
                    $(".loading-spinner").addClass("hidden");
                    setTimeout(function () {


                        window.location.href = document.location.origin;

                        //End
                    }, 5000);

                }
            }
        },
        fail: function () {

        }
    });
};
function AjaxSubmitEditDoctorForm() {
    $("#butEditDoctor").attr("disabled", "disabled");
    $(".loading-spinner").removeClass("hidden");
    var formData = new FormData($('#UpdateDoctor')[0]);
    console.log('formData', formData);
    $.ajax({
        url: '/Doctor/EditDoctor',
        type: 'POST',
        data: formData, // serializes the form's elements.
        contentType: false,
        processData: false,
        success: function (response) {

            if (response != null) {
                if (!response.Result) {
                    //$("#Register-dangky").attr("disabled", "");
                    $('#butEditDoctor').removeAttr("disabled");
                    swal("Sửa Profile Bác sĩ", response.Message, "warning");
                    $(".loading-spinner").addClass("hidden");
                } else {

                    swal("Sửa Profile Bác sĩ ", response.Message, "success");
                    $(".loading-spinner").addClass("hidden");
                    setTimeout(function () {


                        window.location.href = document.location.origin;

                        //End
                    }, 5000);

                }
            }
        },
        fail: function () {

        }
    });
};
function AjaxSubmitSearchDoctor() {
  
    $.ajax({
            url: '/Doctor/SearchDoctorResult',
            dataType: "html",
            type: "GET",
            data: { specicality: $("#DepartmentId").val(), name: $("#SearchName").val() },
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#SearchDoctorResult").empty();
                $('#SearchDoctorResult').append(data);
            },
            error: function (xhr) {
                alert("Đã xãy ra lỗi, chưa lấy được thông tin");
            }
        });
   
};
function AjaxSubmitSearchDoctorForm() {

    $.ajax({
        url: '/Doctor/SearchDoctor',
        dataType: "html",
        type: "GET",
        data: { specicality: $("#DepartmentForm").val(), name: $("#SearchNameForm").val() },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            
        },
        error: function (xhr) {
            alert("Đã xãy ra lỗi, chưa lấy được thông tin");
        }
    });
};