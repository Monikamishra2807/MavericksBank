import "./App.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import { Header } from "./components/Header";
import { ProtectedRoute } from "./layouts/ProtectedRoute";

import { Home } from "./pages/Home";
import { Login } from "./pages/Login";
import { Register } from "./pages/Register";
import { NotFound } from "./pages/NotFound";

import { Dashboard } from "./components/Dashboard";

import { Customer } from "./pages/Customer";
import { Account } from "./pages/Account";
import { Beneficiary } from "./pages/Beneficiary";
import { Transaction } from "./pages/Transaction";
import { Loan } from "./pages/Loan";
import { LoanApplication } from "./pages/LoanApplication";

function App() {

    return (

        <BrowserRouter>

            <Header />

            <Routes>

                <Route path="/" element={<Home />} />

                <Route path="/login" element={<Login />} />

                <Route path="/register" element={<Register />} />

                <Route
                    path="/dashboard"
                    element={
                        <ProtectedRoute>
                            <Dashboard />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/customer"
                    element={
                        <ProtectedRoute>
                            <Customer />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/account"
                    element={
                        <ProtectedRoute>
                            <Account />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/beneficiary"
                    element={
                        <ProtectedRoute>
                            <Beneficiary />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/transaction"
                    element={
                        <ProtectedRoute>
                            <Transaction />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/loan"
                    element={
                        <ProtectedRoute>
                            <Loan />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/loanapplication"
                    element={
                        <ProtectedRoute>
                            <LoanApplication />
                        </ProtectedRoute>
                    }
                />
                 <Route
                    path="*"
                    element={<NotFound />}
                />


            </Routes>

        </BrowserRouter>

    );

}

export default App;