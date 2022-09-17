using System;
using System.Drawing;
using System.Windows.Forms;

namespace GodWaitingSoundApplication
{
    class MainEditor : Form
    {
        public const string DATA_FOLDER = /*@"E:\Documents\Hafaka Sounds\Final Sounds\";*//*@"../Data/"*/"Sounds/";

        public static readonly int EditorWidth = 1000;
        public static readonly int MarginX = 50;
        public static readonly int EditorHeight = 700;

        public int SceneStartHeight { get; private set; }

        Label sceneLabel;
        Button nextButton;
        Button previousButton;
        int sceneNumber = 1;

        SceneDrawer[] drawers;

        const int SCENE_COUNT = 16;

        public MainEditor()
        {
            sceneLabel = Utilities.CreateLabel("תמונה 1", 30, true);
            sceneLabel.Location = new Point(EditorWidth / 2 - (sceneLabel.Width / 2), 20);

            nextButton = Utilities.CreateButton("הבא");
            nextButton.AutoSize = true;
            nextButton.Location = new Point(sceneLabel.Location.X - nextButton.Width - 10, sceneLabel.Location.Y + sceneLabel.Height / 2);
            nextButton.Click += (obj, e) => SetScene(sceneNumber + 1);

            previousButton = Utilities.CreateButton("הקודם");
            previousButton.AutoSize = true;
            previousButton.Location = new Point(sceneLabel.Location.X + sceneLabel.Width + previousButton.Width + 10, sceneLabel.Location.Y + sceneLabel.Height / 2);
            previousButton.Visible = false;
            previousButton.Click += (obj, e) => SetScene(sceneNumber - 1);

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(EditorWidth, EditorHeight);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainWindow";
            Text = "סאונד לאלוהים מחכה בתחנה";

            Controls.Add(sceneLabel);
            Controls.Add(nextButton);
            Controls.Add(previousButton);

            SceneStartHeight = sceneLabel.Height + sceneLabel.Location.Y + 50;

            drawers = new SceneDrawer[SCENE_COUNT];
            drawers[0] = new Scene1(this);
            drawers[1] = new TextScene(this);
            drawers[2] = new SoundCollectionScene(this, DATA_FOLDER + "שיר תינוקות.mp3", "שיר תינוקות", 0.3f);
            drawers[3] = new SoundCollectionScene(this, DATA_FOLDER + "ים.mp3", "ים", 0.1f);
            drawers[4] = new SoundCollectionScene(this, new SoundInfo[]
            {
                new SoundInfo(DATA_FOLDER + "Yaels/Yael 2.mp3", "יעל"),
                new SoundInfo(DATA_FOLDER + "מטרונום (שמיניות 70).mp3", "מטרונום")
            });

            drawers[5] = new TextScene(this);
            drawers[6] = new Scene7(this);//new TextScene(this, "זה אחד די מסובך אז אני אחר כך אעשה אותו");
            drawers[7] = new Scene8(this);
            drawers[8] = new SoundCollectionScene(this, new SoundInfo[]
            {
                new SoundInfo(DATA_FOLDER + "תעליל.mp3", "תעליל", 0.8f, true, 2),
                new SoundInfo(DATA_FOLDER + "Anonimim/Anonimim 1 (Eb).mp3", "אנונימים")
            });

            drawers[9] = new Scene10(this);
            drawers[10] = new SoundCollectionScene(this, DATA_FOLDER + "ים.mp3", "ים", 0.1f); //new TextScene(this);
            drawers[11] = new SoundCollectionScene(this, DATA_FOLDER + "Anonimim/Anonimim 2 (G).mp3", "אנונימים");
            drawers[12] = new Scene13(this);
            drawers[13] = new SoundCollectionScene(this, new SoundInfo[]
            {
                new SoundInfo(DATA_FOLDER + "מטרונום.mp3", "מטרונום"),
                new SoundInfo(DATA_FOLDER + "Anonimim/Anonimim 2 (Ab).mp3", "אנונימים"),
                new SoundInfo(DATA_FOLDER + "צלצול טלפון.mp3", "צלצול טלפון", 0.5f)
            });

            drawers[14] = new SoundCollectionScene(this, new SoundInfo[]
            {
                new SoundInfo(DATA_FOLDER + "Anonimim/Anonimim 2 (Bb).mp3", "אנונימים"),
                //new SoundInfo(DATA_FOLDER + "רעש מעבר מוזר.mp3", "רעש מעבר מוזר")
                new SoundInfo(DATA_FOLDER + "צלצול טלפון.mp3", "צלצול טלפון", 0.5f)
            });
            //drawers[14].GetAudio(1).AddUIOffset(new Size(-50, 0));

            drawers[15] = new SoundCollectionScene(this, new SoundInfo[]
            {
                new SoundInfo(DATA_FOLDER + "צלצול טלפון.mp3", "צלצול טלפון", 0.5f),
                new SoundInfo(DATA_FOLDER + "Anonimim/Anonimim 2 (C2).mp3", "אנונימים 1"),
                new SoundInfo(DATA_FOLDER + "Anonimim/Anonimim 3 (C2).mp3", "אנונימים 2"),
                new SoundInfo(DATA_FOLDER + "שיר סיום.mp3", "שיר סיום", 0.5f, false)
            });

            drawers[0].Load();

            ResumeLayout(false);
            PerformLayout();
        }

        void SetScene(int number)
        {
            if (number > SCENE_COUNT || number < 1)
                return;

            drawers[sceneNumber - 1].Unload();
            drawers[number - 1].Load();

            if (number == SCENE_COUNT)
                nextButton.Visible = false;
            else
                nextButton.Visible = true;

            if (number == 1)
                previousButton.Visible = false;
            else
                previousButton.Visible = true;

            sceneNumber = number;
            sceneLabel.Text = "תמונה " + number;
        }

        protected override void OnClosed(EventArgs e)
        {
            foreach (SceneDrawer drawer in drawers)
                drawer.Dispose();
        }
    }
}