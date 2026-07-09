import { useState } from "react";
import { createTransactionUsingAxios } from "../api/transactionAxiosApi";
import { getAccountByNumberUsingAxios } from "../api/accountAxiosApi";

export function TransactionForm() {

    const [transaction, setTransaction] = useState({
        toAccountId: "",
        accountNumber: "",
        amount: "",
        transactionType: ""
    });

    const [receiver, setReceiver] = useState(null);

    const [errors, setErrors] = useState({});
    const [success, setSuccess] = useState("");
    const [error, setError] = useState("");

    function handleChange(event) {

        setTransaction({
            ...transaction,
            [event.target.name]: event.target.value
        });

        setErrors({
            ...errors,
            [event.target.name]: ""
        });

        setSuccess("");
        setError("");

    }

    async function searchAccount() {

        if (!transaction.accountNumber.trim()) {
            return;
        }

        try {

            const account = await getAccountByNumberUsingAxios(transaction.accountNumber);

            if (!account) {

                setReceiver(null);

                setErrors({
                    ...errors,
                    accountNumber: "Account not found."
                });

                return;

            }

            setReceiver(account);

            setErrors({
                ...errors,
                accountNumber: ""
            });

            setTransaction(prev => ({
                ...prev,
                toAccountId: account.accountId
            }));

        }
        catch (error) {

            setReceiver(null);

            setError(error.message);

        }

    }

    function validate() {

        let validationErrors = {};

        if (!receiver) {

            validationErrors.accountNumber =
                "Please search and select a valid account.";

        }

        if (!transaction.amount.trim()) {

            validationErrors.amount = "Amount is required.";

        }
        else if (Number(transaction.amount) <= 0) {

            validationErrors.amount =
                "Amount must be greater than zero.";

        }

        if (!transaction.transactionType) {

            validationErrors.transactionType =
                "Please select a Transaction Type.";

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

            await createTransactionUsingAxios(transaction);

            setSuccess("Transaction completed successfully.");

            setError("");

            setReceiver(null);

            setTransaction({

                accountNumber: "",
                toAccountId: "",
                amount: "",
                transactionType: ""

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

                    💸 Transfer Money

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

                    <div className="row mb-4">

                        <label className="col-sm-4 col-form-label fw-semibold">

                            Receiver Account Number

                        </label>

                        <div className="col-sm-6">

                            <input

                                type="text"

                                name="accountNumber"

                                className={`form-control ${errors.accountNumber ? "is-invalid" : ""}`}

                                value={transaction.accountNumber}

                                onChange={handleChange}

                                placeholder="Enter Account Number"

                            />

                            {errors.accountNumber && (

                                <small className="text-danger">

                                    {errors.accountNumber}

                                </small>

                            )}

                        </div>

                        <div className="col-sm-2">

                            <button

                                type="button"

                                className="btn btn-primary w-100"

                                onClick={searchAccount}

                            >

                                Search

                            </button>

                        </div>

                    </div>

                    {receiver && (

                        <div className="alert alert-info">

                            <h5>Receiver Details</h5>

                            <p><strong>Account:</strong> {receiver.accountNumber}</p>

                            <p><strong>Branch:</strong> {receiver.branchName}</p>

                            <p><strong>IFSC:</strong> {receiver.ifscCode}</p>

                            <p><strong>Type:</strong> {receiver.accountType}</p>

                        </div>

                    )}

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">

                            Amount

                        </label>

                        <div className="col-sm-8">

                            <input

                                type="number"

                                min="1"

                                name="amount"

                                className={`form-control rounded-3 ${errors.amount ? "is-invalid" : ""}`}

                                placeholder="Enter Transfer Amount"

                                value={transaction.amount}

                                onChange={handleChange}

                            />

                            {errors.amount && (

                                <small className="text-danger">

                                    {errors.amount}

                                </small>

                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">

                            Payment Method

                        </label>

                        <div className="col-sm-8">

                            <select

                                name="transactionType"

                                className={`form-select rounded-3 ${errors.transactionType ? "is-invalid" : ""}`}

                                value={transaction.transactionType}

                                onChange={handleChange}

                            >

                                <option value="">

                                    Select Payment Method

                                </option>

                                <option value="NEFT">NEFT</option>

                                <option value="RTGS">RTGS</option>

                                <option value="IMPS">IMPS</option>

                                <option value="UPI">UPI</option>

                            </select>

                            {errors.transactionType && (

                                <small className="text-danger">

                                    {errors.transactionType}

                                </small>

                            )}

                        </div>

                    </div>

                    <div className="text-center mt-4">

                        <button

                            type="submit"

                            className="btn btn-success px-5 py-2 rounded-pill fw-bold shadow"

                        >

                            💸 Transfer Money

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

}