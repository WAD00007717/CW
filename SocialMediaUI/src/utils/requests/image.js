import request from "../api";

const image = {
  upload: (file) => request.post(`Images/Upload`, file),
};

export default image;
