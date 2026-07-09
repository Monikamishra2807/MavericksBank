import { useEffect, useState } from "react";
import { createLoanApplicationUsingAxios } from "../api/loanApplicationAxiosApi";
import { getLoansUsingAxios } from "../api/loanAxiosApi";
import { getMyProfileUsingAxios } from "../api/customerAxiosApi";

export function LoanApplicationForm() {

    const [loanApplication, setLoanApplication] = useState({
        customerId: "",
        loanId: "",
        requestedAmount: ""
    });

    const [customerName, setCustomerName] = useState("");
    const [loans, setLoans] = useState([]);

    const [errors, setErrors] = useState({});
    const [success, setSuccess] = useState("");
    const [error, setError] = useState("");

    useEffect(() => {

        loadPage();

    }, []);

    async function loadPage() {

        try {

            const customer = await getMyProfileUsingAxios();

            if (customer) {

                setCustomerName(customer.fullName);

                setLoanApplication(prev => ({
                    ...prev,
                    customerId: customer.customerId
                }));

            }

            const loanList = await getLoansUsingAxios();

            setLoans(loanList);

        }
        catch (error) {

            setError(error.message);

        }

    }

    function handleChange(event) {

        setLoanApplication({

            ...loanApplication,

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

        if (!loanApplication.loanId) {

            validationErrors.loanId = "Please select a loan.";

        }

        if (!loanApplication.requestedAmount) {

            validationErrors.requestedAmount = "Requested Amount is required.";

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

            await createLoanApplicationUsingAxios(loanApplication);

            setSuccess("Loan Application submitted successfully.");

            setError("");

            setLoanApplication({

                customerId: loanApplication.customerId,
                loanId: "",
                requestedAmount: ""

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

                    📄 Apply for Loan

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

                            Customer

                        </label>

                        <div className="col-sm-8">

                            <input

                                className="form-control rounded-3"

                                value={customerName}

                                readOnly

                            />

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">

                            Loan

                        </label>

                        <div className="col-sm-8">

                            <select

                                name="loanId"

                                className={`form-select rounded-3 ${errors.loanId ? "is-invalid" : ""}`}

                                value={loanApplication.loanId}

                                onChange={handleChange}

                            >

                                <option value="">

                                    Select Loan

                                </option>

                                {loans.map((loan) => (

                                    <option

                                        key={loan.loanId}

                                        value={loan.loanId}

                                    >

                                        {loan.loanName}

                                    </option>

                                ))}

                            </select>

                            {errors.loanId && (

                                <small className="text-danger">

                                    {errors.loanId}

                                </small>

                            )}

                        </div>

                    </div>

                    <div className="row mb-4 align-items-center">

                        <label className="col-sm-4 col-form-label fw-semibold">

                            Requested Amount

                        </label>

                        <div className="col-sm-8">

                            <input

                                type="number"

                                name="requestedAmount"

                                className={`form-control rounded-3 ${errors.requestedAmount ? "is-invalid" : ""}`}

                                placeholder="Enter Requested Amount"

                                value={loanApplication.requestedAmount}

                                onChange={handleChange}

                            />

                            {errors.requestedAmount && (

                                <small className="text-danger">

                                    {errors.requestedAmount}

                                </small>

                            )}

                        </div>

                    </div>

                    <div className="text-center mt-4">

                        <button

                            type="submit"

                            className="btn btn-success px-5 py-2 rounded-pill fw-bold shadow"

                        >

                            📄 Apply for Loan

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

}