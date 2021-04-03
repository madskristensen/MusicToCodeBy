using System;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace MusicToCodeBy
{
    internal sealed class VolumeUp : BaseCommand<VolumeUp>
    {
        private static MusicPlayer _player;

        public VolumeUp() 
            : base(PackageGuids.CommandSet, PackageIds.VolumeUp) { }

        public static Task InitializeAsync(AsyncPackage package, MusicPlayer player)
        {
            _player = player;
            return InitializeAsync(package);
        }

        protected override void Execute(object sender, EventArgs e)
        {
            _player.VolumeUp();
        }
    }
}
