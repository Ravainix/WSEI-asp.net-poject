import React, { useState } from 'react'

const Ingredient = ({ children }) => {
    const [strikethrough, setStrikethrough] = useState(false)

    const handleClick = () => {
        setStrikethrough(state => !state)
    }
    return (
        <li className={`ingredient ingredient-marginLeft ${strikethrough ? 'ingredient-strikethrough' : ''}`} onClick={handleClick} >
            {children}
        </li>
        )
}

export default Ingredient