using UnityEngine;
using System.Collections;

public class TownGenerator : MonoBehaviour {

	[SerializeField]
	Transform _route_droite;

	[SerializeField]
	Transform _route_angle;

	[SerializeField]
	Transform _route_croisement;


	[SerializeField]
	public static int taille = 100;

	public static int[,] _TownTable;
	/*
	 * signification des nombres :
	 * 0 ==> Nothing
	 * 1 ==> Simple Road
	 * 2 ==> angle Road
	 * 4 ==> intersection Road
	 * 5 ==> Zone de quartier
	*/
		

	// Use this for initialization
	void Start () {

		_TownTable = new int[taille,taille];
		_TownTable = TableIntitialisation(taille);

		bool _roading;

		Position _centre = new Position(taille/2,taille/2);
		Position _ActualPosition;
		Position _NewPosition;

		//creation of the Roads
		for(int j = 0; j<1 ; j++){
			//recupération du centre et initialisation du booleen
			_ActualPosition = _centre;
			_TownTable[ _ActualPosition.x, _ActualPosition.y] = 1;
			_roading = true;

			//parcours total jusqu'a interuption de la route
			while(_roading){

				//aleatoiriser une direction
				_NewPosition = NewRoad( _ActualPosition, _TownTable );

				//si la position n'as pas changée on stop le roading sinon on road
				if( _NewPosition.Equals(_ActualPosition) ){
					_roading = false;
					Debug.Log("end of road because new position equals actual position");
				}
				else{
					//roader (c'est a dire ajouter la route au tableau
					_TownTable[ _ActualPosition.x, _ActualPosition.y ] = 1;
				}

			}

		}

		//instanciations of all objects on the scene
		Instanciate(_TownTable);

	}

	#region functions

	int[,] TableIntitialisation(int taille){

		int[,] table = new int[taille,taille];

		for (int j=0 ; j < taille ; j++){
			for (int k=0 ; k < taille ; k++){
				table[j,k]= 0;
			}
		}

		return table;
	}

	void Instanciate(int[,] Table){

		for (int j=0 ; j < taille ; j++){
			for (int k=0 ; k < taille ; k++){
				if(Table[j,k] == 1){
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _route_croisement.rotation;
					Instantiate(_route_croisement,position,rotation);
				}
			}
		}

	}
	
	#region fonction de tracé de route
	//fonction qui envoi la position de la poursuite de la route
	public Position NewRoad(Position _AP  , int[,] _TownTable){

		int _newdirection;
		Position _NewPosition = new Position(_AP.x,_AP.y);
		/*
		_newdirection = (int)Random.Range(1,4);

		//verification que la direction est bonne
		if(_newdirection == 1){
			if(_AP.x-1 >= 0 ){
				if((_TownTable[		_AP.x-1,	_AP.y-1 ] 	!= -1000 && _TownTable[	_AP.x-1,	_AP.y-1 ] 	!= 1 ) &&
				   (_TownTable[		_AP.x-1,	_AP.y 	] 	!= -1000 && _TownTable[	_AP.x-1,	_AP.y+1 ] 	!= 1 ) &&
				   (_TownTable[		_AP.x-1,	_AP.y +1] 	!= -1000 && _TownTable[	_AP.x-1,	_AP.y 	] 	!= 1 ) 
				   ){

					//Debug.Log("New Direction North");
					_NewPosition.x = _NewPosition.x - 1;
				}
			}
			else{
				//Debug.Log ("out of bounds exception : "+ _AP.x);
				return _AP;
			}
		}
		if(_newdirection == 2){
			if(_AP.y+1 < 100){	//taille tableau
				if(	(_TownTable[	_AP.x+1,	_AP.y+1 ] 	!= -1000 && _TownTable[_AP.x+1,	_AP.y+1 ] 	!= 1 ) &&
				   	(_TownTable[	_AP.x,		_AP.y+1 ] 	!= -1000 && _TownTable[_AP.x,	_AP.y+1 ] 	!= 1 ) &&
			   		(_TownTable[	_AP.x-1,	_AP.y+1 ] 	!= -1000 && _TownTable[_AP.x-1,	_AP.y+1 ] 	!= 1 ) ){

					Debug.Log("New Direction  East");
					_NewPosition.y = _NewPosition.y + 1;
				}
			}
			else{
				//Debug.Log ("out of bounds exception : "+ _AP.y);
				return _AP;
			}
		}
		if(_newdirection == 3){
			if(_AP.x+1 < 100){
				if((_TownTable[		_AP.x+1,	_AP.y+1 ] 	!= -1000 && _TownTable[_AP.x+1,	_AP.y+1 ] 	!= 1 ) &&
				   (_TownTable[		_AP.x+1,	_AP.y 	] 	!= -1000 && _TownTable[_AP.x+1,	_AP.y 	] 	!= 1 ) &&
				   (_TownTable[		_AP.x-1,	_AP.y-1 ] 	!= -1000 && _TownTable[_AP.x-1,	_AP.y-1 ]	!= 1 ) ){

					Debug.Log("New Direction South");
					_NewPosition.x = _NewPosition.x + 1;
				}
			}
			else{
				//Debug.Log ("out of bounds exception : "+ _AP.x);
				return _AP;
			}
		}
		if(_newdirection == 4){
			if(_AP.y-1 >= 0){
				if((_TownTable[		_AP.x+1,	_AP.y-1 ] 	!= -1000 && _TownTable[_AP.x+1,	_AP.y-1 ] 	!= 1 ) &&
				   (_TownTable[		_AP.x,		_AP.y-1 ] 	!= -1000 && _TownTable[_AP.x,	_AP.y-1 ] 	!= 1 ) &&
				   (_TownTable[		_AP.x-1,	_AP.y-1 ] 	!= -1000 && _TownTable[_AP.x-1,	_AP.y-1 ] 	!= 1 ) ){

					//Debug.Log("New Direction West");
					_NewPosition.y = _NewPosition.y - 1;
				}
			}
			else{
				Debug.Log ("out of bounds exception : "+ _AP.y);
				return _AP;
			}
		}
		*/

		//deplacer la position actuelle a la nouvelle position
		return _NewPosition;
	}
	#endregion

	#endregion
	

	// Update is called once per frame
	void Update () {
	
	}


}

