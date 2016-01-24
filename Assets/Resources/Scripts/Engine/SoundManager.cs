using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;
	public AudioClip[] AudioClips;
	private AudioSource Speaker;
	
	void Awake()
	{
		instance = this;
		Speaker = gameObject.GetComponent<AudioSource>();
		Speaker.bypassListenerEffects = true;
	}


	public void PlaySound(string inClipName,bool isLoop = false)
	{
		Speaker.loop = isLoop;
		Speaker.PlayOneShot(FindClipByName(inClipName));
		//Speaker.PlayOneShot(FindClipByName(inClipName));
	}
	
	public void StopAllSounds()
	{
		//print("stopAllSounds");
		Speaker.Stop();
	}
	
	//TODO make hash or dict on Start
	private AudioClip FindClipByName(string inClipName)
	{
		AudioClip returnClip = null;
		foreach(AudioClip clip in AudioClips)
		{
			if (clip.name == inClipName) returnClip = clip;
		}
		if (!returnClip) print("AudioClip not in SoundManager array " + inClipName);
		return returnClip;
	}
	
}
