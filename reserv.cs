using System;
using System.IO;
using System.Collections.Generic;

class SAE
{
    static void Main()
    {
        //test
        string[] temp = Extraire(1, 15);
        afficherHoraire(temp);

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
        public Dictionary<string, string> passager;

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
        public Vehicule[] vehicule;


    }
    static string[] Extraire(int id, int jour)
    {

        string[] temp =null;
        string nomFichier = "";
        switch (id) //associ id avec le bon nom de fichier
        {
            case 1:
                nomFichier = "Lorient_groix.csv";
                break;
            case 2:
                nomFichier = "Groix_lorient.csv";
                break;
            case 3:
                nomFichier = "Quiberon_palais.csv";
                break;
            case 4:
                nomFichier = "Palais_quiberon.csv";
                break;
            default:
                break;
        }

        // on ouvre le fichier
        using (FileStream fs = new FileStream(nomFichier, FileMode.Open, FileAccess.Read))
        using (StreamReader horaire = new StreamReader(fs))
        {
            int i = 1;
            string ligne;
	//on parcour le fichier jusqu'a que l'on arrive au bon jour(num ligne)
            while (!horaire.EndOfStream && i <= jour)
            {
                ligne = horaire.ReadLine();
                temp = ligne.Split(",");
                i++;
            }
        }
        return temp;
    }
    static void afficherHoraire(string[] tab)
    {
        foreach (var item in tab)
        {
            Console.WriteLine(item);
        }
    }
}
