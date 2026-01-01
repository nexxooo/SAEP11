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

	data.forEach(element => {
		selectMain.innerHTML += `<section> <p>heure:${element.heure}</p>
<p>passager: ${element.nbReservationPassagers}/${element.capacitePassagers}</p>
<p>vehicule: ${element.nbReservationVoitures}/ ${element.capaciteVoitures}</p> 
</section>`

	});

}
