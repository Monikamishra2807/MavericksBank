import { useEffect, useState } from "react";
import { getLoansUsingAxios } from "../api/loanAxiosApi";

export function LoanList() {

    const [loans, setLoans] = useState([]);

    useEffect(() => {
        loadLoans();
    }, []);

    async function loadLoans() {

        try {

            const data = await getLoansUsingAxios();
            setLoans(data);

        }
        catch (error) {

            alert(error.message);

        }

    }

    return (

        <div className="card shadow-lg border-0 rounded-4 mt-5">

            <div className="card-header bg-primary text-white rounded-top-4 py-2">

                <h4 className="fw-bold text-center mb-0">
                    🏦 Available Loan Types
                </h4>

            </div>

            <div className="card-body p-4">

                <div className="table-responsive">

                    <table className="table table-hover table-striped align-middle">

                        <thead className="table-dark">

                            <tr>

                                <th>Loan ID</th>
                                <th>Loan Name</th>
                                <th>Interest Rate</th>
                                <th>Tenure</th>
                                <th>Maximum Amount</th>

                            </tr>

                        </thead>

                        <tbody>

                            {
                                loans.length > 0 ?

                                    loans.map((loan) => (

                                        <tr key={loan.loanId}>

                                            <td className="fw-bold">
                                                #{loan.loanId}
                                            </td>

                                            <td className="fw-semibold">
                                                {loan.loanName}
                                            </td>

                                            <td>

                                                <span className="badge bg-warning text-dark">
                                                    {loan.interestRate}%
                                                </span>

                                            </td>

                                            <td>

                                                {loan.tenureInMonths} Months

                                            </td>

                                            <td className="fw-bold text-success">

                                                ₹ {loan.maximumAmount.toLocaleString()}

                                            </td>

                                        </tr>

                                    ))

                                    :

                                    <tr>

                                        <td
                                            colSpan="5"
                                            className="text-center py-5 text-muted"
                                        >

                                            No Loan Types Available

                                        </td>

                                    </tr>

                            }

                        </tbody>

                    </table>

                </div>

            </div>

        </div>

    );

}