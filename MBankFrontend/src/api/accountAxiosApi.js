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

export async function getAccountsUsingAxios() {

    try {
        const response = await axiosClient.get("/Account");
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to fetch accounts."));
    }
}

export async function getAccountByIdUsingAxios(accountId) {

    try {
        const response = await axiosClient.get(`/Account/${accountId}`);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to fetch account."));
    }
}

export async function createAccountUsingAxios(accountData) {

    try {
        const response = await axiosClient.post("/Account", accountData);
        return response.data;
    }
    catch (error) {
        if (error.response?.data) {
        throw new Error(error.response.data);
    }

    throw new Error("Failed to create account.");
    }
}

export async function updateAccountUsingAxios(accountId, accountData) {

    try {
        const response = await axiosClient.put(`/Account/${accountId}`, accountData);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to update account."));
    }
}

export async function deleteAccountUsingAxios(accountId) {

    try {
        const response = await axiosClient.delete(`/Account/${accountId}`);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to delete account."));
    }
}
export async function getMyAccountUsingAxios() {

    try {

        const response = await axiosClient.get("/Account/MyAccount");

        return response.data;

    }
    catch (error) {

        if (error.response?.status === 404) {
            return null;
        }

        throw new Error(getErrorMessage(error, "Failed to fetch account."));
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