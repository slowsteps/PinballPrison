using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;
	public AudioClip[] AudioClips;
	private AudioSource Speaker;
	
	void Start()
	{
		instance = this;
		Speaker = gameObject.GetComponent<AudioSource>();
	}


	public void PlaySound(string inClipName)
	{
		print ("playing " + inClipName);
		Speaker.PlayOneShot(FindClipByName(inClipName));
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
