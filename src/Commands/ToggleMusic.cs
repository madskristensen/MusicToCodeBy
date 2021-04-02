using System;
using System.CodeDom;
using System.Threading.Tasks;
using System.Windows.Forms;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace MusicToCodeBy
{
    internal sealed class ToggleMusic : BaseCommand<ToggleMusic>
    {
        private readonly MusicPlayer _player = MusicToCodeByPackage.player;

        public ToggleMusic() : base(PackageGuids.guidMusicToCodeByPackageCmdSet, PackageIds.PlayPause)
        { }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            if (_player.IsPlaying)
            {
                Command.Checked = false;
                _player.Pause();
                return;
            }

            if (!await IsMusicFolderDefinedAsync())
            {
                return;
            }

            Command.Checked = true;
            _player.Start();
        }

        private async Task<bool> IsMusicFolderDefinedAsync()
        {
            General options = await General.GetLiveInstanceAsync();

            if (!string.IsNullOrEmpty(options.MusicFolder))
            {
                return true;
            }
            else
            {
                var dialog = new FolderBrowserDialog();
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    options.MusicFolder = dialog.SelectedPath;
                    await options.SaveAsync();
                    return true;
                }
            }

            return false;
        }
    }
}
