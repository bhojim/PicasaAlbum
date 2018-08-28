var PICASA = {
    loadPicasaTopic: function (topic, maxImages, processPhotosCallback, optionsCallback) {
        var DEFAULT_MAX_IMAGES = 20;
        var maxResults = maxImages || DEFAULT_MAX_IMAGES

        $.getJSON("https://picasaweb.google.com/data/feed/api/all?q=" + topic + "&max-results=" + maxResults + "&access=public&kind=photo&alt=json-in-script&callback=?",
            function (data, status) {
                debugger;
                processPhotosCallback(data, status);
                if (optionsCallback)
                    optionsCallback();
            }
        );
    },

    loadPicasaAlbum: function (userid, albumid, processPhotosCallback, optionsCallback) {
        $.getJSON("https://picasaweb.google.com/data/feed/base/user/" + userid + "/album/" + albumid + "?kind=photo&access=public&alt=json-in-script&callback=?",
            function (data, status) {
                processPhotosCallback(data, status);
                if (optionsCallback)
                    optionsCallback();
            }
        );
    },

    loadPicasaAlbums: function (userid, processAlbumsCallback, processPhotosCallback, optionsCallback) {
        $.getJSON("https://picasaweb.google.com/data/feed/base/user/" + userid + "?kind=album&alt=json-in-script&callback=?",
            function (data, status) {
                processAlbumsCallback(data, status);
                // display photos of first album. How?
                // How to get name of first album
                var firstAlbumName = PICASA.getFirstAlbum(data);
                if (firstAlbumName.length > 0)
                    PICASA.loadPicasaAlbum(userid, firstAlbumName, processPhotosCallback, optionsCallback);

                // on album click, display photos
                $(document).on('click', '.thumb, .link', function () {
                    var albumName = $(this).attr("album");
                    $("#picasaPhotos").empty().removeAttr("class");
                    PICASA.loadPicasaAlbum(userid, albumName, processPhotosCallback, optionsCallback);
                });
            }
        );
    },

    getFirstAlbum: function (data) {
        var albumName = '';
        if (data.feed.entry.length > 0) {
            var gallery = data.feed.entry[0];
            $.each(gallery.link, function (i, link) {
                if (link.rel == 'alternate') {
                    var parts = link.href.split('/');
                    albumName = parts[parts.length - 1];
                }
            });
        }
        return albumName;
    }

}


