import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { Redirect, useHistory } from "react-router";
import { useLocalStorage } from "../../hooks/useLocalStorage";
import { requests } from "../../utils/requests";

function SignUp() {
  const { register, handleSubmit } = useForm();
  const history = useHistory();

  //
  const [isMatch, setIsMatch] = useState(true);
  const [hasError, setHasError] = useState(null);

  const [token, setToken] = useLocalStorage("token");
  const [userId, setUserId] = useLocalStorage("user_id");
  const [username, setUsername] = useLocalStorage("username");

  const onSubmit = async (data) => {
    console.log(`data:>>`, data);
    if (data.password !== data.confirm) {
      setIsMatch(false);
    } else {
      try {
        const {
          data: { token, id, username },
        } = await requests.users.register(data);
        setToken(token);
        setUserId(id);
        setUsername(username);

        history.push("/");
      } catch (error) {
        setHasError(error);
        console.log(`error`, error);
      }
    }
  };
  const onError = (error) => {
    console.log(`error:>>`, error);
  };

  if (token) return <Redirect to="/" />;
  return (
    <div className="container">
      <form className="white" onSubmit={handleSubmit(onSubmit, onError)}>
        <h5 className="grey-text text-darken-3">Sign Up</h5>
        <div className="input-field">
          <label htmlFor="username">Username</label>
          <input
            type="text"
            {...register("username", {
              required: "Username is required",
            })}
            id="username"
          />
        </div>
        <div className="input-field">
          <label htmlFor="password">Password</label>
          <input
            type="password"
            {...register("password", {
              required: "Password is required",
            })}
            id="password"
          />{" "}
        </div>
        <div className="input-field">
          <label htmlFor="password">Confirm password</label>
          <input {...register("confirm")} type="password" id="confirm" />
          {!isMatch && <p style={{ color: "red" }}>Passwords do not match</p>}
        </div>
        <div className="input-field">
          <button className="btn pink lighten-1 z-depth-0">Sign Up</button>
          <div className="center red-text">
            {hasError ? (
              <p style={{ color: "red" }}>{hasError.message}</p>
            ) : null}
          </div>
        </div>
      </form>
    </div>
  );
}

export default SignUp;
