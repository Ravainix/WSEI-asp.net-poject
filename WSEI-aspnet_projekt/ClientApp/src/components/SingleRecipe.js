import React from 'react'
import {
    Card, CardImg, CardText, CardBody,
    CardTitle, Col
} from 'reactstrap';

import DestroyRecipeButton from './DestroyRecipeButton';
import UpdateRecipeButton from "./UpdateRecipeButton";
import ShowRecipeButton from "./ShowRecipeButton";

const SingleRecipe = ({ name, description, image, id, userRecipes }) => {
    image = image ? image : `http://placehold.jp/320x180.png`
    return (
        <Col sm={{ size: 3 }} className="py-3">
            <Card>
                <CardBody>
                    <CardTitle>{name}</CardTitle>
                </CardBody>
                <CardImg width="100%" src={image} alt="Card image cap" />
                <CardBody>
                <CardText>{description}</CardText>
                <div className="d-flex justify-content-between">
                    <ShowRecipeButton id={id} />
                    {userRecipes && <UpdateRecipeButton id={id} />}
                    {userRecipes && <DestroyRecipeButton id={id} />}
                </div>
                </CardBody>
            </Card>
        </Col>
    )
}

export default SingleRecipe