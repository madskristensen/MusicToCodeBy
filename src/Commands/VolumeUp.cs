using System;
using Community.VisualStudio.Toolkit;

namespace MusicToCodeBy
{
    internal sealed class VolumeUp : BaseCommand<VolumeUp>
    {
        public VolumeUp() : base(PackageGuids.guidMusicToCodeByPackageCmdSet, PackageIds.VolumeUp)
        { }

        protected override void Execute(object sender, EventArgs e)
        {
            MusicToCodeByPackage.player.VolumeUp();
        }
    }
}
