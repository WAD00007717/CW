import React, { useEffect, useRef, useState } from "react";
import { useForm } from "react-hook-form";
import { Redirect, useHistory, useParams } from "react-router-dom";
import { useLocalStorage } from "../../hooks/useLocalStorage";
import { requests } from "../../utils/requests";

function CreatePost() {
  const { id } = useParams();
  const history = useHistory();
  const [userId] = useLocalStorage("user_id");
  if (!userId) {
    return <Redirect to="/signin" />;
  }
  const [imageUrl, setImageUrl] = useState("");

  const { register, handleSubmit } = useForm();

  const onSubmit = async (data) => {
    console.log(`data:>>`, data);
    if (id) {
      try {
        data.image = imageUrl;
        data.userId = Number(userId);
        const response = await requests.posts.update(id, data);
        history.push("/");
      } catch (error) {
        console.error("error", error);
      }
    } else {
      try {
        data.image = imageUrl;
        data.userId = Number(userId);
        const response = await requests.posts.post(data);
        history.push("/");
      } catch (error) {
        console.error("error", error);
      }
    }
  };
  const onError = (err) => console.log(`err`, err);

  const handleUpload = async (evt) => {
    try {
      const file = evt.target.files[0];
      const fileName = evt.target.files[0].name;
      if (file.type && !file.type.startsWith("image/")) {
        console.log("File is not an image.", file.type, file);
        return;
      }

      const formData = new FormData();
      formData.append("file", file);
      formData.append("fileName", fileName);

      const response = await requests.image.upload(formData);
      setImageUrl(response.data.imageUrl);
    } catch (error) {
      console.error("error", error);
    }
  };

  //  Edit logic
  const [editData, setEditData] = useState(null);

  useEffect(() => {
    if (id) {
      requests.posts
        .getOne(id)
        .then((res) => {
          console.log(`res`, res);
          setEditData(res.data);
          setImageUrl(res.data.image);
        })
        .catch((err) => console.error("error", err));
    } else setEditData({});
  }, [id]);

  const pageTitle = id ? `Update your post with id ${id}` : "Create a New Post";
  return (
    <div className="container">
      <form className="white" onSubmit={handleSubmit(onSubmit, onError)}>
        <h5 className="grey-text text-darken-3">{pageTitle}</h5>
        <div className="input-field">
          <input
            type="text"
            id="title"
            defaultValue={editData?.title || ""}
            {...register("title", {
              required: "Title is required",
            })}
          />
          <label htmlFor="title">Post Title</label>
        </div>
        <div className="input-field">
          <textarea
            {...register("description")}
            defaultValue={editData?.description || ""}
            id="description"
            className="materialize-textarea"
          ></textarea>
          <label htmlFor="content">Post Content</label>
        </div>
        <div className="file-field input-field">
          <div className="btn">
            <span>Upload Image</span>
            <input
              type="file"
              onChange={handleUpload}
              accept=".jpg, .jpeg, .png"
            />
          </div>
          <div className="file-path-wrapper">
            <input className="file-path validate" type="text" />
          </div>
        </div>
        <img src={imageUrl} className="post-image" />
        <div className="input-field">
          <button className="btn pink lighten-1">
            {id ? "Update" : "Create"}
          </button>
        </div>
      </form>
    </div>
  );
}

export default CreatePost;
