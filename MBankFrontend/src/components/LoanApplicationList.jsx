import { useEffect, useState } from "react";
import {
    getLoanApplicationsUsingAxios,
    updateLoanApplicationUsingAxios
} from "../api/loanApplicationAxiosApi";

export function LoanApplicationList() {

    const [applications, setApplications] = useState([]);

    useEffect(() => {
        loadLoanApplications();
    }, []);

    async function loadLoanApplications() {

        try {

            const data = await getLoanApplicationsUsingAxios();
            setApplications(data);

        }
        catch (error) {

            alert(error.message);

        }

    }

    async function approveLoan(application) {

        try {

            const updatedApplication = {
                ...application,
                status: "Approved"
            };

            await updateLoanApplicationUsingAxios(
                application.loanApplicationId,
                updatedApplication
            );

            alert("✅ Loan Approved Successfully");

            loadLoanApplications();

        }
        catch (error) {

            alert(error.message);

        }

    }

    async function rejectLoan(application) {

        try {

            const updatedApplication = {
                ...application,
                status: "Rejected"
            };

            await updateLoanApplicationUsingAxios(
                application.loanApplicationId,
                updatedApplication
            );

            alert("❌ Loan Rejected Successfully");

            loadLoanApplications();

        }
        catch (error) {

            alert(error.message);

        }

    }

    return (

        <div className="card shadow-lg border-0 rounded-4 mt-5">

            <div className="card-header bg-primary text-white rounded-top-4 py-2">

                <h4 className="fw-bold text-center mb-0">
                    📄 Loan Applications
                </h4>

            </div>

            <div className="card-body p-4">

                <div className="table-responsive">

                    <table className="table table-hover table-striped align-middle">

                        <thead className="table-dark">

                            <tr>

                                <th>Application ID</th>
                                <th>Customer ID</th>
                                <th>Loan ID</th>
                                <th>Requested Amount</th>
                                <th>Status</th>
                                <th>Action</th>

                            </tr>

                        </thead>

                        <tbody>

                            {

                                applications.length > 0 ?

                                    applications.map((application) => (

                                        <tr key={application.loanApplicationId}>

                                            <td className="fw-bold">
                                                #{application.loanApplicationId}
                                            </td>

                                            <td>
                                                {application.customerId}
                                            </td>

                                            <td>
                                                {application.loanId}
                                            </td>

                                            <td className="fw-bold text-success">
                                                ₹ {application.requestedAmount.toLocaleString()}
                                            </td>

                                            <td>

                                                {application.status === "Pending" && (
                                                    <span className="badge bg-warning text-dark">
                                                        Pending
                                                    </span>
                                                )}

                                                {application.status === "Approved" && (
                                                    <span className="badge bg-success">
                                                        Approved
                                                    </span>
                                                )}

                                                {application.status === "Rejected" && (
                                                    <span className="badge bg-danger">
                                                        Rejected
                                                    </span>
                                                )}

                                            </td>

                                            <td>

                                                {application.status === "Pending" ? (

                                                    <>

                                                        <button
                                                            className="btn btn-success btn-sm rounded-pill me-2"
                                                            onClick={() => approveLoan(application)}
                                                        >
                                                            ✔ Approve
                                                        </button>

                                                        <button
                                                            className="btn btn-danger btn-sm rounded-pill"
                                                            onClick={() => rejectLoan(application)}
                                                        >
                                                            ✖ Reject
                                                        </button>

                                                    </>

                                                ) : (

                                                    <span className="text-muted">
                                                        Completed
                                                    </span>

                                                )}

                                            </td>

                                        </tr>

                                    ))

                                    :

                                    <tr>

                                        <td
                                            colSpan="6"
                                            className="text-center py-5 text-muted"
                                        >

                                            No Loan Applications Found

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