document.getElementById("form").addEventListener("submit", async (e) => {
    e.preventDefault(); // Prevent page reload

    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value;

    try {
        const res = await fetch("/api/auth/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password })
        });

        if (res.status === 401) {
            document.getElementById("msg").innerText = "Invalid email or password!";
            return;
        }

        const data = await res.json();

        // ✅ Save to LocalStorage
        localStorage.setItem("token", data.token);
        localStorage.setItem("role", data.role);
        localStorage.setItem("userId", data.userId);

        // 🔹 Role-based redirection
        switch(data.role) {
            case "Admin":
                window.location.href = "/html/adminDashboard.html";
                break;
            case "Instructor":
                window.location.href = "/html/instructorDashboard.html";
                break;
            case "Student":
                window.location.href = "/html/studentDashboard.html";
                break;
            default:
                document.getElementById("msg").innerText = "Role is not defined!";
        }

    } catch (err) {
        console.error(err);
        document.getElementById("msg").innerText = "Server error, please try again.";
    }
});
