import React from "react";
import { formatDate } from "../../utils/formatDate";

const PostSummary = ({ post }) => {
  return post ? (
    <div className="card z-depth-0 post-summary">
      <div className="card-content grey-text text-darken-3">
        <span className="card-title ">{post.title}</span>
        <p>Posted by {post.user.username}</p>
        <p className="grey-text">{formatDate(post.createdAt)}</p>
      </div>
    </div>
  ) : (
    <p>Loading...</p>
  );
};

export default PostSummary;
