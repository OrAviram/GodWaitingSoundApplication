namespace GodWaitingSoundApplication
{
    struct SoundInfo
    {
        public string path;
        public string labelText;
        public float defaultVolume;
        public bool resetOnStopClick;
        public float internalVolumeMultiplier;

        public SoundInfo(string path, string labelText, float defaultVolume = 0, bool resetOnStopClick = true, float internalVolumeMultiplier = 1)
        {
            this.path = path;
            this.labelText = labelText;
            this.defaultVolume = defaultVolume;
            this.resetOnStopClick = resetOnStopClick;
            this.internalVolumeMultiplier = internalVolumeMultiplier;
        }

        public SoundInfo(string path, string labelText, bool resetOnStopClick) : this(path, labelText, 0, resetOnStopClick)
        {
        }
    }

    class SoundCollectionScene : SceneDrawer
    {
        SoundInfo[] sounds;

        public SoundCollectionScene(MainEditor form, string path, string labelText, float defaultVolume = 0, bool resetOnStopClick = true) : base(form)
        {
            sounds = new SoundInfo[1];
            sounds[0] = new SoundInfo(path, labelText, defaultVolume, resetOnStopClick);
            Init();
        }

        public SoundCollectionScene(MainEditor form, SoundInfo[] sounds) : base(form)
        {
            this.sounds = sounds;
            Init();
        }

        void Init()
        {
            foreach (var sound in sounds)
            {
                PlayableAudio a = AddAudio(sound.path, sound.labelText, sound.resetOnStopClick);
                a.internalVolumeMultiplier = sound.internalVolumeMultiplier;
            }
        }

        public override void Load()
        {
            base.Load();
            for (int i = 0; i < sounds.Length; i++)
            {
                PlayableAudio audio = GetAudio(i);
                audio.Volume = sounds[i].defaultVolume;
            }
        }
    }
}