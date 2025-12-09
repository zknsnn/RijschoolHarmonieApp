// common.js
// Utility functions for authentication, alerts, fetch helpers, and general dashboard actions

const timeoutMs = 3000;

// --- Token & Authentication ---
export function getToken() {
    return localStorage.getItem("token") || localStorage.getItem("accessToken");
}

export function authHeaders() {
    return { Authorization: "Bearer " + getToken(), "Content-Type": "application/json" };
}

export function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    localStorage.removeItem("userId");
    window.location.href = "/index.html"; // redirect to login page
}

export function redirectToLogin() {
    logout();
}

// --- Alerts & Dialogs ---
export function alertDialog(message, type = "info") {
    const toast = document.createElement("div");
    toast.className = `toast toast-${type}`;
    toast.textContent = message;

    toast.style.marginTop = `${document.querySelectorAll('.toast').length * 60}px`;
    toast.addEventListener("animationend", () => toast.remove());

    document.body.appendChild(toast);
    setTimeout(() => toast.classList.add("fade-out"), timeoutMs);
}

export function confirmDialog(message) {
    return new Promise((resolve) => {
        const dialog = document.createElement("div");
        dialog.className = "toast toast-confirm";
        dialog.innerHTML = `
            <p>${message}</p>
            <button class="btn-ok">OK</button>
            <button class="btn-cancel">Cancel</button>
        `;
        document.body.appendChild(dialog);

        dialog.querySelector(".btn-ok").onclick = () => {
            dialog.remove();
            resolve(true);
        };
        dialog.querySelector(".btn-cancel").onclick = () => {
            dialog.remove();
            resolve(false);
        };
    });
}

// --- Fetch response helpers ---
export function handleResponse(response, customMessage = "Request failed") {
    if (!response.ok) {
        return response.json().then(err => { throw new Error(err.message || customMessage); });
    }
    return response.json();
}

export function fail(error, fallbackMessage = "Something went wrong.") {
    console.error("An error occurred:", error);
    alertDialog(error?.message || fallbackMessage, "error");
}

// --- Date formatting ---
export function formatDate(dateString) {
    const d = new Date(dateString);
    const day = String(d.getDate()).padStart(2, "0");
    const month = String(d.getMonth() + 1).padStart(2, "0");
    const year = d.getFullYear();
    return `${day}/${month}/${year}`;
}

// --- Sidebar leave confirmation ---
export function sidebarClickEventForCRUDpages() {
    const sidebarLinks = document.querySelectorAll(".sidebar nav a");
    sidebarLinks.forEach(link => {
        link.addEventListener("click", async (e) => {
            e.preventDefault();
            const ok = await confirmDialog("Are you sure you want to leave this page? Unsaved changes will be lost.");
            if (ok) window.location.href = link.getAttribute("href");
        });
    });
}
