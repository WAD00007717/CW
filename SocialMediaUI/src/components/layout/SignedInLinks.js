import React from "react";
import { NavLink, useHistory } from "react-router-dom";
import { useLocalStorage } from "../../hooks/useLocalStorage";

const SignedInLinks = ({ setToken }) => {
  const history = useHistory();
  const [username, setUsername] = useLocalStorage("username");
  const [token] = useLocalStorage("token");
  const [userId, setUserId] = useLocalStorage("user_id");
  const handleLogout = () => {
    setToken(null);
    setUserId(null);
    setUsername(null);
    history.push("/signin");
  };
  return (
    <div>
      <ul className="right">
        <li>
          <NavLink to="/create">New Post</NavLink>
        </li>
        <li>
          <NavLink to="/my-posts">My Posts</NavLink>
        </li>
        <li>
          <a onClick={handleLogout}>Log Out</a>
        </li>
        <li>
          <NavLink to="/" className="btn btn-floating pink lighten-1">
            {username?.slice(0, 2)?.toUpperCase()}
          </NavLink>
        </li>
      </ul>
    </div>
  );
};

export default SignedInLinks;
