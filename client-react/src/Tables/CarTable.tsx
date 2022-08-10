import { Button } from "react-bootstrap";
import {Table} from "react-bootstrap";
import { car } from "../Interfaces";

export default function CarTable(props: carTableProps){
    
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
                    </tr>)}
            </tbody>
        </Table>
        
    </>
}

interface carTableProps {
    cars: car[];
    className?: string;
}