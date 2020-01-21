import React, { Component } from 'react'
import { Row } from 'reactstrap';
import SingleRecipe from './SingleRecipe'

import * as RecipeApi from '../helpers/recipesApi'


export default class RecipiesContainer extends Component {

    constructor(props) {
        super(props)

        this.state = {
            recipes: null
        }
    }

    componentDidMount = async () => {
        const recipes = await RecipeApi.getAll()
        this.setState({recipes})
    }

    render() {
        const { recipes } = this.state
        return (
            <Row> 
                {recipes && recipes.map(recipe => <SingleRecipe key={recipe.id} {...recipe} />)}
            </Row>
        )
    }
}
