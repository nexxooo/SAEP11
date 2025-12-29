using System;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;

class SAE
{
    static void Main()
    {
        //test
        //string[] temp = Extraire(1, 15);
        //afficherHoraire(temp);
        //Console.WriteLine(int.Parse("01"));
        //DateTime date = DateTime.Now;
        //Console.WriteLine(date.ToString("yy-mm-dd hh:mm:ss"));
        //Traverse test = new Traverse();
        //test = demandeNom(test);
        //Passager[] test = DemandePassager();
        //Console.WriteLine(test[0].nom + test[0].prenom + test[0].codeCategorie);
        //Console.WriteLine(test.nom + "\n" + test.idLiaison + "\n" + test.date + test.heure);
        //Console.WriteLine(test.heure + "\n" + test.horodatage);
        //Vehicule[] test = DemandeVehicule();
        //Console.WriteLine(test[0].codeCategorie+" "+test[0].quantite);
	Reservation resev = new Reservation();
	resev = demandeReserv();
	//Console.WriteLine(resev.passager[0].nom);	
	faireJson(resev);



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
        public string nom;
        public string prenom;
        public string codeCategorie;

    }
    struct Vehicule
    {
        public string codeCategorie;
        public int quantite;

    }
    struct Reservation
    {
        public Traverse reservation;
        public Passager[] passagers;
        public Vehicule[] vehicules;


    }
    static string[] Extraire(int id, int jour)
    {

        string[] temp = null;
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
        for (int i = 0; i < tab.GetLength(0); i++)
        {
            Console.WriteLine(i + 1 + ") " + tab[i]);
        }
    }
    static void afficheLogo()
    {
        string msghaut = " ⡷⣸ ⢀⡀ ⡀⢀ ⡀⢀ ⢀⡀ ⡇ ⡇ ⢀⡀   ⡀⣀ ⢀⡀ ⢀⣀ ⢀⡀ ⡀⣀ ⡀⢀ ⢀⣀ ⣰⡀ ⠄ ⢀⡀ ⣀⡀";
        string msgbas = " ⠇⠹ ⠣⠜ ⠣⠼ ⠱⠃ ⠣⠭ ⠣ ⠣ ⠣⠭   ⠏  ⠣⠭ ⠭⠕ ⠣⠭ ⠏  ⠱⠃ ⠣⠼ ⠘⠤ ⠇ ⠣⠜ ⠇⠸";
        Console.WriteLine(msghaut);
        Console.WriteLine(msgbas);


    }
    // on va remplir une structure Traverse pour commencer la reservation
    static Traverse demandeNom()
    {
        afficheLogo();
        string nom = "";
        Traverse res = new Traverse();
        Console.WriteLine("entrer votre nom :");
        nom = Console.ReadLine();
        res.nom = nom;
        res = demandeId(res);
        return res;
    }
    // on va diviser le trvail en faisant une fonction pour chaque argument de la structure
    // chaque fonction appele la suivante a la fin pour tous remplir 
    static Traverse demandeId(Traverse traverse)
    {
        int id = 0;
        Console.Clear();
        afficheLogo();
        Console.WriteLine("Quelle liaison voulez vous prendre ? \n 1) Lorient --> Groix \n 2) Groix --> Lorient \n 3) Quiberon --> Palais \n 4) Palais --> Quiberon ");
        Console.WriteLine("Entre le numéro");
        int.TryParse(Console.ReadLine(), out id);
        traverse.idLiaison = id;
        traverse = demandeJour(traverse);
        return traverse;
    }
    static Traverse demandeJour(Traverse traverse)
    {
        string jour = "";

        string date = "2025-11-";
        Console.Clear();
        afficheLogo();
        Console.WriteLine("Quelle jour de novembre 2025 vous vous partir ?");
        jour = Console.ReadLine();
        if (jour.Length < 2) // on rajoute un zero si l'utilisateur donne qu'un chiffre pour respecter le format demandé
        {
            jour = "0" + jour;
            date = date + jour;
        }
        else
        {
            date = date + jour;
        }
        traverse.date = date;
        traverse = demandeHoraire(traverse);

        return traverse;

    }
    static Traverse demandeHoraire(Traverse traverse)
    {
        Console.Clear();
        afficheLogo();

        int heure;
        Console.WriteLine("Quelle heure ?");
        // on prend les 2 dernier caractère pour avoir le jour
        int jour = int.Parse(traverse.date.Substring(8, 2));
        //on appelle Extraire pour récuperer un tableau des horaire du jour
        string[] horaire = Extraire(traverse.idLiaison, jour);
        //on affiche les horaires
        afficherHoraire(horaire);
        heure = int.Parse(Console.ReadLine());
        //l'heure entrer par l'utilisateur est décalé de 1
        traverse.heure = horaire[heure - 1];
        Console.WriteLine(traverse.heure);

        traverse = Horodatage(traverse);
        return traverse;
    }
    static Traverse Horodatage(Traverse traverse)
    {
        DateTime date = DateTime.Now;
        string res = date.ToString("yy-mm-dd hh:mm:ss");
        traverse.horodatage = res;

        return traverse;

    }

/////// on remplie les info pour chaque passager 
///
///

