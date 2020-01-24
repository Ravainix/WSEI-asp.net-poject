import React, { useState } from 'react'

const RecipesMenu = ({getRecipes}) => {
    const [active, setActive] = useState("all")
    return (
        <div className="">
            <ul className="d-flex justify-content-center nav nav-tabs my-2">
                <li className="nav-item cursor-pointer">
                    <button 
                        className={"nav-link " + (active === "all" ? "active" : "") } 
                        onClick={() => {
                            getRecipes()
                            setActive("all")
                        }}
                    >
                        All recipes
                    </button>
                </li>
                <li className="nav-item cursor-pointer">
                    <button
                        className={"nav-link " + (active === "user" ? "active" : "") }
                        onClick={() => {
                            getRecipes('GET_CURRENT_USER')
                            setActive("user")
                        }}
                    >
                        My recipes
                    </button>
                </li>
            </ul>
        </div>
    )
}

export default RecipesMenu
