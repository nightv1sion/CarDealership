import { useEffect, useState } from "react";
import { Modal, Table } from "react-bootstrap";
import { dealerShop } from "../Interfaces";

export default function DealerShopTable(props: dealerShopsTableProps){
    
    const [isEditFormOpened, setIsEditFormOpened] = useState(false);

    const deleteShop = (dealershopId: string) => {
        console.log(dealershopId);
        const uri = process.env.REACT_APP_API + "dealershop/" + dealershopId;
        console.log(uri);
        fetch(uri, {
            method: "DELETE",
            headers: {
                "Accept": "application/json",
                "Content-type": "application/json"
            }
        })
        .then((response) => response.json())
        .then((data) => {
            console.log(data);
            props.getData();
        })
    }

    const editShop = (dealerShop: dealerShop)=>{
        const uri = process.env.REACT_APP_API + "dealershop/";
        fetch(uri, {
            method: "EDIT",
            headers: {
                "Accept": "application/json",
                "Content-type": "application/json"
            }
        })
        .then((response) => response.json())
        .then((data) => {
            console.log(data);
            props.getData();
        });
    }
    
    return <>
        <Table striped bordered hover size="sm" className = {props.className}>
            <thead>
                <tr>
                <th>#</th>
                <th>Country</th>
                <th>City</th>
                <th>Address</th>
                <th>Edit/Delete</th>
                </tr>
            </thead>
            <tbody>
                {props.shops.map((shop,index) => <tr key = {index}>
                    <td>{shop.ordinalNumber}</td>
                    <td>{shop.country}</td>
                    <td>{shop.city}</td>
                    <td>{shop.address}</td>
                    <td>{<><button key = {index} className="text-danger" onClick = {() => deleteShop(shop.dealerShopId)}>Delete</button>
                        <Modal show={isEditFormOpened} onHide = {() => setIsEditFormOpened(false)}>
                            <Modal.Header>
                            

                            </Modal.Header>
                        </Modal>
                        <button key = {index} className="text-primary" onClick = {() => editShop(shop)}>Edit</button>
                    </>
                    }</td>
                </tr>)}
            </tbody>
        </Table>
    </>
}

interface dealerShopsTableProps {
    className: string;
    shops: dealerShop[];
    getData: Function;
}