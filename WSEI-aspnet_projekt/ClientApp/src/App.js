import React from "react";
import { Switch, Route } from "react-router-dom";

import Layout from "./components/common/Layout";
import HeroPage from "./components/Pages/HeroPage";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/api-authorization/ApiAuthorizationConstants";
import RecipesContainer from "./components/BrowseRecipes/Container/RecipesContainer";
import RecipePage from "./components/Pages/RecipePage";
import AddRecipe from "./components/AddRecipe";
import UpdateRecipe from "./components/UpdateRecipe";
import NoMatch from "./components/common/NoMatch";
import BrowseRecipesPage from "./components/Pages/BrowseRecipesPage";

import * as RecipeApi from "./helpers/recipesApi";

const App = () => (
  <Layout>
    <Switch>
      <Route exact path="/" component={HeroPage} />
      <Route
        path={ApplicationPaths.ApiAuthorizationPrefix}
        component={ApiAuthorizationRoutes}
      />
      <Route path="/recipes" component={Recipes} />
      <Route path="*" component={NoMatch} />
    </Switch>
  </Layout>
);

const Recipes = ({ match: { url } }) => (
  <Switch>
    <AuthorizeRoute path={`${url}/update/:id`} component={UpdateRecipe} />
    <AuthorizeRoute path={`${url}/add`} component={AddRecipe} />
    <Route
      exact
      path={`${url}/user`}
      render={() => <BrowseRecipesPage queryFn={RecipeApi.getAllUser} />}
    />
    <Route path={`${url}/:id`} component={RecipePage} />
    <Route
      exact
      path={`${url}/`}
      render={() => <BrowseRecipesPage queryFn={RecipeApi.getAll} />}
    />
  </Switch>
);

export default App;
