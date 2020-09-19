import React, { Component } from 'react';

import RecipesAll from './RecipesAll' 

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <>
            <div className="text-center">
              <h2>Check out the featured recipes!</h2>
            </div>
            <RecipesAll />
      </>
    );
  }
}
