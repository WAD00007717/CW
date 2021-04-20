import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { Redirect, useHistory } from "react-router-dom";
import { useLocalStorage } from "../../hooks/useLocalStorage";
import { requests } from "../../utils/requests";

function SignIn() {
  const history = useHistory();

  const [hasError, setHasError] = useState(null);

  const [token, setToken] = useLocalStorage("token");
  const [userId, setUserId] = useLocalStorage("user_id");
  const [username, setUsername] = useLocalStorage("username");

  const { register, handleSubmit } = useForm();
  const onSubmit = async (data) => {
    setHasError(null);
    try {
      const {
        data: { token, id, username },
      } = await requests.users.login(data);
      setToken(token);
      setUserId(id);
      setUsername(username);
      history.push("/");
      window.location.reload(false);
    } catch (error) {
      console.error("error", error);
      setHasError(error);
    }
  };
  const onError = (error) => {
    console.error("error", error);
  };
  if (token) return <Redirect to="/" />;

  return (
    <div className="container">
      <form className="white" onSubmit={handleSubmit(onSubmit, onError)}>
        <h5 className="grey-text text-darken-3">Sign In</h5>
        <div className="input-field">
          <label htmlFor="username">Username</label>
          <input
            {...register("username", {
              required: "Username is required",
            })}
            type="text"
            id="username"
          />
        </div>
        <div className="input-field">
          <label htmlFor="password">Password</label>
          <input
            {...register("password", {
              required: "Password is required",
            })}
            type="password"
            id="password"
          />
        </div>
        <div className="input-field">
          <button className="btn pink lighten-1 z-depth-0">Login</button>
          <div className="center red-text">
            {hasError ? <p>{hasError.message}</p> : null}
          </div>
        </div>
      </form>
    </div>
  );
}

export default SignIn;
