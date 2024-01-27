using System.Collections.Generic;
using UnityEngine;
using static SoundPlayer;


[System.Serializable]
public class MultiAudioClip
{
    public AudioClip[] bank;
    int last = -1;
    [Range(0f, 1f)]
    public float volume = .75f;
    public AudioClip GetRandomClip()
    {
        int index = -1;
        do
        {
            index = Random.Range(0, bank.Length);
        } while (index == last);

        return bank[index];
    }
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Bitcrusher))]
public class SoundPlayer : MonoBehaviour
{
    [System.Serializable]
    private class _AudioClip
    {
        public string key;
        public AudioClip clip;
        [Range(0, 1)]
        public float volume = .75f;
    }

    [System.Serializable]
    private class _MultiAudioClip
    {
        public string key;
        public MultiAudioClip bank;
    }

    [System.Serializable]
    private class ImplAudioClip
    {
        public AudioClip clip;
        [Range(0, 1)]
        public float volume = .75f;
    }

    public enum Bank { Single, Multi };

    [SerializeField] List<_AudioClip> singleSoundBank/* = new List<_AudioClip>()*/;
    [SerializeField] List<_MultiAudioClip> multiSoundBank = new List<_MultiAudioClip>();

    Dictionary<string, MultiAudioClip> _multi = new Dictionary<string, MultiAudioClip>();
    Dictionary<string, ImplAudioClip> _single = new Dictionary<string, ImplAudioClip>();

    AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();

        foreach (_AudioClip clip in singleSoundBank)
        {
            ImplAudioClip c = new ImplAudioClip();
            c.volume = clip.volume;
            c.clip = clip.clip;
            _single.Add(clip.key, c);
        }
        singleSoundBank.Clear();

        foreach (_MultiAudioClip mclip in multiSoundBank)
        {
            _multi.Add(mclip.key, mclip.bank);
        }
        multiSoundBank.Clear();
    }

    public void Play(string key)
    {

        if (_single.ContainsKey(key))
        {
            _source.clip = _single[key].clip;
            _source.volume = _single[key].volume;
            _source.Play();
        }
        else if (_multi.ContainsKey(key))
        {
            AudioClip clip = _multi[key].GetRandomClip();
            _source.clip = clip;
            _source.volume = _multi[key].volume;
            _source.Play();
        }
        else
        {
            Debug.LogError("SoundManager -> Could not find " + key + " in sound banks");
        }
    }

    public void Play(string key, Bank bank)
    {
        if (bank == Bank.Single && _single.ContainsKey(key))
        {
            _source.clip = _single[key].clip;
            _source.volume = _single[key].volume;
            _source.Play();
            return;
        }
        else if (bank == Bank.Single)
        {
            Debug.LogError("SoundManager -> Could not find " + key + " in single sound bank");
            return;
        }

        if (bank == Bank.Multi && _multi.ContainsKey(key))
        {
            AudioClip clip = _multi[key].GetRandomClip();
            _source.clip = clip;
            _source.volume = _multi[key].volume;
            _source.Play();
        }
        else
        {
            Debug.LogError("SoundManager -> Could not find " + key + " in multi sound bank");
        }
    }

    public float GetLengthOfSingle(string key)
    {
        if (_single.ContainsKey(key))
        {
            return _single[key].clip.length;
        }
        Debug.LogError("SoundManager -> Could not find " + key + " in single sound bank");
        return 0f;
    }


}