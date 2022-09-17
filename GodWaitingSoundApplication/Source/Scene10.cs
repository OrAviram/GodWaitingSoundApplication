using System;

namespace GodWaitingSoundApplication
{
    class Scene10 : SceneDrawer
    {
        PlayableAudio anonimim1;
        PlayableAudio anonimim2;
        bool changingAnonimim1 = false;

        public Scene10(MainEditor form) : base(form)
        {
            anonimim1 = AddAudio(DATA_FOLDER + "Anonimim/Anonimim 1 (F).mp3", "אנונימים 1", true);
            anonimim2 = AddAudio(DATA_FOLDER + "Anonimim/Anonimim 2 (F).mp3", "אנונימים 2", true);
            AddAudio(DATA_FOLDER + "נויז.mp3", "נויז", true);

            anonimim1.volumeBar.ValueChanged += Anonimim1Change;
            anonimim2.volumeBar.ValueChanged += Anonimim2Change;
        }

        private void Anonimim1Change(object sender, EventArgs e)
        {
            changingAnonimim1 = true;
            anonimim2.Volume = 1 - anonimim1.Volume;
            changingAnonimim1 = false;
        }

        private void Anonimim2Change(object sender, EventArgs e)
        {
            if (!changingAnonimim1)
                anonimim1.Volume = 1 - anonimim2.Volume;
        }
    }
}