using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quartier : MonoBehaviour {
	


}

public class quartier {

	public string Type;
	public List<Rue> Rues = new List<Rue>();
	
	public quartier(int _position_x,
	                int _position_y,
	                int _taille_X,
	                int _taille_Y,
	                string _type) {

		this.Type = _type;
		this.Rues = null;
	}
	
}

public class Rue {
	
	public Position Begin;
	public Position End;
	
	public Rue(Position _begin, Position _End){
		this.Begin = _begin;
		this.End = _End;
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

public class Taille {
	public int x;
	public int y;
	
	Taille(int _x,
	       int _y){
		this.x = _x;
		this.y = _y;
	}
	
}
