document.addEventListener("DOMContentLoaded", () => {

    // ==============================
    // CONFIG
    // ==============================
    const API_BASE = "https://can.iutrs.unistra.fr/api";

    const userInput = prompt("Veuillez saisir l'ID de la réservation :", "1");
    const reservationId = userInput || 1;
    // ==============================
    // HELPERS
    // ==============================
    const formatPrix = (value) => value.toFixed(2).replace(".", ",") + " €";

    const fetchJSON = async (url) => {
        const res = await fetch(url);
        if (!res.ok) throw new Error("Erreur API : " + url);
        return res.json();
    };

    // ==============================
    // CHARGEMENT RÉSERVATION
    // ==============================
    async function loadReservation() {
        const reservation = await fetchJSON(`${API_BASE}/reservation/${reservationId}`);

        document.querySelector(".info-section .value").textContent = reservation.id;
        document.querySelectorAll(".info-section .value")[1].textContent = reservation.nom;

        document.querySelectorAll(".info-section")[1].querySelectorAll(".value")[0].textContent =
            `${reservation.portDepart} - ${reservation.portArrivee}`;
        document.querySelectorAll(".info-section")[1].querySelectorAll(".value")[1].textContent =
            reservation.date.split("-").reverse().join("/");
        document.querySelectorAll(".info-section")[1].querySelectorAll(".value")[2].textContent =
            reservation.heure;
        document.querySelectorAll(".info-section")[1].querySelectorAll(".value")[3].textContent =
            reservation.bateau;

        return reservation;
    }

    // ==============================
    // PASSAGERS
    // ==============================
    async function loadPassagers(nbPassagers) {
        const tbody = document.querySelectorAll(".data-table tbody")[0];
        tbody.innerHTML = "";

        const categories = {};
        let total = 0;

        for (let i = 1; i <= nbPassagers; i++) {
            const p = await fetchJSON(`${API_BASE}/reservation/${reservationId}/passager/${i}`);

            if (!categories[p.libelleCategorie]) {
                categories[p.libelleCategorie] = {
                    count: 0,
                    price: p.price
                };
            }
            categories[p.libelleCategorie].count++;
        }

        Object.entries(categories).forEach(([libelle, data]) => {
            const lineTotal = data.count * data.price;
            total += lineTotal;

            tbody.innerHTML += `
                <tr>
                    <td>${libelle}</td>
                    <td>${data.count}</td>
                    <td>${data.price === 0 ? "gratuit" : formatPrix(data.price)}</td>
                    <td>${formatPrix(lineTotal)}</td>
                </tr>
            `;
        });

        document.querySelectorAll(".sous-total span")[0].textContent = formatPrix(total);
        return total;
    }

    // ==============================
    // VÉHICULES
    // ==============================
    async function loadVehicules(nbVehicules) {
        const tbody = document.querySelectorAll(".data-table tbody")[1];
        tbody.innerHTML = "";

        let total = 0;

        for (let i = 1; i <= nbVehicules; i++) {
            const v = await fetchJSON(`${API_BASE}/reservation/${reservationId}/vehicule/${i}`);
            const lineTotal = v.quantite * v.prix;
            total += lineTotal;

            tbody.innerHTML += `
                <tr>
                    <td>${v.libelle}</td>
                    <td>${v.quantite}</td>
                    <td>${formatPrix(v.prix)}</td>
                    <td>${formatPrix(lineTotal)}</td>
                </tr>
            `;
        }

        document.querySelectorAll(".sous-total span")[1].textContent = formatPrix(total);
        return total;
    }

    // ==============================
    // MAIN
    // ==============================
    async function init() {
        try {
            const reservation = await loadReservation();
            const totalPassagers = await loadPassagers(reservation.nbPassagers);
            const totalVehicules = await loadVehicules(reservation.nbVehicules);

            const total = totalPassagers + totalVehicules;
            document.querySelector(".prix-total .value").textContent = formatPrix(total);

        } catch (e) {
            console.error(e);
            alert("Erreur lors du chargement de la facture");
        }
    }

    init();
});
