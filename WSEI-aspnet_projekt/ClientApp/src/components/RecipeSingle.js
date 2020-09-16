import React, { useEffect, useState } from 'react'
import { useParams } from "react-router-dom";
import { get } from '../helpers/recipesApi'


import { useSelector } from 'react-redux'
import { Row, Col } from 'reactstrap'
import Ingredient from './Ingredient'
import ShareRecipe from './ShareRecipe'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUserFriends, faHourglass } from '@fortawesome/free-solid-svg-icons' 

const RecipeSingle = () => {
    const { id } = useParams();
    const [recipe, setRecipe] = useState(null)

   // const recipe = useSelector(state => state.recipes.entities.find(el => el.id === id))

    useEffect(() => {
        const fetchData = async () => {
            get(id).then( (data) => 
            setRecipe(data))
        }
        fetchData()
    }, [])

    return (
        recipe ? (
            <>
                <Row className="singleRecipe-image">
                    <Col>
                        <img  src="http://michalwrzosek.pl/wp-content/uploads/2020/07/bok-5.jpg" alt={recipe.name} />
                    </Col>
                </Row>
                <Row className="mt-2 align-items-center">
                    <Col>
                        <ShareRecipe />
                    </Col>
                </Row>
                <Row className="mt-3">
                    <Col xs='12' sm='6'>
                        <Row className="singleRecipe-heading">
                            <Col>
                                <h2>{recipe.name}</h2>
                            </Col>
                        </Row>
                        <Row className="singleRecipe-description my-3">
                            <Col>
                                <p>{recipe.description}</p>
                            </Col>
                        </Row>
                        <Row className="singleRecipe-preparation ">
                            <Col>
                                <h4>Preparation</h4>
                            </Col>
                        </Row>
                        <Row className="singleRecipe-preparation my-3">
                            <Col>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
                            </Col>
                        </Row>
                    </Col>
                    <Col xs='12' sm='6' className="singleRecipe-ingredients ">
                        <h4>Ingredients</h4>
                        <p>Click to select which ingredient <br/>you already have</p>
                        <div className="singleRecipe-kinds">
                            <div className="singleRecipe-kinds-wrapper" >
                                <FontAwesomeIcon icon={faUserFriends} size="2x" style={{color: '#FFF'}} />
                                <span>1-2</span>
                            </div>
                            <div className="singleRecipe-kinds-wrapper">
                                <FontAwesomeIcon icon={faHourglass} size="2x" style={{color: '#FFF'}} />
                                <span>15 min</span>
                            </div>
                        </div>
                        <ul style={{ userSelect: 'none'}}>
                            <Ingredient>{recipe.description}</Ingredient>
                            <Ingredient>{recipe.description}</Ingredient>
                            <Ingredient>{recipe.description}</Ingredient>
                        </ul>

                    </Col>
                </Row>
            </>
        ) : (
            <>
                Loading...
            </>
        )
    )
}

export default RecipeSingle