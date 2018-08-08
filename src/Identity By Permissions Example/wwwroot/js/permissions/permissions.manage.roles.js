//Get roles page
function GetRoles(url) {
    $.ajax({
        url: url,
        data: { __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() },
        type: "post",
        success: function (data) {
            $("#content").html(data);
            $("#btnUsers").removeClass("active");
            $("#btnRoles").addClass("active");
        },
        error: function () {
            alert("Oops, we have a problem...");
        }
    });
}
//save new role
function saveNewRole(url, permissions) {
    if (!$("#roleName").val()) {
        alert("Needed a role name");
        return;
    }
    $.ajax({
        url: url,
        data: {
            roleName: $("#roleName").val(),
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        type: "post",
        success: function (data) {
            $("#listRolesUpd").append(new Option($("#roleName").val(), data, false, true));
            $("#listRolesDel").append(new Option($("#roleName").val(), data, false, true));
            getPermissions(permissions);
        },
        error: function () {
            alert("Oops, we have a problem...");
        }
    });
}
//delete a role
function removeRole(url, claims) {
    if (!$("#listRolesDel").val()) {
        alert("You should select any role first");
        return;
    }
    $.ajax({
        url: url,
        data: {
            roleId: $("#listRolesDel").val(),
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        type: "post",
        success: function (data) {
            $(".desplegables option[value=" + $("#listRolesDel").val() + "]").remove();
            getPermissions(claims);
        },
        error: function () {
            alert("Oops, we have a problem...");
        }
    });
}
//show the permissions
function getPermissions(url) {
    if (!$("#listRolesUpd").val()) {
        $("#permissions").html("");
        return;
    }
    $.ajax({
        url: url,
        data: {
            roleId: $("#listRolesUpd").val(),
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        type: "post",
        success: function (data) {
            $("#permissions").html(data);
        },
        error: function () {
            alert("Oops, we have a problem...");
        }
    });
}
//update the permissions of the role
function updatePermissions(url, id) {
    $.ajax({
        url: url,
        data: {
            roleId: $("#roleId").val(),
            policyId: id,
            set: $("#" + id).is(':checked'),
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        },
        type: "post",
        //success: function (data) {

        //},
        error: function () {
            $("#" + id).checked = !$("#" + id).is(':checked');
            alert("Oops, we have a problem...");
        }
    });
}