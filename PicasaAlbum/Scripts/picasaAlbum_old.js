// picasaAlbum jQuery plug-in

(function ($) {

    $.fn.picasaAlbum = function (options) {    // this is the name of the plugin

        // This is the easiest way to have default options.
        var settings = $.extend({       // here we set defaults
            // These are the defaults.
            userid: "bhojim",               // these are the properties of settings
            albumName: "photoshop",
            thumbsize: 64,
            maxImages: 40,
            imageSize: 400,     // maximum image size along the largest dimension. this is to limit the size of an image
            callback: function () { }
        }, options);            // and let options override the defaults

        // Generate url for photo scaled to specified size
        function imgScaledUrl(url, size) {
            var split = url.lastIndexOf("/");
            return url.substring(0, split) + "/s" + size + url.substring(split);
        }


        function processPhotos(data, status, options) {
            var thumbsize;
            var photosize;
            var ts = 1;

            // Sizes for photographs to be displayed by slimbox
            var PHOTO_SMALL = 640;
            var PHOTO_MEDIUM = 800;
            var PHOTO_LARGE = 1024;

            var DEFAULT_PHOTOSIZE = PHOTO_MEDIUM;
            var ps = photosize || DEFAULT_PHOTOSIZE;

            $(options.titleContainer).text(data.feed.title.$t);
            //$(options.subtitleContainer).text(data.feed.subtitle.$t);
            var jsonObj = [];
            $.each(data.feed.entry, function (i, pic) {
                var thumbs = pic.media$group.media$thumbnail;
                for (var i = 0; i < thumbs.length; i++) {
                    if (thumbs[i].width == options.thumbsize) {
                        ts = i;
                        break;
                    }
                }

                var thumb = pic.media$group.media$thumbnail[ts];
                var photo = pic.media$group.media$content[0];
                var desc = pic.media$group.media$description.$t;
                debugger;
                jsonObj.push({
                    thumb: thumb,
                    photo: photo,
                    desc: desc,
                    style: "margin: 1px;",
                    url: photo.url   //imgScaledUrl(photo.url, ps)
                });
            });
            debugger;
            if (options.initialize) {
                options.initialize.call(this);
            }
            debugger;
            $(options.photoContainer).html(
                 $(options.photoTemplate).render(jsonObj)
            );
            if (options.callback) {
                options.callback.call(this);
            }

        }

        function processAlbums(data, status, options) {
            $(options.titleContainer).text(data.feed.title.$t);
            $(options.subtitleContainer).text(data.feed.subtitle.$t);
            var jsonObj = [];

            $.each(data.feed.entry, function (i, gallery) {
                var thumb = gallery.media$group.media$thumbnail[0];
                // Get the name of the gallery from the alternate link url.  Is this reliable?  Seems to work.
                var textId = '';
                $.each(gallery.link, function (i, link) {
                    if (link.rel == 'alternate') {
                        var parts = link.href.split('/');
                        textId = parts[parts.length - 1];
                    }
                });

                jsonObj.push({
                    albumId: textId,
                    title: gallery.title.$t,
                    url: thumb.url
                });
            });

            $(options.albumContainer).html(
                    $(options.albumTemplate).render(jsonObj)
                );
        }

        // opt could be 'small' or a number
        function getSize(opt) {
            debugger;
            result = 0;
            sz = [32, 48, 64, 72, 94, 104, 110, 128, 144, 150, 160, 200, 220, 288, 320, 400, 512, 576, 640, 720, 800, 912, 1024, 1152, 1280, 1440, 1600];

            norm = {
                small: '72c',
                thumb: '104u',
                medium: '640u',
                big: '1024u',
                original: '1600u'
            };
            if (opt in norm) {
                result = norm[opt];
            }
            else {
                n = opt;
                if (n > 1600) {
                    n = 1600;
                } else {
                    $.each(sz, function (i) {
                        if (n < this) {
                            n = sz[i - 1];
                            return false;
                        }
                    });
                }
                result = n;
            };
            debugger;
            return result;
        }

        function loadPicasaTopic(topic, maxImages, options) {
            var maxResults = maxImages;
            var url = buildUrl(options);
            $.getJSON(url,
                    function (data, status) {
                        processPhotos(data, status, options);
                    }
                    );
        }

        function loadPicasaAlbum(userid, albumid, options) {
            var url = buildUrl(options);
            $.getJSON(url,
                    function (data, status) {
                        processPhotos(data, status, options);
                    }
                );
        }

        function loadPicasaAlbums(userid, options) {
            var url = buildUrl(options);
            $.getJSON(url,
                    function (data, status) {
                        processAlbums(data, status, options);
                        // display photos of first album. How?
                        // How to get name of first album
                        var firstAlbumName = getFirstAlbum(data);
                        if (firstAlbumName.length > 0)
                            loadPicasaAlbum(userid, firstAlbumName, options);

                        // on album click, display photos
                        $(document).on('click', '.thumb, .link', function () {
                            var albumName = $(this).attr("album");
                            $(options.photoContainer).empty().removeAttr("class");
                            loadPicasaAlbum(userid, albumName, options);
                        });
                    }
                );
        }

        function buildUrl(options) {
            debugger;
            var url = 'https://picasaweb.google.com/data/feed/api/';
            switch (options.action) {
                case 'loadAlbum':
                    url += 'user/' + options.userid + '/album/' + options.albumName + '?';
                    params = {};
                    break;
                case 'loadAlbums':
                    url += 'user/' + options.userid + '?';
                    params = {};
                    break;
                case 'loadTopic':
                    url += 'all?';
                    params = { 'q': options.topic };
            }

            params = $.extend({
                'kind': 'photo',
                'access': 'public',
                'thumbsize': getSize(options.thumbsize),
                'imgmax': getSize(options.imageSize),
                'alt': 'json-in-script',
                'callback': '?'
            }, params);

            if (options.maxImages) {
                params = $.extend({
                    'max-results': options.maxImages
                }, params);
            }

            $.each(params, function (key, value) {
                url += '&' + key + '=' + value;
            });
            return url;
        }

        function getFirstAlbum(data) {
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

        if (settings.action == "loadAlbum") {
            loadPicasaAlbum(settings.userid, settings.albumName, settings);
        }
        else if (settings.action == "loadTopic") {
            loadPicasaTopic(settings.topic, settings.maxImages, settings);
        }
        else if (settings.action == "loadAlbums") {
            loadPicasaAlbums(settings.userid, settings);
        }
    };

} (jQuery));
