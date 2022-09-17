using System;
using System.Drawing;
using System.Windows.Forms;

namespace GodWaitingSoundApplication
{
    class Scene13 : SceneDrawer
    {
        PlayableAudio trafficJam;
        PlayableAudio yael2;
        CheckBox connect;
        Label b;

        float previousVolume;

        public Scene13(MainEditor form) : base(form)
        {
            trafficJam = AddAudio(DATA_FOLDER + "פקק תנועה.mp3", "פקק תנועה", true);
            yael2 = AddAudio(DATA_FOLDER + "Yaels/Yael 2.mp3", "יעל", false);
            AddAudio(DATA_FOLDER + "נויז.mp3", "נויז", true);

            connect = new CheckBox();
            connect.Size = new Size(20, 20);
            connect.Location = new Point(form.Width / 4, (yael2.button.Location.Y + trafficJam.button.Location.Y) / 2);
            form.Controls.Add(connect);
            connect.Checked = true;
            connect.Visible = false;
            //connect.Text = "extreme fun";

            b = Utilities.CreateLabel("זה עדיין לא בדיוק מוכן כי זה קצת מסובך (הכזה בצד הוא לחיבור הסאונדים)", 16);
            b.Location = new Point(form.Width / 2 - b.Width - 250, form.Height / 2 - b.Height / 2);
            form.Controls.Add(b);
            b.Visible = false;

            yael2.volumeBar.ValueChanged += YaelVolumeChanged;
        }

        private void YaelVolumeChanged(object sender, EventArgs e)
        {
            if (connect.Checked)
            {
                trafficJam.Volume = Utilities.Clamp(0, 1, 0.4f / 0.3f * (0.3f - yael2.Volume));//Utilities.Clamp(0, 1, trafficJam.Volume + (previousVolume - yael1.Volume));
                previousVolume = yael2.Volume;
            }
        }

        public override void Load()
        {
            base.Load();
            previousVolume = 0;
            //b.Visible = true;
            //connect.Visible = true;
        }

        public override void Unload()
        {
            base.Unload();
            b.Visible = false;
            connect.Visible = false;
        }
    }
}