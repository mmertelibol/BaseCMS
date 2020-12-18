
function AddNews() {
    let Title = $("#title").val();
    let Author = $("#author").val();
    let RoutingUrl = $("#routingurl").val();
    let MetaDescription = $("#description").val();
    let MetaKeywords = $("#keywords").val();
    let Category = $("#category").val();
    let addNews = {
        Title: Title,
        Author=Author,
        NewsCategoryId=Category,

        RoutingUrl=RoutingUrl,
        MetaDescription=MetaDescription,
        MetaKeywords=MetaKeywords,


    };

    $.ajax({
        type: "POST",
        url: "/News/AddNews",
        data: addNews,
        dataType: "Json",
        success: function (response) {
            if (response) {
                location.href = "/News/Index"
                console.log("İşlem Başarılı");
            }
            else {
                alert("Hata");
                // location.href = "/News/AddNews"
            }
        },
        error: function () {

            console.log("Hata Oluştu");
        }
    });
};


