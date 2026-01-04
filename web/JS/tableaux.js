async function validez() {
	let date = document.getElementById("date"); //on recupere la date
	let destination = document.getElementById("destination") //on recuper la destination
	//let selectelement = document.querySelector("main")

	//console.log(date.value)
	//console.log(destination.value)

	//selectelement.innerHTML += `test`
	data = await info(destination.value, date.value); //on recupere les info des traverse (heure etc)
	//console.log(data[0].heure)
	placement(data) //fonction qui placera toutes les carte avec les info des différente traversé 
}

async function info(id, date) { //on recupere les info
	const API = `https://can.iutrs.unistra.fr/api/liaison/${id}/remplissage/${date}`
	try {
		const reponse = await fetch(API);
		const data = await reponse.json();

		return data

	} catch (error) {
		console.log("erreur api")
	}
}

function placement(data) {
	let selectMain = document.querySelector("main");
	selectMain.innerHTML = "" //on enleve toute les carte 

	data.forEach(element => { // on regarde le taux de remplissage pour mettre la bonne couleur qui sera une classe dans le css

		capa = element.nbReservationVoitures;
		capamax = element.capaciteVoitures;
		let code;
		if (capa > capamax) {
			code = "jaune"
		} else if (capa <= capamax / 2) {
			code = "vert"
		} else if (capa <= capamax * 0.75) {
			code = "orange"
		} else {
			code = "rouge"
		}

// on place les carte avec la couleur (classe) et toute les info
		selectMain.innerHTML += `<section class="${code}" > <p>heure:${element.heure}</p>
<p>passager: ${element.nbReservationPassagers}/${element.capacitePassagers}</p>
<p>vehicule: ${element.nbReservationVoitures}/ ${element.capaciteVoitures}</p> 
</section>`

	});

}
