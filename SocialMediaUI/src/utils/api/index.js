import axios from "axios";

const request = axios.create({
  baseURL: process.env.REACT_APP_BASE_URL,
  timeout: 6000,
  // headers: { Authorization: "Bearer " + token },
  headers: {
    "Content-Type": "application/json",
  },
});

request.interceptors.request.use(
  function (config) {
    // Do something before request is sent
    const token = JSON.parse(localStorage.getItem("token"));
    config.headers.Authorization = token ? `Bearer ${token}` : "";

    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

// request.defaults.headers.common["Content-Type"] = "application/json";
export default request;
