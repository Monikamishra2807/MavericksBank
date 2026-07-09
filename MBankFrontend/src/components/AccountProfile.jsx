export function AccountProfile({ account }) {

    return (

        <div className="card shadow-lg border-0 rounded-4">

            <div className="card-header bg-primary text-white text-center py-3 rounded-top-4">

                <h3 className="fw-bold mb-0">
                    🏦 My Account
                </h3>

            </div>

            <div className="card-body p-4">

                <table className="table">

                    <tbody>

                        <tr>
                            <th>Account ID</th>
                            <td>{account.accountId}</td>
                        </tr>

                        <tr>
                            <th>Account Number</th>
                            <td>{account.accountNumber}</td>
                        </tr>

                        <tr>
                            <th>Account Type</th>
                            <td>{account.accountType}</td>
                        </tr>

                        <tr>
                            <th>Branch</th>
                            <td>{account.branchName}</td>
                        </tr>

                        <tr>
                            <th>IFSC Code</th>
                            <td>{account.ifscCode}</td>
                        </tr>

                        <tr>
                            <th>Balance</th>
                            <td>₹ {account.balance.toLocaleString()}</td>
                        </tr>

                        <tr>
                            <th>Status</th>
                            <td>{account.status}</td>
                        </tr>

                    </tbody>

                </table>

            </div>

        </div>

    );

}