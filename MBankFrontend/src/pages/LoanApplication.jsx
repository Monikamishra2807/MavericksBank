import { LoanApplicationForm } from "../components/LoanApplicationForm";
import { LoanApplicationList } from "../components/LoanApplicationList";

export function LoanApplication() {

    const role = localStorage.getItem("role");

    return (
        <div className="container mt-4">

            <h2 className="text-center mb-4">
                Loan Application
            </h2>

            {role === "Customer" && (
                <LoanApplicationForm />
            )}

            {role === "Admin" && (
                <LoanApplicationList />
            )}

        </div>
    );
}