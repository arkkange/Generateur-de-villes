using UnityEngine;
using System.Collections;

public class AviationController : MonoBehaviour {

	[SerializeField]
	Transform _MyTransform;

	[SerializeField]
	Vector3 _Destination;

	[SerializeField]
	private NavMeshAgent agent;

	[SerializeField]
	bool animated = true;

	[SerializeField]
	int  IndiceMin_pop = 50;
	[SerializeField]
	int  IndiceMax_pop = 150;

	// Use this for initialization
	void Start () {
		if(animated){
			agent = GetComponent<NavMeshAgent>();


			int _x = UnityEngine.Random.Range(IndiceMin_pop,IndiceMax_pop);
			int _y =	UnityEngine.Random.Range(IndiceMin_pop,IndiceMax_pop);
			_MyTransform.position = new Vector3(_x, 0, _y);

			_x = UnityEngine.Random.Range(IndiceMin_pop,IndiceMax_pop);
			_y =UnityEngine.Random.Range(IndiceMin_pop,IndiceMax_pop);
			_Destination =  new Vector3(_x, 0, _y);
			agent.destination = _Destination;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(animated){
			if(agent.remainingDistance <= 1 ){
				int _x = UnityEngine.Random.Range(IndiceMin_pop,IndiceMax_pop);
				int _y =UnityEngine.Random.Range(IndiceMin_pop,IndiceMax_pop);
				_Destination =  new Vector3(_x, 0, _y);
				agent.destination = _Destination;
			}
		}
		else{
			agent.destination = _MyTransform.position;
		}
	
	}
}
