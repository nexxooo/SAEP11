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
        Traverse test = new Traverse();
        test = demandeNom(test);
        Console.WriteLine(test.nom + test.idLiaison);



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
        foreach (var item in tab)
        {
            Console.WriteLine(item);
        }
    }
    static void afficheLogo()
    {
        string msghaut = " ⡷⣸ ⢀⡀ ⡀⢀ ⡀⢀ ⢀⡀ ⡇ ⡇ ⢀⡀   ⡀⣀ ⢀⡀ ⢀⣀ ⢀⡀ ⡀⣀ ⡀⢀ ⢀⣀ ⣰⡀ ⠄ ⢀⡀ ⣀⡀";
        string msgbas = " ⠇⠹ ⠣⠜ ⠣⠼ ⠱⠃ ⠣⠭ ⠣ ⠣ ⠣⠭   ⠏  ⠣⠭ ⠭⠕ ⠣⠭ ⠏  ⠱⠃ ⠣⠼ ⠘⠤ ⠇ ⠣⠜ ⠇⠸";
        Console.WriteLine(msghaut);
        Console.WriteLine(msgbas);


    }
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
    static Traverse demandeId(Traverse traverse)
    {
        int id = 0;
        Console.Clear();
	afficheLogo();
        Console.WriteLine("Quelle liaison voulez vous prendre ? \n 1) Lorient --> Groix \n 2) Groix --> Lorient \n 3) Quiberon --> Palais \n 4) Palais --> Quiberon ");
        Console.WriteLine("Entre le numéro");
        int.TryParse(Console.ReadLine(), out id);
        traverse.idLiaison = id;
        return traverse;

    }

}
