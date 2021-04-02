using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace MusicToCodeBy
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid("43ce9aba-807e-49a7-8de3-f2ba6d484d37")]
    [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), Vsix.Name, "General", 0, 0, true)]
    [ProvideProfile(typeof(OptionsProvider.GeneralOptions), Vsix.Name, "General", 0, 0, true)]
    public sealed class MusicToCodeByPackage : AsyncPackage
    {
        public static MusicPlayer player = new();

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            await VolumeUp.InitializeAsync(this);
            await ToggleMusic.InitializeAsync(this);
            await VolumeDown.InitializeAsync(this);
        }
    }
}