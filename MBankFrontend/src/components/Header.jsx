import { Link, useNavigate } from "react-router-dom";
import { useContext, useEffect } from "react";
import { AuthContext } from "../context/AuthContext";

export function Header() {

    const navigate = useNavigate();

    const {
        isAuthenticated,
        role,
        fullName,
        logout
    } = useContext(AuthContext);

    useEffect(() => {
        document.body.className = "light";
    }, []);

    function handleLogout() {

        logout();

        navigate("/login");

    }

    return (

        <nav className="navbar navbar-expand-lg navbar-dark bg-primary shadow">

            <div className="container-fluid px-5">

                <Link
                    className="navbar-brand fw-bold fs-4"
                    to="/"
                >
                    🏦 Mavericks Bank
                </Link>

                <button
                    className="navbar-toggler"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#navbarNav"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>

                <div
                    className="collapse navbar-collapse"
                    id="navbarNav"
                >

                    <ul className="navbar-nav ms-auto align-items-center">

                        <li className="nav-item">
                            <Link className="nav-link" to="/">
                                Home
                            </Link>
                        </li>

                        {!isAuthenticated && (

                            <>

                                <li className="nav-item">
                                    <Link className="nav-link" to="/login">
                                        Login
                                    </Link>
                                </li>

                                <li className="nav-item">
                                    <Link className="nav-link" to="/register">
                                        Register
                                    </Link>
                                </li>

                            </>

                        )}

                        {isAuthenticated && (

                            <>

                                <li className="nav-item">
                                    <Link
                                        className="nav-link"
                                        to="/dashboard"
                                    >
                                        Dashboard
                                    </Link>
                                </li>

                                <li className="nav-item mx-3">

                                   <div className="d-flex align-items-center me-3">

                                     <div className="profile-circle">
 
                                       {fullName ? fullName.charAt(0).toUpperCase() : "U"}
   
                                      </div>

                                    <div className="ms-2">

                                     <div className="profile-name">

                                         {fullName}

                                      </div>

                                     <small className="text-light">

                                        {role}

                                     </small>

                                    </div>

                                </div>

                                </li>

                                <li className="nav-item">

                                    <button
                                        className="btn btn-danger btn-sm"
                                        onClick={handleLogout}
                                    >
                                        🚪 Logout
                                    </button>

                                </li>

                            </>

                        )}

                    </ul>

                </div>

            </div>

        </nav>

    );

}