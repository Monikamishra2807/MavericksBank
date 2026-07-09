import { Link } from "react-router-dom";

export function Home() {

    const isLoggedIn = localStorage.getItem("token");

    return (

        <>

            {/* Hero Section */}

            <section className="hero">

                <div className="container-fluid px-5">

                    <div className="row align-items-center">

                        <div className="col-lg-6">

                            <h1>
                                Smart Banking <br />
                                Made Simple
                            </h1>

                            <p>

                                Experience secure digital banking with
                                instant money transfer, loan services,
                                beneficiary management and much more.

                            </p>

                            {!isLoggedIn &&

                                <div className="mt-4">

                                    <Link
                                        to="/login"
                                        className="btn btn-light btn-lg me-3"
                                    >
                                        Login
                                    </Link>

                                    <Link
                                        to="/register"
                                        className="btn btn-outline-light btn-lg"
                                    >
                                        Register
                                    </Link>

                                </div>

                            }

                        </div>

                        <div className="col-lg-6 text-center">

                            <img
                                src="https://cdn-icons-png.flaticon.com/512/2489/2489756.png"
                                className="hero-image"
                                alt="Bank"
                            />

                        </div>

                    </div>

                </div>

            </section>

            {/* Services */}

            <section className="container my-5">

                <h2 className="section-title">

                    Our Services

                </h2>

                <div className="row g-4">

                    <div className="col-md-3">

                        <div className="service-card">

                            <div className="service-icon">

                                💳

                            </div>

                            <h4>Accounts</h4>

                            <p>

                                Savings & Current Accounts

                            </p>

                        </div>

                    </div>

                    <div className="col-md-3">

                        <div className="service-card">

                            <div className="service-icon">

                                💸

                            </div>

                            <h4>Transfer</h4>

                            <p>

                                Fast & Secure Payments

                            </p>

                        </div>

                    </div>

                    <div className="col-md-3">

                        <div className="service-card">

                            <div className="service-icon">

                                🏦

                            </div>

                            <h4>Loans</h4>

                            <p>

                                Personal • Home • Education

                            </p>

                        </div>

                    </div>

                    <div className="col-md-3">

                        <div className="service-card">

                            <div className="service-icon">

                                🔒

                            </div>

                            <h4>Security</h4>

                            <p>

                                Protected Banking Experience

                            </p>

                        </div>

                    </div>

                </div>

            </section>

            {/* Statistics */}

            <section className="stats-section">

                <div className="container">

                    <div className="row text-center">

                        <div className="col-md-3">

                            <h2>1M+</h2>

                            <p>Customers</p>

                        </div>

                        <div className="col-md-3">

                            <h2>500+</h2>

                            <p>Branches</p>

                        </div>

                        <div className="col-md-3">

                            <h2>99.9%</h2>

                            <p>Secure Transactions</p>

                        </div>

                        <div className="col-md-3">

                            <h2>24 × 7</h2>

                            <p>Support</p>

                        </div>

                    </div>

                </div>

            </section>

        </>

    );

}