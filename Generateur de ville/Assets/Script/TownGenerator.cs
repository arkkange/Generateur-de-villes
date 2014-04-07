using UnityEngine;
using System.Collections;

public class TownGenerator : MonoBehaviour {

	#region variables

	[SerializeField]
	public static int taille = 100;

	public static int[,] _Road_Table = new int[taille,taille];

	#endregion

	#region Start

	// Use this for initialization
	void Start () {
	
	}

	#endregion

	#region Update

	// Update is called once per frame
	void Update () {
	
	}

	#endregion


}

