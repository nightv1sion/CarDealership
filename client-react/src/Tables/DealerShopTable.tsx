import { useEffect, useState } from "react";
import { Modal, Table } from "react-bootstrap";
import DealerShopForm from "../Forms/DealerShopForm";
import { dealerShop } from "../Interfaces";

export default function DealerShopTable(props: dealerShopsTableProps){

    const [isEditFormOpened, setIsEditFormOpened] = useState<boolean[]>(props.shops.map(shop => false));

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

    const changeArray = (array: boolean[], index:number, state:boolean) => {
        const newArray = Array.from(array);
        newArray[index] = state;
        return newArray;
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
                {Array.isArray(props.shops) && props.shops?.map((shop,index) => <tr key = {shop.dealerShopId}>
                    <td>{shop.ordinalNumber}</td>
                    <td>{shop.country}</td>
                    <td>{shop.city}</td>
                    <td>{shop.address}</td>
                    <td>{<>
                    <button className="btn btn-danger" onClick = {() => deleteShop(shop.dealerShopId)}>Delete</button>
                        <button className="btn btn-primary" onClick = {() => setIsEditFormOpened(changeArray(isEditFormOpened, index, true))}>Edit</button>
                        <Modal key = {index} show={isEditFormOpened[index]} onHide = {() => setIsEditFormOpened(changeArray(isEditFormOpened, index, false))}>
                            <Modal.Header>
                                <Modal.Title>Edit Dealershop form</Modal.Title>
                            </Modal.Header>
                            <Modal.Body>
                                <DealerShopForm shop={shop} getData = {props.getData} closeForm = {() => setIsEditFormOpened(changeArray(isEditFormOpened, index, false))} allOrdinalNumbers = {props.allOrdinalNumbers.filter(value => value != shop.ordinalNumber)}/>
                            </Modal.Body>
                        </Modal>
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
    closeForm: Function;
    allOrdinalNumbers: number[];
}