using System.ComponentModel;
using Community.VisualStudio.Toolkit;

namespace MusicToCodeBy
{
    internal partial class OptionsProvider
    {
        // Register the options with these attributes on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "MusicToCodeBy", "General", 0, 0, true)]
        // [ProvideProfile(typeof(OptionsProvider.GeneralOptions), "MusicToCodeBy", "General", 0, 0, true)]
        public class GeneralOptions : BaseOptionPage<General> { }
    }

    public class General : BaseOptionModel<General>
    {
        [Category("General")]
        [DisplayName("Music Folder")]
        [Description("A folder on disk containing .mp3 files.")]
        [DefaultValue("")]
        public string MusicFolder { get; set; } = "";

        [Category("General")]
        [DisplayName("Volume")]
        [Description("Set the volume between 0 and 100.")]
        [DefaultValue(10)]
        public int Volume { get; set; } = 10;
    }
}
