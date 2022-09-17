using System;
using System.Collections.Generic;

namespace GodWaitingSoundApplication
{
    abstract class SceneDrawer : IDisposable
    {
        protected const string DATA_FOLDER = MainEditor.DATA_FOLDER;

        List<PlayableAudio> audioSources;
        MainEditor form;

        protected int CurrentAudioHeight { get; private set; }

        public SceneDrawer(MainEditor form)
        {
            audioSources = new List<PlayableAudio>();
            this.form = form;
            CurrentAudioHeight = form.SceneStartHeight;
        }

        protected PlayableAudio AddAudio(string path, string labelText, bool resetOnStopClick)
        {
            PlayableAudio audio = new PlayableAudio(path, labelText, resetOnStopClick, CurrentAudioHeight, form);
            CurrentAudioHeight += 50;
            audioSources.Add(audio);
            return audio;
        }

        public PlayableAudio GetAudio(int index)
        {
            return audioSources[index];
        }
        
        public virtual void Dispose()
        {
            foreach (PlayableAudio audio in audioSources)
                audio.Dispose();
        }

        public virtual void Load()
        {
            foreach (PlayableAudio audio in audioSources)
                audio.Load();
        }

        public virtual void Unload()
        {
            foreach (PlayableAudio audio in audioSources)
                audio.Unload();
        }
    }
}