using System.Globalization;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SoundType
{
    BGM_Volume,
    SFX_Volume
}

public class testValue
{
    public int valueA;
    public string valueB;
    public float valueC;
}

public class OptionPopup : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer mixer; // 사운드의 그룹을 나누어서, 각 그룹별로 볼륨을 컨트롤해주기 위해서

    private testValue test= new testValue();

    private void Awake()
    {
        //test.valueA = 50;
        //test.valueB = "Hello World";
        //test.valueC = 50.14f;

        //string saveData = JsonUtility.ToJson(test);

        //PlayerPrefs.SetString("Test", saveData);

        //string valueS = PlayerPrefs.GetString("Test");
        //test = JsonUtility.FromJson<testValue>(valueS);
        //Debug.Log($"A : {test.valueA} , B : {test.valueB}");
       
        // 배경음
        float values = PlayerPrefs.GetFloat(SoundType.BGM_Volume.ToString(), 1.0f);
        bgmSlider.value = values;
        SetSoundVolume(SoundType.BGM_Volume, values);

        // 효과음
        values = PlayerPrefs.GetFloat(SoundType.SFX_Volume.ToString(), 1.0f);
        sfxSlider.value = values;
        SetSoundVolume(SoundType.SFX_Volume, values);
    }

    private void OnEnable()
    {
        bgmSlider?.onValueChanged.AddListener(OnBGMChange);
        sfxSlider?.onValueChanged?.AddListener(OnSFXChange);
    }

    private void OnDisable()
    {
        bgmSlider?.onValueChanged.RemoveListener(OnBGMChange);
        sfxSlider?.onValueChanged?.RemoveListener(OnSFXChange);
    }

    private void OnBGMChange(float value)
    {
        PlayerPrefs.SetFloat(SoundType.BGM_Volume.ToString(), value);
        SetSoundVolume(SoundType.BGM_Volume, value);
    }

    private void OnSFXChange(float value)
    {
        PlayerPrefs.SetFloat(SoundType.SFX_Volume.ToString(), value);
        SetSoundVolume(SoundType.SFX_Volume, value);
    }

    private void SetSoundVolume(SoundType type, float value)
    {
        float newVolume = -80f + (80.0f + value);

        mixer.SetFloat(type.ToString(), newVolume);
    }
}
