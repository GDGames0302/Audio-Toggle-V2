using UnityEngine;
using UnityEngine.UI;

namespace GDGames.Audio
{
    public class AudioToggle : MonoBehaviour
    {
        [SerializeField]
        AudioManager[] audioManagers;

        Toggle volumeToggle;


        void Awake()
        {
            GetToggleComponent();
        }

        void Start()
        {
            if (volumeToggle == null) return;

            LoadToggleValue();
        }

        void OnEnable()
        {
            if (volumeToggle == null) return;

            AddListenerToToggle();
        }

        void OnDisable()
        {
            if (volumeToggle == null) return;

            RemoveListenerFromToggle();
        }


        void GetToggleComponent()
        {
            volumeToggle = GetComponent<Toggle>();
        }

        void LoadToggleValue()
        {
            volumeToggle.isOn = audioManagers[0].GetVolume() > -20f;
        }

        void AddListenerToToggle()
        {
            volumeToggle.onValueChanged.AddListener(ToggleValueChanged);
        }

        void RemoveListenerFromToggle()
        {
            volumeToggle.onValueChanged.RemoveListener(ToggleValueChanged);
        }

        void ToggleValueChanged(bool value)
        {
            if (!value)
            {
                foreach (AudioManager audioManager in audioManagers)
                {
                    audioManager.MuteAudio();
                }
            }
            else
            {
                foreach (AudioManager audioManager in audioManagers)
                {
                    audioManager.SetVolumeToInitialMixerValue();
                }
            }
        }
    }
}