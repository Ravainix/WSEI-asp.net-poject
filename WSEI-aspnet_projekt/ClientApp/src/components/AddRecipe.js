import React, { useState } from "react";
import RecipeForm from "./RecipeForm";
import { Redirect } from "react-router";

import * as RecipeApi from "../helpers/recipesApi";
import authService from "./api-authorization/AuthorizeService";

const AddRecipe = () => {
  const [recipe, setRecipe] = useState(null);

  const handleFormSubmit = async (formData) => {
    const resposne = await RecipeApi.create(formData);
    setRecipe({ recipe: resposne });
  };

  return (
    <>
      <RecipeForm handleSubmitForm={handleFormSubmit} />

      {recipe && (
        <Redirect
          to={{ pathname: "/recipes", state: { isRecipeCreated: true } }}
        />
      )}
    </>
  );
};

export default AddRecipe;