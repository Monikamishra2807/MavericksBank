import { useEffect, useState } from "react";

import { CustomerForm } from "../components/CustomerForm";
import { CustomerList } from "../components/CustomerList";
import { CustomerProfile } from "../components/CustomerProfile";

import { getMyProfileUsingAxios } from "../api/customerAxiosApi";

export function Customer() {

    const role = localStorage.getItem("role");

    const [profile, setProfile] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        if (role === "Customer") {

            loadProfile();

        }
        else {

            setLoading(false);

        }

    }, []);

    async function loadProfile() {

        try {

            const data = await getMyProfileUsingAxios();

            setProfile(data);

        }

        catch (error) {

            alert(error.message);

        }

        finally {

            setLoading(false);

        }

    }

    if (loading) {

        return <h4 className="text-center mt-5">Loading...</h4>;

    }

    return (

        <div className="container mt-4">

            <h2 className="text-center mb-4">
                Customer Details
            </h2>

            {role === "Admin" && (
                <>
                    <CustomerForm />
                    <hr />
                    <CustomerList />
                </>
            )}

            {role === "Customer" && (
                profile
                    ? <CustomerProfile customer={profile} />
                    : <CustomerForm />
            )}

        </div>

    );

}