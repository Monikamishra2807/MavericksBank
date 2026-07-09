import { LoanForm } from "../components/LoanForm";
import { LoanList } from "../components/LoanList";

export function Loan() {

    const role = localStorage.getItem("role");

    return (

        <div className="container mt-4">

            <h2 className="text-center mb-4">
                Loan Management
            </h2>

            {role === "Admin" && (
                <>
                    <LoanForm />
                    <hr />
                </>
            )}

            <LoanList />

        </div>

    );

}