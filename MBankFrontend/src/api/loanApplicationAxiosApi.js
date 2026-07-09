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

export async function getLoanApplicationsUsingAxios() {

    try {

        const response = await axiosClient.get("/LoanApplication");

        return response.data;

    }
    catch (error) {

        throw new Error(getErrorMessage(error, "Failed to fetch loan applications."));

    }

}

export async function getLoanApplicationByIdUsingAxios(loanApplicationId) {

    try {

        const response = await axiosClient.get(`/LoanApplication/${loanApplicationId}`);

        return response.data;

    }
    catch (error) {

        throw new Error(getErrorMessage(error, "Failed to fetch loan application."));

    }

}

export async function createLoanApplicationUsingAxios(loanApplicationData) {

    try {

        const response = await axiosClient.post("/LoanApplication", loanApplicationData);

        return response.data;

    }
    catch (error) {

        throw new Error(getErrorMessage(error, "Failed to create loan application."));

    }

}

export async function updateLoanApplicationUsingAxios(loanApplicationId, loanApplicationData) {

    try {

        const response = await axiosClient.put(`/LoanApplication/${loanApplicationId}`, loanApplicationData);

        return response.data;

    }
    catch (error) {

        throw new Error(getErrorMessage(error, "Failed to update loan application."));

    }

}

export async function deleteLoanApplicationUsingAxios(loanApplicationId) {

    try {

        const response = await axiosClient.delete(`/LoanApplication/${loanApplicationId}`);

        return response.data;

    }
    catch (error) {

        throw new Error(getErrorMessage(error, "Failed to delete loan application."));

    }

}