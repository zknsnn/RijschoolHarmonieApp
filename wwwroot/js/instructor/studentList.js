
import { getToken, logout, redirectToLogin,getUserId } from "../global.js";

window.addEventListener("load", () => {
    const token = getToken();
    const role = localStorage.getItem("role");

    // Token or role check
    if (!token || role !== "Instructor") redirectToLogin();

    // Optional: show user ID
    console.log("Logged in userId:", localStorage.getItem("userId"));
});

// Attach logout button
const logoutBtn = document.getElementById("logoutBtn");
if (logoutBtn) logoutBtn.addEventListener("click", logout);


function createUserTable() {
    const instructorId = getUserId();
    fetch(`/api/User/instructor/${instructorId}`, { method: "GET" })
        .then(response => {
            console.log(response);
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Error with API");
            }
        })
        .then(users => {
            const tableBody = document.querySelector("#userTableBody");
            tableBody.innerHTML = "";

            users.forEach(user => {
                const row = document.createElement("tr");

                row.innerHTML = `
                    <td>${user.userId}</td>
                    <td>${user.firstName}</td>
                    <td>${user.lastName}</td>
                    <td>${user.email}</td>
                    <td>${user.phoneNumber}</td>
                `;

                tableBody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error", error);
            const msg = document.getElementById("msg");
            if (msg) msg.textContent = "Error loading users: " + error.message;
        });
}

window.addEventListener("load", createUserTable);