import { Module } from "module";
import { useState } from "react";
import { Button, Container, Row, Col, Modal } from "react-bootstrap";
import {Table} from "react-bootstrap";
import CarFormForCreate from "../Forms/CarFormForCreate";
import CarFormForEdit from "../Forms/CarFormForEdit";
import { car, dealerShopMiniDTO } from "../Interfaces";

export default function CarTable(props: carTableProps){
    
    const [isOpenedEditForm, setIsOpenedEditForm] = useState<boolean>(false);

    const handleCloseEditForm = () => {
        setIsOpenedEditForm(false);
    }
    
    const handleOpenEditForm = () => {
        setIsOpenedEditForm(true);
    }

    const handleDelete = (carForDelete: car) => {
        let carId = carForDelete.carId;
        let shop = props.dealerShops.find(ds => ds.ordinalNumber == carForDelete.dealerShopOrdinalNumber);
        if(!shop)
            return;
        let dealerShopId = shop.id;
        fetch(process.env.REACT_APP_API + "dealershops/" + dealerShopId + "/cars/" + carId, 
        {
            method: "DELETE",
            headers: {}
        })
        .then(response => {
            if(response.status == 204)
                  console.log("Car successfully deleted")
            else 
                console.log("Something went wrong when deleting the car");
            props.getAllCars();
        });
    }

    return <>
        <Table striped bordered hover className = "w-80 m-auto">
            <thead>
                <tr>
                    <th>CarId</th>
                    <th>Licence Plates</th>
                    <th>Brand</th>
                    <th>Model</th>
                    <th>Country</th>
                    <th>BodyType</th>
                    <th>Modification</th>
                    <th>Transmission</th>
                    <th>Drive</th>
                    <th>EngineType</th>
                    <th>Color</th>
                    <th>Production Year</th>
                    <th>Amount Of Owners</th>
                    <th>Ordinal number of dealer shop</th>
                    <th>Operations</th>
                </tr>
            </thead>
            <tbody>
                {props.cars.map((car, index)=> <tr key = {index}>
                    <td>{car.carId}</td>
                    <td>{car.licencePlates}</td>
                    <td>{car.brand}</td>
                    <td>{car.model}</td>
                    <td>{car.country}</td>
                    <td>{car.bodyType}</td>
                    <td>{car.modification}</td>
                    <td>{car.transmission}</td>
                    <td>{car.drive}</td>
                    <td>{car.engineType}</td>
                    <td>{car.color}</td>
                    <td>{car.productionYear}</td>
                    <td>{car.numberOfOwners}</td>
                    <td>{car.dealerShopOrdinalNumber}</td>
                    <td>
                        <div className = "w-100">
                        <Container>
                            <Row>
                                <Col sm="6">
                                    <Button className = "btn btn-primary w-100" onClick = {handleOpenEditForm} >Edit</Button>
                                    <Modal show = {isOpenedEditForm} onHide = {handleCloseEditForm} >
                                        <Modal.Header closeButton>
                                            Car Edit Form
                                        </Modal.Header>
                                        <Modal.Body>
                                            <CarFormForEdit closeForm={handleCloseEditForm} dealerShops={props.dealerShops} carForEdit={car} />
                                        </Modal.Body>
                                    </Modal>
                                </Col>
                                <Col sm="6">
                                    <Button className = "btn btn-danger w-100" onClick = {() => handleDelete(car)}>Delete</Button>
                                </Col>
                            </Row>
                        </Container>
                        </div>
                    </td>
                    </tr>)}
            </tbody>
        </Table>
        
    </>
}

interface carTableProps {
    cars: car[];
    dealerShops: dealerShopMiniDTO[];
    getAllCars: Function;
    className?: string;
}