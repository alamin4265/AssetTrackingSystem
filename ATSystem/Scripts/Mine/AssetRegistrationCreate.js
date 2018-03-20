


$(document).ready(function () {

    $("#RegistrationDate").datepicker();

    $("#AssetId").change(function () {
        var assetId = $("#AssetId").val();
        GetAssetInfoByAssetId(assetId);
    });

    $("#OrganizationId").change(function () {
        var organizationId = $("#OrganizationId").val();
        GetBranchByOrganizationId(organizationId);

    });


    $("#addItemButton").click(function () {
        var assetId = $("#AssetId").val();
        var code = $("#Code").val();
        var serialNo = $("#SerialNo").val();
        var registrationNo = $("#RegistrationNo").val();


        if ((assetId == "" || assetId == undefined) || (serialNo == "" || serialNo == "undefined") || (code == "" || code == undefined) || (registrationNo == "" || registrationNo == undefined)) {
            $("#errorMsgForForm").fadeTo(2000, 500).slideUp(500);
            return false;
        } else {
            AddRowForItem();
        }
        return false;

    });

    $("#clear").click(function() {
        $("#assetlisttable").empty();
    });

});



var itemList = [];
function AddRowForItem() {

    var index = $("#itemsListTable").children("tr").length;
    var selectedItem = getSelectedtItem();

    var selectedItemId = $("#AssetId").val();

    if (selectedItemId != null) {
        var isAdded = $.inArray(selectedItemId, itemList) > -1 ? true : false;
        if (isAdded) {
            $("#errorMsg").fadeTo(2000, 500).slideUp(500);
            $("#successMsg").hide();
            return;
        } else {
            itemList.push(selectedItemId);

            $("#errorMsg").hide();
            $("#successMsg").fadeTo(2000, 500).slideUp(500);

        }
    }

    var indexCell = "<td style='display:none'><input name='AssetRegistrationDetailses.Index' type='hidden' value='" + index + "' /></td>";

    var serialCell = "<td><label id='AssetDetails" + index + "_Serial' value='" + (index + 1) + "' />" + (index + 1) + "</td>";
    var registrationNoCell = "<td><input type='hidden' id='AssetDetails" + index + "_RegistrationCode' name='AssetRegistrationDetailses[" + index + "].RegistrationNo' value='" + selectedItem.RegistrationName + "' />" + selectedItem.RegistrationName + "</td>";
    var codeCell = "<td><input type='hidden' id='AssetDetails" + index + "_PartsCode' name='AssetRegistrationDetailses[" + index + "].Code' value='" + selectedItem.Code + "' />" + selectedItem.Code + "</td>";
    var organizationCell = "<td><input type='hidden' id='AssetDetails" + index + "_VendorId' name='AssetRegistrationDetailses[" + index + "].OrganizationId' value='" + selectedItem.OrganizationId + "' />" + selectedItem.OrganizationName + "</td>";
    var assetCell = "<td><input type='hidden' id='AssetDetails" + index + "_Quantity' name='AssetRegistrationDetailses[" + index + "].AssetId' value='" + selectedItem.AssetId + "' />" + selectedItem.AssetName + "</td>";
    var serialNoCell = "<td><input type='hidden' id='AssetDetails" + index + "_Price' name='AssetRegistrationDetailses[" + index + "].SerialNo' value='" + selectedItem.SerialNo + "' />" + selectedItem.SerialNo + "</td>";
    var branchCell = "<td><input type='hidden' id='AssetDetails" + index + "_Price' name='AssetRegistrationDetailses[" + index + "].BranchId' value='" + selectedItem.BranchId + "' />" + selectedItem.BranchName + "</td>";
    var removeCell = "<td><input id='btnRemoveItem' class='btn btn-xs btn-danger btn-rounded' type='button' value='Remove' onclick='RemoveRowForItem(" + index + ");' /></td>";

    var newRow = "<tr id='trItems" + index + "'>" +
        indexCell + serialCell + registrationNoCell + codeCell + serialNoCell + assetCell + organizationCell + branchCell + removeCell + "</tr>";
    $("#itemsListTable").append(newRow);
}


function getSelectedtItem() {
    var assetId = $("#AssetId option:selected").val();
    var assetName = $("#AssetId option:selected").text();

    var serialNo = $("#SerialNo").val();

    var code = $("#Code").val();

    var registrationNo = $("#RegistrationNo").val();

    var organizationId = $("#OrganizationId option:selected").val();
    var organizationName = $("#OrganizationId option:selected").text();

    var branchId = $("#BranchId option:selected").val();
    var branchName = $("#BranchId option:selected").text();

    var itemObj = {
        "AssetId": assetId,
        "AssetName": assetName,
        "SerialNo": serialNo,
        "Code": code,
        "RegistrationName": registrationNo,
        "OrganizationId": organizationId,
        "OrganizationName": organizationName,
        "BranchId": branchId,
        "BranchName": branchName
    }
    return itemObj;
}

function RemoveRowForItem(id) {
    var controlToBeRemoved = "#trItems" + id;
    $(controlToBeRemoved).remove();
    itemList.splice(controlToBeRemoved, 1);
}


function GetAssetInfoByAssetId(assetId) {

   var json = { assetId: assetId };
    $.ajax({
        type: "POST",
        url: "/JsonLoader/GetAssetById",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(json),
        success: function (data) {

            if (data == "NotFound") {
                document.getElementById("Code").value = data;
                document.getElementById("SerialNo").value = data;
            }
            else {
                document.getElementById("Code").value = data.Code;
                document.getElementById("SerialNo").value = data.SerialNo;
            }
                
            }
           
    });
}

function GetBranchByOrganizationId(organizationId) {

    var json = { organizationId: organizationId };
    $.ajax({
        type: "POST",
        url: "/JsonLoader/GetBranchByOrganizationId",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(json),
        success: function (data) {
            $('#BranchId').empty();
            var blankOption = "<option value=''> Select...</option>";

            $('#BranchId').append(blankOption);
            $.each(data, function (key, value) {
                $("#BranchId").append('<option value=' + value.Id + '>' + value.Name + '</option>');
            });
            

        }

    });
}