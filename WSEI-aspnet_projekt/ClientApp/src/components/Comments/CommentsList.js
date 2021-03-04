import React from "react";
import Col from "reactstrap/lib/Col";
import Emoji from "../common/Emoji";
import Comment from "./Comment";

const CommentsList = ({ comments = [] }) => {
  if (comments.length === 0) {
    return (
      <Col>
        <span>
          Brak komentarzy.
          <Emoji symbol="😓" label="Downcast face with sweat emoji" />
        </span>
      </Col>
    );
  }

  return (
    <>
      {comments.map((comment) => (
        <Col key={comment.createdOn} xs="12">
          <Comment comment={comment} />
        </Col>
      ))}
    </>
  );
};

export default CommentsList;
