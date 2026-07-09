import { useState } from "react";
import { createBeneficiaryUsingAxios } from "../api/beneficiaryAxiosApi";

export function BeneficiaryForm() {

    const [beneficiary, setBeneficiary] = useState({
        beneficiaryName: "",
        bankName: "",
        accountNumber: "",
        ifscCode: ""
    });

    const [errors, setErrors] = useState({});
    const [success, setSuccess] = useState("");
    const [error, setError] = useState("");

    function handleChange(event) {

        setBeneficiary({
            ...beneficiary,
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

        if (!beneficiary.beneficiaryName.trim()) {
            validationErrors.beneficiaryName = "Beneficiary Name is required.";
        }

        if (!beneficiary.bankName.trim()) {
            validationErrors.bankName = "Bank Name is required.";
        }

        if (!beneficiary.accountNumber.trim()) {
            validationErrors.accountNumber = "Account Number is required.";
        }

        if (!beneficiary.ifscCode.trim()) {
            validationErrors.ifscCode = "IFSC Code is required.";
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

            await createBeneficiaryUsingAxios(beneficiary);

            setSuccess("Beneficiary added successfully.");
            setError("");

            setBeneficiary({
                beneficiaryName: "",
                bankName: "",
                accountNumber: "",
                ifscCode: ""
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
                    👥 Add Beneficiary
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
                            Beneficiary Name
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="text"
                                name="beneficiaryName"
                                className={`form-control rounded-3 ${errors.beneficiaryName ? "is-invalid" : ""}`}
                                placeholder="Enter Beneficiary Name"
                                value={beneficiary.beneficiaryName}
                                onChange={handleChange}
                            />

                            {errors.beneficiaryName && (
                                <small className="text-danger">
                                    {errors.beneficiaryName}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            Bank Name
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="text"
                                name="bankName"
                                className={`form-control rounded-3 ${errors.bankName ? "is-invalid" : ""}`}
                                placeholder="Enter Bank Name"
                                value={beneficiary.bankName}
                                onChange={handleChange}
                            />

                            {errors.bankName && (
                                <small className="text-danger">
                                    {errors.bankName}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            Account Number
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="text"
                                name="accountNumber"
                                className={`form-control rounded-3 ${errors.accountNumber ? "is-invalid" : ""}`}
                                placeholder="Enter Account Number"
                                value={beneficiary.accountNumber}
                                onChange={handleChange}
                            />

                            {errors.accountNumber && (
                                <small className="text-danger">
                                    {errors.accountNumber}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            IFSC Code
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="text"
                                name="ifscCode"
                                className={`form-control rounded-3 ${errors.ifscCode ? "is-invalid" : ""}`}
                                placeholder="Enter IFSC Code"
                                value={beneficiary.ifscCode}
                                onChange={handleChange}
                            />

                            {errors.ifscCode && (
                                <small className="text-danger">
                                    {errors.ifscCode}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="text-center mt-4">

                        <button
                            type="submit"
                            className="btn btn-success px-5 py-2 rounded-pill fw-bold shadow">

                            👥 Add Beneficiary

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

}