async function validez() {
    let date = document.getElementById("date"); // on récupère la date
    let destination = document.getElementById("destination"); // on récupère la destination

    // Appel de l'API pour récupérer les données
    let data = await info(destination.value, date.value); 
    
    // Si on a des données, on lance l'affichage
    if (data) {
        placement(data);
    }
}

async function info(id, date) { 
    // On récupère les infos via l'API
    const API = `https://can.iutrs.unistra.fr/api/liaison/${id}/remplissage/${date}`;
    
    try {
        const reponse = await fetch(API);
        
        if (!reponse.ok) {
            throw new Error(`Erreur HTTP: ${reponse.status}`);
        }

        const data = await reponse.json();
        return data;

    } catch (error) {
        console.error("Erreur API :", error);
        alert("Impossible de récupérer les horaires. Vérifiez votre connexion.");
        return null;
    }
}

function placement(data) {
    let selectMain = document.querySelector("main");
    selectMain.innerHTML = ""; // On vide le contenu précédent (reset)

    if (data.length === 0) {
        selectMain.innerHTML = "<p style='color:white; width:100%; text-align:center;'>Aucune traversée trouvée pour cette date.</p>";
        return;
    }

    data.forEach(element => { 
        // Calcul du taux de remplissage pour la couleur
        let capa = element.nbReservationVoitures;
        let capamax = element.capaciteVoitures;
        let code = "rouge"; // Par défaut

        if (capa > capamax) {
            code = "jaune"; // Surchargé (cas d'erreur ?)
        } else if (capa <= capamax / 2) {
            code = "vert"; // Peu rempli
        } else if (capa <= capamax * 0.75) {
            code = "orange"; // Moyennement rempli
        } else {
            code = "rouge"; // Très rempli
        }

        // Injection du HTML propre (avec balises section, h3, p bien fermées)
        selectMain.innerHTML += `
        <section class="${code}">
            <h3>Heure : ${element.heure}</h3>
            <p>Passagers : ${element.nbReservationPassagers} / ${element.capacitePassagers}</p>
            <p>Véhicules : ${element.nbReservationVoitures} / ${element.capaciteVoitures}</p>
        </section>
        `;
    });
}
