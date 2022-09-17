using System;
using System.Drawing;

namespace GodWaitingSoundApplication
{
    class Scene1 : SceneDrawer
    {
        PlayableAudio anonimim;
        PlayableAudio restaurant;
        PlayableAudio highPitchedSound;
        float previousRestaurantVolume = 0;

        public Scene1(MainEditor form) : base(form)
        {
            anonimim = AddAudio(DATA_FOLDER + "Anonimim/Anonimim 1 (C).mp3", "אנונימים", true);
            restaurant = AddAudio(DATA_FOLDER + "מסעדה.mp3", "מסעדה", false);
            highPitchedSound = AddAudio(DATA_FOLDER + "רעש גבוה אחרי פיצוץ.mp3", "רעש של אחרי פיצוץ", false);
            
            highPitchedSound.AddUIOffset(new Size(-100, 0));

            restaurant.volumeBar.ValueChanged += RestaurantVolumeChange;
        }

        private void RestaurantVolumeChange(object sender, EventArgs e)
        {
            float change = restaurant.Volume - previousRestaurantVolume;
            if (change > 0)
                anonimim.Volume = Utilities.Clamp(0, 1, anonimim.Volume - change);

            previousRestaurantVolume = restaurant.Volume;
        }
    }
}