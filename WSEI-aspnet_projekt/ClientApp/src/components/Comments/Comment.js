import React from "react";
import { Col, Row } from "reactstrap";
import styled from "styled-components";

const CommentContent = styled(Row)`
  margin: 1.2rem -15px;
`;

const Username = styled.i`
  font-size: 0.9rem;
`;

const CreatedAt = styled.span`
  font-weight: 700;
  color: #bababa;
  font-size: 0.7rem;
  margin-left: 0.5rem;
`;

const CommentText = styled.div`
  margin-top: 0.7rem;
`;

const Comment = ({}) => {
  return (
    <CommentContent>
      <Col xs="12">
        <Username>Sample username</Username>
        <CreatedAt>12.12.2012</CreatedAt>
      </Col>
      <Col xs="12">
        <CommentText>To jest testowy komentarz 🤣</CommentText>
      </Col>
    </CommentContent>
  );
};

export default Comment;
