using UnityEngine;
using System.Collections;
using System;

public class TownGenerator : MonoBehaviour {

	//routes
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
	Transform _route_secondaire;

	[SerializeField]
	Transform _Centre_commercial;
	[SerializeField]
	Transform _centre_commercial2;
	[SerializeField]
	Transform _immeuble_grand;
	[SerializeField]
	Transform _immeuble_grand2;
	[SerializeField]
	Transform _immeuble_grand3;
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

		//creation d'une classe ville
		Ville _NotreVille = new Ville();

		//generation des quartiers
		for(int j = 0; j < taille - 1; j++)
		{
			for(int k = 0; k < taille - 1; k++)
			{
				if(_TownTable[j,k] == 0)
				{
					//generation aleatoire d'un nouveua quartier
					int typeQuartier = UnityEngine.Random.Range(15,19);

					//creation d'un nouveau quartier (qui recupère son type)
					quartier _Nouveau_Quartier = new quartier(typeQuartier);

					//generation du nouveau quartier (a partir des routes tracées)
					creerQuartier(j,k,typeQuartier, _Nouveau_Quartier);


					//nouvelle instance de quartier copiée afin de l'ajouter a la ville
					quartier _UnQuartier = new quartier( _Nouveau_Quartier );

					//Ajout du quartier a la ville
					_NotreVille.AjouterQuartier(_UnQuartier);

				}
			}
		}

		//mettre a zero les contour de la ville
		int numeroCase = _TownTable[5,5];
		MettreAZero(5,5,numeroCase);

		/*

		//generation des routes secondaires (pas de soucis)
		for(int i=0; i< _NotreVille.Taille ; i++){
			quartier _QuartierCourant = new quartier(_NotreVille.MesQuartier[i]);

			bool boucle = false;

			//pour chaque quartier de notre ville on cré un certain nombre de routes dépendant de la taille du quartier
			if(_QuartierCourant.Taille>20){
				for(int j =0; j < _QuartierCourant.Taille*10/100; j++){
					int count = 0;
					while(!boucle && count < 300){

						boucle = GenererRouteSecondaire(_QuartierCourant, _TownTable);
						Debug.Log(boucle);
						count++;
					}
				}
			}

		}

		*/


