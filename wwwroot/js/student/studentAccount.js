import { getToken, logout, redirectToLogin, formatDateDayTime, authHeaders } from "../global.js";

const API_STUDENTACCOUNT_WITH_USERID = '/api/StudentAccounts/studentId';

function getUserId() {
    const id = localStorage.getItem("userId");
    return id ? parseInt(id) : null;
}

// --- WINDOW LOAD ---
window.addEventListener("load", () => {
    const token = getToken();
    const role = localStorage.getItem("role");

    // Token veya role kontrolü
    if (!token || role !== "Student") redirectToLogin();

    // UserId log
    console.log("Logged in userId:", getUserId());



    // Sayfa initialization
    createPaymentTable();
    initStudentOverview();
});

// --- LOGOUT BUTTON ---
const logoutBtn = document.getElementById("logoutBtn");
if (logoutBtn) logoutBtn.addEventListener("click", logout);

// --- CREATE PAYMENT TABLE ---
async function createPaymentTable() {
    try {
        const userId = getUserId();
        const res = await fetch(`/api/Payment/student/${userId}`);

        if (!res.ok) throw new Error("Error fetching payments");

        const payments = await res.json();
        console.log("Payments received:", payments);
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
    } catch (err) {
        console.error("Error loading payments:", err);
        const msg = document.getElementById("msg");
        if (msg) msg.textContent = "Error loading payments: " + err.message;
    }
}

// --- INIT STUDENT OVERVIEW ---
async function initStudentOverview() {
    const userId = getUserId();
    if (!userId) return redirectToLogin();

    try {
        const student = await fetchStudentAccountWithUserId(userId);
        if (!student) return console.warn("No matching student found");
        fillStudentForm(student);
    } catch (err) {
        console.error("Initialization failed:", err);
    }
}


// --- FILL STUDENT FORM ---
function fillStudentForm(student) {
    document.querySelector("#studentId .form-value").value = student.studentId ?? "-";
    document.querySelector("#studentName .form-value").value = student.studentName ?? "-";
    document.querySelector("#totalDebit .form-value").value = student.totalDebit ?? "-";
    document.querySelector("#totalCredit .form-value").value = student.totalCredit ?? "-";
    document.querySelector("#balance .form-value").value = student.balance ?? "-";
}

// --- FETCH STUDENT BY USERID ---
async function fetchStudentAccountWithUserId(id) {
    try {
        const res = await fetch(`${API_STUDENTACCOUNT_WITH_USERID}/${id}`, { headers: authHeaders() });
        if (!res.ok) throw new Error("Failed to fetch student account");
        return await res.json();
    } catch (err) {
        console.error(err);
        return null;
    }
}
