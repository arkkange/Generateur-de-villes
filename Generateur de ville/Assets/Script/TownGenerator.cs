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
<<<<<<< HEAD
	public static int taille = 200;	//minimum 50
=======
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
	public static int taille = 150;	//minimum 50
>>>>>>> origin/Louis

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

		Vector3 DA_VOCOTOR = new Vector3(0.1f,0.1f,0.1f);
		_Centre_commercial.transform.localScale = DA_VOCOTOR;
		_maison.transform.transform.localScale = DA_VOCOTOR;
		_maison1.transform.localScale = DA_VOCOTOR;
		_maison2.transform.localScale = DA_VOCOTOR;
		_maison3.transform.localScale = DA_VOCOTOR;
		_maison4.transform.localScale = DA_VOCOTOR;
		_maison5.transform.localScale = DA_VOCOTOR;
		_centre_commercial2.transform.localScale = DA_VOCOTOR;
		_park.transform.localScale = DA_VOCOTOR;
		_champ.transform.localScale = new Vector3(0.4f,0.4f,0.001f);
		_statue.transform.localScale = DA_VOCOTOR;
		_immeuble_petit.transform.localScale = DA_VOCOTOR;
		_immeuble_petit2.transform.localScale = DA_VOCOTOR;
		_immeuble_grand.transform.localScale = DA_VOCOTOR;
		_immeuble_grand2.transform.localScale = DA_VOCOTOR;
		_TownTable = new int[taille,taille];
		_TownTable = TableIntitialisation(taille);
		bool _position_is_road;

		//generation des 20 routes de la ville aleatoirement a partir du centre de la carte
		for(int i =0; i< 200*20/100 ; i++){

<<<<<<< HEAD
			_position_is_road = false;
=======
>>>>>>> origin/Louis
			Position _position= new Position();

			
			//generation aleatoire de point de depart de la route
<<<<<<< HEAD
			int _x = (int)UnityEngine.Random.Range(taille/2 , taille/2 + taille/4);
			int _y =	(int)UnityEngine.Random.Range(taille/2 , taille/2 + taille/4);
=======
			int _x = UnityEngine.Random.Range(taille/2 , taille/2 + taille/4);
			int _y = UnityEngine.Random.Range(taille/2 , taille/2 + taille/4);
>>>>>>> origin/Louis
			_position.SetPosition(_x,_y);


			//lancement de la construction de route
			for(int j=0; j<4;j++){
				RoadCreation(_position, _TownTable, taille);
			}
<<<<<<< HEAD
=======
		}

		for(int j = 0; j < taille - 1; j++)
		{
			for(int k = 0; k < taille - 1; k++)
			{
				if(_TownTable[j,k] == 0)
				{
				int typeQuartier = UnityEngine.Random.Range(15,20);
				creerQuartier(j,k,typeQuartier);
				}
			}
		}
		int numeroCase = _TownTable[5,5];
		MettreAZero(5,5,numeroCase);
		//instanciations de tous les objets dans la scène
		Instanciate(_TownTable);

	}
	
	/*
	 * Fonctions
	 * 
	 */
	public void MettreAZero(int x, int y, int numnerOfDaBeast)
	{
		_TownTable[x,y] = 0;
		if(x<taille-1)
		{
			//si ya une route non principal on vas chercher plus loin!
			//on renvoie en bas!
			if(_TownTable[x+1,y] == numnerOfDaBeast)
			{
				MettreAZero(x+1,y,numnerOfDaBeast);
			}
		}
		//si on est a la fin du tableau
		if(y<taille-1)
		{
			//on renvoie a droite
			if(_TownTable[x,y+1] == numnerOfDaBeast)
			{
				MettreAZero(x,y+1,numnerOfDaBeast);
			}
		}
		//si on est au début du tableau
		if(x>0)
		{
			//on envoie en haut!
			if(_TownTable[x-1,y] == numnerOfDaBeast)
			{
				MettreAZero(x-1,y,numnerOfDaBeast);
			}
		}
		//si on est au début du tableau
		if(y>0)
		{
			//on envoie a gauche!
			if(_TownTable[x,y-1] == numnerOfDaBeast)
			{
				MettreAZero(x,y-1,numnerOfDaBeast);
			}
>>>>>>> origin/Louis
		}
	}

<<<<<<< HEAD
		//instanciations de tous les objets dans la scène
		Instanciate(_TownTable);
