using System;
using System.Drawing;
using System.Windows.Forms;

namespace GodWaitingSoundApplication
{
    class Scene8 : SceneDrawer
    {
        PlayableAudio trafficJam;
        PlayableAudio yael1;
        Label b;

        float previousVolume;

        public Scene8(MainEditor form) : base(form)
        {
            trafficJam = AddAudio(DATA_FOLDER + "פקק תנועה.mp3", "פקק תנועה", true);
            yael1 = AddAudio(DATA_FOLDER + "Yaels/Yael 1.mp3", "יעל", false);

            b = Utilities.CreateLabel("זה עדיין לא בדיוק מוכן כי זה קצת מסובך", 16);
            b.Location = new Point(form.Width / 2 - b.Width / 2, form.Height / 2 - b.Height / 2);
            form.Controls.Add(b);
            b.Visible = false;

            yael1.volumeBar.ValueChanged += YaelVolumeChanged;
        }

        private void YaelVolumeChanged(object sender, EventArgs e)
        {
            trafficJam.Volume = Utilities.Clamp(0, 1, 0.4f / 0.3f * (0.3f - yael1.Volume));//Utilities.Clamp(0, 1, trafficJam.Volume + (previousVolume - yael1.Volume));
            previousVolume = yael1.Volume;
        }

        public override void Load()
        {
            base.Load();
            previousVolume = 0;
            //b.Visible = true;
        }

        public override void Unload()
        {
            base.Unload();
            b.Visible = false;
        }
    }
}