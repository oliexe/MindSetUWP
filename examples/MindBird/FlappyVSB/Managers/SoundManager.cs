using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;

namespace FlappyBird.Managers
{
    public class SoundManager
    {
        private AudioEngine _engine;
        private SoundBank _soundBank;
        private WaveBank _waveBank;

        public SoundManager()
        {
            Statics.MANAGER_SOUND = this;
        }

        public void LoadContent()
        {
            
        }

        public void Play(string sound)
        {
            try
            {
                _soundBank.PlayCue(sound);
            }
            catch { }
        }
    }
}
