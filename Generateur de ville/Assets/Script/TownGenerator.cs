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
	Transform _Centre_commercial;
	[SerializeField]
	Transform _centre_commercial2;
	[SerializeField]
	Transform _immeuble_grand;
	[SerializeField]
	Transform _immeuble_grand2;
	[SerializeField]
	Transform _immeuble_petit;
	[SerializeField]
	Transform _immeuble_petit2;
	[SerializeField]
	Transform _maison;
	[SerializeField]
	Transform _maison1;
	[SerializeField]
	Transform _maison2;
	[SerializeField]
	Transform _maison3;
	[SerializeField]
	Transform _maison4;
	[SerializeField]
	Transform _maison5;
	[SerializeField]
	Transform _statue;
	[SerializeField]
	Transform _park;
	[SerializeField]
	Transform _champ;


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
		//Position _centre = new Position(0,0);
		Position _ActualPosition = new Position(_centre);
		Position _NewPosition= new Position(_centre);

		//creation of the Roads
		for(int j = 0; j<4 ; j++){
			//recupération du centre et initialisation du booleen
			_ActualPosition.SetPosition(_centre);
			_NewPosition.SetPosition(_ActualPosition);
			_TownTable[ _ActualPosition.x, _ActualPosition.y] = 1;
			_roading = true;

			int compte = 0;
			//parcours total jusqu'a interuption de la route
			while(_roading){

				compte ++;

				//aleatoiriser une direction
				NewRoad(_NewPosition, _TownTable );

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

			Debug.Log (" compte = "+compte);

		}
		for(int j = 0; j < taille - 1; j++)
		{
			for(int k = 0; k < taille-1; k++)
			{
				if(_TownTable[j,k] == 0)
				{
					int _typequartier = Random.Range(15,20);
					creerQuartier(j,k, _typequartier);
				}
			}

		}
		//instanciations of all objects on the scene
		Instanciate(_TownTable);

	}

	#region functions

	//creer quartier
	public void creerQuartier(int x, int y, int type)
	{
		_TownTable[x,y] = type;
		//si on est a la fin du tableau
		if(x<taille-1)
		{
			//si ya une route non principal on vas chercher plus loin!
			if(_TownTable[x+1,y] >5 && _TownTable[x+1,y] < 10)
			{
				if(_TownTable[x+2,y] == 0)
				{
					creerQuartier(x+2,y,type);
				}
			}
			//on renvoie en bas!
			if(_TownTable[x+1,y] == 0)
			{
				creerQuartier(x+1,y,type);
			}
		}
		//si on est a la fin du tableau
		if(y<taille-1)
		{
			if(_TownTable[x,y+1] >5 && _TownTable[x,y+1] < 10)
			{
				if(_TownTable[x,y+2] == 0)
				{
					creerQuartier(x,y+2,type);
				}
			}
			//on renvoie a droite
			if(_TownTable[x,y+1] == 0)
			{
				creerQuartier(x,y+1,type);
			}
		}
		//si on est au début du tableau
		if(x>0)
		{
			if(_TownTable[x-1,y] >5 && _TownTable[x-1,y] < 10)
			{
				if(_TownTable[x-2,y] == 0)
				{
					creerQuartier(x-2,y,type);
				}
			}
			//on envoie en haut!
			if(_TownTable[x-1,y] == 0)
			{
				creerQuartier(x-1,y,type);
			}
		}
		//si on est au début du tableau
		if(y>0)
		{
			if(_TownTable[x,y-1] >5 && _TownTable[x,y-1] < 10)
			{
				if(_TownTable[x,y-2] == 0)
				{
					creerQuartier(x,y-2,type);
				}
			}
			//on envoie a gauche!
			if(_TownTable[x,y-1] == 0)
			{
				creerQuartier(x,y-1,type);
			}
		}
	}


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
				if(Table[j,k] == 15)
				{
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _Centre_commercial.rotation;
					Instantiate(_Centre_commercial,position,rotation);
				}
				if(Table[j,k] == 16)
				{
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _Centre_commercial.rotation;
					Instantiate(_Centre_commercial,position,rotation);
				}
				if(Table[j,k] == 17)
				{
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _Centre_commercial.rotation;
					Instantiate(_Centre_commercial,position,rotation);
				}
				if(Table[j,k] == 18)
				{
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _Centre_commercial.rotation;
					Instantiate(_Centre_commercial,position,rotation);
				}
				if(Table[j,k] == 19)
				{
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _Centre_commercial.rotation;
					Instantiate(_Centre_commercial,position,rotation);
				}
			}
		}

	}
	
	#region fonction de tracé de route
	//fonction qui envoi la position de la poursuite de la route
	public void NewRoad(Position _NewPosition  , int[,] _TownTable){

		int _newdirection;

		bool no_path = false;
		bool N_Iscorrect = true;
		bool E_Iscorrect = true;
		bool S_Iscorrect = true;
		bool W_Iscorrect = true;

		do{

			_newdirection = (int)Random.Range(1,5); 	//randome entre 1 et 4 (5 pour 4 attention!)

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
				if(	_NewPosition.x-1 >= 0	&&
				   	_NewPosition.y-1 >= 0 	&&
				   	_NewPosition.y+1 < 100	){
					if((_TownTable[		_NewPosition.x-1,	_NewPosition.y -1 	] 	!= -1000 && _TownTable[	_NewPosition.x-1,	_NewPosition.y-1 	] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x-1,	_NewPosition.y 		] 	!= -1000 && _TownTable[	_NewPosition.x-1,	_NewPosition.y 		] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x-1,	_NewPosition.y +1	] 	!= -1000 && _TownTable[	_NewPosition.x-1,	_NewPosition.y+1	] 	!= 1 ) )
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
				if(	_NewPosition.y+1 <  100	&&
					_NewPosition.x-1 >= 0 	&&
				   	_NewPosition.x+1 <	100){

					if(	(_TownTable[	_NewPosition.x+1,	_NewPosition.y+1 ] 	!= -1000 && _TownTable[_NewPosition.x+1,	_NewPosition.y+1 	] 	!= 1 ) &&
					   	(_TownTable[	_NewPosition.x,		_NewPosition.y+1 ] 	!= -1000 && _TownTable[_NewPosition.x,		_NewPosition.y+1 	] 	!= 1 ) &&
				   		(_TownTable[	_NewPosition.x-1,	_NewPosition.y+1 ] 	!= -1000 && _TownTable[_NewPosition.x-1,	_NewPosition.y+1 	] 	!= 1 ) )
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
				if(	_NewPosition.x+1 < 100	&&
				   _NewPosition.y-1 >= 0 	&&
				   _NewPosition.y+1 < 100	){
					if((_TownTable[		_NewPosition.x+1,	_NewPosition.y+1 ] 	!= -1000 && _TownTable[_NewPosition.x+1,	_NewPosition.y+1 ] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x+1,	_NewPosition.y 	] 	!= -1000 && _TownTable[_NewPosition.x+1,	_NewPosition.y 	] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x+1,	_NewPosition.y-1 ] 	!= -1000 && _TownTable[_NewPosition.x+1,	_NewPosition.y-1 ]	!= 1 ) )
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
				if(	_NewPosition.y-1 >= 0	&&
				   _NewPosition.x-1 >= 0 	&&
				   _NewPosition.x+1 < 100	){
					if((_TownTable[		_NewPosition.x+1,	_NewPosition.y-1 ] 	!= -1000 && _TownTable[_NewPosition.x+1,	_NewPosition.y-1 ] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x,		_NewPosition.y-1 ] 	!= -1000 && _TownTable[_NewPosition.x,		_NewPosition.y-1 ] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x-1,	_NewPosition.y-1 ] 	!= -1000 && _TownTable[_NewPosition.x-1,	_NewPosition.y-1 ] 	!= 1 ) )
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
	#endregion

	#endregion
	

	// Update is called once per frame
	void Update () {
	
	}


}

