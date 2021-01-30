import React from 'react'

import { Row } from 'reactstrap'
import { default as LoaderNPM} from 'react-loader-spinner'

const Loader = () => {
    return (
        <Row style={{height: "80vh", alignItems: "center", justifyContent: "center", flexDirection: "column"}}>
            <LoaderNPM
                type="Oval"
                color="#000"
                height={60}
                width={60}
            />
        </Row>
    )
}

export default Loader