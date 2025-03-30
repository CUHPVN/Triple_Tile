using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilderUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        if(transform.name == "MusicSlider")
        {
            SoundManager.Instance.LoadMusicSlider(GetComponent<UnityEngine.UI.Slider>());
            GetComponent<UnityEngine.UI.Slider>().value = SoundManager.Instance.musicVolume;
        }
        else if (transform.name == "SoundSlider")
        {
            SoundManager.Instance.LoadSFXSlider(GetComponent<UnityEngine.UI.Slider>());
            GetComponent<UnityEngine.UI.Slider>().value = SoundManager.Instance.effectsVolume;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
