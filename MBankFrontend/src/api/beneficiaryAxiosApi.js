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

export async function getBeneficiariesUsingAxios() {

    try {
        const response = await axiosClient.get("/Beneficiary");
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to fetch beneficiaries."));
    }
}

export async function getBeneficiaryByIdUsingAxios(beneficiaryId) {

    try {
        const response = await axiosClient.get(`/Beneficiary/${beneficiaryId}`);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to fetch beneficiary."));
    }
}

export async function createBeneficiaryUsingAxios(beneficiaryData) {

    try {
        const response = await axiosClient.post("/Beneficiary", beneficiaryData);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to create beneficiary."));
    }
}

export async function updateBeneficiaryUsingAxios(beneficiaryId, beneficiaryData) {

    try {
        const response = await axiosClient.put(`/Beneficiary/${beneficiaryId}`, beneficiaryData);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to update beneficiary."));
    }
}

export async function deleteBeneficiaryUsingAxios(beneficiaryId) {

    try {
        const response = await axiosClient.delete(`/Beneficiary/${beneficiaryId}`);
        return response.data;
    }
    catch (error) {
        throw new Error(getErrorMessage(error, "Failed to delete beneficiary."));
    }
}