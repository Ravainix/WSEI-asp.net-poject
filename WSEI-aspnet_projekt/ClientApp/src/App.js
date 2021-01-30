import React, { Component } from "react";
import { Switch, Route } from "react-router-dom";

import Layout from "./components/common/Layout";
import Hero from "./components/Heropage/Hero";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/api-authorization/ApiAuthorizationConstants";
import RecipesContainer from "./components/RecipesContainer";
import RecipeSingle from "./components/RecipeSingle";
import AddRecipe from "./components/AddRecipe";
import UpdateRecipe from "./components/UpdateRecipe";
import NoMatch from "./components/common/NoMatch";

const App = () => (
  <Layout>
    <Switch>
      <Route exact path="/" component={Hero} />
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
    <Route path={`${url}/:id`} component={RecipeSingle} />
    <Route exact path={`${url}/`} component={RecipesContainer} />
  </Switch>
);

export default App;
