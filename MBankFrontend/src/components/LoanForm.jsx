import { useState } from "react";
import { createLoanUsingAxios } from "../api/loanAxiosApi";

export function LoanForm() {

    const [loan, setLoan] = useState({
        loanName: "",
        interestRate: "",
        tenureInMonths: "",
        maximumAmount: ""
    });

    const [errors, setErrors] = useState({});
    const [success, setSuccess] = useState("");
    const [error, setError] = useState("");

    function handleChange(event) {

        setLoan({
            ...loan,
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

        if (!loan.loanName.trim()) {
            validationErrors.loanName = "Loan Name is required.";
        }

        if (!loan.interestRate.trim()) {
            validationErrors.interestRate = "Interest Rate is required.";
        }

        if (!loan.tenureInMonths.trim()) {
            validationErrors.tenureInMonths = "Loan Tenure is required.";
        }

        if (!loan.maximumAmount.trim()) {
            validationErrors.maximumAmount = "Maximum Amount is required.";
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

            await createLoanUsingAxios(loan);

            setSuccess("Loan created successfully.");
            setError("");

            setLoan({
                loanName: "",
                interestRate: "",
                tenureInMonths: "",
                maximumAmount: ""
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
                    🏦 Loan Management
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
                            Loan Name
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="text"
                                name="loanName"
                                className={`form-control rounded-3 ${errors.loanName ? "is-invalid" : ""}`}
                                placeholder="Enter Loan Name"
                                value={loan.loanName}
                                onChange={handleChange}
                            />

                            {errors.loanName && (
                                <small className="text-danger">
                                    {errors.loanName}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            Interest Rate (%)
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="number"
                                name="interestRate"
                                className={`form-control rounded-3 ${errors.interestRate ? "is-invalid" : ""}`}
                                placeholder="Enter Interest Rate"
                                value={loan.interestRate}
                                onChange={handleChange}
                            />

                            {errors.interestRate && (
                                <small className="text-danger">
                                    {errors.interestRate}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            Tenure (Months)
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="number"
                                name="tenureInMonths"
                                className={`form-control rounded-3 ${errors.tenureInMonths ? "is-invalid" : ""}`}
                                placeholder="Enter Loan Tenure"
                                value={loan.tenureInMonths}
                                onChange={handleChange}
                            />

                            {errors.tenureInMonths && (
                                <small className="text-danger">
                                    {errors.tenureInMonths}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            Maximum Amount
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="number"
                                name="maximumAmount"
                                className={`form-control rounded-3 ${errors.maximumAmount ? "is-invalid" : ""}`}
                                placeholder="Enter Maximum Loan Amount"
                                value={loan.maximumAmount}
                                onChange={handleChange}
                            />

                            {errors.maximumAmount && (
                                <small className="text-danger">
                                    {errors.maximumAmount}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="text-center mt-4">

                        <button
                            type="submit"
                            className="btn btn-success px-5 py-2 rounded-pill fw-bold shadow">

                            🏦 Save Loan

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

}