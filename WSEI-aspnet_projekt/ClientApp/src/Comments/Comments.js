import React, { useState, useEffect } from "react";
import { Row, Col } from "reactstrap";
import { useQuery } from "react-query";
import { useParams } from "react-router-dom";
import { getAll } from "../helpers/commentsApi";
import CommentsList from "./CommentsList";
import AuthorizeService from "../components/api-authorization/AuthorizeService";
import CommentForm from "./CommentForm";

const Comments = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const { id } = useParams();
  const { data, isLoading, error } = useQuery(`comments-${id}`, () =>
    getAll(id)
  );

  useEffect(() => {
    const authenticated = async () => {
      const result = await AuthorizeService.isAuthenticated();

      setIsAuthenticated(result);
    };
    authenticated();
  });

  return (
    <Col>
      <Row>
        <Col>
          <h5>Komentarze</h5>
        </Col>
      </Row>
      <Row>
        {isAuthenticated ? (
          <>
            <Col xs="12">
              <h6>Dodaj swój komentarz:</h6>
            </Col>
            <Col xs="12">
              <CommentForm />
            </Col>
          </>
        ) : null}
      </Row>
      <Row>
        <CommentsList comments={[1, 2]} />
      </Row>
    </Col>
  );
};

export default Comments;
