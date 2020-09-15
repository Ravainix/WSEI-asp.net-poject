import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux'
import { fetchAllRecipes } from '../features/recipes/recipesSlice'

import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import Footer from './Footer';


export const Layout = ({ children }) => {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(fetchAllRecipes())
    }, [])

    return (
      <div>
        <NavMenu />
        <Container style={{marginTop: 100, minHeight: 700}}>
          {children}
        </Container>
        <Footer />
      </div>
    );
}
