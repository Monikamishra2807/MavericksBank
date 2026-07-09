import { useEffect, useState } from "react";
import { getTransactionsUsingAxios } from "../api/transactionAxiosApi";

export function TransactionList() {

    const [transactions, setTransactions] = useState([]);

    useEffect(() => {
        loadTransactions();
    }, []);

    async function loadTransactions() {

        try {

            const data = await getTransactionsUsingAxios();
            setTransactions(data);

        }
        catch (error) {

            alert(error.message);

        }

    }

    return (

        <div className="card shadow-lg border-0 rounded-4 mt-5">

            <div className="card-header bg-primary text-white rounded-top-4 py-2">

                <h4 className="fw-bold text-center mb-0">
                    💸 Transaction History
                </h4>

            </div>

            <div className="card-body p-4">

                <div className="table-responsive">

                    <table className="table table-hover table-striped align-middle">

                        <thead className="table-dark">

                            <tr>
                                <th>Transaction ID</th>
                                <th>From Account</th>
                                <th>To Account</th>
                                <th>Amount</th>
                                <th>Type</th>
                                <th>Reference Number</th>
                                <th>Status</th>
                            </tr>

                        </thead>

                        <tbody>

                            {
                                transactions.length > 0 ?

                                    transactions.map((transaction) => (

                                        <tr key={transaction.transactionId}>

                                            <td className="fw-bold">
                                                #{transaction.transactionId}
                                            </td>

                                            <td>
                                                {transaction.fromAccountId}
                                            </td>

                                            <td>
                                                {transaction.toAccountId}
                                            </td>

                                            <td className="fw-bold text-success">
                                                ₹ {transaction.amount.toLocaleString()}
                                            </td>

                                            <td>

                                                <span className="badge bg-info text-dark">
                                                    {transaction.transactionType}
                                                </span>

                                            </td>

                                            <td
                                                style={{
                                                    maxWidth: "180px",
                                                    overflow: "hidden",
                                                    textOverflow: "ellipsis",
                                                    whiteSpace: "nowrap"
                                                }}
                                                title={transaction.referenceNumber}
                                            >
                                                {transaction.referenceNumber}
                                            </td>

                                            <td>

                                                {transaction.status === "Success" ?

                                                    <span className="badge bg-success">
                                                        Success
                                                    </span>

                                                    :

                                                    <span className="badge bg-danger">
                                                        Failed
                                                    </span>

                                                }

                                            </td>

                                        </tr>

                                    ))

                                    :

                                    <tr>

                                        <td
                                            colSpan="7"
                                            className="text-center py-5 text-muted"
                                        >

                                            No Transactions Found

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