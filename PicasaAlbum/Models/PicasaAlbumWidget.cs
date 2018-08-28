using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System.ComponentModel;

namespace PicasaAlbum.Models
{
    public class PicasaAlbumWidgetRecord : ContentPartRecord
    {
        public virtual string UserName { get; set; }
        public virtual string AlbumName { get; set; }
        public virtual string TopicName { get; set; }
        public virtual int SliderType { get; set; }
        public virtual int SelectedRequestType { get; set; }
        public virtual int SelectedPlugin { get; set; }
        public virtual int MaxImages { get; set; }
        public virtual int ThumbSize { get; set; }  // optional. If not provided, default will be used.
        public virtual int PhotoSize { get; set; }  // optional. If not provided, default will be used.
        public virtual int SliderOrientation { get; set; }  // for RoyalSlider only
    }

    public class PicasaAlbumWidgetPart : ContentPart<PicasaAlbumWidgetRecord>
    {
        [Required]
        [DisplayName("Picasa UserName")]
        public string UserName
        {
            get { return Record.UserName; }
            set { Record.UserName = value; }
        }

        [DisplayName("Picasa AlbumName")]
        public string AlbumName
        {
            get { return Record.AlbumName; }
            set { Record.AlbumName = value; }
        }

        [DisplayName("Picasa TopicName")]
        public string TopicName
        {
            get { return Record.TopicName; }
            set { Record.TopicName = value; }
        }

        [DisplayName("Picasa SliderType")]
        public SliderTypeEnum SliderType
        {
            get { return (SliderTypeEnum)Record.SliderType; }
            set { Record.SliderType = (int)value; }
        }

        /// <summary>
        /// Presentation plugin selected.
        /// </summary>
        public Plugin SelectedPlugin
        {
            get { return (Plugin)Record.SelectedPlugin; }
            set { Record.SelectedPlugin = (int)value; }
        }

        /// <summary>
        /// Request type selected. List of albums, list of photos in album, 
        /// </summary>
        public RequestType SelectedRequestType
        {
            get { return (RequestType)Record.SelectedRequestType; }
            set { Record.SelectedRequestType = (int)value; }
        }

        /// <summary>
        /// Maximum number of images. Should be enabled only for Community Search
        /// </summary>
        public int MaxImages
        {
            get { return Record.MaxImages; }
            set { Record.MaxImages = value; }
        }

        /// <summary>
        /// Thumb size. Optional
        /// </summary>
        public ThumbSize ThumbSize
        {
            get { return (ThumbSize)Record.ThumbSize; }
            set { Record.ThumbSize = (int)value; }
        }

        /// <summary>
        /// Photo size. Optional. Should be enabled for Slimbox only
        /// </summary>
        public int PhotoSize
        {
            get { return Record.PhotoSize; }
            set { Record.PhotoSize = value; }
        }

        public Orientation SliderOrientation
        {
            get { return (Orientation)Record.SliderOrientation; }
            set { Record.SliderOrientation = (int)value; }
        }

        /// <summary>
        /// Presentation plugins available to be selected.
        /// </summary>
        public IEnumerable<SelectListItem> AvailablePlugins { get; set; } // used on editor

        public IEnumerable<SelectListItem> AvailableRequestTypes { get; set; } // used on editor

        public IEnumerable<SelectListItem> AvailableThumbSizes { get; set; } // used on editor

        public IEnumerable<SelectListItem> AvailableOrientations { get; set; } // used on editor

        public IEnumerable<SelectListItem> AvailableSliderTypes { get; set; } // used on editor
    }
}