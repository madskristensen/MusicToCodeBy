using System;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace MusicToCodeBy
{
    internal sealed class VolumeDown : BaseCommand<VolumeDown>
    {
        private static MusicPlayer _player;

        public VolumeDown()
            : base(PackageGuids.CommandSet, PackageIds.VolumeDown) { }

        public static Task InitializeAsync(AsyncPackage package, MusicPlayer player)
        {
            _player = player;
            return InitializeAsync(package);
        }

        protected override void Execute(object sender, EventArgs e)
        {
            _player.VolumeDown();
        }
    }
}
