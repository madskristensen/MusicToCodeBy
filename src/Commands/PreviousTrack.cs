using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace MusicToCodeBy
{
    internal sealed class PreviousTrack : BaseCommand<PreviousTrack>
    {
        private static MusicPlayer _player;

        public PreviousTrack()
            : base(PackageGuids.CommandSet, PackageIds.PreviousTrack) { }

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
            player.PreviousTrack();
        }
    }
}
