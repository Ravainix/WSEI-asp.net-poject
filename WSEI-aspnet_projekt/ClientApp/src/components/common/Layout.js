import React from "react";

import { Container } from "reactstrap";
import NavMenu from "../Navbar/NavMenu";
import Footer from "../Footer/Footer";

const Layout = ({ children }) => (
  <div>
    <NavMenu />
    <main>
      <Container style={{ marginTop: 100, minHeight: 700 }}>
        {children}
      </Container>
    </main>
    <Footer />
  </div>
);

export default Layout;
