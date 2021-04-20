import React, { Fragment } from "react";
import PostSummary from "./PostSummary";
import { Link, Redirect, useHistory } from "react-router-dom";
import { requests } from "../../utils/requests";
import { useLocalStorage } from "../../hooks/useLocalStorage";

const PostList = ({ posts, setPosts }) => {
  const history = useHistory();

  const [authenticatedUserId] = useLocalStorage("user_id");
  if (!authenticatedUserId) {
    return <Redirect to="/signin" />;
  }

  const handleEdit = (postId, userId) => {
    history.push(`/edit/${postId}`);
  };

  const handleRemove = async (postId, userId) => {
    if (confirm("Do you really want to delete your post?"))
      try {
        await requests.posts.delete(postId);
        const {
          data: { posts },
        } = await requests.posts.getAll();
        setPosts(posts);
      } catch (error) {
        console.error("error", error);
      }
    else return;
  };

  const handleComment = (postId) => {
    history.push(`/post/${postId}`);
  };
  return (
    <div className="post-list section">
      {posts &&
        posts.map((post) => {
          const {
            id: postId,
            user: { id: userId },
          } = post;
          const isSameUser = userId === Number(authenticatedUserId);
          return (
            <Fragment key={postId}>
              <Link to={"/post/" + postId}>
                <PostSummary post={post} />
              </Link>
              <button
                style={{ margin: "0 8px 8px 0" }}
                className=" waves-effect waves-light btn-small"
                onClick={() => handleComment(postId)}
              >
                Comment
              </button>
              {isSameUser ? (
                <>
                  <button
                    style={{ margin: "0 8px 8px 0" }}
                    className="waves-effect green btn-small"
                    onClick={() => handleEdit(postId)}
                  >
                    Edit
                  </button>
                  <button
                    style={{ margin: "0 8px 8px 0" }}
                    className="waves-effect red btn-small"
                    onClick={() => handleRemove(postId)}
                  >
                    Remove
                  </button>
                </>
              ) : null}
            </Fragment>
          );
        })}
    </div>
  );
};

export default PostList;
