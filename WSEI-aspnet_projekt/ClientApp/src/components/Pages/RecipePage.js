import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useQuery } from "react-query";
import { getWithIngredients } from "../../helpers/recipesApi";

import { Row, Col } from "reactstrap";
import Ingredient from "../Ingredient";
import ShareRecipe from "../ShareRecipe";
import Loader from "../common/Loader";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUserFriends, faHourglass } from "@fortawesome/free-solid-svg-icons";
import { Alert } from "reactstrap";
import StarRating from "../BrowseRecipes/common/StarRating";
import Comments from "../Comments/Comments";

const RecipePage = () => {
  const { id } = useParams();
  const { data, isLoading, isError } = useQuery(`recipe-${id}`, () =>
    getWithIngredients(id)
  );

  if (isLoading) {
    return <Loader />;
  }

  if (isError) {
    return (
      <Alert color="danger">Wystąpił problem podczas ładowania zasobu.</Alert>
    );
  }

  return (
    <>
      <Row className="singleRecipe-image">
        <Col>
          <img src={data.recipe.image} alt={data.recipe.name} />
        </Col>
      </Row>
      <Row className="mt-2 align-items-center">
        <Col>
          <ShareRecipe />
        </Col>
        <Col className="flex">
          <StarRating rating={Math.round(data.recipe.avgRate)} />
          <span>({data.recipe.rateCount})</span>
        </Col>
      </Row>
      <Row className="mt-3">
        <Col xs="12" sm="6">
          <Row className="singleRecipe-heading">
            <Col>
              <h2>{data.recipe.name}</h2>
            </Col>
          </Row>
          <Row className="singleRecipe-description my-3">
            <Col>
              <p>{data.recipe.description}</p>
            </Col>
          </Row>
          <Row className="singleRecipe-preparation ">
            <Col>
              <h4>Sposób przygotowania</h4>
            </Col>
          </Row>
          <Row className="singleRecipe-preparation my-3">
            <Col>
              <p>{data.recipe.instruction}</p>
            </Col>
          </Row>
        </Col>
        <Col xs="12" sm="6" className="singleRecipe-ingredients ">
          <h4>Składniki</h4>
          <p>
            Zaznacz, aby odhaczyć składniki, <br />
            które już masz
          </p>
          <div className="singleRecipe-kinds">
            <div className="singleRecipe-kinds-wrapper">
              <FontAwesomeIcon
                icon={faUserFriends}
                size="2x"
                style={{ color: "#FFF" }}
              />
              <span>{data.recipe.portions}</span>
            </div>
            <div className="singleRecipe-kinds-wrapper">
              <FontAwesomeIcon
                icon={faHourglass}
                size="2x"
                style={{ color: "#FFF" }}
              />
              <span>{data.recipe.prepareTime} min</span>
            </div>
          </div>
          <ul style={{ userSelect: "none" }}>
            {data.ingredients.map(({ name, amount }) => (
              <Ingredient key={name}>
                {name} <span style={{ color: "#8C8B8B" }}>({amount})</span>
              </Ingredient>
            ))}
          </ul>
        </Col>
      </Row>
      <Row>
        <Comments />
      </Row>
    </>
  );
};

export default RecipePage;
