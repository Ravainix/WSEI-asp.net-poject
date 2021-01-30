import React from 'react'
import { Container } from 'reactstrap'
import styled from 'styled-components'

const Grid = styled.div`
    display: grid;
    grid-template-columns: repeat(3, minmax(0, 1fr));
`

const StyledDiv = styled.div`
    background-color: #EDEDED;
`

const Footer = () => {
    return (
        <StyledDiv className="mt-3">
            <Container className="text-center py-5 ">
                <Grid className="grid">
                </Grid>
                <div>
                    <span >© 2020 Cookbook All Rights Reserved</span>
                </div>
            </Container>
        </StyledDiv>
    )
}

export default Footer
