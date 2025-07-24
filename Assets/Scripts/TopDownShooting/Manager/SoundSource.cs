using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooting
{
    public class SoundSource : MonoBehaviour
    {
        private AudioSource _audioSource;

        public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
        {
            if(_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }

            CancelInvoke();
            _audioSource.clip = clip;
            _audioSource.volume = soundEffectVolume;
            _audioSource.Play();
            _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

            Invoke("Disable", clip.length + 2);
        }

        public void Disable()
        {
            _audioSource?.Stop();
            Destroy(gameObject);
        }
    }
}
