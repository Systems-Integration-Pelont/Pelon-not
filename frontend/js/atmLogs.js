function AtmLogsViewModel() {
    var self = this;
    self.atmLogs = ko.observableArray([]);

    self.loadLogs = function () {
        fetch("http://localhost:5242/api/LogAtm")
            .then(response => response.json())
            .then(data => {
                let logs = data.LogAtms.map(t => ({ 
                    logAtmId: t.LogAtmId, 
                    atmId: t.AtmId, 
                    errorMessage: t.ErrorMessage, 
                    date: new Date(t.Date).toLocaleDateString(),
                    time: t.Time
                }));
                self.atmLogs(logs);
            })
            .catch(error => {
                console.error("Error al cargar logs de ATMs:", error);
            });
    };

    self.logout = function () {
        localStorage.removeItem("isAuthenticated");
        window.location.href = "login.html";
    };

    self.loadLogs();
}

ko.applyBindings(new AtmLogsViewModel());
