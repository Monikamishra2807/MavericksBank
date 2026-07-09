import { useState } from "react";
import { createCustomerUsingAxios } from "../api/customerAxiosApi";

export function CustomerForm() {

    const [customer, setCustomer] = useState({
        dob: "",
        aadharNumber: "",
        panNumber: "",
        address: ""
    });

    const [errors, setErrors] = useState({});
    const [success, setSuccess] = useState("");
    const [error, setError] = useState("");

    function handleChange(event) {

        setCustomer({
            ...customer,
            [event.target.name]: event.target.value
        });

        setErrors({
            ...errors,
            [event.target.name]: ""
        });

        setSuccess("");
        setError("");

    }

    function validate() {

        let validationErrors = {};

        if (!customer.dob) {
            validationErrors.dob = "Date of Birth is required.";
        }

        if (!customer.aadharNumber.trim()) {
            validationErrors.aadharNumber = "Aadhaar Number is required.";
        }

        if (!customer.panNumber.trim()) {
            validationErrors.panNumber = "PAN Number is required.";
        }

        if (!customer.address.trim()) {
            validationErrors.address = "Address is required.";
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

            await createCustomerUsingAxios(customer);

            setSuccess("Customer created successfully.");
            setError("");

            setCustomer({
                dob: "",
                aadharNumber: "",
                panNumber: "",
                address: ""
            });

            setErrors({});

        }
        catch (error) {

            setSuccess("");
            setError(error.message);

        }

    }

    return (

        <div className="card shadow-lg border-0 rounded-4">

            <div className="card-header bg-primary text-white rounded-top-4 py-3">

                <h3 className="fw-bold text-center mb-0">
                    👤 Customer Details
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

                <form onSubmit={handleSubmit} noValidate autoComplete="off">

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            Date of Birth
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="date"
                                name="dob"
                                className={`form-control rounded-3 ${errors.dob ? "is-invalid" : ""}`}
                                value={customer.dob}
                                onChange={handleChange}
                            />

                            {errors.dob && (
                                <small className="text-danger">
                                    {errors.dob}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            Aadhaar Number
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="text"
                                name="aadharNumber"
                                className={`form-control rounded-3 ${errors.aadharNumber ? "is-invalid" : ""}`}
                                placeholder="Enter Aadhaar Number"
                                value={customer.aadharNumber}
                                onChange={handleChange}
                            />

                            {errors.aadharNumber && (
                                <small className="text-danger">
                                    {errors.aadharNumber}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            PAN Number
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="text"
                                name="panNumber"
                                className={`form-control rounded-3 ${errors.panNumber ? "is-invalid" : ""}`}
                                placeholder="Enter PAN Number"
                                value={customer.panNumber}
                                onChange={handleChange}
                            />

                            {errors.panNumber && (
                                <small className="text-danger">
                                    {errors.panNumber}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-start">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            Address
                        </label>

                        <div className="col-sm-8">

                            <textarea
                                name="address"
                                className={`form-control rounded-3 ${errors.address ? "is-invalid" : ""}`}
                                rows="3"
                                placeholder="Enter Address"
                                value={customer.address}
                                onChange={handleChange}
                            ></textarea>

                            {errors.address && (
                                <small className="text-danger">
                                    {errors.address}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="text-center mt-4">

                        <button
                            type="submit"
                            className="btn btn-success px-5 py-2 rounded-pill fw-bold shadow">

                            👤 Save Customer

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

}