//Get the users page
function GetUsers(url) {
    $.ajax({
        url: url,
        data: { __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() },
        type: "post",
        success: function (data) {
            $("#content").html(data);
            $("#btnUsers").addClass("active");
            $("#btnRoles").removeClass("active");
        },
        error: function () {
            alert("Oops, we have a problem...");
        }
    });
}

//GEt users grid
function getUsersGrid(url, search, sort, ascending, page, pageSize) {
    $.ajax({
        url: url,
        data: {
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val(),
            Text: search,
            Sort: sort,
            Ascending: ascending,
            Page: page,
            PageSize: pageSize
        },
        type: "post",
        success: function (data) {
            $("#users").html(data);
        },
        error: function () {
            alert("Oops, we have a problem...");
        }
    });
}

//Update user role
function updateUserRole(url, userId,roleName) {
    $.ajax({
        url: url,
        data: {
            id: userId,
            roleName: roleName,
            set: $("#" + roleName).is(':checked'),
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        type: "post",
        //success: function (data) {

        //},
        error: function () {
            $("#" + roleName).checked = !$("#" + roleName).is(':checked');
            alert("Oops, we have a problem...");
        }
    });
}
//Get user role
function getUserRoles(url, search, sort, ascending, page, pageSize,userId) {
    $.ajax({
        url: url,
        data: {
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val(),
            Text: search,
            Sort: sort,
            Ascending: ascending,
            Page: page,
            PageSize: pageSize,
            id : userId
        },
        type: "post",
        success: function (data) {
            $("#users").html(data);
        },
        error: function () {
            alert("Oops, we have a problem...");
        }
    });
}