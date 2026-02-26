using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public AudioSource music;

    void Start()
    {
        slider.value = music.volume; 
        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        music.volume = value;
    }
}