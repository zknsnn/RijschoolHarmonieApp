import { getToken, logout, redirectToLogin } from "./global.js";

window.addEventListener("load", () => {
    const token = getToken();
    const role = localStorage.getItem("role");

    // Token or role check
    if (!token || role !== "Student") redirectToLogin();

    // Optional: show user ID
    console.log("Logged in userId:", localStorage.getItem("userId"));
});

const logoutBtn = document.getElementById("logoutBtn");
if (logoutBtn) logoutBtn.addEventListener("click", logout);

let allAppointments = [];
let currentStartDate = new Date(); // today

// ------------------------
// LOAD APPOINTMENTS
// ------------------------
function loadAppointments() {
    fetch("/api/appointments")
        .then(res => {
            if (!res.ok) throw new Error("API error: " + res.status);
            return res.json();
        })
        .then(data => {
            console.log("API data:", data);
            allAppointments = data.map(a => ({
                ...a,
                startTime: new Date(a.startTime),
                endTime: new Date(a.endTime)
            }));
            renderFiveDayView();
        })
        .catch(err => {
            console.error("Loading error:", err);
            document.getElementById("msg").textContent =
                "Appointments could not be loaded: " + err.message;
        });
}

// ------------------------
// 5-DAY AGENDA
// ------------------------
function renderFiveDayView() {
    const start = new Date(currentStartDate);
    const end = new Date(currentStartDate);
    end.setDate(start.getDate() + 4);

    // Header: show 5-day range
    document.getElementById("weekRange").textContent =
        start.toLocaleDateString("tr-TR") + " - " + end.toLocaleDateString("tr-TR");

    // Create the time column
    const timeColumn = document.querySelector(".time-column");
    timeColumn.innerHTML = "";
    for (let hour = 8; hour <= 20; hour++) {
        const div = document.createElement("div");
        div.classList.add("time-slot");
        div.textContent = hour + ":00";
        timeColumn.appendChild(div);
    }

    // Populate the 5-day columns
    const dayColumns = document.querySelectorAll(".day-column");
    const dayHeaders = document.querySelectorAll(".day-column-header");

    dayColumns.forEach((col, index) => {
        col.innerHTML = "";

        const columnDate = new Date(start);
        columnDate.setDate(start.getDate() + index);

        // -----------------------
        // Gün başlıklarını ayrı header container'a ekle
        // -----------------------
        const dayName = columnDate.toLocaleDateString("en-EN", { weekday: "long" });
        const formattedDate = columnDate.toLocaleDateString("tr-TR");

        dayHeaders[index].innerHTML = `${dayName}<br>${formattedDate}`;

        // -----------------------
        // Saatleri ve randevuları ekle
        // -----------------------
        for (let hour = 8; hour <= 20; hour++) {
            const slot = document.createElement("div");
            slot.classList.add("day-slot");

            const slotDate = new Date(columnDate);
            slotDate.setHours(hour, 0, 0, 0);

            const appt = allAppointments.find(a =>
                a.startTime <= slotDate && slotDate < a.endTime
            );

            if (appt) {
                if (appt.type.toLowerCase() === "exam") slot.classList.add("exam");
                else slot.classList.add("busy");

                slot.textContent = `${appt.type}\n${appt.instructorName} - ${appt.studentName}`;
            }

            col.appendChild(slot);
        }
    });
}

// ------------------------
// NAVIGATION BUTTONS: SHIFT 5 DAYS
// ------------------------
document.getElementById("prevWeek").onclick = () => {
    currentStartDate.setDate(currentStartDate.getDate() - 5);
    renderFiveDayView();
};

document.getElementById("nextWeek").onclick = () => {
    currentStartDate.setDate(currentStartDate.getDate() + 5);
    renderFiveDayView();
};

// Initial load
loadAppointments();
