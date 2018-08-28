using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicasaAlbum.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace PicasaAlbum.Drivers
{
    public class PicasaAlbumDriver : ContentPartDriver<PicasaAlbumWidgetPart>
    {
        protected override DriverResult Display(PicasaAlbumWidgetPart part, string displayType, dynamic shapeHelper)
        {
                return ContentShape("Parts_PicasaAlbum", () => shapeHelper.Parts_PicasaAlbum(
                    UserName: part.UserName,
                    AlbumName: part.AlbumName,
                    SelectedPlugin: part.SelectedPlugin,
                    TopicName: part.TopicName,
                    SliderType: part.SliderType,
                    SelectedRequestType: part.SelectedRequestType,
                    MaxImages: part.MaxImages,
                    ThumbSize: part.ThumbSize,
                    PhotoSize: part.PhotoSize,
                    SliderOrientation: part.SliderOrientation
                ));
        }

        // GET
        protected override DriverResult Editor(PicasaAlbumWidgetPart part, dynamic shapeHelper)
        {
            part.AvailablePlugins = Enum.GetNames(typeof(Plugin))
                .Select(o => new SelectListItem
                {
                    Text = o,
                    Value = o
                });
            part.AvailableRequestTypes = Enum.GetNames(typeof(RequestType))
                .Select(o => new SelectListItem
                {
                    Text = o,
                    Value = o
                });
            part.AvailableSliderTypes = Enum.GetNames(typeof(SliderTypeEnum))
                .Select(o => new SelectListItem
                {
                    Text = o,
                    Value = o
                });
            part.AvailableThumbSizes = Enum.GetNames(typeof(ThumbSize))
                .Select(o => new SelectListItem
                {
                    Text = o,
                    Value = o
                });
            part.AvailableOrientations = Enum.GetNames(typeof(Orientation))
                .Select(o => new SelectListItem
                {
                    Text = o,
                    Value = o
                });

            return ContentShape("Parts_PicasaAlbum_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/PicasaAlbum",
                    Model: part,
                    Prefix: Prefix));
        }

        //POST
        protected override DriverResult Editor(
            PicasaAlbumWidgetPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

    }
}