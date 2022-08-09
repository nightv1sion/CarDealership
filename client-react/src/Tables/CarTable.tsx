import { Button } from "react-bootstrap";
import {Table} from "react-bootstrap";
import { car } from "../Interfaces";

export default function CarTable(props: carTableProps){
    
    return <>
        <Table striped bordered hover className = "w-50 m-auto">
            <thead>
                <tr>
                    <th>CarId</th>
                    <th>Brand</th>
                    <th>Model</th>
                    <th>Country</th>
                </tr>
            </thead>
            <tbody>
                {props.cars.map(car => <tr key = {car.carId}>
                    <td>{car.carId}</td>
                    <td>{car.brand}</td>
                    <td>{car.model}</td>
                    <td>{car.country}</td>
                    </tr>)}
            </tbody>
        </Table>
        
    </>
}

interface carTableProps {
    cars: car[];
    className?: string;
}