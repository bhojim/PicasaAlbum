using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using PicasaAlbum.Models;

namespace PicasaAlbum
{
    public class Migrations : DataMigrationImpl
    {

        public int Create()
        {
            // Creating table PicasaAlbumWidgetRecord
            SchemaBuilder.CreateTable("PicasaAlbumWidgetRecord", table => table
                .ContentPartRecord()
                .Column<string>("UserName")
                .Column<string>("AlbumName")
                .Column<int>("SelectedPlugin")
            );

            ContentDefinitionManager.AlterPartDefinition(
                typeof(PicasaAlbumWidgetRecord).Name, cfg => cfg.Attachable());

            // Create a new widget content type with our map
            ContentDefinitionManager.AlterTypeDefinition("PicasaAlbumWidget", cfg => cfg
                .WithPart("PicasaAlbumWidgetPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 1;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.AlterTable("PicasaAlbumWidgetRecord", table => table
                .AddColumn<int>("SelectedRequestType")
            );
            return 2;
        }

        public int UpdateFrom2()
        {
            SchemaBuilder
                .AlterTable("PicasaAlbumWidgetRecord", table => table.AddColumn<int>("TopicName"))
                .AlterTable("PicasaAlbumWidgetRecord", table => table.AddColumn<int>("MaxImages"))
                .AlterTable("PicasaAlbumWidgetRecord", table => table.AddColumn<int>("ThumbSize"))
                .AlterTable("PicasaAlbumWidgetRecord", table => table.AddColumn<int>("PhotoSize"))
                .AlterTable("PicasaAlbumWidgetRecord", table => table.AddColumn<int>("SliderOrientation"));
            return 3;

        }

        public int UpdateFrom3()
        {
            SchemaBuilder
                .AlterTable("PicasaAlbumWidgetRecord", table => table.AddColumn<int>("SliderType"));
            return 4;
        }

        public int UpdateFrom4()
        {
            return 5;
        }
        //    // Skin: Light, Dark-Default, Universal
        //    //Size and Scaling:  Orientation, Width, Height
        //    //Image options
        //    // Thumbnails, bullets, tabs
        //    // Arrows
        //    // Autoplay
        //    // Fullscreen
        //    // Caption
        //    // Nearby slides
        //    // Video
        //    // Deep Linking (Permalinks)
        //    // Miscellaneous

        //    SchemaBuilder
        //        .AlterTable("PicasaAlbumWidgetRecord", table => table.AddColumn<int>("SliderOrientation"))
        //        .AlterTable("PicasaAlbumWidgetRecord", table => table.AddColumn<int>("SliderWidth"))
        //        .AlterTable("PicasaAlbumWidgetRecord", table => table.AddColumn<int>("SliderHeight"))
        //        ;
        //    return 3;
        //}
    }
}