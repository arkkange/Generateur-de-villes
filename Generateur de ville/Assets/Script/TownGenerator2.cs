﻿using UnityEngine;
using System.Collections;

public class TownGenerator2 : MonoBehaviour {
	
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
		Position _ActualPosition = new Position(0,0);

		for(int j = 0; j < taille; j++)
		{
			for(int k = 0; k < taille; k++)
			{
				if((j == 0 || k == 0 || j == taille - 1 || k == taille -1) && _TownTable[j,k] == 2)
				{
					Position _NewPosition = new Position(j,k);
					NewRoad2(_NewPosition, _TownTable);
				}
			}
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
		Instanciate(_TownTable);
		
	}
	
	#region functions

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
		int leRandom;
		int last = 0;
		for (int j=0 ; j < taille ; j++){
			for (int k=0 ; k < taille ; k++){
				// Louis : ici on vas générer les bases de dépars aléatoires
				if(j == 0 || k == 0)
				{
					//conditions a changer pour avoir plus ou moins de proba de routes
					leRandom = Random.Range(0,100);
					if(((j>15 && j<85) || (k>15 && k<85)) && leRandom >90 && last > 7)
					{
						table[j,k] = 2;
						last = 0;
					}
					else
					{
						table[j,k] = 1;
						last += 1;
					}
					}
				else
				{
				table[j,k]= 0;
				}
			}
		}
		
