using System;
using Community.VisualStudio.Toolkit;

namespace MusicToCodeBy
{
    internal sealed class VolumeUp : BaseCommand<VolumeUp>
    {
        public VolumeUp() : base(PackageGuids.CommandSet, PackageIds.VolumeUp)
        { }

        protected override void Execute(object sender, EventArgs e)
        {
            MusicToCodeByPackage.Player.VolumeUp();
        }
    }
}
