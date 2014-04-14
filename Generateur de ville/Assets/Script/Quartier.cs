using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quartier : MonoBehaviour {
	


}

public class Ville {
	public List<quartier> MesQuartier;
	public int Taille;

	public Ville(){
		this.MesQuartier = new List<quartier>();
		this.Taille = 0;
	}

	public void AjouterQuartier(quartier new_quartier){
		this.MesQuartier.Add(new_quartier);
		this.Taille ++;
	}

}

public class quartier {

	public int Type;
	public int Taille;
	public int TailleRoutesP;
	public List<Position> MyPositions;			//les positions qui composent le quartier	
	public List<Position> MesRoutesPrimaires;	//positions des routes primaires
	public List<Position> MesRoutesSecondaires;	//positions des routes secondaires
	
	public quartier(int _Type) {
		this.Type = _Type;
		this.Taille = 0;
		this.TailleRoutesP = 0;
		this.MyPositions = new List<Position>();
		this.MesRoutesPrimaires = new List<Position>();
		this.MesRoutesSecondaires = new List<Position>();
	}

	public Position GetPositionRouteP(int indice){
		return this.MesRoutesPrimaires[indice];
	}

	//ajout et suppression d'une position
	public void AddPosition(Position _pos){

		MyPositions.Add(_pos);
		this.Taille ++;
	}

	public void RemovePosition(Position _pos){
		if(MyPositions.Contains(_pos)){
			MyPositions.Remove(_pos);
		}
		else{
			Debug.LogError("mon quartier ne possède pas cette position");
			MyPositions.Add(_pos);
			this.Taille --;
		}
	}

	//ajout et suppression d'une route primaire
	public void AddRoutePrimaire(Position _pos){

		/*if(this.MesRoutesPrimaires.Contains(_pos)){
			Debug.Log("mon quartier contient déja cette route");
		}
		else{*/
			this.MesRoutesPrimaires.Add(_pos);
			this.TailleRoutesP++;
		
		//}


	}

	public void RemoveRoutePrimaire(Position _pos){
		if(this.MesRoutesPrimaires.Contains(_pos)){
			MesRoutesPrimaires.Remove(_pos);
		}
		else{
			Debug.Log("mon quartier ne contient pas cette route");
		}
	}

	//ajout et suppression d'une route secondaire
	public void AddRouteSecondaire(Position _pos){
		//if(!MesRoutesSecondaires.Contains(_pos)){

			MesRoutesSecondaires.Add(_pos);
		//}
	}
	
	public void RemoveRouteSecondaire(Position _pos){
		if(MesRoutesSecondaires.Contains(_pos)){
			MesRoutesSecondaires.Remove(_pos);
		}
		else{
			Debug.Log("mon quartier ne contient pas cette route");
		}
	}

	public Position FindSisterRoad(Position _ActualPosition, string _direction){
		//verifier avant tout que la position fait partie de la route


		List<Position> _Nord = new List<Position>();
		List<Position> _Est = new List<Position>();
		List<Position> _Ouest = new List<Position>();
		List<Position> _Sud = new List<Position>();

		//on recupère toutes les positions du quartier sur les 4 cardinaux
		foreach( Position p in this.MesRoutesPrimaires){
			if( p.x == _ActualPosition.x ){
				if(p.y > _ActualPosition.y){
					_Sud.Add(p);
				}
				if(p.y < _ActualPosition.y){
					_Nord.Add(p);
				}
			}
			if(p.y == _ActualPosition.y){
				if(p.x > _ActualPosition.x){
					_Est.Add(p);
				}
				if(p.x < _ActualPosition.x){
					_Ouest.Add(p);
				}
			}
		}

		bool N = false;
		bool E = false;
		bool S = false;
		bool O = false;
		bool _N_Tested = false;
		bool _S_Tested = false;
		bool _O_Tested = false;
		bool _E_Tested = false;
		Position _result = new Position();
		do{
			int _random = UnityEngine.Random.Range(0,5);

			//nord
			if(_random == 1){
				if(_Nord.Count > 0){
					_result.SetPosition(_Nord[0]);
					foreach (Position i in _Nord){
						if(i.x > _result.x){
							_result.SetPosition(i);
							_direction = "N";
						}
					}
					N = true;
				}
				_N_Tested = true;
			}

			//sud
			if(_random == 2){
				if(_Nord.Count > 0){
					_result = _Sud[0];
					foreach (Position i in _Nord){
						if(i.x < _result.x){
							_result.SetPosition(i);
							_direction = "S";
						}
					}
					S = true;
				}
				_S_Tested = true;
			}

			//Ouest
			if(_random == 3){
				if(_Ouest.Count > 0){
					_result = _Ouest[0];
					foreach (Position i in _Nord){
						if(i.y < _result.y){
							_result.SetPosition(i);
							_direction = "O";
						}
					}
					O = true;
				}
				_O_Tested = true;
			}

			//Est
			if(_random == 4){
				if(_Est.Count > 0){
					_result = _Est[0];
					foreach (Position i in _Nord){
						if(i.y > _result.y){
							_result.SetPosition(i);
							_direction = "E";
						}
					}
					E = true;
				}
				_E_Tested = true;
			}


		}while( N || E || S || O || (_N_Tested && _S_Tested && _O_Tested && _E_Tested));

		return _result;

	}
	
}

public class Position {
	public int x;
	public int y;
	
	public Position(int _x, int _y){
		this.x = _x;
		this.y = _y;
	}

	public Position(){
		this.x = 0;
		this.y = 0;
	}

	public Position(Position P){
		this.x = P.x;
		this.y = P.y;
	}
	
	public bool Equals(Position p){
		if( (p.x == this.x) && (p.y == this.y) ){
			return true;
		}
		else{
			return false;
		}
	}

	public void SetPosition(Position p){
		this.x = p.x;
		this.y = p.y;
	}

	public void SetPosition(int i, int j){
		this.x = i;
		this.y = j;
	}
	
}
