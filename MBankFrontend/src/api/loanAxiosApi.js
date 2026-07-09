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

export async function getLoansUsingAxios() {

    try {

        const response = await axiosClient.get("/Loan");

        return response.data;

    }
    catch (error) {

        throw new Error(getErrorMessage(error, "Failed to fetch loans."));

    }

}

export async function getLoanByIdUsingAxios(loanId) {

    try {

        const response = await axiosClient.get(`/Loan/${loanId}`);

        return response.data;

    }
    catch (error) {

        throw new Error(getErrorMessage(error, "Failed to fetch loan."));

    }

}

export async function createLoanUsingAxios(loanData) {

    try {

        const response = await axiosClient.post("/Loan", loanData);

        return response.data;

    }
    catch (error) {

        throw new Error(getErrorMessage(error, "Failed to create loan."));

    }

}

export async function updateLoanUsingAxios(loanId, loanData) {

    try {

        const response = await axiosClient.put(`/Loan/${loanId}`, loanData);

        return response.data;

    }
    catch (error) {

        throw new Error(getErrorMessage(error, "Failed to update loan."));

    }

}

export async function deleteLoanUsingAxios(loanId) {

    try {

        const response = await axiosClient.delete(`/Loan/${loanId}`);

        return response.data;

    }
    catch (error) {

        throw new Error(getErrorMessage(error, "Failed to delete loan."));

    }

}