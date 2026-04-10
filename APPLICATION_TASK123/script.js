document.getElementById("loginForm")?.addEventListener("submit", function(e) {
  e.preventDefault();

  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;

  const userRegex = /^[a-zA-Z0-9]{3,15}$/;
  const passRegex = /^[a-zA-Z0-9@#]{6,20}$/;

  if (!userRegex.test(username)) {
    alert("Invalid username");
    return;
  }

  if (!passRegex.test(password)) {
    alert("Invalid password");
    return;
  }

  window.location.href = "dashboard.html";
});