    static Passager[] DemandePassager()
    {
        Console.Clear();
        afficheLogo();
        Console.WriteLine("combien de passager ?"); // on demande le nombre de passager
        int nombre;
        nombre = int.Parse(Console.ReadLine());


        Passager[] tablePassager; // on crée le tableau 
        tablePassager = new Passager[nombre];
        for (int i = 0; i < nombre; i++)
        {
            Passager nouveauxPassager = new Passager();
            nouveauxPassager = NomPassager(nouveauxPassager);
            tablePassager[i] = nouveauxPassager;

        }
        return tablePassager;

    }
    static Passager NomPassager(Passager passager)
    {
        Console.Clear();
        afficheLogo();
        Console.WriteLine("entrer nom passager");

        string nom;
        nom = Console.ReadLine();
        passager.nom = nom;

        passager = PrenomPassager(passager);
        return passager;

    }
    static Passager PrenomPassager(Passager passager)
    {
        Console.Clear();
        afficheLogo();
        Console.WriteLine("entrer prénom passager");

        string prenom;
        prenom = Console.ReadLine();
        passager.prenom = prenom;

        passager = CategoriePassager(passager);
        return passager;
    }
    static Passager CategoriePassager(Passager passager)
    {
        Dictionary<int, string[]> categorie = new Dictionary<int, string[]>
{
    { 1, new string[] { "Adulte 26 ans et plus ", "adu26p " } },
    { 2, new string[] { "Jeune 18 à 25 ans inclus", "jeu1825" } },
    { 3, new string[] { "Enfant 4 à 17 ans inclus", "enf417" } },
    { 4, new string[] { "Bébé moins de 4 ans", "bebe" } },
    { 5, new string[] { "Animal de compagnie", "ancomp" } }
};
        Console.Clear();
        afficheLogo();
        Console.WriteLine("quelle catégorie");

        foreach (int item in categorie.Keys)
        {
            Console.WriteLine(item + " " + categorie[item][0]);

        }
        int r = int.Parse(Console.ReadLine());
        passager.codeCategorie = categorie[r][1];

        return passager;

    }
//// on remplie les info pour les véhicules
///
///
///

    static Vehicule[] DemandeVehicule()
    {
        Console.Clear();
        afficheLogo();
        Console.WriteLine("combien de Vehicule ?"); // on demande le nombre de passager
        int nombre;
        nombre = int.Parse(Console.ReadLine());


        Vehicule[] tableVehicule; // on crée le tableau 
        tableVehicule = new Vehicule[nombre];
        for (int i = 0; i < nombre; i++)
        {
            Vehicule nouveauxVehicule = new Vehicule();
            nouveauxVehicule = CategorieVehicule(nouveauxVehicule);
            tableVehicule[i] = nouveauxVehicule;

        }
        return tableVehicule;
    }
    static Vehicule CategorieVehicule(Vehicule vehicule)
    {
        Dictionary<int, string[]> categorieVehicules = new Dictionary<int, string[]>
{
    { 1, new string[] { "Trottinette électrique", "trot" } },
    { 2, new string[] { "Vélo ou remorque à vélo", "velo" } },
    { 3, new string[] { "Vélo électrique", "velelec" } },
    { 4, new string[] { "Vélo cargo ou tandem", "cartand" } },
    { 5, new string[] { "Deux-roues <= 125 cm3", "mobil" } },
    { 6, new string[] { "Deux-roues > 125 cm3", "moto" } },
    { 7, new string[] { "Voiture moins de 4 m", "cat1" } },
    { 8, new string[] { "Voiture de 4 m à 4.39 m", "cat2" } },
    { 9, new string[] { "Voiture de 4.40 m à 4.79 m", "cat3" } },
    { 10, new string[] { "Voiture 4.80 m et plus", "cat4" } },
    { 11, new string[] { "Camping-car - véhicule plus de 2.10 de haut", "camp" } }
};
        Console.Clear();
        afficheLogo();
        Console.WriteLine("quelle catégorie");
        foreach (var item in categorieVehicules.Keys)
        {
            Console.WriteLine(item + " " + categorieVehicules[item][0]);
        }

        int r = int.Parse(Console.ReadLine());
        vehicule.codeCategorie = categorieVehicules[r][1];

	vehicule = qtVehicule(vehicule);
        return vehicule;

    }
static Vehicule qtVehicule(Vehicule vehicule)
{
	Console.Clear();
	afficheLogo();
	Console.WriteLine("quelle quantitée ?");

	int qt = int.Parse(Console.ReadLine());
	vehicule.quantite = qt;

	return vehicule;	
}


/// on remplie toute les structure reservation
///
///
static Reservation demandeReserv()
{
	Reservation resrv = new Reservation();

	resrv.reservation = demandeNom();
	resrv.passagers = DemandePassager();
	resrv.vehicules = DemandeVehicule();

	return resrv;
}

static void faireJson(Reservation reserv)
{
	JavaScriptSerializer serializer = new JavaScriptSerializer();
	string json = serializer.Serialize(reserv);

	json = "["+ json+"]"; 

	File.WriteAllText("donnees.json", json);

}

}
