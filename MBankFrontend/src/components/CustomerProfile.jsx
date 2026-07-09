export function CustomerProfile({ customer }) {

    return (

        <div className="card shadow-lg border-0 rounded-4">

            <div className="card-header bg-primary text-white text-center py-3 rounded-top-4">

                <h3 className="fw-bold mb-0">
                    👤 My Profile
                </h3>

            </div>

            <div className="card-body p-4">

                <table className="table">

                    <tbody>
                        <tr>
                           <th>Full Name</th>
                           <td>{customer.fullName}</td>
                        </tr>

                        <tr>
                          <th>Email</th>
                          <td>{customer.email}</td>
                       </tr>

                        <tr>
                          <th>Mobile</th>
                          <td>{customer.mobile}</td>
                        </tr>

                        <tr>
                            <th>Customer ID</th>
                            <td>{customer.customerId}</td>
                        </tr>

                        <tr>
                            <th>User ID</th>
                            <td>{customer.userId}</td>
                        </tr>

                        <tr>
                            <th>Date of Birth</th>
                            <td>{customer.dob?.split("T")[0]}</td>
                        </tr>

                        <tr>
                            <th>Aadhaar Number</th>
                            <td>{customer.aadharNumber}</td>
                        </tr>

                        <tr>
                            <th>PAN Number</th>
                            <td>{customer.panNumber}</td>
                        </tr>

                        <tr>
                            <th>Address</th>
                            <td>{customer.address}</td>
                        </tr>

                    </tbody>

                </table>

            </div>

        </div>

    );

}