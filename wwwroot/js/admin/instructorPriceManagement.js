import { getToken, logout, redirectToLogin ,formatDateDayTime} from "../global.js";

window.addEventListener("load", () => {
    const token = getToken();
    const role = localStorage.getItem("role");

    // Token or role check
    if (!token || role !== "Admin") redirectToLogin();

    // Optional: show user ID
    console.log("Logged in userId:", localStorage.getItem("userId"));
});

// Attach logout button
const logoutBtn = document.getElementById("logoutBtn");
if (logoutBtn) logoutBtn.addEventListener("click", logout);

function createPriceTable() {
    fetch("/api/InstructorPrice", { method: "GET" })
        .then(response => {
            console.log(response);
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Error with API");
            }
        })
        .then(prices => {
            const tableBody = document.querySelector("#priceTableBody");
            tableBody.innerHTML = "";

            prices.forEach(price => {
                const row = document.createElement("tr");

                row.innerHTML = `
                    <td>${price.instructorPriceId}</td>
                    <td>${price.instructorId}</td>
                    <td>${price.lessonPrice}</td>
                    <td>${price.examPrice}</td>
                    <td>${formatDateDayTime(price.lastUpdateDate)}</td> 
                `;

                tableBody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error", error);
            const msg = document.getElementById("msg");
            if (msg) msg.textContent = "Error loading appointments: " + error.message;
        });
}

window.addEventListener("load", createPriceTable);