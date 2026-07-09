import { Link } from "react-router-dom";

export function Dashboard() {

    const role = localStorage.getItem("role");
    const fullName = localStorage.getItem("fullName");

    return (

        <div className="container py-5">

            {/* Welcome Banner */}

            <div className="dashboard-banner mb-5">

                <h2>

                    Welcome back,
                    <span className="text-warning"> {fullName}</span> 👋

                </h2>

                <p className="mt-2">

                    Manage all your banking services securely from one place.

                </p>

            </div>

            {/* Summary */}

            {role === "Customer" && (

                <div className="row g-4 mb-5">

                    <SummaryCard
                        emoji="💳"
                        title="Account"
                        value="Active"
                    />

                    <SummaryCard
                        emoji="💸"
                        title="Transfers"
                        value="Secure"
                    />

                    <SummaryCard
                        emoji="🏦"
                        title="Loans"
                        value="Available"
                    />

                    <SummaryCard
                        emoji="🔒"
                        title="Security"
                        value="Protected"
                    />

                </div>

            )}

            <div className="row g-4">

                {role === "Customer" && (

                    <>

                        <DashboardCard
                            title="Customer Profile"
                            icon="👤"
                            text="View and manage your profile"
                            color="primary"
                            link="/customer"
                        />

                        <DashboardCard
                            title="My Account"
                            icon="💳"
                            text="Check account details & balance"
                            color="success"
                            link="/account"
                        />

                        <DashboardCard
                            title="Beneficiaries"
                            icon="👥"
                            text="Manage saved beneficiaries"
                            color="warning"
                            link="/beneficiary"
                        />

                        <DashboardCard
                            title="Transfer Money"
                            icon="💸"
                            text="Transfer money securely"
                            color="danger"
                            link="/transaction"
                        />

                        <DashboardCard
                            title="Loans"
                            icon="🏦"
                            text="View available loan schemes"
                            color="info"
                            link="/loan"
                        />

                        <DashboardCard
                            title="Loan Application"
                            icon="📄"
                            text="Apply & Track Loan"
                            color="secondary"
                            link="/loanapplication"
                        />

                    </>

                )}

                {role === "Admin" && (

                    <>

                        <DashboardCard
                            title="Customers"
                            icon="👤"
                            text="Manage customers"
                            color="primary"
                            link="/customer"
                        />

                        <DashboardCard
                            title="Accounts"
                            icon="💳"
                            text="Manage accounts"
                            color="success"
                            link="/account"
                        />

                        <DashboardCard
                            title="Beneficiaries"
                            icon="👥"
                            text="Manage beneficiaries"
                            color="warning"
                            link="/beneficiary"
                        />

                        <DashboardCard
                            title="Transactions"
                            icon="💸"
                            text="View transactions"
                            color="danger"
                            link="/transaction"
                        />

                        <DashboardCard
                            title="Loans"
                            icon="🏦"
                            text="Manage loans"
                            color="info"
                            link="/loan"
                        />

                        <DashboardCard
                            title="Loan Applications"
                            icon="📄"
                            text="Approve / Reject Applications"
                            color="secondary"
                            link="/loanapplication"
                        />

                    </>

                )}

            </div>

        </div>

    );

}

function SummaryCard({ emoji, title, value }) {

    return (

        <div className="col-lg-3 col-md-6">

            <div className="summary-card">

                <div className="summary-emoji">

                    {emoji}

                </div>

                <h6>{title}</h6>

                <h4>{value}</h4>

            </div>

        </div>

    );

}

function DashboardCard({ title, icon, text, color, link }) {

    return (

        <div className="col-lg-4 col-md-6">

            <div className="dashboard-card h-100">

                <div className="dashboard-icon">

                    {icon}

                </div>

                <h4 className={`text-${color}`}>

                    {title}

                </h4>

                <p>{text}</p>

                <Link
                    to={link}
                    className={`btn btn-${color} w-100 mt-3`}
                >
                    Open
                </Link>

            </div>

        </div>

    );

}