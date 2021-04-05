using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace MusicToCodeBy
{
    internal sealed class NextTrack : BaseCommand<NextTrack>
    {
        private static MusicPlayer _player;

        public NextTrack()
            : base(PackageGuids.CommandSet, PackageIds.NextTrack) { }

        public static Task InitializeAsync(AsyncPackage package, MusicPlayer player)
        {
            _player = player;
            return InitializeAsync(package);
        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            MusicPlayer player = _player;

            if (!player.IsPlaying)
            {
                Command.Checked = false;
                player.Play();
                return;
            }

            General options = await General.GetLiveInstanceAsync();

            if (string.IsNullOrEmpty(options.MusicFolder) && !await PickFolder.TryPickFolderAsync())
            {
                return;
            }

            Command.Checked = true;
            player.NextTrack();
        }
    }
}
