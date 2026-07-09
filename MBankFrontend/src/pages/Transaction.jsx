import { TransactionForm } from "../components/TransactionForm";
import { TransactionList } from "../components/TransactionList";

export function Transaction() {

    const role = localStorage.getItem("role");

    return (
        <div className="container mt-4">

            <h2 className="text-center mb-4">
                Transaction
            </h2>

            {role === "Customer" && (
                <>
                    <hr />
                    <TransactionForm />
                </>
            )}

            {role === "Admin" && (
                <>
                    <hr />
                    <TransactionList />
                </>
            )}

        </div>
    );
}