		return table;
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
					if(j <= 30 || k <= 30 || j >= 70 || k >= 70)
					{
						if(j <=20 && j >= 10 || k <= 20 && k >= 10 || j >= 80 && j <= 90|| k >= 70 && k <= 90)
						{
							int batoupas = Random.Range(0,20);
							if(batoupas <18){
								
							}
							else
							{
								Vector3 position = new Vector3(j,0,k);
								Quaternion rotation = _Centre_commercial.rotation;
								Instantiate(_Centre_commercial,position,rotation);
							}
						}
						else
						{
							int batoupas = Random.Range(0,10);
							if(batoupas >5){
								Vector3 position = new Vector3(j,0,k);
								Quaternion rotation = _Centre_commercial.rotation;
								Instantiate(_Centre_commercial,position,rotation);
							}
						}

					}
					else
					{
						Vector3 position = new Vector3(j,0,k);
						Quaternion rotation = _Centre_commercial.rotation;
						Instantiate(_Centre_commercial,position,rotation);
					}
				}

				if(Table[j,k] == 16)
				{
					int DA_RANDOM = Random.Range(0,5);
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



	//test de newroad
	//louis : ici je fais mes deux routes droites, ya juste a faire la meme chose en L-system
	public void NewRoad2(Position _NewPosition, int[,] _TownTable) {
		Position _lastPosition = (_NewPosition);
		int cpt = 0;
		bool ended = false;
		bool no_Path = false;
		int lastDirection = 0;

		//TOUT PLEINS DE CONDITIONS POUR PLEINS DE DIRECTIONS :D
		do{
			if(_NewPosition.x == 0 && ended == false)
			{
				if(_NewPosition.y < 50 && _NewPosition.y > 30)
				{
			while(_NewPosition.x != taille-1 && _NewPosition.y != taille -2 && _NewPosition.y != 0 )
			{

					int leRandomX = Random.Range(0,40);
					if(leRandomX < 35)
					{
						_NewPosition.x = _NewPosition.x+1;
						lastDirection = 0;
					}
					if(leRandomX >= 35 && leRandomX < 37 && lastDirection != 2)
					{
						_NewPosition.y = _NewPosition.y+1;
						lastDirection = 1;
					}
					if(leRandomX >= 37 && lastDirection != 1)
					{
						_NewPosition.y = _NewPosition.y-1;
						lastDirection = 2;
					}
					Debug.Log(_NewPosition.x);
				_TownTable[_NewPosition.x, _NewPosition.y] = 1;
				_lastPosition = _NewPosition;
				cpt++;
			}
				}

				if(_NewPosition.y <= 30)
				{
					while(_NewPosition.x != taille-1 && _NewPosition.y != taille -2 && _NewPosition.y != 0 )
					{
						
						int leRandomX = Random.Range(0,40);
						if(leRandomX < 25)
						{
							_NewPosition.x = _NewPosition.x+1;
							lastDirection = 0;
						}
						if(leRandomX >= 25 && leRandomX < 39 && lastDirection != 2)
						{
							_NewPosition.y = _NewPosition.y+1;
							lastDirection = 1;
						}
						if(leRandomX >= 39 && lastDirection != 1)
						{
							_NewPosition.y = _NewPosition.y-1;
							lastDirection = 2;
						}
						Debug.Log(_NewPosition.x);
						_TownTable[_NewPosition.x, _NewPosition.y] = 1;
						_lastPosition = _NewPosition;
						cpt++;
					}
				}
				if(_NewPosition.y >= 50 && _NewPosition.y < 80)
				{
					while(_NewPosition.x != taille-1 && _NewPosition.y != taille -2 && _NewPosition.y != 0 )
					{
						
						int leRandomX = Random.Range(0,40);
						if(leRandomX < 35)
						{
							_NewPosition.x = _NewPosition.x+1;
							lastDirection = 0;
						}
						if(leRandomX >= 35 && leRandomX < 37 && lastDirection != 2)
						{
							_NewPosition.y = _NewPosition.y-1;
							lastDirection = 1;
						}
						if(leRandomX >= 37 && lastDirection != 1)
						{
							_NewPosition.y = _NewPosition.y+1;
							lastDirection = 2;
						}
						Debug.Log(_NewPosition.x);
						_TownTable[_NewPosition.x, _NewPosition.y] = 1;
						_lastPosition = _NewPosition;
						cpt++;
					}
				}
				if(_NewPosition.y >= 80)
				{
					while(_NewPosition.x != taille-1 && _NewPosition.y != taille -2 && _NewPosition.y != 0 )
					{
						
						int leRandomX = Random.Range(0,40);
						if(leRandomX < 25)
						{
							_NewPosition.x = _NewPosition.x+1;
							lastDirection = 0;
						}
						if(leRandomX >= 25 && leRandomX < 39 && lastDirection != 2)
						{
							_NewPosition.y = _NewPosition.y-1;
							lastDirection = 1;
						}
						if(leRandomX >= 39 && lastDirection != 1)
						{
							_NewPosition.y = _NewPosition.y+1;
							lastDirection = 2;
						}
						Debug.Log(_NewPosition.x);
						_TownTable[_NewPosition.x, _NewPosition.y] = 1;
						_lastPosition = _NewPosition;
						cpt++;
					}
				}
				ended = true;
			}

			//l'autre coté
			if(_NewPosition.y == 0 && ended == false)
			{
				if(_NewPosition.x < 50 && _NewPosition.x > 30)
				{
					while(_NewPosition.x != taille-1 && _NewPosition.y != taille -1 && _NewPosition.x != 0 )
					{
						
						int leRandomX = Random.Range(0,40);
						if(leRandomX < 35)
						{
							_NewPosition.y = _NewPosition.y+1;
							lastDirection = 0;
						}
						if(leRandomX >= 35 && leRandomX < 37 && lastDirection != 2)
						{
							_NewPosition.x = _NewPosition.x+1;
							lastDirection = 1;
						}
						if(leRandomX >= 37 && lastDirection != 1)
						{
							_NewPosition.x = _NewPosition.x-1;
							lastDirection = 2;
						}
						Debug.Log(_NewPosition.x);
						_TownTable[_NewPosition.x, _NewPosition.y] = 1;
						_lastPosition = _NewPosition;
						cpt++;
					}
				}
				
				if(_NewPosition.x <= 30)
				{
					while(_NewPosition.x != taille-1 && _NewPosition.y != taille -1 && _NewPosition.x != 0 )
					{
						
						int leRandomX = Random.Range(0,40);
						if(leRandomX < 25)
						{
							_NewPosition.y = _NewPosition.y+1;
							lastDirection = 0;
						}
						if(leRandomX >= 25 && leRandomX < 39 && lastDirection != 2)
						{
							_NewPosition.x = _NewPosition.x+1;
							lastDirection = 1;
						}
						if(leRandomX >= 39 && lastDirection != 1)
						{
							_NewPosition.x = _NewPosition.x-1;
							lastDirection = 2;
						}
						Debug.Log(_NewPosition.x);
						_TownTable[_NewPosition.x, _NewPosition.y] = 1;
						_lastPosition = _NewPosition;
						cpt++;
					}
				}
				if(_NewPosition.x >= 50 && _NewPosition.x < 80)
				{
					while(_NewPosition.x != taille-1 && _NewPosition.y != taille -1 && _NewPosition.x != 0 )
					{
						
						int leRandomX = Random.Range(0,40);
						if(leRandomX < 35)
						{
							_NewPosition.y = _NewPosition.y+1;
							lastDirection = 0;
						}
						if(leRandomX >= 35 && leRandomX < 37 && lastDirection != 2)
						{
							_NewPosition.x = _NewPosition.x-1;
							lastDirection = 1;
						}
						if(leRandomX >= 37 && lastDirection != 1)
						{
							_NewPosition.x = _NewPosition.x+1;
							lastDirection = 2;
						}
						Debug.Log(_NewPosition.x);
						_TownTable[_NewPosition.x, _NewPosition.y] = 1;
						_lastPosition = _NewPosition;
						cpt++;
					}
				}
				if(_NewPosition.y >= 80)
				{
					while(_NewPosition.x != taille-1 && _NewPosition.y != taille -1 && _NewPosition.x != 0 )
					{
						
						int leRandomX = Random.Range(0,40);
						if(leRandomX < 25)
						{
							_NewPosition.y = _NewPosition.y+1;
							lastDirection = 0;
						}
						if(leRandomX >= 25 && leRandomX < 39 && lastDirection != 2)
						{
							_NewPosition.x = _NewPosition.x-1;
							lastDirection = 1;
						}
						if(leRandomX >= 39 && lastDirection != 1)
						{
							_NewPosition.x = _NewPosition.x+1;
							lastDirection = 2;
						}
						Debug.Log(_NewPosition.x);
						_TownTable[_NewPosition.x, _NewPosition.y] = 1;
						_lastPosition = _NewPosition;
						cpt++;
					}
				}
				ended = true;
			}
			no_Path = true;


		}while(!no_Path);

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
			
			////debug direction du random
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
					//debug.Log ("out of bounds exception : "+ _NewPosition.x);
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
					//debug.Log ("out of bounds exception : "+ _NewPosition.y);
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
					//debug.Log ("out of bounds exception : "+ _NewPosition.x);
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
	#endregion
	
	#endregion
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
}
