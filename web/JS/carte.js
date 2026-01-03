async function valider() {
	const reserv = document.getElementById("reserv");

	data = await infoReserv(reserv.value);

	modif(reserv, data)

}
function modif(reserv, data) {
	changeGauche(data)
	nb = data.nbPassagers + data.nbVehicules - 1

	ajoutCarte(nb);
	modiDroitPassager(reserv.value, data.nbPassagers)
	modiDroitVehicule(reserv.value, data.nbVehicules, nb)


	element = document.querySelector(".card").classList.toggle("cacher")
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

function changeGauche(data) {
	const element = document.querySelectorAll(".left-section .value");

	const element2 = document.querySelectorAll(".reservation-info .value")

	element[0].innerHTML = data.portDepart
	element[1].innerHTML = data.portArrivee
	element[2].innerHTML = data.date
	element[3].innerHTML = data.heure
	element[4].innerHTML = data.bateau

	element2[1].innerHTML = data.nom

}

function ajoutCarte(nb) {
	const main = document.querySelector("main");
	const carte = document.querySelector(".card");


	for (let index = 0; index < nb; index++) {

		const nouvelleCarte = carte.cloneNode(true);
		main.appendChild(nouvelleCarte)
	}

}

async function infoPassager(reserv, id) {
	const api = `https://can.iutrs.unistra.fr/api/reservation/${reserv}/passager/${id}`;

	try {
		reponse = await fetch(api);

		data = await reponse.json();
		return data;
	} catch (error) {
		console.log("erreur api")
	}
}

async function modiDroitPassager(reserv, nb) {
	const listeCarte = document.querySelectorAll(".card")

	for (let index = 1; index <= nb; index++) {
		data = await infoPassager(reserv, index);
		const carteEncour = listeCarte[index - 1].querySelectorAll(".passenger-info .value")
		carteEncour[0].innerHTML = data.nom
		carteEncour[1].innerHTML = data.prenom
		carteEncour[2].innerHTML = data.libelleCategorie
		carteEncour[3].innerHTML = data.price + " €"
	}
}
async function infoVehicule(reserv, id) {
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
		const carteEncour = listeCarte[((max + 1) - index)].querySelectorAll(".passenger-info")

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
	for (let index = 1; index <= nb; index++) {
		data = await infoVehicule(reserv, index);
		const carteEncour = listeCarte[((max + 1) - index)].querySelectorAll(".passenger-header #titre-droit")

		carteEncour[0].innerHTML = "Vehicule"
	}
}

index = 0
function suivant() {
	const listCarte = document.querySelectorAll(".card");
	index += 1
	if (index < 0) { index = 0 }
	if (index < listCarte.length) {
		listCarte.forEach(element => {
			if (!element.classList.contains("cacher")) {
				element.classList.toggle("cacher")
			}
		});

		listCarte[index].classList.toggle("cacher")

	}
}
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
