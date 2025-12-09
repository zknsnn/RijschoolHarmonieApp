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


function createAppointmentTable() {
    fetch("/api/appointments", { method: "GET" })
        .then(response => {
            console.log(response);
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Error with API");
            }
        })
        .then(appointments => {
            const tableBody = document.querySelector("#appointmentTableBody");
            tableBody.innerHTML = "";

            appointments.forEach(appointment => {
                const row = document.createElement("tr");

                row.innerHTML = `
                    <td>${appointment.appointmentId}</td>
                    <td>${appointment.instructorId}</td>
                    <td>${appointment.instructorName}</td>
                    <td>${appointment.studentId}</td>
                    <td>${appointment.studentName}</td>
                    <td>${appointment.type}</td>
                    <td>${formatDateDayTime(appointment.startTime)}</td>
                    <td>${formatDateDayTime(appointment.endTime)}</td>
                    <td>${appointment.price}</td>
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

window.addEventListener("load", createAppointmentTable);