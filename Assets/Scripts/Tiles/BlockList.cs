using UnityEngine;
using System.Collections;
using System;

public class BlockList : MonoBehaviour
{
	public GameObject[] blocks;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public GameObject getOriginalBlock(BlockType type) {
		return Array.Find(blocks, gObject => gObject.GetComponent<Block>().type.Equals(type));
	}
}

