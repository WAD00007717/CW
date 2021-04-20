import React, { Fragment } from "react";
import { useLocalStorage } from "../../hooks/useLocalStorage";
import { formatDate } from "../../utils/formatDate";

const CommentList = ({
  handleDelete,
  comments,
  setComment,
  commentRef,
  setEditing,
  setEditingCommentId,
}) => {
  const [userId] = useLocalStorage("user_id");

  const handleEdit = (content, id) => {
    setComment(content);
    commentRef.current.focus();
    setEditing(true);
    setEditingCommentId(id);
  };

  return (
    <div className="section">
      {comments &&
        comments.map((comment) => {
          const isSameUser = Number(userId) === comment.user.id;

          return (
            <Fragment key={comment.id}>
              <div className="card z-depth-0 post-summary">
                <div className="card-content grey-text text-darken-3">
                  <span className="card-title ">{comment.content}</span>
                  <p>Commented by {comment.user.username}</p>
                  <p className="grey-text">{formatDate(comment.createdAt)}</p>
                  {isSameUser ? (
                    <>
                      <button
                        style={{ margin: "0 8px 8px 0" }}
                        className="waves-effect green btn-small"
                        onClick={() => handleEdit(comment.content, comment.id)}
                      >
                        Edit
                      </button>
                      <button
                        style={{ margin: "0 8px 8px 0" }}
                        className="waves-effect red btn-small"
                        onClick={() => handleDelete(comment.id)}
                      >
                        Remove
                      </button>
                    </>
                  ) : (
                    ""
                  )}
                </div>
              </div>
            </Fragment>
          );
        })}
    </div>
  );
};

export default CommentList;
