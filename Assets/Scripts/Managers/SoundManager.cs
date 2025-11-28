using UnityEngine;

public enum SfxType
{
    SFX_PlayerFire,
    SFX_EnemyDie,
    SFX_GetPickup,
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioClip[] sfxClips;

    private AudioSource[] sfxPools;
    private int nextIndex = 0;
    private int poolSize = 0;

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    private void InitSoundManager()
    {
        sfxPools = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = new GameObject($"SFX Source _{i}");
            go.transform.parent = transform;

            AudioSource source = go.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;

            sfxPools[i] = source;
        }
    }

    public void PlaySFX(SfxType type)
    {
        AudioClip clip = sfxClips[(int)type];
        if (clip == null) return;

        AudioSource source = sfxPools[nextIndex++];
        if (nextIndex >= poolSize)
            nextIndex = 0;

        if (source == null) return;

        source.Stop();
        source.clip = clip;
        source.Play();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
