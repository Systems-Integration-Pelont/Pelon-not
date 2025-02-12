function TransactionsViewModel() {
    var self = this;
    self.transactions = ko.observableArray([]);

    self.loadTransactions = function () {
        var bankAccountId = localStorage.getItem("bankAccountId");
        if (!bankAccountId) {
            console.error("No se encontró bankAccountId en localStorage");
            return;
        }

        fetch(`http://localhost:5270/api/BankAccount/GetAllTransactionsByAccount/${bankAccountId}`)
            .then(response => response.json())
            .then(data => {

                let transactions = [
                    ...data.selfOperations.map(t => ({ 
                        transactionId: t.selfTransactionId, 
                        fromAccountId: t.bankAccountId, 
                        toAccountId: "N/A", 
                        amount: t.amount, 
                        transactionDes: `Operación propia (${t.selfOperationTypeId})`, 
                        formattedDate: new Date(t.dateTime).toLocaleString() 
                    })),
                    ...data.selfATMOperations.map(t => ({ 
                        transactionId: t.selfAtmtransactionId, 
                        fromAccountId: t.bankAccountId, 
                        toAccountId: "N/A", 
                        amount: t.amount, 
                        transactionDes: "Operación ATM propia", 
                        formattedDate: new Date(t.dateTime).toLocaleString() 
                    })),
                    ...data.atmInterUserTransactions.map(t => ({ 
                        transactionId: t.atminterUserTransactionId, 
                        fromAccountId: t.fromBankAccountId, 
                        toAccountId: t.toBankAccountId, 
                        amount: t.amount, 
                        transactionDes: "Transferencia ATM", 
                        formattedDate: new Date(t.dateTime).toLocaleString() 
                    })),
                    ...data.interUserTransactions.map(t => ({ 
                        transactionId: t.appInterUserTransactionId, 
                        fromAccountId: t.fromBankAccountId, 
                        toAccountId: t.toBankAccountId, 
                        amount: t.amount, 
                        transactionDes: "Transferencia App", 
                        formattedDate: new Date(t.dateTime).toLocaleString() 
                    }))
                ];

                self.transactions(transactions);
            })
            .catch(error => console.error("Error al obtener transacciones:", error));
    };

    self.logout = function () {
        alert("Sesión cerrada");
        window.location.href = "../index.html";
    };

    self.loadTransactions();
}

ko.applyBindings(new TransactionsViewModel());
