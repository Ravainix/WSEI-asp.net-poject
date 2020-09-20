import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux'
import { fetchAllRecipes } from '../features/recipes/recipesSlice'

import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import Footer from './Footer';
import Loader from './Loader';


export const Layout = ({ children }) => {
    const dispatch = useDispatch();
    const data = useSelector(state => state.recipes.entities)

    useEffect(() => {
        dispatch(fetchAllRecipes())
    }, [])

    return (
      <div>
        <NavMenu />
        <main>
          <Container style={{ marginTop: 100, minHeight: 700 }}>
            {data ? children : <Loader />}
          </Container>
        </main>
        <Footer />
      </div>
    );
}
