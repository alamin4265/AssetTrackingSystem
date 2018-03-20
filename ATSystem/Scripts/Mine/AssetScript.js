
$(document).ready(function ()
{

    $("#CategoryId").empty();
    $("#BrandId").empty();

    $("#GeneralCategoryId").change(function ()
    {
        var generalcategoryId = $("#GeneralCategoryId").val();
        GetCategoriesByGeneralCategoryId(generalcategoryId);
    });


    $("#CategoryId").change(function ()
    {
        var categoryId = $("#CategoryId").val();
        GetSubCategoriesByCategoryId(categoryId);
    });
    
   
 
    

});


function GetCategoriesByGeneralCategoryId(generalcategoryId)
{
    var json = {
        generalCategoryId: generalcategoryId
    };

    $.ajax(
        {
            type: "POST",
            url: '@Url.Action("GetCategoriesByGeneralCategoryId", "Category")',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {

                $('#CategoryId').empty();
                var blankOption = "<option value=''> Select...</option>";

                $('#CategoryId').append(blankOption);
                $.each(data, function (key, value) {

                    var option = "<option value='" + value.Id + "'>" + value.Name + "</option>";
                    $('#CategoryId').append(option);

                });

            },
            error: function (data) {

            }
        });

    
}

function GetSubCategoriesByCategoryId(categoryId)
{
    $("#BrandId").empty();
   
    var json = { categoryId: categoryId };
    $.ajax({
        ype: "POST",
        url: '@Url.Action("GetCategoriesByGeneralCategoryId", "Category")',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(json),
        success: function (data)
        {
            $.each(data, function (key, value)
            {
                $("#BrandId").append('<option value=' + value.Id + '>' + value.Name + '</option>');
            });
        }
    });
}
