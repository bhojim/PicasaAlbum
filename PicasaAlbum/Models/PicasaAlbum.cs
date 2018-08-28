using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicasaAlbum.Models
{
    public class PicasaAlbum
    {
        public string UserName { get; set; }
        public string AlbumName { get; set; }
        public string TopicName { get; set; }
        public SliderTypeEnum SliderType { get; set; }
        public RequestType SelectedRequestType { get; set; }
        public Plugin SelectedPlugin { get; set; }
        public int MaxImages { get; set; }
        public ThumbSize ThumbSize { get; set; }  // optional. If not provided, default will be used.
        public int PhotoSize { get; set; }  // optional. If not provided, default will be used.
        public Orientation SliderOrientation { get; set; }  // for RoyalSlider only

    }
}