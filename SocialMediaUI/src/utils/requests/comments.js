import request from "../api";

const comments = {
  getAll: (filter) => request.get(`Comments?${filter || ""}`),
  getOne: (id) => request.get(`Comments/${id}`),
  post: (data) => request.post(`Comments`, data),
  update: (id, data) => request.put(`Comments/${id}`, data),
  delete: (id) => request.delete(`Comments/${id}`),
};

export default comments;
