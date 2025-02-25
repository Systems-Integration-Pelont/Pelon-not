function LoginViewModel() {
    var self = this;

    self.email = ko.observable("");
    self.password = ko.observable("");
    self.errorMessage = ko.observable("");

    self.login = function () {
        if (self.email() === "user@example.com" && self.password() === "1234") {
            localStorage.setItem("isAuthenticated", "true");
            localStorage.setItem("bankAccountId", "1");
            window.location.href = "transactions.html";
        } else {
            self.errorMessage("Credenciales incorrectas");
        }
    };
}

ko.applyBindings(new LoginViewModel());
