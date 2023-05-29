namespace GibbousTetris
{
    public class AudioManager
    {
        private static AudioManager? _instance;
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AudioManager();
                }

                return _instance;
            }
        }

        private float _soundEffectsVolume;
        private float _backgroundMusicsVolume;

        public float SoundVolume
        {
            get
            {
                if (_soundEffectsVolume < 0)
                {
                    _soundEffectsVolume = 0;
                }
                else if (_soundEffectsVolume > 1)
                {
                    _soundEffectsVolume = 1;
                }
                return _soundEffectsVolume;
            }
            set => _soundEffectsVolume = value;
        }
        public float MusicVolume
        {
            get
            {
                if (_backgroundMusicsVolume < 0)
                {
                    _backgroundMusicsVolume = 0;
                }
                else if (_backgroundMusicsVolume > 1)
                {
                    _backgroundMusicsVolume = 1;
                }
                return _backgroundMusicsVolume;
            }
            set => _backgroundMusicsVolume = value;
        }

        private AudioManager() 
        {
            this.SoundVolume = 0.75f;
            this.MusicVolume = 0.25f;
        }

        public void SaveVolumesTo()
        {
            StreamWriter writer = new StreamWriter(Constants.AUDIO_TEXT_FOLRDER_LOCATION);
            try
            {
                writer.WriteLine(this.SoundVolume);
                writer.WriteLine(this.MusicVolume);
            }
            finally 
            { 
                writer.Close(); 
            }
        }
        public void LoadVolumesFrom() 
        {
            StreamReader reader = new StreamReader(Constants.AUDIO_TEXT_FOLRDER_LOCATION);
            try
            {
                this.SoundVolume = reader.ReadFloat();
                this.MusicVolume = reader.ReadFloat();
            }
            finally 
            {
                reader.Close(); 
            }
        }
    }
}