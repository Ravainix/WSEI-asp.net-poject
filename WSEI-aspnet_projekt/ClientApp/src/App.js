import React, { Component } from 'react';
import { Switch, Route } from "react-router-dom";


import { Layout } from './components/Layout';
import { Home } from './components/Home';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import RecipesContainer from './components/RecipesContainer';
import RecipeSingle from './components/RecipeSingle';
import RecipesAll from './components/RecipesAll';
import AddRecipe from './components/AddRecipe';
import UpdateRecipe from './components/UpdateRecipe'
import NoMatch from './components/NoMatch'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Layout>
            <Switch>
                <Route exact path='/' component={RecipesAll} />
                <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
                <AuthorizeRoute path="/recipes" component={Recipes} />
                <Route path="*" component={NoMatch} />
            </Switch>
      </Layout>
    );
  }
}

const Recipes = ({ match: { url } }) => (
        <Switch>
            <Route path={`${url}/update/:id`} component={UpdateRecipe} />
            <Route path={`${url}/add`} component={AddRecipe} />
            <Route path={`${url}/:id`} component={RecipeSingle} />
            <Route exact path={`${url}/`} component={RecipesContainer} />
        </Switch>
)

