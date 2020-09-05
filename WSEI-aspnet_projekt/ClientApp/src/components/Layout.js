import React, { Component, useEffect } from 'react';
import { useDispatch } from 'react-redux'
import { fetchAllRecipes } from '../features/recipes/recipesSlice'

import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';


export const Layout = ({ children }) => {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(fetchAllRecipes())
    }, [])

    return (
      <div>
        <NavMenu />
        <Container>
          {children}
        </Container>
      </div>
    );
}
