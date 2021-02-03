import React from "react";
import Col from "reactstrap/lib/Col";
import Comment from "./Comment";

const CommentsList = ({ comments = [] }) => {
  if (comments.length == 0) {
    return (
      <Col>
        <span>Brak komentarzy. 😓</span>
      </Col>
    );
  }

  return (
    <>
      {comments.map((comment) => (
        <Col xs="12">
          <Comment />
        </Col>
      ))}
    </>
  );
};

export default CommentsList;
