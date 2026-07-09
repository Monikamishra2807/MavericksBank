import { BeneficiaryForm } from "../components/BeneficiaryForm";
import { BeneficiaryList } from "../components/BeneficiaryList";

export function Beneficiary() {
    const role = localStorage.getItem("role");
    return (
        
        <div className="container mt-4">

            <h2 className="text-center mb-4">
                Beneficiary Management
            </h2>

            {role === "Customer" && (
             <>
                  <hr />
                 <BeneficiaryForm />
             </>
           )}
            {role === "Admin" && (
             <>
                  <hr />
                 <BeneficiaryList />
             </>
           )}

            
        </div>
    );
}