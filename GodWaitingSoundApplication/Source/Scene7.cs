using System;
using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;

namespace GodWaitingSoundApplication
{
    class Scene7 : SceneDrawer
    {
        PlayableAudio anonimim;

        Mp3FileReader[] yaelSources;
        LoopedStream[] yaelLoops;
        VolumeWaveProvider16[] yaelProviders;

        Button yaelPlay;
        Button yaelNext;
        Button yaelPrevious;
        TrackBar yaelVolumeBar;
        Label mainLabel;
        Label volumeLabel;
        Label currentYaelLabel;

        int currentYael;
        WaveOutEvent e;

        public Scene7(MainEditor form) : base(form)
        {
            anonimim = AddAudio(DATA_FOLDER + "Anonimim/Anonimim 1 (D).mp3", "אנונימים", true);

            yaelSources = new Mp3FileReader[5];
            yaelSources[0] = new Mp3FileReader(DATA_FOLDER + "Yaels/Yael 1.mp3");
            yaelSources[4] = new Mp3FileReader(DATA_FOLDER + "Yaels/Yael 2.mp3");
            for (int i = 1; i <= 3; i++)
                yaelSources[i] = new Mp3FileReader(DATA_FOLDER + "Yaels/Yael Transition " + i + ".mp3");

            yaelLoops = new LoopedStream[5];
            for (int i = 0; i < 5; i++)
                yaelLoops[i] = new LoopedStream(yaelSources[i]);

            yaelProviders = new VolumeWaveProvider16[5];
            for (int i = 0; i < 5; i++)
            {
                yaelProviders[i] = new VolumeWaveProvider16(yaelLoops[i]);
                yaelProviders[i].Volume = 0;
            }

            currentYaelLabel = Utilities.CreateLabel("יעל", 14);
            currentYaelLabel.Location = new Point(form.Width / 2, form.SceneStartHeight + anonimim.button.Height + 50);

            yaelNext = Utilities.CreateButton("הבא");
            yaelNext.Location = new Point(currentYaelLabel.Location.X - yaelNext.Width - 10, currentYaelLabel.Location.Y);
            yaelNext.Click += AdvanceYael;

            yaelPrevious = Utilities.CreateButton("הקודם");
            yaelPrevious.Location = new Point(currentYaelLabel.Location.X + currentYaelLabel.Size.Width + 10, currentYaelLabel.Location.Y);
            yaelPrevious.Click += ReturnYael;

            yaelPlay = Utilities.CreateButton("נגן");
            yaelPlay.Click += PlayYael;
            yaelPlay.Location = new Point(
                currentYaelLabel.Location.X + currentYaelLabel.Width/* / 2 - yaelPlay.Width / 2*/,
                currentYaelLabel.Location.Y + currentYaelLabel.Height + yaelPlay.Height + 10);

            mainLabel = Utilities.CreateLabel("סאונד משתנה", 16);
            mainLabel.Location = new Point(20 + Math.Max(yaelPlay.Location.X + yaelPlay.Width, yaelPrevious.Location.X + yaelPrevious.Width), (yaelPlay.Location.Y + yaelPrevious.Location.Y) / 2);
            mainLabel.Visible = false;

            yaelVolumeBar = new TrackBar();
            yaelVolumeBar.Size = new Size(form.Width / 3, yaelPlay.Height);
            yaelVolumeBar.Location = new Point(yaelPlay.Location.X - yaelVolumeBar.Width, yaelPlay.Location.Y);
            yaelVolumeBar.Visible = false;
            yaelVolumeBar.ValueChanged += YaelVolumeChanged;
            yaelVolumeBar.Maximum = PlayableAudio.VOLUME_BAR_VALUE_COUNT;

            volumeLabel = Utilities.CreateLabel("0.0", 13);
            volumeLabel.Location = new Point(yaelVolumeBar.Location.X - volumeLabel.Width, yaelVolumeBar.Location.Y);

            form.Controls.Add(volumeLabel);
            form.Controls.Add(yaelVolumeBar);
            form.Controls.Add(mainLabel);
            form.Controls.Add(currentYaelLabel);
            form.Controls.Add(yaelNext);
            form.Controls.Add(yaelPrevious);
            form.Controls.Add(yaelPlay);
            
            UnloadYael();

            e = new WaveOutEvent();
            e.Init(yaelProviders[0]);
        }

        private void YaelVolumeChanged(object sender, EventArgs e)
        {
            float volume = yaelVolumeBar.Value / (float)PlayableAudio.VOLUME_BAR_VALUE_COUNT;
            yaelProviders[currentYael].Volume = volume;
            volumeLabel.Text = volume.ToString();
        }

        private void PlayYael(object sender, EventArgs ee)
        {
            if (e.PlaybackState == PlaybackState.Playing)
            {
                yaelPlay.Text = "נגן";
                e.Pause();
            }
            else
            {
                yaelPlay.Text = "עצור";
                e.Play();
            }
        }

        void SetYael(int newYael)
        {
            bool playing = e.PlaybackState == PlaybackState.Playing;
            float volume = yaelProviders[currentYael].Volume;
            yaelLoops[currentYael].Position = 0;

            currentYael = newYael;
            yaelProviders[currentYael].Volume = volume;
            e.Dispose();
            e.Init(yaelProviders[currentYael]);
            if (playing)
                e.Play();

            currentYaelLabel.Text = "יעל (" + (newYael + 1).ToString() + "/5)";
        }

        private void ReturnYael(object sender, EventArgs ee)
        {
            if (currentYael == 0)
                SetYael(4);
            else
                SetYael(currentYael - 1);
        }

        private void AdvanceYael(object sender, EventArgs ee)
        {
            if (currentYael == 4)
                SetYael(0);
            else
                SetYael(currentYael + 1);
        }

        public override void Load()
        {
            base.Load();
            yaelPlay.Visible = true;
            yaelNext.Visible = true;
            yaelPrevious.Visible = true;
            mainLabel.Visible = true;
            yaelVolumeBar.Visible = true;
            volumeLabel.Visible = true;
            currentYaelLabel.Visible = true;

            SetYael(0);
            yaelPlay.Text = "נגן";
            yaelVolumeBar.Value = 0;
            foreach (var provider in yaelProviders)
                provider.Volume = 0;
        }

        void UnloadYael()
        {
            yaelPlay.Visible = false;
            yaelNext.Visible = false;
            yaelPrevious.Visible = false;
            mainLabel.Visible = false;
            yaelVolumeBar.Visible = false;
            volumeLabel.Visible = false;
            currentYaelLabel.Visible = false;
        }

        public override void Unload()
        {
            base.Unload();

            UnloadYael();
            e.Stop();
            foreach (var loop in yaelLoops)
                loop.Position = 0;
        }

        public override void Dispose()
        {
            base.Dispose();
            yaelPlay.Dispose();
            yaelNext.Dispose();
            yaelPrevious.Dispose();
            yaelVolumeBar.Dispose();
            volumeLabel.Dispose();
            currentYaelLabel.Dispose();
        }
    }
}