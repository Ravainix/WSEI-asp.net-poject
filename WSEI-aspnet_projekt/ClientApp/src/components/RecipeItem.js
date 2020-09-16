import React, { useState } from 'react'
import {
    Card, CardImg, CardText, CardBody,
    CardTitle, Col
} from 'reactstrap';
import { Link } from "react-router-dom";


import DestroyRecipeButton from './DestroyRecipeButton';
import UpdateRecipeButton from "./UpdateRecipeButton";

const RecipeItem = ({ name, description, image, id, userRecipes }) => {
    const [isHover, setIsHover] = useState(false);

    image = image ? image : `http://placehold.jp/150x150.png`
    return (
        <Col sm={{ size: 1 }} md={{ size: 3 }} className="py-3">
            <Card
                className="recipeItem-card"
                onMouseEnter={() => setIsHover(true)}
                onMouseLeave={() => setIsHover(false)}
            >
                <Link to={`/recipes/${id}`}>
                <CardImg className={`recipeItem-img ${isHover ? 'recipeItem-img--hover': '' }`} src={image} alt="Card image cap" />
                <CardBody>
                    <CardTitle>{name}</CardTitle>
                    <div className={`recipeItem-description ${isHover ? 'recipeItem-description--hover': ''}`}> 
                        <CardText>{description}</CardText>
                        <div className="d-flex justify-content-between">
                            {userRecipes && <UpdateRecipeButton id={id} />}
                            {userRecipes && <DestroyRecipeButton id={id} />}
                        </div>
                    </div>
                </CardBody>
                </Link>
            </Card>
        </Col>
    )
}

export default RecipeItem