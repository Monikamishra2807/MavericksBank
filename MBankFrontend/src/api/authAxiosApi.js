import axios from "axios";

const axiosClient = axios.create({
    baseURL: "https://localhost:7173/api",
    headers: {
        "Content-Type": "application/json",
    },
});

function getErrorMessage(error, fallbackMessage) {

    if (error.response) {

        switch (error.response.status) {

            case 400:
                return (
                    error.response.data?.message ||
                    "Validation failed. Please check your inputs."
                );

            case 401:
                return "Invalid Email or Password.";

            case 403:
                return "Access Denied. You are not authorized.";

            case 404:
                return "Requested resource was not found.";

            case 500:
                return "Internal Server Error. Please try again later.";

            default:
                return (
                    error.response.data?.message ||
                    error.response.data?.title ||
                    fallbackMessage
                );

        }

    }

    if (error.request) {
        return "Unable to connect to the server.";
    }

    return error.message || fallbackMessage;

}

export async function registerUserUsingAxios(registerData) {

    try {

        const response = await axiosClient.post(
            "/Auth/Register",
            registerData
        );

        return response.data;

    }
    catch (error) {

        throw new Error(
            getErrorMessage(error, "Registration Failed.")
        );

    }

}

export async function loginUserUsingAxios(loginData) {

    try {

        const response = await axiosClient.post(
            "/Auth/Login",
            loginData
        );

        return response.data;

    }
    catch (error) {

        throw new Error(
            getErrorMessage(error, "Login Failed.")
        );

    }

}