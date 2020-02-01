using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	#region Properties
	public GameObject objectToPool;
	public float initialAmount;
	public bool canGrow = true;

	[SerializeField]
	private List<GameObject> pool = new List<GameObject>();

	//Accessors
	public int count
	{
		get
		{
			return pool.Count;
		}
	}
	public int activeCount
	{
		get
		{
			int amount = 0;

			for (int i = 0; i < pool.Count; i++)
			{
				if (pool[i].activeSelf) amount++;
			}

			return amount;
		}
	}
	public int inactiveCount
	{
		get
		{
			return count - activeCount;
		}
	}
	#endregion

	#region MonoBehavior
	// Use this for initialization
	void Start()
	{
		for (int i = 0; i < initialAmount; i++)
		{
			CreateNewPooledObject();
		}
	}
	#endregion

	#region Functionality
	public void ReturnAllObjectsToPool()
	{
		for (int i = 0; i < pool.Count; i++)
		{
			pool[i].transform.SetParent(transform);
			pool[i].SetActive(false);
		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pool.Count; i++)
		{
			if (!pool[i].activeSelf)
			{
				pool[i].SetActive(true);
				return pool[i];
			}
		}

		if (canGrow)
		{
			GameObject obj = CreateNewPooledObject();
			obj.SetActive(true);
			return obj;
		}

		return null;
	}

	public GameObject GetPooledObject(Transform parent, bool resetTransform = true, bool worldPositionStays = false)
	{
		GameObject obj = GetPooledObject();

		if (parent != null) obj.transform.SetParent(parent, worldPositionStays);
		if (resetTransform) ResetTransform(obj.transform);

		return obj;
	}

	public T GetPooledObject<T>() where T : Component
	{
		GameObject obj = GetPooledObject();
		T typeObj = obj.GetComponent<T>();

		if (typeObj == null)
		{
			ReturnObjectToPool(obj);
		}

		return typeObj;
	}

	public T GetPooledObject<T>(Transform parent, bool resetTransform = true, bool worldPositionStays = false) where T : Component
	{
		T obj = GetPooledObject<T>();

		if (parent != null) obj.transform.SetParent(parent, worldPositionStays);
		if (resetTransform) ResetTransform(obj.transform);

		return obj;
	}

	private GameObject CreateNewPooledObject()
	{
		GameObject obj = Instantiate(objectToPool, transform);
		obj.transform.localPosition = Vector3.zero;
		obj.transform.rotation = Quaternion.identity;

		pool.Add(obj);

		obj.SetActive(false);

		return obj;
	}

	public void ReturnObjectToPool(GameObject obj)
	{
		if (pool.Contains(obj))
		{
			obj.transform.SetParent(transform);
			obj.SetActive(false);
		}
	}

	private void ResetTransform(Transform objTransform)
	{
		objTransform.position = Vector3.zero;
		objTransform.rotation = Quaternion.identity;
		objTransform.localScale = Vector3.one;
	}
	#endregion
}
