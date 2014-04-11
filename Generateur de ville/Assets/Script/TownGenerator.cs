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
	Transform _route_et_virage;

	[SerializeField]
	Transform _route_cul_de_sac;

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
	public static int taille = 150;	//minimum 50

	public static int[,] _TownTable;
	/*
	 * signification des nombres :
	 * 0 ==> Nothing
	 * 1 ==> Simple Road
	 * 2 ==> secondary road
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
		_TownTable = new int[taille,taille];
		_TownTable = TableIntitialisation(taille);
		bool _position_is_road;

		//generation des 20% routes de la ville aleatoirement a partir du centre de la carte
		for(int i =0; i< taille*20/100 ; i++){

			_position_is_road = false;
			Position _position= new Position();

			do{
				//generation aleatoire de point de depart de la route
				int _x = UnityEngine.Random.Range(taille/2 , taille/2 + taille/4);
				int _y =	UnityEngine.Random.Range(taille/2 , taille/2 + taille/4);
				_position.SetPosition(_x,_y);
			}while(IsRoute("N", _position, _TownTable) || IsRoute("E", _position, _TownTable) || IsRoute("S", _position, _TownTable) || IsRoute("WN", _position, _TownTable));

			//lancement de la construction de route
			for(int j=0; j<4;j++){
				RoadCreation(_position, _TownTable, taille);
			}
			


		}

		//generation des quartiers
		for(int j = 0; j < taille - 1; j++)
		{
			for(int k = 0; k < taille - 1; k++)
			{
				if(_TownTable[j,k] == 0)
				{
					int typeQuartier = UnityEngine.Random.Range(15,19);
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
		}
	}

	public void creerQuartier(int x, int y, int type)
	{
		_TownTable[x,y] = type;
		//si on est a la fin du tableau
		if(x<taille-1)
		{
			//si ya une route non principal on vas chercher plus loin!
			//on renvoie en bas!
		if(_TownTable[x+1,y] == 0)
			{
				creerQuartier(x+1,y,type);
			}
		}
		//si on est a la fin du tableau
		if(y<taille-1)
		{
			//on renvoie a droite
			if(_TownTable[x,y+1] == 0)
			{
				creerQuartier(x,y+1,type);
			}
		}
		//si on est au début du tableau
		if(x>0)
		{
			//on envoie en haut!
			if(_TownTable[x-1,y] == 0)
			{
				creerQuartier(x-1,y,type);
			}
		}
		//si on est au début du tableau
		if(y>0)
		{
			//on envoie a gauche!
			if(_TownTable[x,y-1] == 0)
			{
				creerQuartier(x,y-1,type);
			}
		}
	}

	void DessinerRoute(int j, int k,int[,] _TownTable){
		Position _thisPosition = new Position(j,k);

		//routes droite
		if( IsRoute("N", _thisPosition, _TownTable) && IsRoute("S", _thisPosition, _TownTable) &&
		   	!IsRoute("E", _thisPosition, _TownTable) && !IsRoute("W", _thisPosition, _TownTable)  ){
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,0);	//rotation de 90° sur la route
			Instantiate(_route_droite,position,rotation);
		}
		else if( IsRoute("E", _thisPosition, _TownTable) && IsRoute("W", _thisPosition, _TownTable) &&
		   !IsRoute("N", _thisPosition, _TownTable) && !IsRoute("S", _thisPosition, _TownTable)  ){
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,90);	//pas de rotation de 90° sur la route
			Instantiate(_route_droite,position,rotation);
		}

		//angles a 90°
		else if( IsRoute("S", _thisPosition, _TownTable) && IsRoute("W", _thisPosition, _TownTable)	&&
		   !IsRoute("N", _thisPosition, _TownTable)&& !IsRoute("E", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,-90);			//pas de rotation sur la route
			Instantiate(_route_angle,position,rotation);
			
		}
		else if( IsRoute("S", _thisPosition, _TownTable) && IsRoute("E", _thisPosition, _TownTable)	&&
		   	!IsRoute("W", _thisPosition, _TownTable)&& !IsRoute("N", _thisPosition, _TownTable) ){

			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,180);			//rotation de -90° sur la route
			Instantiate(_route_angle,position,rotation);
			
		}
		else if( IsRoute("W", _thisPosition, _TownTable) && IsRoute("N", _thisPosition, _TownTable)	&&
		   !IsRoute("S", _thisPosition, _TownTable)&& !IsRoute("E", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,0);
			Instantiate(_route_angle,position,rotation);
			
		}
		else if( IsRoute("N", _thisPosition, _TownTable) && IsRoute("E", _thisPosition, _TownTable)	&&
		   !IsRoute("S", _thisPosition, _TownTable)&& !IsRoute("W", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,90);
			Instantiate(_route_angle,position,rotation);
			
		}

		//routes et virage
		else if( IsRoute("S", _thisPosition, _TownTable) && IsRoute("E", _thisPosition, _TownTable)	&&
		        IsRoute("W", _thisPosition, _TownTable)	 &&	!IsRoute("N", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,-90);
			Instantiate(_route_et_virage,position,rotation);
			
		}
		else if( IsRoute("S", _thisPosition, _TownTable) && IsRoute("N", _thisPosition, _TownTable)	&&
		        IsRoute("W", _thisPosition, _TownTable)	 &&	!IsRoute("E", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,0);
			Instantiate(_route_et_virage,position,rotation);
			
		}
		else if( IsRoute("S", _thisPosition, _TownTable) && IsRoute("N", _thisPosition, _TownTable)	&&
		        IsRoute("E", _thisPosition, _TownTable)	 &&	!IsRoute("W", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,180);
			Instantiate(_route_et_virage,position,rotation);
			
		}
		else if( IsRoute("W", _thisPosition, _TownTable) && IsRoute("N", _thisPosition, _TownTable)	&&
		        IsRoute("E", _thisPosition, _TownTable)	 &&	!IsRoute("S", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,90);
			Instantiate(_route_et_virage,position,rotation);
			
		}

		//routes cul de sac
		else if( IsRoute("N", _thisPosition, _TownTable) && !IsRoute("W", _thisPosition, _TownTable) &&
		        !IsRoute("E", _thisPosition, _TownTable) &&	!IsRoute("S", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,0);
			Instantiate(_route_cul_de_sac,position,rotation);
			
		}
		else if( IsRoute("W", _thisPosition, _TownTable) && !IsRoute("N", _thisPosition, _TownTable) &&
		        !IsRoute("E", _thisPosition, _TownTable) &&	!IsRoute("S", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,-90);
			Instantiate(_route_cul_de_sac,position,rotation);
			
		}
		else if( IsRoute("E", _thisPosition, _TownTable) && !IsRoute("N", _thisPosition, _TownTable) &&
		        !IsRoute("W", _thisPosition, _TownTable) &&	!IsRoute("S", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,90);
			Instantiate(_route_cul_de_sac,position,rotation);
			
		}
		else if( IsRoute("S", _thisPosition, _TownTable) && !IsRoute("N", _thisPosition, _TownTable) &&
		        !IsRoute("W", _thisPosition, _TownTable) &&	!IsRoute("E", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,180);
			Instantiate(_route_cul_de_sac,position,rotation);
			
		}

		//carefour
		else if( IsRoute("N", _thisPosition, _TownTable) && IsRoute("E", _thisPosition, _TownTable)	&&
		   IsRoute("S", _thisPosition, _TownTable)	&& IsRoute("W", _thisPosition, _TownTable) ){
			
			Vector3 position = new Vector3(j,0,k);
			Quaternion rotation = Quaternion.Euler(-90,0,0);
			Instantiate(_route_croisement,position,rotation);
			
		}
		else{

			Debug.Log ("erreur pas de generation : ("+_thisPosition.x+","+_thisPosition.y+")");
		}

	}

	//permet de verifier a partir d'une position si oui ou non il ya une route dans cette direction
	bool IsRoute(string _direction,Position _P, int[,] _TownTable){

		if(_direction == "N"){
			if( _TownTable[_P.x-1, _P.y] == 1){
				return true;
			}
			else{
				return false;
			}
		}
		else if(_direction == "S"){
			if( _TownTable[_P.x+1, _P.y] == 1){
				return true;
			}
			else{
				return false;
			}
		}
		else if(_direction == "E"){
			if( _TownTable[_P.x, _P.y+1] == 1){
				return true;
			}
			else{
				return false;
			}
		}
		else if(_direction == "W"){
			if( _TownTable[_P.x, _P.y-1] == 1){
				return true;
			}
			else{
				return false;
			}
		}
		else{
			return false;
			Debug.Log("erreur fonction : cette direction n'existe pas !");
		}

	}
	
	
	void Instanciate(int[,] Table){
		
		for (int j=0 ; j < taille ; j++){
			for (int k=0 ; k < taille ; k++){

				if(Table[j,k] == 1){
					//pas afficher les routes des bords
					if( !(j == 0 || k == 0 || k >= taille-1 || j >= taille-1 ) )
					{	
						//fonction qui instancie la route en fonction de son voisin
						DessinerRoute(j,k,_TownTable);
					}
				}

				if(Table[j,k] == 2)
				{
					//dessin des routes secondaires
				
				}
				if(Table[j,k] == 15)
				{
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
					int randomBatTaille = UnityEngine.Random.Range(10,42);
					int randomBat = UnityEngine.Random.Range(1,42);
					if(randomBat <=19)
					{
						_immeuble_grand.transform.localScale = new Vector3(0.4f,0.2f,randomBatTaille/100f);
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _immeuble_grand.rotation;
						Instantiate(_immeuble_grand,position,rotation);
					}
					if(randomBat >19 && randomBat<=38)
					{
						_immeuble_grand2.transform.localScale = new Vector3(0.4f,0.2f,randomBatTaille/100f);
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _immeuble_grand2.rotation;
						Instantiate(_immeuble_grand2,position,rotation);

					}
					if(randomBat > 38 && (_TownTable[j+1,k]==1 || _TownTable[j-1,k]==1 || _TownTable[j,k-1]==1 || _TownTable[j,k+1]==1))
					{
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _statue.rotation;
						Instantiate(_statue,position,rotation);
					}
					else
					{
						_immeuble_grand2.transform.localScale = new Vector3(0.4f,0.2f,randomBatTaille/100f);
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _immeuble_grand2.rotation;
						Instantiate(_immeuble_grand2,position,rotation);
					}
				}
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
				_newdirection = UnityEngine.Random.Range(1,5);
			}
			_direction_tested = true;

			//verification que la direction est bonne
			if(_newdirection == 1 && N_Iscorrect == true){
				if(	_NewPosition.x-1 >= 1	&&
				   	_NewPosition.y-1 >= 1 	&&
				   	_NewPosition.y+1 < 150-1	){
					if((_TownTable[		_NewPosition.x-1,	_NewPosition.y -1 	] 	!= -1500 && _TownTable[	_NewPosition.x-1,	_NewPosition.y-1 	] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x-1,	_NewPosition.y 		] 	!= -1500 && _TownTable[	_NewPosition.x-1,	_NewPosition.y 		] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x-1,	_NewPosition.y +1	] 	!= -1500 && _TownTable[	_NewPosition.x-1,	_NewPosition.y+1	] 	!= 1 ))
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
				if(	_NewPosition.y+1 <  150-1	&&
					_NewPosition.x-1 >= 1 	&&
				   	_NewPosition.x+1 <	150-1){

					if(	(_TownTable[	_NewPosition.x+1,	_NewPosition.y+1 ] 	!= -1500 && _TownTable[_NewPosition.x+1,	_NewPosition.y+1 	] 	!= 1 ) &&
					   	(_TownTable[	_NewPosition.x,		_NewPosition.y+1 ] 	!= -1500 && _TownTable[_NewPosition.x,		_NewPosition.y+1 	] 	!= 1 ) &&
				   		(_TownTable[	_NewPosition.x-1,	_NewPosition.y+1 ] 	!= -1500 && _TownTable[_NewPosition.x-1,	_NewPosition.y+1 	] 	!= 1 ))
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
				if(	_NewPosition.x+1 < 150-1	&&
				   _NewPosition.y-1 >= 1 	&&
				   _NewPosition.y+1 < 150-1	){
					if((_TownTable[		_NewPosition.x+1,	_NewPosition.y+1 ] 	!= -1500 && _TownTable[_NewPosition.x+1,	_NewPosition.y+1 ] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x+1,	_NewPosition.y 	] 	!= -1500 && _TownTable[_NewPosition.x+1,	_NewPosition.y 	] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x+1,	_NewPosition.y-1 ] 	!= -1500 && _TownTable[_NewPosition.x+1,	_NewPosition.y-1 ]	!= 1 ))
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
				   _NewPosition.x+1 < 150-1	){
					if((_TownTable[		_NewPosition.x+1,	_NewPosition.y-1 ] 	!= -1500 && _TownTable[_NewPosition.x+1,	_NewPosition.y-1 ] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x,		_NewPosition.y-1 ] 	!= -1500 && _TownTable[_NewPosition.x,		_NewPosition.y-1 ] 	!= 1 ) &&
					   (_TownTable[		_NewPosition.x-1,	_NewPosition.y-1 ] 	!= -1500 && _TownTable[_NewPosition.x-1,	_NewPosition.y-1 ] 	!= 1 ))
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

