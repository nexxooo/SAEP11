async function main() {

	for (i = 1; i < 5; i++) {
		liaison = await infoL(i)
		placement(liaison)
	}
	caglobale()

}

async function infoL(id) {
	const api = `https://can.iutrs.unistra.fr/api/liaison/${id}/chiffreAffaire`

	try {
		const reponse = await fetch(api)

		const data = await reponse.json()

		return data

	} catch (error) {
		return error
	}
}

function placement(data) {
	selectmain = document.querySelector("main");


	console.log(data.nom)
	selectmain.innerHTML += `<section> <p>nom: ${data.nom}</p> 
<p>Passager:</p> <hr> <p>nombre passagers: ${data.passagers.nombre}</p> <p>CA passagers: ${Math.round(data.passagers.chiffreAffaire)}</p>

<p>Véhicule:</p> <hr> <p>nombre véhicule: ${data.vehicules.quantite}</p> <p>CA vehicule: ${Math.round(data.vehicules.chiffreAffaire)}</p>
<p>CA:</p> <hr><p class="ca">${Math.round(data.vehicules.chiffreAffaire + data.passagers.chiffreAffaire)}</p>  
</section>`
}


function caglobale(){
	selectca = document.querySelectorAll(".ca")

	sum = 0 
	selectca.forEach(element => {
		sum += parseInt(element.textContent)
	});

	selectmain = document.querySelector("main")

	selectmain.innerHTML += `<p id="cag"> CA GLOBALE: ${sum}</p>`
}
