using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	public List<AudioClip> sounds;
	private List<AudioSource> loops;

	void Awake()
	{
		loops = new List<AudioSource> ();
	}

	public AudioClip getSound(string soundName)
	{
		foreach(AudioClip s in sounds)
		{
			if(s.name == soundName) return s;
		}
		
		return null;
	}
	
	public AudioSource getLoopSource(string soundName)
	{
		for(int i = 0; i < loops.Count; i++)
		{
			AudioSource loop = loops[i] as AudioSource;
			if(loop.clip.name == soundName) return loop;
		}
		
		return null;
	}
	
	public AudioSource loopSound(string soundName)
	{
		return loopSound(soundName, 1.0f);
	}
	
	public AudioSource loopSound(string soundName, float volume)
	{
		AudioClip sound = getSound(soundName);
		
		if(sound != null)
		{
			AudioSource loop = gameObject.AddComponent<AudioSource>() as AudioSource;
			loop.volume = volume;
			loop.clip = sound;
			loop.loop = true;
			loops.Add(loop);
			loop.Play();
			
			return loop;
			
		} else {
			return null;
		}
	}
	
	public bool stopLoop(string soundName)
	{
		AudioSource loop = getLoopSource(soundName);
		
		if(loop != null)
		{
			if(loop.isPlaying)
			{
				for(int i = 0; i < loops.Count; i++)
				{
					if(loop == loops[i])
					{
						loops.RemoveAt(i);
						break;
					}
				}
				
				loop.Stop();
				GameObject.Destroy(loop);
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
	
	public AudioClip playSound(string soundName)
	{
		return playSound(soundName, 1.0f);
	}
	
	public AudioClip playSound(string soundName, float volume)
	{
		AudioClip sound = getSound(soundName);
		
		if(sound != null)
		{
			AudioSource.PlayClipAtPoint(sound, new Vector3(), volume);
			return sound;
		} else {
			return null;
		}
	}
}
