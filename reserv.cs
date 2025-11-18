using System;
using System.IO;
using System.Collections.Generic;

class SAE
{
	static void Main()
	{

	}
	struct Traverse
	{
		public string nom;
		public int idLiaison;
		public string date;
		public string heure;
		public string horodatage;

	}
	struct Passager
	{
		public Dictionary<string,string> passager;

	}
	struct Vehicule
	{
	    public string codeCategorie;
	    public int quantite;

	}
	struct Reservation
	{
		public Traverse Traversé;
		public Passager[] passager;
		public véhicules[] vehicule;


	}

    
}
