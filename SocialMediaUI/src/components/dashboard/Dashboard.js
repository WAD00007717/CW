import React, { useEffect, useState } from "react";
import { Redirect } from "react-router";
import { requests } from "../../utils/requests";
import PostList from "../posts/PostList";
import ReactPaginate from "react-paginate";
import { useLocalStorage } from "../../hooks/useLocalStorage";

function Dashboard() {
  const [token] = useLocalStorage("token");
  if (!token) {
    return <Redirect to="/signin" />;
  }
  const [posts, setPosts] = useState([]);
  const [pageCount, setPageCount] = useState(1);
  const [pageNumber, setPageNumber] = useState(1);

  const handlePageClick = (e) => {
    setPageNumber(e.selected + 1);
  };
  useEffect(() => {
    requests.posts
      .getAll(`pageNumber=${pageNumber}`)
      .then((res) => {
        setPosts(res.data.posts);
        setPageCount(Math.ceil(res.data.count / 3));
      })
      .catch((err) => console.log(`err`, err));
  }, [pageNumber]);
  return posts ? (
    <div className="dashboard container">
      <div className="row">
        <div className="col s12 m7">
          <PostList posts={posts} setPosts={setPosts} />
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
  ) : (
    <p>Loading...</p>
  );
}

export default Dashboard;
