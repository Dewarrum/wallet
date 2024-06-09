export const PaymentMethod = {
    get Other() {
        return {
            name: 'Other',
            value: 0,
        }
    },

    get Cash() {
        return {
            name: 'Cash',
            value: 1,
        }
    },

    get CreditCard() {
        return {
            name: 'Credit Card',
            value: 2,
        }
    },

    get DebitCard() {
        return {
            name: 'Debit Card',
            value: 3,
        }
    },

    get BankTransfer() {
        return {
            name: 'Bank Transfer',
            value: 4,
        }
    },

    get All(): { name: string, value: number }[] {
        return [
            this.Other, this.Cash, this.CreditCard, this.DebitCard, this.BankTransfer
        ]
    }
}