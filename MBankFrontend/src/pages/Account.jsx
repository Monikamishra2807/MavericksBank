import { useEffect, useState } from "react";

import { AccountForm } from "../components/AccountForm";
import { AccountList } from "../components/AccountList";
import { AccountProfile } from "../components/AccountProfile";

import { getMyAccountUsingAxios } from "../api/accountAxiosApi";

export function Account() {

    const role = localStorage.getItem("role");

    const [account, setAccount] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        if (role === "Customer") {

            loadAccount();

        }
        else {

            setLoading(false);

        }

    }, []);

    async function loadAccount() {

        try {

            const data = await getMyAccountUsingAxios();

            setAccount(data);

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


            {role === "Admin" && (
                <>
                    <hr />
                    <AccountList />
                </>
            )}

            {role === "Customer" && (
                account
                    ? <AccountProfile account={account} />
                    : <AccountForm />
            )}

        </div>

    );

}