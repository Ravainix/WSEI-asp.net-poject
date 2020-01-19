import React, { Component } from 'react'
import SingleRecipe from './SingleRecipe'

export default class RecipiesContainer extends Component {

    constructor(props) {
        super(props)

        this.state = {
            data: null
        }
    }

    componentDidMount() {
        this.fetchAllRecipes()
    }

    fetchAllRecipes = async () => {
        fetch("/api/Recipes")
            .then(response => response.json())
            .then(data => this.setState({data: data}))
    }

    render() {
        const {data} = this.state
        return (
            <div>
                {data && data.map(recipe => <SingleRecipe key={recipe.id} {...recipe} />)}
            </div>
        )
    }
}
