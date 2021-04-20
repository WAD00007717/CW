import request from "../api";

const posts = {
  getAll: (filter) => request.get(`Posts?${filter || ""}`),
  getOne: (id) => request.get(`Posts/${id}`),
  post: (data) => request.post(`Posts`, data),
  update: (id, data) => request.put(`Posts/${id}`, data),
  delete: (id) => request.delete(`Posts/${id}`),
};

export default posts;
