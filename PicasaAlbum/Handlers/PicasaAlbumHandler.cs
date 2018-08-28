using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicasaAlbum.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace PicasaAlbum.Handlers
{
    public class PicasaAlbumHandler : ContentHandler
    {
        public PicasaAlbumHandler(IRepository<PicasaAlbumWidgetRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}