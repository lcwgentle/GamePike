using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ±≥æ∞“Ù “Ù–ß
/// </summary>
public class Sound : MonoSingleton<Sound>
{
    AudioSource m_bg;
    AudioSource m_effect;
    public string resourceDir = "";

    protected override void Awake()
    {
        base.Awake();
        m_bg = gameObject.AddComponent<AudioSource>();
        m_bg.loop = true;
        m_bg.playOnAwake = false;
        m_bg.volume = 0.5f;

        m_effect = gameObject.AddComponent<AudioSource>();
    }

    public void PlayBG(string name)
    {
        string oldName;
        if(m_bg.clip==null)
        {
            oldName = "";
        }else
        {
            oldName = m_bg.clip.name;
        }

        if(oldName!=name)
        {
            AudioClip clip = Resources.Load<AudioClip>(resourceDir +"/"+ name);

            if(clip!=null)
            {
                m_bg.clip = clip;
                m_bg.Play();
            }
        }
    }
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(resourceDir + "/" + name);
        m_effect.PlayOneShot(clip);
    }
}
