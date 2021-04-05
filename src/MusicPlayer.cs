using System;
using System.IO;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using WMPLib;

namespace MusicToCodeBy
{
    public class MusicPlayer
    {
        private readonly WindowsMediaPlayer _player;

        public MusicPlayer()
        {
            _player = new();
            General.Saved += OnOptionsSaved;
        }

        public bool IsPlaying => _player.playState == WMPPlayState.wmppsPlaying;

        public void Play()
        {
            if (_player.playState != WMPPlayState.wmppsPaused)
            {
                _player.currentPlaylist = GeneratePlaylist();
                _player.settings.volume = General.Instance.Volume;
            }

            _player.controls.play();
        }

        public void Pause()
        {
            _player.controls.pause();
        }

        public void Stop()
        {
            _player.controls.stop();
        }

        public void VolumeUp()
        {
            _player.settings.volume = Math.Min(100, _player.settings.volume + 5);

            General.Instance.Volume = _player.settings.volume;
            General.Instance.Save();
        }

        public void VolumeDown()
        {
            _player.settings.volume = Math.Max(1, _player.settings.volume - 5);
            General.Instance.Volume = _player.settings.volume;
            General.Instance.Save();
        }

        private IWMPPlaylist GeneratePlaylist()
        {
            IWMPPlaylistArray playlists = _player.playlistCollection.getByName(nameof(MusicPlayer));
            IWMPPlaylist playlist;

            if (playlists.count > 1)
            {
                playlist = playlists.Item(0);
                playlist.clear();
            }
            else
            {
                playlist = _player.playlistCollection.newPlaylist(nameof(MusicPlayer));
            }

            foreach (var file in Directory.EnumerateFiles(General.Instance.MusicFolder, "*.mp3"))
            {
                IWMPMedia media = _player.newMedia(file);
                playlist.appendItem(media);
            }

            return playlist;
        }
        
        public void NextTrack()
        {
            _player.controls.next();
        }

        public void PreviousTrack()
        {
            _player.controls.previous();
        }

        private void OnOptionsSaved(object sender, General e)
        {
            _player.settings.volume = e.Volume;
            VS.Notifications.SetStatusbarTextAsync($"Music volume set to {e.Volume}").FireAndForget();
        }
    }
}
