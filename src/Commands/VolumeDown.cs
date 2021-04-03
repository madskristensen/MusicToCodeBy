using System;
using Community.VisualStudio.Toolkit;

namespace MusicToCodeBy
{
    internal sealed class VolumeDown : BaseCommand<VolumeDown>
    {
        public VolumeDown() : base(PackageGuids.CommandSet, PackageIds.VolumeDown)
        { }

        protected override void Execute(object sender, EventArgs e)
        {
            MusicToCodeByPackage.Player.VolumeDown();
        }
    }
}
