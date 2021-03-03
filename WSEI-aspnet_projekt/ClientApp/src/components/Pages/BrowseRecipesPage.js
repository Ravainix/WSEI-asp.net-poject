import React from "react";
import RecipesContainer from "../BrowseRecipes/Container/RecipesContainer";
import { useQuery } from "react-query";
import { Alert } from "reactstrap";

import Loader from "../common/Loader";

const BrowseRecipesPage = ({ queryFn }) => {
  const { data, isFetching, error } = useQuery(
    `recipes-${queryFn.name}`,
    queryFn,
    {
      refetchOnWindowFocus: false,
    }
  );

  if (isFetching) {
    return <Loader />;
  }

  if (error) {
    return (
      <Alert color="danger">
        Something went wrong.{" "}
        <span role="img" aria-label="unamused face">
          😒
        </span>
      </Alert>
    );
  }

  return (
    <>
      <RecipesContainer recipes={data} />
    </>
  );
};

export default BrowseRecipesPage;
