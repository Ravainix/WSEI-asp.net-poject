import React, { Component } from 'react'

import * as RecipeApi from '../helpers/recipesApi'
import RecipesMenu from "./RecipesMenu";
import RecipesList from './RecipesList'


export default class RecipesContainer extends Component {

    constructor(props) {
        super(props)

        this.state = {
            recipes: null
        }
    }

    componentDidMount = async () => {
        this.getRecipes()
    }
    
    getRecipes = async (action) => {
        let recipes
        switch(action) {
            case 'GET_CURRENT_USER':
                recipes = await RecipeApi.getAllUser()
                this.setState({recipes})
                break;
            default:
                recipes = await RecipeApi.getAll()
                break;
        }
        this.setState({recipes})
    }

    render() {
        const { recipes } = this.state
        return (
            <div className="border rounded">
                <RecipesMenu getRecipes={this.getRecipes} />
                {recipes && <RecipesList recipes={recipes}/>}
            </div>
        )
    }
}