		//instanciations de tous les objets dans la scène
		Instanciate(_TownTable);

	}
	
	/*
	 * Fonctions
	 * 
	 */

	public bool GenererRouteSecondaire(quartier _QuartierCourant, int[,] _TownTable){

		Debug.Log(" generation d'une route secondaire ");

		Position _PositionFin= new Position();
		Position _PositionDeDepart  = new Position();
		Position _referencePos = new Position();
		bool _FoundWay = false;
		int nb_roads;
		int max_roads;
		bool _CheminEstCorrect = false;
		string _direction ="null";

		//choix aleatoire d'une position de route primaire
		int _indiceRoute = UnityEngine.Random.Range(0 ,_QuartierCourant.TailleRoutesP);

		//recupère la position de la route a la position _indiceRoute
		_PositionDeDepart.SetPosition( _QuartierCourant.MesRoutesPrimaires[_indiceRoute] );

		//recuperation d'une position d'arrivée possible
		_PositionFin.SetPosition( _QuartierCourant.FindSisterRoad(_PositionDeDepart ) );
	

		//si la route possède un voie possible
		if( !(_PositionFin.Equals(_referencePos) ) ){
			Debug.Log(" positon fin != position courante ");

			//calcul de la direction
			if( _PositionFin.x > _PositionDeDepart.x){
				_direction = "S";
			}
			else if( _PositionFin.x < _PositionDeDepart.x ){
				_direction = "N";
			}
			else if( _PositionFin.y > _PositionDeDepart.y ){
				_direction = "E";
			}
			else if( _PositionFin.y < _PositionDeDepart.y){
				_direction = "W";
			}
			else{
				Debug.Log ("error de direction de la route secondaire");
			}

			//calcul la taille de la route maximum
			int _taille_chemin_maximum = 0;
			int _taille_finale = 0;

			if(_direction == "N" ){
				_taille_chemin_maximum = _PositionFin.x - _PositionDeDepart.x-1;
				_taille_finale = _taille_chemin_maximum;//UnityEngine.Random.Range(1 , _taille_chemin_maximum+1);

				//test si le tracé est correct et trace
				for(int i = _PositionDeDepart.x-1; i >= (_PositionDeDepart.x - _taille_finale) ; i--){
					if(_TownTable[i,_PositionFin.y]   != 2 && _TownTable[i,_PositionFin.y] 	 != 1 &&
					   _TownTable[i,_PositionFin.y+1] != 2 && _TownTable[i,_PositionFin.y+1] != 1 &&
					   _TownTable[i,_PositionFin.y-1] != 2 && _TownTable[i,_PositionFin.y-1] != 1 )
					{
						_TownTable[i,_PositionFin.y] = 2;
						_CheminEstCorrect = true;

					}
				}

			}
			else if( _direction == "S"){
				_taille_chemin_maximum = _PositionDeDepart.x - _PositionFin.x -1;
				_taille_finale = _taille_chemin_maximum;//UnityEngine.Random.Range(1 , _taille_chemin_maximum+1);

				for(int i = _PositionDeDepart.x+1; i <= (_PositionDeDepart.x + _taille_finale) ; i++){
					if(_TownTable[i,_PositionFin.y]   != 2 && _TownTable[i,_PositionFin.y] 	 != 1 &&
					   _TownTable[i,_PositionFin.y+1] != 2 && _TownTable[i,_PositionFin.y+1] != 1 &&
					   _TownTable[i,_PositionFin.y-1] != 2 && _TownTable[i,_PositionFin.y-1] != 1 )
					{
						_TownTable[i,_PositionFin.y] = 2;
						_CheminEstCorrect = true;
					}
				}

			}
			else if(_direction == "E"){
				_taille_chemin_maximum = _PositionFin.y - _PositionDeDepart.y -1;
				_taille_finale = _taille_chemin_maximum;//UnityEngine.Random.Range(1 , _taille_chemin_maximum+1);

				//tracé de la route vers l'est
				for(int i = _PositionDeDepart.y+1; i <= (_PositionDeDepart.x + _taille_finale) ; i++){
					if(_TownTable[i,_PositionFin.x]   != 2 && _TownTable[i,_PositionFin.x] 	 != 1 &&
					   _TownTable[i,_PositionFin.x+1] != 2 && _TownTable[i,_PositionFin.x+1] != 1 &&
					   _TownTable[i,_PositionFin.x-1] != 2 && _TownTable[i,_PositionFin.x-1] != 1 )
					{
						Debug.Log ("generation d'une porotion de route realisée");
						_TownTable[i,_PositionFin.y] = 2;
						_CheminEstCorrect = true;
					}
				}

			}
			else if( _direction == "W"){
				_taille_chemin_maximum = _PositionDeDepart.y - _PositionFin.y -1;
				_taille_finale = _taille_chemin_maximum;//UnityEngine.Random.Range(1 , _taille_chemin_maximum+1);

				//tracé de la route vers l'est
				for(int i = _PositionDeDepart.y-1; i > (_PositionDeDepart.x - _taille_finale) ; i--){
					if(_TownTable[i,_PositionFin.x]   != 2 && _TownTable[i,_PositionFin.x] 	 != 1 &&
					   _TownTable[i,_PositionFin.x+1] != 2 && _TownTable[i,_PositionFin.x+1] != 1 &&
					   _TownTable[i,_PositionFin.x-1] != 2 && _TownTable[i,_PositionFin.x-1] != 1 )
					{
						_TownTable[i,_PositionFin.y] = 2;
						_CheminEstCorrect = true;
					}
				}
				
			}


		}

		return _CheminEstCorrect;

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


	//julien : j'ai modifié ce script pour qu'il remplisse la classe quartier (qui sert a créer les routes secondaires)
	public void creerQuartier(int x, int y, int type, quartier _Nouveau_Quartier)
	{
		_TownTable[x,y] = type;

		//ajout de la position actuelle au tableau de positions
		Position _newPos = new Position(x,y);
		_Nouveau_Quartier.AddPosition(_newPos);

		//si on est a la fin du tableau
		if(x<taille-1){
			//si ya une route non principal on vas chercher plus loin!
			//on renvoie en bas!
			if(_TownTable[x+1,y] == 0){
				creerQuartier(x+1,y, type, _Nouveau_Quartier);
			}
			else if(_TownTable[x+1,y] == 1){
				_newPos.SetPosition(x+1,y);
				_Nouveau_Quartier.AddRoutePrimaire(_newPos);
			}
		}
		//si on est a la fin du tableau
		if(y<taille-1){

			//on renvoie a droite
			if(_TownTable[x,y+1] == 0){
				creerQuartier(x,y+1,type, _Nouveau_Quartier);
			}
			else if(_TownTable[x,y+1] == 1){
				_newPos.SetPosition(x,y+1);
				_Nouveau_Quartier.AddRoutePrimaire(_newPos);
			}

		}
		//si on est au début du tableau
		if(x>0){

			//on envoie en haut!
			if(_TownTable[x-1,y] == 0){
				creerQuartier(x-1,y,type, _Nouveau_Quartier);
			}
			else if(_TownTable[x-1,y] ==  1){
				_newPos.SetPosition(x-1,y);
				_Nouveau_Quartier.AddRoutePrimaire(_newPos);
			}

		}
		//si on est au début du tableau
		if(y>0){

			//on envoie a gauche!
			if(_TownTable[x,y-1] == 0){
				creerQuartier(x,y-1,type, _Nouveau_Quartier);
			}
			else if(_TownTable[x,y-1] == 1){
				_newPos.SetPosition(x,y-1);
				_Nouveau_Quartier.AddRoutePrimaire(_newPos);
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
			}
			else{
				//roader (c'est a dire ajouter la route au tableau
				_ActualPosition.SetPosition(_NewPosition);
				_TownTable[ _ActualPosition.x, _ActualPosition.y ] = 1;
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
			
			//Debug.Log ("erreur pas de generation : ("+_thisPosition.x+","+_thisPosition.y+")");
		}
		
	}

	//permet de verifier a partir d'une position si oui ou non il ya une route dans cette direction
	bool IsRoute(string _direction,Position _P, int[,] _TownTable){
		
		if(_direction == "N"){
			if( _TownTable[_P.x-1, _P.y] == 1 || _TownTable[_P.x-1, _P.y] == 2){
				return true;
			}
			else{
				return false;
			}
		}
		else if(_direction == "S"){
			if( _TownTable[_P.x+1, _P.y] == 1 || _TownTable[_P.x+1, _P.y] == 2){
				return true;
			}
			else{
				return false;
			}
		}
		else if(_direction == "E"){
			if( _TownTable[_P.x, _P.y+1] == 1 || _TownTable[_P.x, _P.y+1] == 2){
				return true;
			}
			else{
				return false;
			}
		}
		else if(_direction == "W"){
			if( _TownTable[_P.x, _P.y-1] == 1 || _TownTable[_P.x, _P.y-1] == 2){
				return true;
			}
			else{
				return false;
			}
		}
		else{
			return false;
			//Debug.Log("erreur fonction : cette direction n'existe pas !");
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

	
	void Instanciate(int[,] Table){
		
		for (int j=0 ; j < taille ; j++){
			for (int k=0 ; k < taille ; k++){
				
				if(Table[j,k] == 1 /*|| Table[j,k] == 2*/){
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
					Vector3 position = new Vector3(j,0,k);
					Quaternion rotation = _route_secondaire.rotation;
					Instantiate(_route_secondaire,position,rotation);

					
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
						_immeuble_grand.transform.localScale = new Vector3(0.02f,0.02f,randomBatTaille/1000f);
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _immeuble_grand.rotation;
						Instantiate(_immeuble_grand,position,rotation);
					}
					if(randomBat >19 && randomBat<=38)
					{
						_immeuble_grand2.transform.localScale = new Vector3(0.002f,randomBatTaille/20000f,0.002f);
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
					/*if(randomBat > 38 && !(_TownTable[j+1,k]==1 || _TownTable[j-1,k]==1 || _TownTable[j,k-1]==1 || _TownTable[j,k+1]==1))
					{
						_immeuble_grand2.transform.localScale = new Vector3(0.4f,0.2f,randomBatTaille/100f);
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _immeuble_grand2.rotation;
						Instantiate(_immeuble_grand2,position,rotation);
					}*/
				}
			}
		}
		
	}


}

