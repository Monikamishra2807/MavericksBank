import { createContext, useEffect, useState } from "react";

export const AuthContext = createContext();

export function AuthProvider({ children }) {

    const [token, setToken] = useState(localStorage.getItem("token"));
    const [role, setRole] = useState(localStorage.getItem("role"));
    const [fullName, setFullName] = useState(localStorage.getItem("fullName"));

    const isAuthenticated = !!token;

    function login(userToken, userRole, name) {

        localStorage.setItem("token", userToken);
        localStorage.setItem("role", userRole);
        localStorage.setItem("fullName", name);

        setToken(userToken);
        setRole(userRole);
        setFullName(name);
    }

    function logout() {

        localStorage.removeItem("token");
        localStorage.removeItem("role");
        localStorage.removeItem("fullName");

        setToken(null);
        setRole(null);
        setFullName(null);
    }

    useEffect(() => {

        setToken(localStorage.getItem("token"));
        setRole(localStorage.getItem("role"));
        setFullName(localStorage.getItem("fullName"));

    }, []);

    return (

        <AuthContext.Provider
            value={{
                token,
                role,
                fullName,
                isAuthenticated,
                login,
                logout
            }}
        >
            {children}
        </AuthContext.Provider>

    );

}