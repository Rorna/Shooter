using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{
    #region Variables
    AudioSource[] audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    #endregion
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root); 

            //사운드 enum 이름들 추출
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); 

            for (int i=0; i<soundNames.Length -1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            //bgm은 loop
            audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        audioClips.Clear();
    }

    ///플레이 받는 함수
    public void Play(string _path, Define.Sound _type=Define.Sound.Effect, float _pitch =1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(_path, _type);
        Play(audioClip, _type, _pitch);
    }

    //오디오 클립을 직접 받음
    //기본값 effect
    public void Play(AudioClip _audioClip, Define.Sound _type = Define.Sound.Effect, float _pitch = 1.0f)
    {
        if (_audioClip == null)
            return;

        //Sound Enum에 맞게
        if (_type == Define.Sound.Bgm)
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying) //이미 다른 bgm이 재생중이라면
                audioSource.Stop(); //멈춤

            audioSource.pitch = _pitch;
            audioSource.clip = _audioClip; //오디오 집어넣음
            audioSource.Play(); //bgm은 루프라서 플레이만 하면 됨
        }
        else
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = _pitch; //재생속도
            audioSource.PlayOneShot(_audioClip);
        }
    }



    //오디오 추가
    AudioClip GetOrAddAudioClip(string _path, Define.Sound _type = Define.Sound.Effect)
    {
        AudioClip audioClip = null;

        if (_path.Contains("Sounds/") == false)
            _path = $"Sounds/{_path}";

        if (_type == Define.Sound.Bgm)
        {
            audioClip = Resources.Load<AudioClip>(_path);
        }
        else
        {
            //없으면 추가
            if (audioClips.TryGetValue(_path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(_path);
                audioClips.Add(_path, audioClip);
            }   
        }
        if (audioClip == null)
        {
            Debug.Log($"audioclip missing {_path}");
        }
        return audioClip;
    }

}
