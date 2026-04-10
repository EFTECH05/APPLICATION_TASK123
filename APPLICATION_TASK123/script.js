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
const paymentForm = document.getElementById("paymentForm");

if (paymentForm) {
  paymentForm.addEventListener("submit", function(e) {
    e.preventDefault();

    const name = document.getElementById("name").value;
    const account = document.getElementById("account").value;
    const amount = document.getElementById("amount").value;

    const nameRegex = /^[a-zA-Z\s]{2,50}$/;
    const accountRegex = /^[0-9]{8,12}$/;
    const amountRegex = /^[0-9]+$/;

    if (!nameRegex.test(name)) {
      alert("Invalid name");
      return;
    }

    if (!accountRegex.test(account)) {
      alert("Invalid account number");
      return;
    }

    if (!amountRegex.test(amount)) {
      alert("Invalid amount");
      return;
    }

    document.getElementById("message").innerText =
      "Payment Sent Successfully!";
  });
}
