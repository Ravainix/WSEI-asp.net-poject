import React from 'react'

const SingleRecipe = ({ name, description }) => {
    return (
        <div>
            {`Name: ${name}, Description: ${description}`}
        </div>
    )
}

export default SingleRecipe