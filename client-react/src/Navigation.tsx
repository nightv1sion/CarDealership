import { Nav, Navbar } from "react-bootstrap";
import { Container } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
export default function Navigation(){
    return <>
        <Navbar bg="dark" variant = "dark">
            <Container>
                <LinkContainer to="/">
                    <Navbar.Brand>CarDealership</Navbar.Brand>
                </LinkContainer>
                <Nav className = "me-auto">
                    <LinkContainer to="/dealershops"><Nav.Link>Dealershops</Nav.Link></LinkContainer>
                    <LinkContainer to = "/cars"><Nav.Link>Cars</Nav.Link></LinkContainer>
                </Nav>
            </Container>
        </Navbar>
    </>
} 