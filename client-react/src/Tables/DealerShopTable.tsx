import { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { dealerShop } from "../Interfaces";

export default function DealerShopTable(props: dealerShopsTableProps){
    const [shops, setShops] = useState<dealerShop[]>();
    const getData = () => {fetch(process.env.REACT_APP_API + "dealershop/all", {
        method: "GET",
        headers: {}
    })
    .then(response => response.json())
    .then(data => {console.log(data); setShops(data)}) };

    useEffect(() => getData(), []);
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
                {shops?.map(shop => <tr key = {shop.ordinalNumber}>
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
}