import React from 'react'
import {
    Card, CardImg, CardText, CardBody,
    CardTitle, CardSubtitle, Button, Col
} from 'reactstrap';


const SingleRecipe = ({ name, description, image }) => {
    image = !!image ? image : `http://placehold.jp/320x180.png`
    return (
        <Col sm={{ size: 3 }} className="py-3">
            <Card>
                <CardBody>
                    <CardTitle>{name}</CardTitle>
                </CardBody>
                <CardImg width="100%" src={image} alt="Card image cap" />
                <CardBody>
                    <CardText>{description}</CardText>
                    {/* <Button>Button</Button> */}
                </CardBody>
            </Card>
        </Col>
    )
}

export default SingleRecipe