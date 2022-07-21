import { useEffect, useState } from "react"
import { Button } from "react-bootstrap";
import {Modal} from "react-bootstrap";
import DealerShopCreateForm from "./Forms/DealerShopCreateForm";
import { dealerShop } from "./Interfaces";
import DealerShopTable from "./Tables/DealerShopTable";

export default function Dealershop(){
    const [isOpenCreateForm, setIsOpenCreateForm] = useState(false);
    
    const handleShow = () => {setIsOpenCreateForm(true)};
    const handleClose = () => {setIsOpenCreateForm(false)};

    return <>
        <div className = "text-center w-75 m-auto">
            <h3 className = "mt-2">DealerShops</h3>
            <DealerShopTable className = "mt-3"/>
            <button onClick = {handleShow}>Create</button>
            <Modal show = {isOpenCreateForm} onHide = {handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Create Dealershop form</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <DealerShopCreateForm/>
                </Modal.Body>
                <Modal.Footer>
                    <Button>Create</Button>
                </Modal.Footer>
            </Modal>
        </div>
    </>
}