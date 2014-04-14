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

	//pour generation des routes secondaire
	public List<Position> Nord;
	public List<Position> Est;
	public List<Position> Ouest;
	public List<Position> Sud;
	
	public quartier(int _Type) {
		this.Type = _Type;
		this.Taille = 0;
		this.TailleRoutesP = 0;
		this.MyPositions = new List<Position>();
		this.MesRoutesPrimaires = new List<Position>();
		this.MesRoutesSecondaires = new List<Position>();

		Nord = new List<Position>();
		Est = new List<Position>();
		Ouest = new List<Position>();
		Sud = new List<Position>();

		MyPositions.Clear();
		MesRoutesPrimaires.Clear();
		MesRoutesSecondaires.Clear();
	}

	public quartier(quartier _Quartier) {
		this.Type = _Quartier.Type;
		this.Taille = _Quartier.Taille;
		this.TailleRoutesP = _Quartier.TailleRoutesP;
		this.MyPositions = new List<Position>(_Quartier.MyPositions);
		this.MesRoutesPrimaires = new List<Position>(_Quartier.MesRoutesPrimaires);
		this.MesRoutesSecondaires = new List<Position>(_Quartier.MesRoutesSecondaires);
		
		this.Nord = new List<Position>(_Quartier.Nord);
		this.Est = new List<Position>(_Quartier.Est);
		this.Ouest = new List<Position>(_Quartier.Ouest);
		this.Sud = new List<Position>(_Quartier.Sud);
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
		this.MesRoutesPrimaires.Add(_pos);
		this.TailleRoutesP++;

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

	public void ClearTableGeneration(){
		this.Sud.Clear();
		this.Nord.Clear();
		this.Est.Clear();
		this.Ouest.Clear();
	}

	public Position FindSisterRoad(Position _ActualPosition){

		ClearTableGeneration();

		//on recupère toutes les positions du quartier sur les 4 cardinaux
		foreach( Position _p in this.MesRoutesPrimaires){

			if(this.TailleRoutesP > 1){
				Debug.Log (TailleRoutesP);
				if( _p.x == _ActualPosition.x ){
					if(_p.y > _ActualPosition.y){
						Sud.Add(_p);
					}
					if(_p.y < _ActualPosition.y){
						Nord.Add(_p);
					}
				}

				if(_p.y == _ActualPosition.y){
					if(_p.x > _ActualPosition.x){
						Est.Add(_p);
					}
					if(_p.x < _ActualPosition.x){
						Ouest.Add(_p);
					}
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
			int _random = UnityEngine.Random.Range(1,5);

			//nord
			if(_random == 1){
				if(Nord.Count > 0){
					_result.SetPosition(Nord[0]);
					foreach (Position i in Nord){
						if( i.x > _result.x){
							_result.SetPosition(i);
						}
					}

					N = true;
				}
				_N_Tested = true;
			}

			//sud
			if(_random == 2){
				if(Sud.Count > 0){
					_result.SetPosition(Sud[0]);
					foreach (Position i in Nord){
						if(i.x < _result.x){
							_result.SetPosition(i);
							//_direction = "S";
						}
					}
					S = true;
				}
				_S_Tested = true;
			}

			//Ouest
			if(_random == 3){
				if(Ouest.Count > 0){
					_result.SetPosition(Ouest[0]);
					foreach (Position i in Nord){
						if(i.y < _result.y){
							_result.SetPosition(i);
							//_direction = "O";
						}
					}
					O = true;
				}
				_O_Tested = true;
			}

			//Est
			if(_random == 4){
				if(Est.Count > 0){
					_result.SetPosition(Est[0]);
					foreach (Position i in Nord){
						if(i.y > _result.y){
							_result.SetPosition(i);
							//_direction = "E";
						}
					}
					E = true;
				}
				_E_Tested = true;
			}

		}while( (!N && !E && !S && !O) && (!_N_Tested && !_S_Tested && !_O_Tested && !_E_Tested) );

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
