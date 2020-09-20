import React, { useEffect, useState } from 'react'
import { useParams } from "react-router-dom";
import { getWithIngredients } from '../helpers/recipesApi'

import { Row, Col } from 'reactstrap'
import Ingredient from './Ingredient'
import ShareRecipe from './ShareRecipe'
import Loader from './Loader';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUserFriends, faHourglass } from '@fortawesome/free-solid-svg-icons'

const RecipeSingle = () => {
    const { id } = useParams();
    const [data, setData] = useState(null)

   // const recipe = useSelector(state => state.recipes.entities.find(el => el.id === id))

    useEffect(() => {
        const fetchData = async () => {
            getWithIngredients(id)
                .then( (data) => setData(data))
        }
        fetchData()
    }, [])

    return (
        data ? (
            <>
                <Row className="singleRecipe-image">
                    <Col>
                        <img src={data.recipe.image} alt={data.recipe.name} />
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
                                <h2>{data.recipe.name}</h2>
                            </Col>
                        </Row>
                        <Row className="singleRecipe-description my-3">
                            <Col>
                                <p>{data.recipe.description}</p>
                            </Col>
                        </Row>
                        <Row className="singleRecipe-preparation ">
                            <Col>
                                <h4>Preparation</h4>
                            </Col>
                        </Row>
                        <Row className="singleRecipe-preparation my-3">
                            <Col>
                                <p></p>
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
                            {data.ingredients.map(({name, amount}) => <Ingredient key={name}>{name} <span style={{color: "#8C8B8B"}}>({amount})</span></Ingredient>)}
                        </ul>

                    </Col>
                </Row>
            </>
        ) : (
            <>
                <Loader />
            </>
        )
    )
}

export default RecipeSingle