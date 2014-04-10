using UnityEngine;
using System.Collections;

public class Initialisation : MonoBehaviour {
	
	//liste des types de quartier
	public Quartier[] lesQuartier;
	
	//les types de quartiers
	public Quartier[] qAffaire;
	public Quartier[] qResidenciel;
	public Quartier[] qCommercial;
	
	//les batiments
	[SerializeField]
	public Batiments batA1;
	[SerializeField]
	public Batiments batR1;
	[SerializeField]
	public Batiments batC1;
	
	
	// Use this for initialization
	void Start () {
		linitialisation();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void linitialisation()
	{
		/*batA1  = new Batiments(1);
		lesQuartier[0] = qAffaire;
		lesQuartier[1] = qResidenciel;
		lesQuartier[2] = qCommercial;*/
	}
	
}
