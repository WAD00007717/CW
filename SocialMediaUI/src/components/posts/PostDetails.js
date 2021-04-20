import React, { useEffect, useRef, useState } from "react";
import { Redirect, useParams } from "react-router";
import { useLocalStorage } from "../../hooks/useLocalStorage";
import { formatDate } from "../../utils/formatDate";
import { requests } from "../../utils/requests";
import CommentList from "../comment/CommentList";

const PostDetails = () => {
  const [comments, setComments] = useState([]);
  const [comment, setComment] = useState("");
  const [editingCommentId, setEditingCommentId] = useState(0);
  const [commentsCount, setCommentsCount] = useState(0);
  const [editing, setEditing] = useState(false);

  const handleDelete = async (id) => {
    if (confirm("Do you really want to delete your comment?")) {
      try {
        await requests.comments.delete(id);
        const res = await requests.comments.getAll(`postId=${id}`);
        setComments(res.data.comments);
      } catch (error) {
        console.error("error", error);
      }
    }
  };

  const [token] = useLocalStorage("token");
  const [userId] = useLocalStorage("user_id");
  if (!token) {
    return <Redirect to="/signin" />;
  }
  const { id } = useParams();

  const commentRef = useRef(null);

  const [hasError, setHasError] = useState(null);
  const [post, setPost] = useState(null);
  useEffect(() => {
    requests.posts
      .getOne(id)
      .then((res) => {
        setPost(res.data);
        setCommentsCount(res.data.comments.length);
        requests.comments
          .getAll(`postId=${id}`)
          .then((res) => {
            setComments(res.data.comments);
          })
          .catch((error) => console.error("error", error));
      })
      .catch((err) => {
        setHasError(err);
        console.log(`err`, err);
      });
  }, [id, commentsCount]);

  // comment
  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!editing) {
      try {
        const data = {
          postId: Number(id),
          userId: Number(userId),
          content: comment,
        };
        const res = await requests.comments.post(data);
        setComment("");
        setCommentsCount((c) => c + 1);
      } catch (error) {
        console.error("error", error);
      }
    } else {
      try {
        const data = {
          content: comment,
        };
        const res = await requests.comments.update(editingCommentId, data);
        setComment("");
        setCommentsCount((c) => c + 1);
        setEditing(false);
      } catch (error) {
        console.error("error", error);
      }
    }
  };
  return post ? (
    <>
      <div className="container section post-details">
        <div className="card z-depth-0">
          <div className="card-content">
            <span className="card-title">{post.title}</span>
            <img className="post-image" src={post.image} />
            <p>{post.description}</p>
          </div>
          <div className="card-action grey lighten-4 grey-text">
            <div>Posted by {post.user.username}</div>
            <div>{formatDate(post.createdAt)}</div>
          </div>
        </div>
      </div>
      <div className="container section">
        <form className="grey lighten-4 comment-form" onSubmit={handleSubmit}>
          <div className="input-field">
            <label htmlFor="comment">Comment here...</label>
            <input
              ref={commentRef}
              required
              value={comment}
              onChange={(e) => setComment(e.target.value)}
              type="text"
              id="comment"
            />
          </div>
          <div className="input-field">
            <button className="btn pink lighten-1 z-depth-0">
              {editing ? "Update" : "Comment"}
            </button>
          </div>
        </form>
        {comments.length ? (
          <CommentList
            handleDelete={handleDelete}
            setEditingCommentId={setEditingCommentId}
            comments={comments}
            setComment={setComment}
            commentRef={commentRef}
            setEditing={setEditing}
          />
        ) : (
          ""
        )}
      </div>
    </>
  ) : hasError ? (
    <div className="container center">
      <p className="red white-text">Post not found</p>
    </div>
  ) : (
    <div className="container center">
      <p>Loading post...</p>
    </div>
  );
};

export default PostDetails;
