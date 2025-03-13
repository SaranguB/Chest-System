using System;
using UnityEngine;

namespace ChestSystem.Sound
{


    public class SoundService : MonoBehaviour
    {


        [SerializeField]
        private AudioSource soundEffect;
        [SerializeField]
        private AudioSource soundMusic;
        [SerializeField]
        private SoundType[] sounds;


        private void Start()
        {
            Play(Sounds.Music);
        }

        public void Play(Sounds sound)
        {
            SoundType soundType = GetSoundType(sound);

            if (soundType != null)
            {
                soundMusic.clip = soundType.soundClip;
                soundMusic.volume = soundType.volume / 100f;
                soundMusic.Play();
            }
        }

        public void PlaySound(Sounds sound)
        {
            SoundType soundType = GetSoundType(sound);

            if (soundType != null)
            {
                soundEffect.volume = soundType.volume / 100f;
                soundEffect.PlayOneShot(soundType.soundClip);
            }
        }

        private SoundType GetSoundType(Sounds sound)
        {
            SoundType type = Array.Find(sounds, i => i.sound == sound);

            return type;
        }
    }

    [Serializable]
    public class SoundType
    {
        public Sounds sound;
        public AudioClip soundClip;

        [Range(1, 100)]
        public int volume;
    }

    public enum Sounds
    {
        Music,
        ChestPressedSound,
        ChestOpen,
        ChestGenerated,
        ButtonPressedSound,
        PopUpSound,
        ChestUnlocked
    }
}