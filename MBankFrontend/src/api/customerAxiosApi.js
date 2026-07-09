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
export async function createCustomerUsingAxios(customerData) {
    try
    {
      const response = await axiosClient.post("/Customer", customerData);
      return response.data;
    }
    catch(error)
    {
        throw new Error(getErrorMessage(error, "Failed to fetch customers."));
    }
}

export async function getCustomersUsingAxios() {
    try
    {
      const response = await axiosClient.get("/Customer");
       return response.data;
    }
    catch(error)
    {
        throw new Error(getErrorMessage(error, "Failed to fetch customers."));
    }
}

export async function updateCustomerUsingAxios(id, customerData) {
    try
    {
      const response = await axiosClient.put(`/Customer/${id}`, customerData);
      return response.data;
    }
     catch(error)
    {
        throw new Error(getErrorMessage(error, "Failed to fetch customers."));
    }
}

export async function deleteCustomerUsingAxios(id) {
    try
    {
       const response = await axiosClient.delete(`/Customer/${id}`);
      return response.data;
    }
    catch(error)
    {
        throw new Error(getErrorMessage(error, "Failed to fetch customers."));
    }
}
export async function getMyProfileUsingAxios() {

    try {

        const response = await axiosClient.get("/Customer/MyProfile");

        return response.data;

    }
    catch (error) {

        if (error.response?.status === 404) {
            return null;
        }

        throw new Error(getErrorMessage(error, "Failed to fetch profile."));
    }

}