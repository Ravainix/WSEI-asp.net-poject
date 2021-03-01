import React, { useState, useEffect } from "react";
import { Row, Col } from "reactstrap";
import { useQuery } from "react-query";
import { useParams } from "react-router-dom";
import { getAll } from "../../helpers/commentsApi";
import CommentsList from "./CommentsList";
import AuthorizeService from "../api-authorization/AuthorizeService";
import CommentAddNew from "./CommentAddNew";

const Comments = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const { id } = useParams();
  const { data, isLoading, isError, isSuccess } = useQuery(
    `comments-${id}`,
    () => getAll(id)
  );

  useEffect(() => {
    const authenticated = async () => {
      const result = await AuthorizeService.isAuthenticated();

      setIsAuthenticated(result);
    };
    authenticated();
  });

  const sortByDate = (array) =>
    array.sort((a, b) => new Date(b.createdOn) - new Date(a.createdOn));

  return (
    <Col>
      <Row>
        <Col>
          <h5>Komentarze</h5>
        </Col>
      </Row>
      <Row className="mt-3">
        {isAuthenticated ? (
          <>
            <Col xs="12">
              <h6>Dodaj swój komentarz:</h6>
            </Col>
            <Col xs="12">
              <CommentAddNew />
            </Col>
          </>
        ) : null}
      </Row>
      <Row>
        {isSuccess ? <CommentsList comments={sortByDate(data)} /> : null}
      </Row>
    </Col>
  );
};

export default Comments;
