using UnityEngine;
using System.Collections;
using System;

public class TownGenerator : MonoBehaviour {

	[SerializeField]
	Transform _route_droite;

	[SerializeField]
	Transform _route_angle;

	[SerializeField]
	Transform _route_croisement;


	[SerializeField]
	public static int taille = 200;	//minimum 50

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
		bool _position_is_road;

		//generation des 20 routes de la ville aleatoirement a partir du centre de la carte
		for(int i =0; i< 200*20/100 ; i++){

			_position_is_road = false;
			Position _position= new Position();

			
			//generation aleatoire de point de depart de la route
			int _x = (int)UnityEngine.Random.Range(taille/2 , taille/2 + taille/4);
			int _y =	(int)UnityEngine.Random.Range(taille/2 , taille/2 + taille/4);
			_position.SetPosition(_x,_y);


			//lancement de la construction de route
			for(int j=0; j<4;j++){
				RoadCreation(_position, _TownTable, taille);
			}
		}

		//instanciations de tous les objets dans la scène
		Instanciate(_TownTable);

	}


	/*
	 * Fonctions
	 * 
	 */


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


	//cette fonction genere une route a l'emplacement donné
	void RoadCreation(Position BeginPosition, int[,] _TownTable, int taille){
		

		bool _roading;
		
		Position _ActualPosition = new Position(BeginPosition);
		Position _NewPosition= new Position(BeginPosition);
		
		//creation of the Roads
		//recupération du centre et initialisation du booleen
		_ActualPosition.SetPosition(BeginPosition);
		_NewPosition.SetPosition(_ActualPosition);
		_TownTable[ _ActualPosition.x, _ActualPosition.y] = 1;
		_roading = true;
		int _direction = 0;

		int _TailleRoad = 10;	//taille des routes minimale droites ( a mettre en parametres)
		int _countRoad = _TailleRoad;
		//parcours total jusqu'a interuption de la route
		while(_roading){

			if(_countRoad == _TailleRoad){
				//aleatoiriser une direction (on ajoute la directio na la liste)
				_direction = (int)UnityEngine.Random.Range(1,5);
			}
			else if( _countRoad > 0 && _countRoad < _TailleRoad){

			}
			else if(_countRoad <= 0){
				_countRoad = _TailleRoad;
			}


			NewRoad(_NewPosition, _TownTable, _direction );
		
		
			//si la position n'as pas changée on stop le roading sinon on road
			if( _NewPosition.Equals(_ActualPosition) ){
				_roading = false;
				Debug.Log("end of road because new position equals actual position");
				Debug.Log("_NewPosition : "+_NewPosition.x+","+_NewPosition.y+" & _ActualPosition : "+_ActualPosition.x+","+_ActualPosition.y);
			}
			else{
				//roader (c'est a dire ajouter la route au tableau
				_ActualPosition.SetPosition(_NewPosition);
				_TownTable[ _ActualPosition.x, _ActualPosition.y ] = 1;
			}
			
			
		}
		
		
	}


	//fonction qui envoi la position de la poursuite de la route
	public void NewRoad(Position _NewPosition  , int[,] _TownTable, int _direction){

		int _newdirection;
		bool _direction_tested = false;


		bool no_path = false;
		bool N_Iscorrect = true;
		bool E_Iscorrect = true;
		bool S_Iscorrect = true;
		bool W_Iscorrect = true;

		_newdirection = _direction; 	//random entre 1 et 4 (5 pour 4 attention!)

		do{

			if(_direction_tested == true){
				_newdirection = (int)UnityEngine.Random.Range(1,5);
			}
			_direction_tested = true;

			//debug direction du random
			if(_newdirection == 1){
				Debug.Log("wantogo: N");
			}
			else if(_newdirection == 2){
				Debug.Log("wantogo:E");
			}
			else if(_newdirection == 3){
				Debug.Log("wantogo: S");
			}
			else if(_newdirection == 4){
				Debug.Log("wantogo: W");
			}


			//verification que la direction est bonne
			if(_newdirection == 1 && N_Iscorrect == true){
				if(	_NewPosition.x-1 >= 1	&&
				   	_NewPosition.y-1 >= 1 	&&
				   	_NewPosition.y+1 < 200-1	){
					if((_TownTable[		_NewPosition.x-1,	_NewPosition.y -1 	] 	!= -2000 && _TownTable[	_NewPosition.x-1,	_NewPosition.y-1 	] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x-1,	_NewPosition.y 		] 	!= -2000 && _TownTable[	_NewPosition.x-1,	_NewPosition.y 		] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x-1,	_NewPosition.y +1	] 	!= -2000 && _TownTable[	_NewPosition.x-1,	_NewPosition.y+1	] 	!= 1 ))
					{
						_NewPosition.x = _NewPosition.x - 1;
						no_path = true;
					}
					else{
						N_Iscorrect = false;
					}
				}
				else{
					Debug.Log ("out of bounds exception : "+ _NewPosition.x);
					N_Iscorrect = false;
				}
			}

			if(_newdirection == 2 && E_Iscorrect == true){
				if(	_NewPosition.y+1 <  200-1	&&
					_NewPosition.x-1 >= 1 	&&
				   	_NewPosition.x+1 <	200-1){

					if(	(_TownTable[	_NewPosition.x+1,	_NewPosition.y+1 ] 	!= -2000 && _TownTable[_NewPosition.x+1,	_NewPosition.y+1 	] 	!= 1 ) &&
					   	(_TownTable[	_NewPosition.x,		_NewPosition.y+1 ] 	!= -2000 && _TownTable[_NewPosition.x,		_NewPosition.y+1 	] 	!= 1 ) &&
				   		(_TownTable[	_NewPosition.x-1,	_NewPosition.y+1 ] 	!= -2000 && _TownTable[_NewPosition.x-1,	_NewPosition.y+1 	] 	!= 1 ))
					{
						_NewPosition.y = _NewPosition.y + 1;
						no_path = true;
					}
					else{
						E_Iscorrect = false;
					}
				}
				else{
					Debug.Log ("out of bounds exception : "+ _NewPosition.y);
					E_Iscorrect = false;
				}
			}

			if(_newdirection == 3 && S_Iscorrect == true){
				if(	_NewPosition.x+1 < 200-1	&&
				   _NewPosition.y-1 >= 1 	&&
				   _NewPosition.y+1 < 200-1	){
					if((_TownTable[		_NewPosition.x+1,	_NewPosition.y+1 ] 	!= -2000 && _TownTable[_NewPosition.x+1,	_NewPosition.y+1 ] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x+1,	_NewPosition.y 	] 	!= -2000 && _TownTable[_NewPosition.x+1,	_NewPosition.y 	] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x+1,	_NewPosition.y-1 ] 	!= -2000 && _TownTable[_NewPosition.x+1,	_NewPosition.y-1 ]	!= 1 ))
					{
						_NewPosition.x = _NewPosition.x + 1;
						no_path = true;
					}
					else{
						S_Iscorrect = false;
					}
				}
				else{
					Debug.Log ("out of bounds exception : "+ _NewPosition.x);
					S_Iscorrect = false;
				}
			}

			if(_newdirection == 4 && W_Iscorrect == true){
				if(	_NewPosition.y-1 >= 1	&&
				   _NewPosition.x-1 >= 1 	&&
				   _NewPosition.x+1 < 200-1	){
					if((_TownTable[		_NewPosition.x+1,	_NewPosition.y-1 ] 	!= -2000 && _TownTable[_NewPosition.x+1,	_NewPosition.y-1 ] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x,		_NewPosition.y-1 ] 	!= -2000 && _TownTable[_NewPosition.x,		_NewPosition.y-1 ] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x-1,	_NewPosition.y-1 ] 	!= -2000 && _TownTable[_NewPosition.x-1,	_NewPosition.y-1 ] 	!= 1 ))
					{
						_NewPosition.y = _NewPosition.y - 1;
						no_path = true;
					}
					else{
						W_Iscorrect = false;
					}
				}
				else{
					Debug.Log ("out of bounds exception : "+ _NewPosition.y);
					W_Iscorrect = false;
				}
			}

			//test final si acun chemin n'est trouvé on quitte
			if( N_Iscorrect == false && E_Iscorrect == false && S_Iscorrect == false && W_Iscorrect == false ){
				no_path = true;
			}

		}while(!no_path);

	}
	

	// Update is called once per frame
	void Update () {
	
	}


}

