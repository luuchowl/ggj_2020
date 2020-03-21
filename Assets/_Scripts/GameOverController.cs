using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
	public VideoPlayer player;
	public VideoClip gameOver, credits;

	private void Awake()
	{
		player.loopPointReached += Player_loopPointReached;
        
    }

    private void Start()
    {
        SoundManager.instance.SetMusicMood(4);
    }

    private void Player_loopPointReached(VideoPlayer source)
	{
		if(source.clip == gameOver)
		{
			source.clip = credits;
			source.Play();
		}
		else
		{
			SceneManager.LoadScene("Final");
		}
	}
}
