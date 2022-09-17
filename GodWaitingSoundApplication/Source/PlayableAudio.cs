using System;
using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;

namespace GodWaitingSoundApplication
{
    class PlayableAudio : IDisposable
    {
        public const int VOLUME_BAR_VALUE_COUNT = 200;

        public readonly Button button;
        public readonly Label label;
        public readonly Label volumeLabel;
        public readonly TrackBar volumeBar;

        Mp3FileReader source;
        LoopedStream loop;
        VolumeWaveProvider16 waveProvider;
        WaveOutEvent e;
        bool reset;

        public float internalVolumeMultiplier = 1;

        public float Volume
        {
            get => waveProvider.Volume / internalVolumeMultiplier;
            set => volumeBar.Value = (int)(value * VOLUME_BAR_VALUE_COUNT);
        }

        public PlayableAudio(string path, string labelText, bool resetOnStopClick, int y, MainEditor form, float volumeMultiplier = 1)
        {
            label = Utilities.CreateLabel(labelText, 16);
            label.Location = new Point(MainEditor.EditorWidth - MainEditor.MarginX - label.Width, y);
            label.Visible = false;

            button = Utilities.CreateButton("נגן");
            button.AutoSize = true;
            button.Location = new Point(label.Location.X - button.Size.Width - 10, y);
            button.Visible = false;
            button.Click += OnButtonClicked;

            volumeBar = new TrackBar();
            volumeBar.Size = new Size(form.Width / 3, button.Height);
            volumeBar.Location = new Point(button.Location.X - volumeBar.Width, y);
            volumeBar.Visible = false;
            volumeBar.ValueChanged += VolumeChanged;
            volumeBar.Maximum = VOLUME_BAR_VALUE_COUNT;

            volumeLabel = Utilities.CreateLabel("0.000", 16);
            volumeLabel.Location = new Point(volumeBar.Location.X - volumeLabel.Width, y);
            volumeLabel.Visible = false;
            volumeLabel.Text = "0";

            form.Controls.Add(label);
            form.Controls.Add(button);
            form.Controls.Add(volumeBar);
            form.Controls.Add(volumeLabel);

            source = new Mp3FileReader(path);
            loop = new LoopedStream(source);
            waveProvider = new VolumeWaveProvider16(loop);
            e = new WaveOutEvent();
            e.Init(waveProvider);

            waveProvider.Volume = 0;
            reset = resetOnStopClick;
        }

        public void AddUIOffset(Size amount)
        {
            button.Location += amount;
            label.Location += amount;
            volumeBar.Location += amount;
            volumeLabel.Location += amount;
        }

        private void VolumeChanged(object sender, EventArgs e)
        {
            float value = volumeBar.Value / (float)VOLUME_BAR_VALUE_COUNT;
            waveProvider.Volume = value * internalVolumeMultiplier;
            volumeLabel.Text = value.ToString();
        }

        private void OnButtonClicked(object sender, EventArgs eArgs)
        {
            if (e.PlaybackState == PlaybackState.Playing)
            {
                button.Text = "נגן";
                if (reset)
                {
                    e.Stop();
                    loop.Position = 0;
                }
                else
                    e.Pause();
            }
            else
            {
                button.Text = "עצור";
                e.Play();
            }
        }

        public void Load()
        {
            button.Visible = true;
            button.Text = "נגן";
            label.Visible = true;
            volumeBar.Visible = true;
            volumeLabel.Visible = true;

            Volume = 0;
        }

        public void Unload()
        {
            button.Visible = false;
            label.Visible = false;
            volumeBar.Visible = false;
            volumeLabel.Visible = false;

            e.Stop();
            loop.Position = 0;
        }

        public void Dispose()
        {
            e.Dispose();
            loop.Dispose();
            source.Dispose();
        }
    }
}