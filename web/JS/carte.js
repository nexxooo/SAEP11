async function valider() {
	const reserv = document.getElementById("reserv");

	data = await infoReserv(reserv.value);

	modif(reserv, data)

}
function modif(reserv, data) { //fonction pricipale 
	changeGauche(data)
	nb = data.nbPassagers + data.nbVehicules - 1 //une carte est deja présente sur la page mais elle est cacher 

	ajoutCarte(nb);
	modiDroitPassager(reserv.value, data.nbPassagers)
	modiDroitVehicule(reserv.value, data.nbVehicules, nb)


	element = document.querySelector(".card").classList.toggle("cacher") //on chacher toute les carte sauf la première
}

async function infoReserv(reserv) {
	const api = `https://can.iutrs.unistra.fr/api/reservation/${reserv}`;

	try {
		reponse = await fetch(api);

		data = await reponse.json();
		return data;
	} catch (error) {
		console.log("erreur api")
	}
}

function changeGauche(data) { //modifie la partie gauche des carte (partie commune a toute)
	const element = document.querySelectorAll(".left-section .value");

	const element2 = document.querySelectorAll(".reservation-info .value")
	//diffrente partie a modifier (toujours les meme)
	element[0].innerHTML = data.portDepart
	element[1].innerHTML = data.portArrivee
	element[2].innerHTML = data.date
	element[3].innerHTML = data.heure
	element[4].innerHTML = data.bateau

	element2[1].innerHTML = data.nom

}

function ajoutCarte(nb) { //ajoute le bon nombre de carte nbPassager +nbVehicule
	const main = document.querySelector("main");
	const carte = document.querySelector(".card");


	for (let index = 0; index < nb; index++) {

		const nouvelleCarte = carte.cloneNode(true);
		main.appendChild(nouvelleCarte)
	}

}

async function infoPassager(reserv, id) { //recupere les info d'un passager
	const api = `https://can.iutrs.unistra.fr/api/reservation/${reserv}/passager/${id}`;

	try {
		reponse = await fetch(api);

		data = await reponse.json();
		return data;
	} catch (error) {
		console.log("erreur api")
	}
}

async function modiDroitPassager(reserv, nb) { // modifie la partie droite si passager (nb = nombre passager)
	const listeCarte = document.querySelectorAll(".card")

	for (let index = 1; index <= nb; index++) { //on commence toujours a remplir les première carte par les passager
		data = await infoPassager(reserv, index); // recupere les info du passager
		const carteEncour = listeCarte[index - 1].querySelectorAll(".passenger-info .value") // -1 a cause du décallager entre id du passager et sa place dans la liste
		carteEncour[0].innerHTML = data.nom
		carteEncour[1].innerHTML = data.prenom
		carteEncour[2].innerHTML = data.libelleCategorie
		carteEncour[3].innerHTML = data.price + " €"
	}
}
async function infoVehicule(reserv, id) { //recuprer les info pour 1 véhicule
	const api = `https://can.iutrs.unistra.fr/api/reservation/${reserv}/vehicule/${id}`;

	try {
		reponse = await fetch(api);

		data = await reponse.json();
		return data;
	} catch (error) {
		console.log("erreur api")
	}
}
async function modiDroitVehicule(reserv, nb, max) {
	const listeCarte = document.querySelectorAll(".card")

	for (let index = 1; index <= nb; index++) {
		data = await infoVehicule(reserv, index);
		//on change les carte après celle des passager 
		const carteEncour = listeCarte[((max + 1) - index)].querySelectorAll(".passenger-info")

		//les carte véhicule on moins de colonne donc on remplace toute cette partie
		carteEncour[0].innerHTML = `<tr>
                                <td class="label">Catégorie</td>
			<td class="value">${data.libelle}</td>
                            </tr>
                            <tr>
                                <td class="label">Nombre</td>
			<td class="value">${data.quantite}</td>
                            </tr>
                        	<tr>
                                <td class="label">Prix</td>
			<td class="value">${data.prix}</td>
                            </tr>`


	}
	// on modifie aussi le titre
	for (let index = 1; index <= nb; index++) {
		data = await infoVehicule(reserv, index);
		const carteEncour = listeCarte[((max + 1) - index)].querySelectorAll(".passenger-header #titre-droit")

		carteEncour[0].innerHTML = "Vehicule"
	}
}

index = 0 // carte actuelle
function suivant() {
	const listCarte = document.querySelectorAll(".card");
	index += 1
	if (index < 0) { index = 0 } //on verif que index ne depasse pas les limte
	if (index < listCarte.length) {
		listCarte.forEach(element => {
			if (!element.classList.contains("cacher")) { //on chache toute les carte
				element.classList.toggle("cacher")
			}
		});

		listCarte[index].classList.toggle("cacher") // on revele la bonne carte

	}
}
//meme fonction
function precedent() {
	const listCarte = document.querySelectorAll(".card");
	index -= 1
	if (index < 0) { index = 0 }
	if (index < listCarte.length && index >= 0) {
		listCarte.forEach(element => {
			if (!element.classList.contains("cacher")) {
				element.classList.toggle("cacher")
			}
		});

		listCarte[index].classList.toggle("cacher")

	}
}
