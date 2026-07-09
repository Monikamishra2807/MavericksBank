import { useState, useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { loginUserUsingAxios } from "../api/authAxiosApi";
import { AuthContext } from "../context/AuthContext";

export function LoginForm() {

    const navigate = useNavigate();

    const { login: loginContext } = useContext(AuthContext);

    const [login, setLogin] =useState({
        email: "",
        password: ""
    });

    const [errors, setErrors] = useState({});

    const [serverError, setServerError] = useState("");

    function handleChange(event) {

        setLogin({
            ...login,
            [event.target.name]: event.target.value
        });

        setErrors({
            ...errors,
            [event.target.name]: ""
        });

        setServerError("");

    }

    function validate() {

        let validationErrors = {};

        if (!login.email.trim()) {
            validationErrors.email = "Email is required.";
        }

        if (!login.password.trim()) {
            validationErrors.password = "Password is required.";
        }

        setErrors(validationErrors);

        return Object.keys(validationErrors).length === 0;

    }

    async function handleSubmit(event) {

        event.preventDefault();

        if (!validate()) {
            return;
        }

        try {

            const response = await loginUserUsingAxios(login);

            loginContext(
                response.token,
                response.role,
                response.fullName || ""
            );

            navigate("/dashboard");

        }

        catch (error) {

            setServerError(error.message);

        }

    }

    return (

        <div className="container mt-5">

            <div className="row justify-content-center">

                <div className="col-md-6">

                    <div className="card shadow-lg border-0 rounded-4">

                        <div className="card-header bg-primary text-white text-center py-3 rounded-top-4">

                            <h3 className="fw-bold mb-0">
                                🔐 Login
                            </h3>

                        </div>

                        <div className="card-body p-4">

                            {serverError && (
                             <div className="login-error">
                                {serverError}
                             </div>
                            )}

                            <form onSubmit={handleSubmit} noValidate>

                                <div className="row mb-4 align-items-center">

                                    <label className="col-sm-4 col-form-label fw-semibold">

                                        Email

                                    </label>

                                    <div className="col-sm-8">

                                        <input
                                            type="email"
                                            name="email"
                                            className={`form-control rounded-3 ${errors.email ? "is-invalid" : ""}`}
                                            placeholder="Enter Email"
                                            value={login.email}
                                            onChange={handleChange}
                                        />

                                        {errors.email && (

                                            <small className="text-danger">

                                                {errors.email}

                                            </small>

                                        )}

                                    </div>

                                </div>

                                <div className="row mb-4 align-items-center">

                                    <label className="col-sm-4 col-form-label fw-semibold">

                                        Password

                                    </label>

                                    <div className="col-sm-8">

                                        <input
                                            type="password"
                                            name="password"
                                            className={`form-control rounded-3 ${errors.password ? "is-invalid" : ""}`}
                                            placeholder="Enter Password"
                                            value={login.password}
                                            onChange={handleChange}
                                        />

                                        {errors.password && (

                                            <small className="text-danger">

                                                {errors.password}

                                            </small>

                                        )}

                                    </div>

                                </div>

                                <div className="text-center">

                                    <button
                                        type="submit"
                                        className="btn btn-success px-5 py-2 rounded-pill fw-bold shadow">

                                        🔑 Login

                                    </button>

                                </div>

                                <div className="text-center mt-4">

                                    <p className="mb-0">

                                        Don't have an account?{" "}

                                        <Link
                                            to="/register"
                                            className="fw-bold text-decoration-none"
                                        >

                                            Register

                                        </Link>

                                    </p>

                                </div>

                            </form>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    );

}