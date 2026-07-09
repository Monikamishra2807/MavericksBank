import { useState } from "react";
import { registerUserUsingAxios } from "../api/authAxiosApi";
import { Link } from "react-router-dom";

export function RegisterForm() {

    const [register, setRegister] = useState({
        fullName: "",
        email: "",
        password: "",
        mobile: "",
        role: "Customer"
    });

    const [errors, setErrors] = useState({});
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    function handleChange(event) {

        setRegister({
            ...register,
            [event.target.name]: event.target.value
        });

        setErrors({
            ...errors,
            [event.target.name]: ""
        });

        setError("");
        setSuccess("");

    }

    function validate() {

        let validationErrors = {};

        if (!register.fullName.trim()) {
            validationErrors.fullName = "Full Name is required.";
        }

        if (!register.email.trim()) {
            validationErrors.email = "Email is required.";
        }

        if (!register.password.trim()) {
            validationErrors.password = "Password is required.";
        }

        if (!register.mobile.trim()) {
            validationErrors.mobile = "Mobile Number is required.";
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

            await registerUserUsingAxios(register);

            setSuccess("Registration completed successfully! You can now login.");
            setError("");

            setRegister({
                fullName: "",
                email: "",
                password: "",
                mobile: "",
                role: "Customer"
            });

            setErrors({});

        }

        catch (error) {

            setSuccess("");
            setError(error.message);

        }

    }

    return (

        <div className="container mt-5">

            <div className="row justify-content-center">

                <div className="col-md-7">

                    <div className="card shadow-lg border-0 rounded-4">

                        <div className="card-header bg-primary text-white text-center py-3 rounded-top-4">

                            <h3 className="fw-bold mb-0">
                                📝 Register
                            </h3>

                        </div>

                        <div className="card-body p-4">

                            {success && (
                                <div className="alert alert-success text-center">
                                    {success}
                                </div>
                            )}

                            {error && (
                                <div className="alert alert-danger text-center">
                                    {error}
                                </div>
                            )}

                            <form onSubmit={handleSubmit} noValidate>

                                <div className="row mb-4 align-items-center">

                                    <label className="col-sm-4 col-form-label fw-semibold">
                                        Full Name
                                    </label>

                                    <div className="col-sm-8">

                                        <input
                                            type="text"
                                            name="fullName"
                                            className={`form-control rounded-3 ${errors.fullName ? "is-invalid" : ""}`}
                                            placeholder="Enter Full Name"
                                            value={register.fullName}
                                            onChange={handleChange}
                                        />

                                        {errors.fullName && (
                                            <small className="text-danger">
                                                {errors.fullName}
                                            </small>
                                        )}

                                    </div>

                                </div>

                                <div className="row mb-4 align-items-center">

                                    <label className="col-sm-4 col-form-label fw-semibold">
                                        Email
                                    </label>

                                    <div className="col-sm-8">

                                        <input
                                            type="email"
                                            name="email"
                                            className={`form-control rounded-3 ${errors.email ? "is-invalid" : ""}`}
                                            placeholder="Enter Email Address"
                                            value={register.email}
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
                                            placeholder="Create Password"
                                            value={register.password}
                                            onChange={handleChange}
                                        />

                                        {errors.password && (
                                            <small className="text-danger">
                                                {errors.password}
                                            </small>
                                        )}

                                    </div>

                                </div>

                                <div className="row mb-4 align-items-center">

                                    <label className="col-sm-4 col-form-label fw-semibold">
                                        Mobile
                                    </label>

                                    <div className="col-sm-8">

                                        <input
                                            type="text"
                                            name="mobile"
                                            className={`form-control rounded-3 ${errors.mobile ? "is-invalid" : ""}`}
                                            placeholder="Enter Mobile Number"
                                            value={register.mobile}
                                            onChange={handleChange}
                                        />

                                        {errors.mobile && (
                                            <small className="text-danger">
                                                {errors.mobile}
                                            </small>
                                        )}

                                    </div>

                                </div>

                                <div className="row mb-4 align-items-center">

                                    <label className="col-sm-4 col-form-label fw-semibold">
                                        Role
                                    </label>

                                    <div className="col-sm-8">

                                        <select
                                            name="role"
                                            className="form-select rounded-3"
                                            value={register.role}
                                            onChange={handleChange}
                                        >
                                            <option value="Customer">
                                                Customer
                                            </option>
                                        </select>

                                    </div>

                                </div>

                                <div className="text-center">

                                    <button
                                        type="submit"
                                        className="btn btn-success px-5 py-2 rounded-pill fw-bold shadow">

                                        📝 Register

                                    </button>

                                </div>

                                <div className="text-center mt-4">

                                    <p className="mb-0">

                                        Already have an account?{" "}

                                        <Link
                                            to="/login"
                                            className="fw-bold text-decoration-none"
                                        >
                                            Login
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