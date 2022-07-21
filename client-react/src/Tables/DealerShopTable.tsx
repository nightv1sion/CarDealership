import { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { dealerShop } from "../Interfaces";

export default function DealerShopTable(props: dealerShopsTableProps){
    
    
    return <>
        <Table striped bordered hover size="sm" className = {props.className}>
            <thead>
                <tr>
                <th>#</th>
                <th>Country</th>
                <th>City</th>
                <th>Address</th>
                </tr>
            </thead>
            <tbody>
                {props.shops?.map((shop,index) => <tr key = {index}>
                    <td>{shop.ordinalNumber}</td>
                    <td>{shop.country}</td>
                    <td>{shop.city}</td>
                    <td>{shop.address}</td>
                </tr>)}
            </tbody>
        </Table>
    </>
}

interface dealerShopsTableProps {
    className: string;
    shops: dealerShop[] | undefined;
}