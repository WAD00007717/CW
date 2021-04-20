import request from "../api";

const users = {
  getAll: () => request.get(`Users`),
  getOne: (id) => request.get(`Users/${id}`),
  register: (data) => request.post(`Users/Register`, data),
  login: (data) => request.post(`Users/Login`, data),
};

export default users;
