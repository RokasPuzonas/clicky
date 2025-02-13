using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{	
	public static AudioManager instance;
	private AudioSource audioSource;
	private static System.Random rng = new System.Random();
	public AudioClip[] buttonClick;
	public AudioClip[] buyUpgrade;


	public void Awake()
	{
		if (instance != null && instance != this )
		{
			Destroy(this);
			return;
		}
		instance = this ;

		Setup();
	
	}

	public void Setup()
	{
		audioSource = GetComponent<AudioSource>();
		UpdateVolume(GameManager.instance.settings.volumeLevel);
	}

	/// <summary>
	/// Static function which is mainly used in `FancyButton` to play custom press sound
	/// </summary>
	public static void PlayButtonClick()
	{
		PlaySound(instance.buttonClick);
	}

	/// <summary>
	/// Play a random sound from list of clips
	/// </summary>
	public static void PlaySound(AudioClip[] clips)
	{
		int n = clips.Length;
		if (n == 0) return;

		var clip = clips[rng.Next() % n];
		instance.audioSource.PlayOneShot(clip);
	}

	/// <summary>
	/// Play a specific sound once
	/// </summary>
	public static void PlaySingleSound(AudioClip clip, float volume = 1)
	{
		var originalVolume = instance.audioSource.volume;
		instance.audioSource.PlayOneShot(clip);
		instance.audioSource.volume *= volume;
		instance.audioSource.volume = originalVolume;
	}

	public void UpdateVolume(float vol)
	{
		print(vol / 100);
		audioSource.volume = vol / 100;
	}
}
