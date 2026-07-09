import { Link } from "react-router-dom";

export function NotFound() {

    return (

        <div className="container mt-5">

            <div className="row justify-content-center">

                <div className="col-md-8">

                    <div className="card shadow-lg border-0 rounded-4">

                        <div className="card-body text-center p-5">

                            <h1
                                className="display-1 fw-bold text-primary">
                                404
                            </h1>

                            <h3 className="fw-bold mt-3">
                                Oops! Page Not Found
                            </h3>

                            <p className="text-muted mt-3">
                                The page you are looking for doesn't exist or has been moved.
                            </p>

                            <Link
                                to="/"
                                className="btn btn-primary rounded-pill px-4 mt-3">

                                🏠 Back to Home

                            </Link>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    );

}