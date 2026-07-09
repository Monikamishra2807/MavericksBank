import { useState } from "react";
import { createAccountUsingAxios } from "../api/accountAxiosApi";

export function AccountForm() {

    const [account, setAccount] = useState({
        accountType: "",
        balance: ""
    });

    const [errors, setErrors] = useState({});
    const [success, setSuccess] = useState("");
    const [error, setError] = useState("");

    function handleChange(event) {

        setAccount({
            ...account,
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

        if (!account.accountType) {
            validationErrors.accountType = "Please select an Account Type.";
        }

        if (!account.balance.trim()) {
            validationErrors.balance = "Initial Balance is required.";
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

            await createAccountUsingAxios(account);

            setSuccess("Account created successfully.");
            setError("");

            setAccount({
                accountType: "",
                balance: ""
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
                    💳 Open New Account
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
                            Account Type
                        </label>

                        <div className="col-sm-8">

                            <select
                                name="accountType"
                                className={`form-select rounded-3 ${errors.accountType ? "is-invalid" : ""}`}
                                value={account.accountType}
                                onChange={handleChange}
                            >

                                <option value="">
                                    Select Account Type
                                </option>

                                <option value="Savings">
                                    Savings
                                </option>

                                <option value="Current">
                                    Current
                                </option>

                            </select>

                            {errors.accountType && (
                                <small className="text-danger">
                                    {errors.accountType}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">
                            Initial Balance
                        </label>

                        <div className="col-sm-8">

                            <input
                                type="number"
                                name="balance"
                                className={`form-control rounded-3 ${errors.balance ? "is-invalid" : ""}`}
                                placeholder="Enter Initial Balance"
                                value={account.balance}
                                onChange={handleChange}
                            />

                            {errors.balance && (
                                <small className="text-danger">
                                    {errors.balance}
                                </small>
                            )}

                        </div>

                    </div>

                    <div className="text-center mt-4">

                        <button
                            type="submit"
                            className="btn btn-success px-5 py-2 rounded-pill fw-bold shadow">

                            💳 Open Account

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

}