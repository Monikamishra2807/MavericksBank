import { useEffect, useState } from "react";
import { getCustomersUsingAxios } from "../api/customerAxiosApi";

export function CustomerList() {

    const [customers, setCustomers] = useState([]);

    useEffect(() => {
        loadCustomers();
    }, []);

    async function loadCustomers() {

        try {

            const data = await getCustomersUsingAxios();
            setCustomers(data);

        }
        catch (error) {

            alert(error.message);

        }

    }

    return (

        <div className="card shadow-lg border-0 rounded-4 mt-5">

            <div className="card-header bg-primary text-white rounded-top-4 py-2">

                <h4 className="fw-bold text-center mb-0">
                    👤 Customer List
                </h4>

            </div>

            <div className="card-body p-4">

                <div className="table-responsive">

                    <table className="table table-hover table-striped align-middle">

                        <thead className="table-dark">

                            <tr>

                                <th>Customer</th>
                                <th>Customer ID</th>
                                <th>User ID</th>
                                <th>Date of Birth</th>
                                <th>Aadhaar Number</th>
                                <th>PAN Number</th>
                                <th>Address</th>

                            </tr>

                        </thead>

                        <tbody>

                            {
                                customers.length > 0 ?

                                    customers.map((customer) => (

                                        <tr key={customer.customerId}>

                                            <td className="fw-bold text-primary">
                                                👤 {customer.fullName}
                                            </td>

                                            <td className="fw-bold">
                                                #{customer.customerId}
                                            </td>

                                            <td>
                                                {customer.userId}
                                            </td>

                                            <td>

                                                {
                                                    new Date(customer.dob).toLocaleDateString(
                                                        "en-GB",
                                                        {
                                                            day: "2-digit",
                                                            month: "short",
                                                            year: "numeric"
                                                        }
                                                    )
                                                }

                                            </td>

                                            <td>
                                                {customer.aadharNumber}
                                            </td>

                                            <td>
                                                {customer.panNumber}
                                            </td>

                                            <td>
                                                {customer.address}
                                            </td>

                                        </tr>

                                    ))

                                    :

                                    <tr>

                                        <td
                                            colSpan="7"
                                            className="text-center py-5 text-muted"
                                        >

                                            No Customers Found

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