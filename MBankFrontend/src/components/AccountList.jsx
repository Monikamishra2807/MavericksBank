import { useEffect, useState } from "react";
import { getAccountsUsingAxios } from "../api/accountAxiosApi";

export function AccountList() {

    const [accounts, setAccounts] = useState([]);

    useEffect(() => {
        loadAccounts();
    }, []);

    async function loadAccounts() {

        try {

            const data = await getAccountsUsingAxios();
            setAccounts(data);

        }
        catch (error) {

            alert(error.message);

        }

    }

    return (

        <div className="card shadow-lg border-0 rounded-4 mt-5">

            <div className="card-header bg-primary text-white rounded-top-4 py-3">

                <h4 className="fw-bold text-center mb-0">
                    💳 Account List
                </h4>

            </div>

            <div className="card-body">

                <div className="table-responsive">

                    <table className="table table-hover align-middle">

                        <thead className="table-primary">

                            <tr>
                                <th>Account ID</th>
                                <th>Customer ID</th>
                                <th>Account Type</th>
                                <th>Balance</th>
                                <th>Status</th>
                            </tr>

                        </thead>

                        <tbody>

                            {
                                accounts.length > 0 ?

                                    accounts.map((account) => (

                                        <tr key={account.accountId}>

                                            <td>{account.accountId}</td>

                                            <td>{account.customerId}</td>

                                            <td>

                                                <span className="badge bg-info text-dark">
                                                    {account.accountType}
                                                </span>

                                            </td>

                                            <td className="fw-bold text-success">

                                                ₹ {account.balance.toLocaleString()}

                                            </td>

                                            <td>

                                                {account.isActive ?

                                                    <span className="badge bg-success">
                                                        Active
                                                    </span>

                                                    :

                                                    <span className="badge bg-danger">
                                                        Inactive
                                                    </span>

                                                }

                                            </td>

                                        </tr>

                                    ))

                                    :

                                    <tr>

                                        <td colSpan="5" className="text-center text-muted py-4">

                                            No Records Found

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