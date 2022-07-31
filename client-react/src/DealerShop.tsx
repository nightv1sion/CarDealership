import { useEffect, useState } from "react"
import { Button } from "react-bootstrap";
import {Modal} from "react-bootstrap";
import { MapContainer, Marker, TileLayer } from "react-leaflet";
import DealerShopForm from "./Forms/DealerShopForm";
import MapInput from "./Forms/MapInput";
import { dealerShop } from "./Interfaces";
import DealerShopTable from "./Tables/DealerShopTable";

export default function Dealershop(){

    const formatData = (data: dealerShop[]) => {
        for(let shop of data){
            shop.location = shop.location.substring(7, shop.location.length-1).split(" ").join(", ");
        }
        return data;
    }

    const [isOpenCreateForm, setIsOpenCreateForm] = useState(false);
    const [shops, setShops] = useState<dealerShop[]>(new Array<dealerShop>(0));

    const [statusOfProcess, setStatusOfProcess] = useState<any>(undefined);
    const [messageAfterProcess, setMessageAfterProcess] = useState<string | undefined>(undefined);

    const setStatus = (status : boolean, message: string) => {
        setStatusOfProcess(status);
        setMessageAfterProcess(message);
        console.log("status now is " + status);
        setTimeout(() => setStatusOfProcess(undefined), 5000);
    }
    
    const getData = () => {fetch(process.env.REACT_APP_API + "dealershop/all", {
        method: "GET",
        headers: {}
    })
    .then(response => response.json())
    .then(data => {console.log(data); setShops(Array.isArray(data) ? formatData(data) : new Array(0))})

    };

    useEffect(() => getData(), []);
    
    const handleShow = () => {setIsOpenCreateForm(true)};
    const handleClose = () => {setIsOpenCreateForm(false)};

    return <>
        <div className = "text-center w-75 m-auto">
            
            {statusOfProcess === true ? 
                <div className="alert alert-success mt-2" role="alert">
                {messageAfterProcess}
              </div>
            : statusOfProcess === false ? 
                <div className = "alert alert-warning mt-2" role="alert">{messageAfterProcess}</div> 
            : <></>}
            <h3 className = "mt-2">DealerShops</h3>
            {
            shops.length === 0 ? <h3>There are no dealershops</h3> 
            : <>
            <DealerShopTable setStatus = {setStatus} shops = {shops ? shops : []} className = "mt-3" getData = {() => getData()} closeForm = {() => handleClose()} allOrdinalNumbers = {Array.isArray(shops) && shops ? shops.map(shop => shop.ordinalNumber): []}/>
            </>
            }
            <button onClick = {handleShow} className = "btn btn-dark">Create a new Dealershop</button>
            <Modal className = "modal-md" show = {isOpenCreateForm} onHide = {handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Create Dealershop form</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <DealerShopForm setStatus = {setStatus} getData={() => getData()} closeForm={() => handleClose()} allOrdinalNumbers = {Array.isArray(shops) && shops ? shops.map(shop => shop.ordinalNumber) : []}/>
                </Modal.Body>
            </Modal>
            
        </div>
    </>
}