using System;
using System.IO;
using System.Collections.Generic;

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
        Passager[] test = DemandePassager();
        Console.WriteLine(test[0].nom + test[0].prenom);
        //Console.WriteLine(test.nom + "\n" + test.idLiaison + "\n" + test.date + test.heure);
        //Console.WriteLine(test.heure + "\n" + test.horodatage);



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
        public Traverse Traversé;
        public Passager[] passager;
        public Vehicule[] vehicule;


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
    static Traverse demandeNom(Traverse traverse)
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
    // on va diser le trvail en faisant une fonction pour chaque argument de la structure
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

        passager = CategoriePassager();
        return passager;
    }
    static Passager CategoriePassger(Passager passager)
    {
        Dictionary<string> categorie =
    {
        {"Adulte 26 ans et plus ","adu26p "},
        {"Jeune 18 à 25 ans inclus","jeu1825"},
        {"Enfant 4 à 17 ans inclus", "enf417"},
	{"Bébé moins de 4 ans","bebe"},
	{"Animal de compagnie","ancomp"}
    };
	Console.Clear();
	afficheLogo();
	Console.WriteLine("quelle catégorie");

	int i =0;
	foreach (string item in categorie.Keys)
	{
		Console.WriteLine();
	    
	}

    }



}
