import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
//import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import RecipesContainer from './components/RecipesContainer';
import AddRecipe from './components/AddRecipe';
import UpdateRecipe from './components/UpdateRecipe'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
            <Route path="/recipes" render={({ match: { url } }) => (
                <>
                    <Route path={`${url}/`} component={RecipesContainer} exact />
                    <Route path={`${url}/add`} component={AddRecipe} />
                    <Route path={`${url}/update/:id`} component={UpdateRecipe} />
                </>
            )} />

      </Layout>
    );
  }
}
