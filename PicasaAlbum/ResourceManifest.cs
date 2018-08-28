using Orchard.UI.Resources;

namespace PicasaAlbum
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            builder.Add().DefineStyle("slimbox2").SetUrl("slimbox/slimbox2.css");
            builder.Add().DefineScript("picasaAlbum").SetDependencies("jquery").SetUrl("picasaAlbum.js");
            builder.Add().DefineScript("slimbox2").SetDependencies("jquery").SetUrl("slimbox/slimbox2.js");
            builder.Add().DefineScript("wookmark").SetDependencies("jquery").SetUrl("wookmark/jquery.wookmark.js");
            builder.Add().DefineScript("jssor").SetDependencies("jquery").SetUrl("Jssor/jssor.slider.mini.js");
            builder.Add().DefineScript("jssor-27").SetDependencies("jquery").SetUrl("Jssor/jssor.slider-27.0.3.min.js");
            builder.Add().DefineScript("jqueryRoyalSlider").SetDependencies("jquery").SetUrl("royalslider/jquery.royalslider.min.js");
            builder.Add().DefineScript("jsrender").SetDependencies("jquery").SetUrl("jsrender.min.js");
            builder.Add().DefineScript("jqueryImagesLoaded").SetDependencies("jquery").SetUrl("jquery.imagesloaded.js");
            builder.Add().DefineScript("jqueryColorbox").SetDependencies("jquery").SetUrl("jquery.colorbox-min.js");
            builder.Add().DefineScript("jqueryFlexslider").SetDependencies("jquery").SetUrl("Flexslider/jquery.flexslider.js");
            builder.Add().DefineScript("magnific").SetDependencies("jquery").SetUrl("Magnific/jquery.magnific-popup.min.js");

            builder.Add().DefineStyle("royalslider").SetUrl("royalslider/royalslider.css?v=1.0.4");
            builder.Add().DefineStyle("rsdefault").SetUrl("royalslider/rs-default.css");
            builder.Add().DefineStyle("bootstrap").SetUrl("bootstrap.min.css");
            builder.Add().DefineStyle("colorbox").SetUrl("colorbox.css");
            builder.Add().DefineStyle("flexsliderdemo").SetUrl("Flexslider/demo.css");
            builder.Add().DefineStyle("flexslider").SetUrl("Flexslider/flexslider.css");
            builder.Add().DefineStyle("magnific").SetUrl("magnific/magnific-popup.css");
        }
    }
}