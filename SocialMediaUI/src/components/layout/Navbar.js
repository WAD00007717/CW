import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useLocalStorage } from "../../hooks/useLocalStorage";
import SignedInLinks from "./SignedInLinks";
import SignedOutLinks from "./SignedOutLinks";

const Navbar = () => {
  const [token, setToken] = useLocalStorage("token");

  const links = token ? (
    <SignedInLinks setToken={setToken} />
  ) : (
    <SignedOutLinks token={token} setToken={setToken} />
  );
  return (
    <nav className="nav-wrapper grey darken-3">
      <div className="container">
        <Link to="/" className="brand-logo">
          WebApp
        </Link>
        {links}
      </div>
    </nav>
  );
};

export default Navbar;
