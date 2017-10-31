
var aRequest = function () {
    $.ajax({
        url: 'http://ws.spotify.com/search/1/album.json?q=hello'
    }).success(function (response) {
        var albums = response.albums;
        var result = "";
        for (var i = 0, len = albums.length; i < len; i++) {
            var album = albums[i];
            result = result + "<li>" + album.name + "</li>";
        }
        $("#results").html(result);
    });

}

$(document).ready(function () {
    $('h1').click(aRequest)
})