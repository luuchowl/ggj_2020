using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
	public int maxHealth;

	private int currentHealth;

	private void Awake()
	{
		currentHealth = maxHealth;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ApplyDamage(Hitbox hitbox, AttackData attackData)
	{
		Debug.Log("Morri");
	}
}
