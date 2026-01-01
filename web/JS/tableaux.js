async function validez() {
	let date = document.getElementById("date");
	let destination = document.getElementById("destination")
	//let selectelement = document.querySelector("main")

	//console.log(date.value)
	//console.log(destination.value)

	//selectelement.innerHTML += `test`
	data = await info(destination.value, date.value);
	//console.log(data[0].heure)
	placement(data)
}

async function info(id, date) {
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
	selectMain.innerHTML = ""

	data.forEach(element => {

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


		selectMain.innerHTML += `<section class="${code}" > <p>heure:${element.heure}</p>
<p>passager: ${element.nbReservationPassagers}/${element.capacitePassagers}</p>
<p>vehicule: ${element.nbReservationVoitures}/ ${element.capaciteVoitures}</p> 
</section>`

	});

}