=======
	public void creerQuartier(int x, int y, int type)
	{
		_TownTable [x, y] = type;
		//si on est a la fin du tableau
		if (x < taille - 1) {
						//si ya une route non principal on vas chercher plus loin!
						//on renvoie en bas!
						if (_TownTable [x + 1, y] == 0) {
								creerQuartier (x + 1, y, type);
						}
				}
		if (y < taille - 1) {

						//on renvoie a droite
						if (_TownTable [x, y + 1] == 0) {
								creerQuartier (x, y + 1, type);
						}
				}
		if (x > 0) {
>>>>>>> origin/Louis

						//on envoie en haut!
						if (_TownTable [x - 1, y] == 0) {
								creerQuartier (x - 1, y, type);
						}
				}
		if(y>0){
						//on envoie a gauche!
						if (_TownTable [x, y - 1] == 0) {
								creerQuartier (x, y - 1, type);
						}
				}
	}
	
	
	void Instanciate(int[,] Table){
		
		for (int j=0 ; j < taille ; j++){
			for (int k=0 ; k < taille ; k++){
				if(Table[j,k] == 1){
					//pas afficher les routes des bords
					if(j == 0 || k == 0)
					{
						
					}
					else
					{
						
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _route_croisement.rotation;
						Instantiate(_route_croisement,position,rotation);
					}
				}

				if(Table[j,k] == 2)
				{
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _route_droite.rotation;
					Instantiate(_route_droite,position,rotation);
				
				}
				if(Table[j,k] == 15)
				{

<<<<<<< HEAD

	/*
	 * Fonctions
	 * 
	 */

=======
						
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _Centre_commercial.rotation;
						Instantiate(_Centre_commercial,position,rotation);

				}
				
				if(Table[j,k] == 16)
				{
					int DA_RANDOM = UnityEngine.Random.Range(0,5);
					if(DA_RANDOM == 0)
					{
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _maison.rotation;
						Instantiate(_maison,position,rotation);
					}
					if(DA_RANDOM == 1)
					{
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _maison1.rotation;
						Instantiate(_maison1,position,rotation);
					}
					if(DA_RANDOM == 2)
					{
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _maison2.rotation;
						Instantiate(_maison2,position,rotation);
					}
					if(DA_RANDOM == 3)
					{
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _maison3.rotation;
						Instantiate(_maison3,position,rotation);
					}
					if(DA_RANDOM == 4)
					{
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _maison4.rotation;
						Instantiate(_maison4,position,rotation);
					}
					if(DA_RANDOM == 5)
					{
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _maison5.rotation;
						Instantiate(_maison5,position,rotation);
					}
					
				}
				if(Table[j,k] == 17)
				{
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _champ.rotation;
					Instantiate(_champ,position,rotation);
				}
				if(Table[j,k] == 18)
				{
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _immeuble_grand.rotation;
					Instantiate(_immeuble_grand,position,rotation);
				}
				if(Table[j,k] == 19)
				{
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _statue.rotation;
					Instantiate(_statue,position,rotation);
				}
			}
		}
		
	}
>>>>>>> origin/Louis

	int[,] TableIntitialisation(int taille){

		int[,] table = new int[taille,taille];

		for (int j=0 ; j < taille ; j++){
			for (int k=0 ; k < taille ; k++){
				table[j,k]= 0;
			}
		}

		return table;
	}

	/*void Instanciate(int[,] Table){

		for (int j=0 ; j < taille ; j++){
			for (int k=0 ; k < taille ; k++){
				if(Table[j,k] == 1){
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _route_croisement.rotation;
					Instantiate(_route_croisement,position,rotation);
				}
			}
		}

	}*/


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
				_direction = UnityEngine.Random.Range(1,5);
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
				//debug.Log("end of road because new position equals actual position");
				//debug.Log("_NewPosition : "+_NewPosition.x+","+_NewPosition.y+" & _ActualPosition : "+_ActualPosition.x+","+_ActualPosition.y);
			}
			else{
				//roader (c'est a dire ajouter la route au tableau
				_ActualPosition.SetPosition(_NewPosition);
				_TownTable[ _ActualPosition.x, _ActualPosition.y ] = 1;
			}
			
			
		}
		
		
	}


<<<<<<< HEAD
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


=======
>>>>>>> origin/Louis
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
<<<<<<< HEAD
				_newdirection = (int)UnityEngine.Random.Range(1,5);
=======
				_newdirection = UnityEngine.Random.Range(1,5);
>>>>>>> origin/Louis
			}
			_direction_tested = true;

			//debug direction du random
			if(_newdirection == 1){
				//debug.Log("wantogo: N");
			}
			else if(_newdirection == 2){
				//debug.Log("wantogo:E");
			}
			else if(_newdirection == 3){
				//debug.Log("wantogo: S");
			}
			else if(_newdirection == 4){
				//debug.Log("wantogo: W");
			}


			//verification que la direction est bonne
			if(_newdirection == 1 && N_Iscorrect == true){
				if(	_NewPosition.x-1 >= 1	&&
				   	_NewPosition.y-1 >= 1 	&&
<<<<<<< HEAD
				   	_NewPosition.y+1 < 200-1	){
=======
				   	_NewPosition.y+1 < taille-1	){
>>>>>>> origin/Louis
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
					//debug.Log ("out of bounds exception : "+ _NewPosition.x);
					N_Iscorrect = false;
				}
			}

			if(_newdirection == 2 && E_Iscorrect == true){
<<<<<<< HEAD
				if(	_NewPosition.y+1 <  200-1	&&
					_NewPosition.x-1 >= 1 	&&
				   	_NewPosition.x+1 <	200-1){
=======
				if(	_NewPosition.y+1 <  taille-1	&&
					_NewPosition.x-1 >= 1 	&&
				   	_NewPosition.x+1 <	taille-1){
>>>>>>> origin/Louis

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
					//debug.Log ("out of bounds exception : "+ _NewPosition.y);
					E_Iscorrect = false;
				}
			}

			if(_newdirection == 3 && S_Iscorrect == true){
<<<<<<< HEAD
				if(	_NewPosition.x+1 < 200-1	&&
				   _NewPosition.y-1 >= 1 	&&
				   _NewPosition.y+1 < 200-1	){
=======
				if(	_NewPosition.x+1 < taille-1	&&
				   _NewPosition.y-1 >= 1 	&&
				   _NewPosition.y+1 < taille-1	){
>>>>>>> origin/Louis
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
					//debug.Log ("out of bounds exception : "+ _NewPosition.x);
					S_Iscorrect = false;
				}
			}

			if(_newdirection == 4 && W_Iscorrect == true){
				if(	_NewPosition.y-1 >= 1	&&
				   _NewPosition.x-1 >= 1 	&&
<<<<<<< HEAD
				   _NewPosition.x+1 < 200-1	){
=======
				   _NewPosition.x+1 < taille-1	){
>>>>>>> origin/Louis
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
					//debug.Log ("out of bounds exception : "+ _NewPosition.y);
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

