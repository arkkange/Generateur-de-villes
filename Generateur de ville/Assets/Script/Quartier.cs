using UnityEngine;
using System.Collections;

public class Quartier : MonoBehaviour {

	public class quartier {
		
		public Position position;
		public Taille Taille;
		public string Type;
		public Rue[] Rues;
		
		public quartier(int _position_x,
		                int _position_y,
		                int _taille_X,
		                int _taille_Y,
		                string _type){
			
			position = new Position(_position_x, _position_y);
			Taille = new Taille(_taille_X, _taille_Y);
			Type = _type;
			
		}
		
	}
	
	public class Rue {
		
		public Position Begin;
		public Position End;
		
		public Rue(Position _begin, Position _End){
			Begin = _begin;
			End = _End;
		}
		
	}
	
	public class Position {
		public int x;
		public int y;
		
		Position(int _x,
		         int _y){
			x = _x;
			y = _y;
		}
		
	}
	
	public class Taille {
		public int x;
		public int y;
		
		Taille(int _x,
		       int _y){
			x = _x;
			y = _y;
		}
		
	}

}
