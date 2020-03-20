using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAMovement : MonoBehaviour
{
    public Vector3 targetPos;
    public float interpolationFactor = 0.002f;
    public Vector3 startPos;
    public GameObject playerTransform;
	public BossFloorManager bossFloor;
	public Enemy enemyController;
	public UnityEvent battleStartEvent = new UnityEvent();

	void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
        playerTransform = GameObject.FindGameObjectWithTag("Player");
		enemyController.deathEvent.AddListener(Die);
	}

	public void StartBattle()
	{
		battleStartEvent.Invoke();
		bossFloor.PickRandomBehavior();
		pickRandomBehaviour();
		SoundManager.instance.SetMusicMood(2);
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, interpolationFactor);
        transform.forward = (playerTransform.transform.position - transform.position).normalized;
    }

    void pickRandomBehaviour()
    {
        StopAllCoroutines();
        int random = Random.Range(0, 1);
        if(random == 0)
        {
            StartCoroutine(CircularPath(Random.Range(-2f, 2f)));
        }
        if(random == 1)
        {
            StartCoroutine(CircularPath(Random.Range(-2f, 2f)));
        }

        SoundManager.instance.PlayDying();
        

        
    }

    IEnumerator CircularPath(float speed)
    {
        float timer = 0;
        float duration = 4;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            targetPos = startPos + new Vector3(Mathf.Sin(Time.time * speed), 0, Mathf.Cos(Time.time * speed)) * 27f;
            yield return new WaitForEndOfFrame();
        }
        targetPos = startPos;
        yield return new WaitForSeconds(2f);
        pickRandomBehaviour();
    }

    IEnumerator StandStill(float duration)
    {
        targetPos = startPos;

        yield return new WaitForSeconds(duration);
        pickRandomBehaviour();
    }

	public void Die()
	{
		SoundManager.instance.SetMusicMood(1);
		Transform fx = Game_Manager.instance.explosionPool.GetPooledObject().transform;
		fx.position = transform.position;
		StopAllCoroutines();
		bossFloor.StopAllCoroutines();
		gameObject.SetActive(false);
	}
}
