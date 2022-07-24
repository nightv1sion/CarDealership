import { useEffect, useState } from "react"
import { Button } from "react-bootstrap";
import {Modal} from "react-bootstrap";
import DealerShopCreateForm from "./Forms/DealerShopCreateForm";
import { dealerShop } from "./Interfaces";
import DealerShopTable from "./Tables/DealerShopTable";

export default function Dealershop(){
    const [isOpenCreateForm, setIsOpenCreateForm] = useState(false);
    const [shops, setShops] = useState<dealerShop[]>(new Array<dealerShop>(0));
    const getData = () => {fetch(process.env.REACT_APP_API + "dealershop/all", {
        method: "GET",
        headers: {}
    })
    .then(response => response.json())
    .then(data => {console.log(data); setShops(Array.isArray(data) ? data : new Array(0))}) };

    useEffect(() => getData(), []);
    
    const handleShow = () => {setIsOpenCreateForm(true)};
    const handleClose = () => {setIsOpenCreateForm(false)};

    return <>
        <div className = "text-center w-75 m-auto">
            <h3 className = "mt-2">DealerShops</h3>
            {
            shops.length === 0 ? <h3>There are no dealershops</h3> 
            : <><DealerShopTable shops = {shops ? shops : []} className = "mt-3" getData = {() => getData()} closeForm = {() => handleClose()} allOrdinalNumbers = {Array.isArray(shops) && shops ? shops.map(shop => shop.ordinalNumber): []}/>
            </>
            }
            <button onClick = {handleShow} className = "btn btn-dark">Create a new Dealershop</button>
            <Modal show = {isOpenCreateForm} onHide = {handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Create Dealershop form</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <DealerShopCreateForm getData={() => getData()} closeForm={() => handleClose()} allOrdinalNumbers = {Array.isArray(shops) && shops ? shops.map(shop => shop.ordinalNumber) : []}/>
                </Modal.Body>
            </Modal>
            
        </div>
    </>
}