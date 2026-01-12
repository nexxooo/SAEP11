async function main() {

	for (i = 1; i < 5; i++) {  // on place les stat des 4 traversé 
		liaison = await infoL(i)
		placement(liaison)
	}
	caglobale()

}

async function infoL(id) { // on recuprer les info des la traversé (id)
	const api = `https://can.iutrs.unistra.fr/api/liaison/${id}/chiffreAffaire`

	try {
		const reponse = await fetch(api)

		const data = await reponse.json()

		return data

	} catch (error) {
		return error
	}
}

function placement(data) { //on place un carte pour la traverser avce toute les info de celle ci 
	selectmain = document.querySelector("main");


	console.log(data.nom)
	// on arrondie les chiffre pour les differents ca 
	selectmain.innerHTML += `<section> <p>nom: ${data.nom}</p> 
<p>Passager:</p> <hr> <p>nombre passagers: ${data.passagers.nombre}</p> <p>CA passagers: ${Math.round(data.passagers.chiffreAffaire)}</p>

<p>Véhicule:</p> <hr> <p>nombre véhicule: ${data.vehicules.quantite}</p> <p>CA vehicule: ${Math.round(data.vehicules.chiffreAffaire)}</p>
<p>CA:</p> <hr><p class="ca">${Math.round(data.vehicules.chiffreAffaire + data.passagers.chiffreAffaire)}</p>  
</section>`
}


function caglobale(){
	selectca = document.querySelectorAll(".ca") //on récupere un liste avec le ca de chaque traversé 

	sum = 0 
	// on les additionne 
	selectca.forEach(element => {
		sum += parseInt(element.textContent)
	});

	//on le rajoute a la fin 

	selectmain = document.querySelector("main")

	selectmain.innerHTML += `<p id="cag"> CA GLOBALE: ${sum}</p>`
}
