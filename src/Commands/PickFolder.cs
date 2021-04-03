using System.Threading.Tasks;
using System.Windows.Forms;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace MusicToCodeBy
{
    internal sealed class PickFolder : BaseCommand<PickFolder>
    {
        public PickFolder() : base(PackageGuids.CommandSet, PackageIds.PickFolder)
        { }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            if (await TryPickFolderAsync() && MusicToCodeByPackage.Player.IsPlaying)
            {
                MusicToCodeByPackage.Player.Stop();
                MusicToCodeByPackage.Player.Play();
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
