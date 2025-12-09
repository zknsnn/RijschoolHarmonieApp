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

function createPaymentTable() {
    fetch("/api/Payment", { method: "GET" })
        .then(response => {
            console.log(response);
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Error with API");
            }
        })
        .then(payments => {
            const tableBody = document.querySelector("#paymentTableBody");
            tableBody.innerHTML = "";

            payments.forEach(payment => {
                const row = document.createElement("tr");

                row.innerHTML = `
                    <td>${payment.paymentId}</td>
                    <td>${payment.studentAccountId}</td>
                    <td>${payment.studentName}</td>
                    <td>${payment.studentLastName}</td>
                    <td>${payment.amount}</td>
                    <td>${formatDateDayTime(payment.date)}</td>
                    <td>${payment.description}</td>
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

window.addEventListener("load", createPaymentTable);