import { useEffect, useState } from "react";
import { getBeneficiariesUsingAxios } from "../api/beneficiaryAxiosApi";

export function BeneficiaryList() {

    const [beneficiaries, setBeneficiaries] = useState([]);

    useEffect(() => {
        loadBeneficiaries();
    }, []);

    async function loadBeneficiaries() {

        try {

            const data = await getBeneficiariesUsingAxios();
            setBeneficiaries(data);

        }
        catch (error) {

            alert(error.message);

        }

    }

    return (

        <div className="card shadow-lg border-0 rounded-4 mt-5">

            <div className="card-header bg-primary text-white rounded-top-4 py-2">

                <h4 className="fw-bold text-center mb-0">
                    👥 Beneficiary List
                </h4>

            </div>

            <div className="card-body p-4">

                <div className="table-responsive">

                    <table className="table table-hover table-striped align-middle">

                        <thead className="table-dark">

                            <tr>

                                <th>Beneficiary ID</th>
                                <th>Customer ID</th>
                                <th>Beneficiary Name</th>
                                <th>Bank Name</th>
                                <th>Account Number</th>
                                <th>IFSC Code</th>

                            </tr>

                        </thead>

                        <tbody>

                            {
                                beneficiaries.length > 0 ?

                                    beneficiaries.map((beneficiary) => (

                                        <tr key={beneficiary.beneficiaryId}>

                                            <td className="fw-bold">
                                                #{beneficiary.beneficiaryId}
                                            </td>

                                            <td>
                                                {beneficiary.customerId}
                                            </td>

                                            <td className="fw-semibold">
                                                {beneficiary.beneficiaryName}
                                            </td>

                                            <td>
                                                {beneficiary.bankName}
                                            </td>

                                            <td>
                                                {beneficiary.accountNumber}
                                            </td>

                                            <td>
                                                <span className="badge bg-secondary">
                                                    {beneficiary.ifscCode}
                                                </span>
                                            </td>

                                        </tr>

                                    ))

                                    :

                                    <tr>

                                        <td
                                            colSpan="6"
                                            className="text-center py-5 text-muted"
                                        >

                                            No Beneficiaries Found

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