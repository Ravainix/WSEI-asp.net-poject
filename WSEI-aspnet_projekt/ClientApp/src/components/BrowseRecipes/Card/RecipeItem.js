import React from "react";
import styled from "styled-components";
import { Card, CardImg, CardText, CardBody, CardTitle, Col } from "reactstrap";
import { Link } from "react-router-dom";
import StarRating from "../common/StarRating";

const StyledCardText = styled(CardText)`
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
  max-width: 150ch;
`;

const StyledLink = styled(Link)`
  color: black;
  transition: color 0.3s cubic-bezier(0.1, 0.15, 0.31, 0.91) 0.1s;

  &:hover {
    color: #8ac256;
    text-decoration: none;
  }
`;

const StarContainer = styled.div`
  display: flex;
`;

const RecipeItem = ({ name, description, image, id, avgRate, rateCount }) => {
  image = `http://placehold.jp/150x150.png`;
  return (
    <Col sm={{ size: 1 }} md={{ size: 3 }} className="py-3">
      <Card>
        <StyledLink to={`/recipes/${id}`}>
          <CardImg top width="100%" src={image} alt="Card image cap" />
        </StyledLink>
        <CardBody>
          <StyledLink to={`/recipes/${id}`}>
            <CardTitle tag="h5">{name}</CardTitle>
          </StyledLink>
          <StyledCardText>
            <small className="text-muted">{description}</small>
          </StyledCardText>
          <StarContainer>
            <StarRating rating={avgRate} />
            <span>({rateCount})</span>
          </StarContainer>
        </CardBody>
      </Card>
    </Col>
  );
};

export default RecipeItem;
