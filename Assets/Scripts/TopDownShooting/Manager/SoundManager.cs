using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooting
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField][Range(0f, 1f)] private float _soundEffectVolume;
        [SerializeField][Range(0f, 1f)] private float _soundEffectPitchVariance;
        [SerializeField][Range(0f, 1f)] private float _musicVolume;

        private AudioSource _musicAudioSource;
        public AudioClip MusicClip;

        public SoundSource SoundSourcePrefab;

        private void Awake()
        {
            Instance = this;

            _musicAudioSource = GetComponent<AudioSource>();
            _musicAudioSource.volume = _musicVolume;
            _musicAudioSource.loop = true;
        }

        private void Start()
        {
            ChangeBackGroundMusic(MusicClip);
        }

        public void ChangeBackGroundMusic(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            _musicAudioSource.Stop();
            _musicAudioSource.clip = clip;
            _musicAudioSource.Play();
        }

        public static void PlayClip(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            SoundSource obj = Instantiate(Instance.SoundSourcePrefab);
            SoundSource soundSource = obj.GetComponent<SoundSource>();
            soundSource.Play(clip, Instance._soundEffectVolume, Instance._soundEffectPitchVariance);
        }
    }
}
