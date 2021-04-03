using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace MusicToCodeBy
{
    internal sealed class PlayPause : BaseCommand<PlayPause>
    {
        private static MusicPlayer _player;

        public PlayPause()
            : base(PackageGuids.CommandSet, PackageIds.PlayPause) { }

        public static Task InitializeAsync(AsyncPackage package, MusicPlayer player)
        {
            _player = player;
            return InitializeAsync(package);
        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            MusicPlayer player = _player;

            if (player.IsPlaying)
            {
                Command.Checked = false;
                player.Pause();
                return;
            }

            General options = await General.GetLiveInstanceAsync();

            if (string.IsNullOrEmpty(options.MusicFolder) && !await PickFolder.TryPickFolderAsync())
            {
                return;
            }

            Command.Checked = true;
            player.Play();
        }
    }
}
