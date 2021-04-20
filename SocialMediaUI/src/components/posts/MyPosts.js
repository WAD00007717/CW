import React, { Fragment, useEffect, useState } from "react";
import PostSummary from "./PostSummary";
import { Link, Redirect, useHistory } from "react-router-dom";
import { requests } from "../../utils/requests";
import ReactPaginate from "react-paginate";
import { useLocalStorage } from "../../hooks/useLocalStorage";

const MyPosts = ({}) => {
  const history = useHistory();

  const [posts, setPosts] = useState([]);

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

  useEffect(() => {
    requests.posts
      .getAll(`userId=${authenticatedUserId}`)
      .then((res) => {
        setPosts(res.data.posts);
      })
      .catch((err) => console.error("error", err));
  }, []);

  // pagination
  const [pageCount, setPageCount] = useState(1);
  const [pageNumber, setPageNumber] = useState(1);

  const handlePageClick = (e) => {
    setPageNumber(e.selected + 1);
  };
  useEffect(() => {
    requests.posts
      .getAll(`pageNumber=${pageNumber}&userId=${authenticatedUserId}`)
      .then((res) => {
        setPosts(res.data.posts);
        setPageCount(Math.ceil(res.data.count / 3));
      })
      .catch((err) => console.log(`err`, err));
  }, [pageNumber]);
  return (
    <div className="dashboard container">
      <div className="row">
        <div className="col s12 m7">
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
        </div>
        <div className="col s12 m4 offset-m1">
          <ReactPaginate
            previousLabel={"previous"}
            nextLabel={"next"}
            breakLabel={"..."}
            breakClassName={"break-me"}
            pageCount={pageCount}
            marginPagesDisplayed={2}
            pageRangeDisplayed={5}
            onPageChange={handlePageClick}
            containerClassName={"pagination"}
            activeClassName={"active"}
          />{" "}
        </div>
      </div>
    </div>
  );
};

export default MyPosts;
