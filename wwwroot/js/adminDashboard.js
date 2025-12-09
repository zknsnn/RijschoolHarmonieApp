import { getToken, logout, redirectToLogin } from "./global.js";

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
