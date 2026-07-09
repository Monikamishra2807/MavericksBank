import axios from "axios";

const axiosClient = axios.create({
    baseURL: "https://localhost:7173/api",
    headers: {
        "Content-Type": "application/json",
    },
});

axiosClient.interceptors.request.use((config) => {

    const token = localStorage.getItem("token");

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});

function getErrorMessage(error, fallbackMessage) {

    if (error.response?.data?.message) {
        return error.response.data.message;
    }

    if (error.response?.data?.title) {
        return error.response.data.title;
    }

    if (error.message) {
        return error.message;
    }

    return fallbackMessage;
}

export async function getTransactionsUsingAxios() {

    try {
        const response = await axiosClient.get("/Transaction");
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to fetch transactions."));
    }

}

export async function getTransactionByIdUsingAxios(transactionId) {

    try {
        const response = await axiosClient.get(`/Transaction/${transactionId}`);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to fetch transaction."));
    }

}

export async function createTransactionUsingAxios(transactionData) {

    try {
        const response = await axiosClient.post("/Transaction", transactionData);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to create transaction."));
    }

}

export async function updateTransactionUsingAxios(transactionId, transactionData) {

    try {
        const response = await axiosClient.put(`/Transaction/${transactionId}`, transactionData);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to update transaction."));
    }

}

export async function deleteTransactionUsingAxios(transactionId) {

    try {
        const response = await axiosClient.delete(`/Transaction/${transactionId}`);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to delete transaction."));
    }

}
export async function getAccountByNumberUsingAxios(accountNumber) {

    try {

        const response = await axiosClient.get(`/Account/AccountNumber/${accountNumber}`);

        return response.data;

    }
    catch (error) {

        if (error.response?.status === 404) {
            return null;
        }

        throw new Error(getErrorMessage(error, "Account not found."));
    }

}