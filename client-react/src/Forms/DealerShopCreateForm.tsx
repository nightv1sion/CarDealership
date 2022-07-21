import { Form } from "react-bootstrap";

export default function DealerShopCreateForm(){
    
    return <>
        <Form>
            <Form.Group>
                <Form.Label>Ordinal Number </Form.Label>
                <Form.Control type="text"/>
            </Form.Group>
            <Form.Group>
                <Form.Label className = "mt-2">Country</Form.Label>
                <Form.Control type="text"/>
            </Form.Group>
            <Form.Group>
                <Form.Label className = "mt-2">City</Form.Label>
                <Form.Control type = "text"/>
            </Form.Group>
            <Form.Group>
                <Form.Label className = "mt-2">Address</Form.Label>
                <Form.Control type = "text"/>
            </Form.Group>
        </Form>
    </>
}