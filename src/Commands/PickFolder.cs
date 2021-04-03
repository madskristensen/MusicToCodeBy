using System.Threading.Tasks;
using System.Windows.Forms;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace MusicToCodeBy
{
    internal sealed class PickFolder : BaseCommand<PickFolder>
    {
        private static MusicPlayer _player;

        public PickFolder()
            : base(PackageGuids.CommandSet, PackageIds.PickFolder) { }

        public static Task InitializeAsync(AsyncPackage package, MusicPlayer player)
        {
            _player = player;
            return InitializeAsync(package);
        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            if (await TryPickFolderAsync() && _player.IsPlaying)
            {
                _player.Stop();
                _player.Play();
            }
        }

        public static async Task<bool> TryPickFolderAsync()
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                General options = await General.GetLiveInstanceAsync();
                options.MusicFolder = dialog.SelectedPath;
                await options.SaveAsync();

                return true;
            }

            return false;
        }
    }
}
