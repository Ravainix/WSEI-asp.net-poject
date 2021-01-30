import React, { useState, useEffect } from "react";
import { Alert } from "reactstrap";

import * as RecipeApi from "../helpers/recipesApi";
import RecipesMenu from "./RecipesMenu";
import RecipesList from "./RecipesList";
import Loader from "./common/Loader";

const RecipesContainer = () => {
  const [recipes, setRecipes] = useState(null);
  const [error, setError] = useState(false);
  const [isUserRecipe, setIsUserRecipe] = useState(false);

  useEffect(() => {
    const fetchRecipes = async () => {
      try {
        const response = await RecipeApi.getAll();
        setRecipes(response);
      } catch (error) {
        setError(true);
        console.error(error);
      }
    };

    fetchRecipes();
  }, []);

  if (error) {
    return <Alert color="danger">Something went wrong.</Alert>;
  }

  if (recipes) {
    return (
      <div className="border rounded">
        {/* <RecipesMenu getRecipes={this.getRecipes} /> */}
        <RecipesList recipes={recipes} userRecipes={isUserRecipe} />
      </div>
    );
  }

  return <Loader />;
};

export default RecipesContainer;
